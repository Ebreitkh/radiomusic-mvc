using MusicRadio.Interfaces;
using MusicRadio.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;


namespace MusicRadio.Data
{
    public class AccessRepository : IAccessRepository
    {
        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.ConfirmPassword))
            {
                throw new ArgumentException("La contraseña y su confirmación son requeridas");
            }

            if (user.Password != user.ConfirmPassword)
            {
                throw new InvalidOperationException("Las contraseñas no coinciden");
            }

            try
            {
                user.Password = ConvertirSha256(user.Password);

                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_RegisterUser", cnx);
                    cmd.Parameters.AddWithValue("Id", user.Mail);
                    cmd.Parameters.AddWithValue("Name", user.Name);
                    cmd.Parameters.AddWithValue("Mail", user.Mail);
                    cmd.Parameters.AddWithValue("Password", user.Password);
                    cmd.Parameters.AddWithValue("Direction", user.Direction);
                    cmd.Parameters.AddWithValue("Phone", user.Phone);
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cnx.Open();
                    cmd.ExecuteNonQuery();

                    string message = cmd.Parameters["Message"].Value.ToString();

                    if (message.Contains("Error") || message.Contains("ya está registrado"))
                    {
                        throw new InvalidOperationException(message);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Error al registrar el usuario en la base de datos", ex);
            }
        }


        public bool ValidateUser(string mail, string password)
        {
            if (string.IsNullOrWhiteSpace(mail))
                throw new ArgumentException("El correo electrónico es requerido", nameof(mail));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña es requerida", nameof(password));

            try
            {
                string hashedPassword = ConvertirSha256(password);
                string message = string.Empty;

                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_ValidateUser", cnx);
                    cmd.Parameters.AddWithValue("Mail", mail);
                    cmd.Parameters.AddWithValue("Password", hashedPassword);
                    cmd.Parameters.Add("Message", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cnx.Open();
                    cmd.ExecuteNonQuery();

                    message = cmd.Parameters["Message"].Value.ToString();
                }

                return message == "1"; 
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al validar el usuario en la base de datos", ex);
            }
        }

        private static string ConvertirSha256(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                throw new ArgumentException("El texto para encriptar no puede estar vacío");
            }

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }

}

