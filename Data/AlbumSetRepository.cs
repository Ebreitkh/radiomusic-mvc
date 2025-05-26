using MusicRadio.Interfaces;
using MusicRadio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MusicRadio.Data
{
    public class AlbumSetRepository : IAlbumSetRepository
    {
        public List<AlbumSet> GetAll()
        {
            var listAlbumSets = new List<AlbumSet>();
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM AlbumSet", cnx);
                    cnx.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listAlbumSets.Add(new AlbumSet
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = dr["Name"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los álbumes", ex);
            }
            return listAlbumSets;
        }

        public AlbumSet GetById(int id)
        {
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAlbumById", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cnx.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AlbumSet
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el álbum con ID {id}", ex);
            }
            return null;
        }

        public void Add(AlbumSet albumSet)
        {
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_ManageAlbum", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.VarChar, 10).Value = "INSERT";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value =
                        !string.IsNullOrWhiteSpace(albumSet.Name) ? albumSet.Name : (object)DBNull.Value;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el álbum", ex);
            }
        }

        public void Update(AlbumSet albumSet)
        {
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_ManageAlbum", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.VarChar, 10).Value = "UPDATE";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = albumSet.Id;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value =
                        !string.IsNullOrWhiteSpace(albumSet.Name) ? albumSet.Name : (object)DBNull.Value;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el álbum con ID {albumSet.Id}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_ManageAlbum", cnx);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", SqlDbType.VarChar, 10).Value = "DELETE";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el álbum con ID {id}", ex);
            }
        }
    }

}