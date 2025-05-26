using MusicRadio.Interfaces;
using MusicRadio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MusicRadio.Data
{
    public class SongSetRepository : ISongSetRepository
    {
        public List<SongSet> GetAll()
        {
            var listSongSet = new List<SongSet>();
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_GetSongsWithAlbumName", cnx);
                    cnx.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listSongSet.Add(new SongSet
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = dr["Name"].ToString(),
                                Album_id = Convert.ToInt32(dr["Album_id"]),
                                AlbumName = dr["AlbumName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de canciones.", ex);
            }
            return listSongSet;
        }

        public void Add(SongSet songSet)
        {
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_ManageSong", cnx)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Action", SqlDbType.VarChar, 10).Value = "INSERT";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = DBNull.Value;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value =
                        !string.IsNullOrWhiteSpace(songSet.Name) ? songSet.Name : (object)DBNull.Value;
                    cmd.Parameters.Add("@Album_id", SqlDbType.Int).Value =
                        songSet.Album_id > 0 ? songSet.Album_id : (object)DBNull.Value;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la canción.", ex);
            }
        }

        public SongSet GetById(int id)
        {
            SongSet songSet = null;
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_GetSongSetById", cnx)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cnx.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            songSet = new SongSet
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
                throw new Exception($"Error al obtener la canción con ID {id}.", ex);
            }

            return songSet;
        }

        public void Update(SongSet songSet)
        {
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_ManageSong", cnx)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Action", SqlDbType.VarChar, 10).Value = "UPDATE";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = songSet.Id;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value =
                        !string.IsNullOrWhiteSpace(songSet.Name) ? songSet.Name : (object)DBNull.Value;
                    cmd.Parameters.Add("@Album_id", SqlDbType.Int).Value =
                        songSet.Album_id > 0 ? songSet.Album_id : (object)DBNull.Value;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la canción con ID {songSet.Id}.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection cnx = DbConnectionHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_ManageSong", cnx)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@Action", SqlDbType.VarChar, 10).Value = "DELETE";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la canción con ID {id}.", ex);
            }
        }
    }

}