using OSGSolutions.Models.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSGSolutions.Services
{
    public class StatusService
    {
        public List<StatusEntry> GetAll()
        {
            List<StatusEntry> statusList = new List<StatusEntry>();


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Statuses_SelectAll", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        StatusEntry model = Mapper(reader);
                        statusList.Add(model);
                    }
                }
                conn.Close();
            }
            return statusList;
        }

        public StatusEntry GetById(int id)
        {
            StatusEntry statusEntry = new StatusEntry();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Statuses_SelectById", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read()) {
                        statusEntry = Mapper(reader);
                    }
                }
                conn.Close();
            }
            return statusEntry;
        }

        public int Insert(StatusEntry model)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Statuses_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Url", model.Url);
                    cmd.Parameters.AddWithValue("@Company", model.Company);
                    cmd.Parameters.AddWithValue("@StatusId", model.StatusId);
                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                };
                conn.Close();
            }
            return id;
        }

        public void Update(StatusEntry model)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Statuses_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Title", model.Title);
                    cmd.Parameters.AddWithValue("@Url", model.Url);
                    cmd.Parameters.AddWithValue("@Company", model.Company);
                    cmd.Parameters.AddWithValue("@StatusId", model.StatusId);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                };
                conn.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Statuses_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                };
                conn.Close();
            }
        }

        private StatusEntry Mapper(SqlDataReader reader)
        {
            StatusEntry model = new StatusEntry();
            int index = 0;
            model.Id = reader.GetInt32(index++);
            model.Title = reader.GetString(index++);
            model.Url = reader.GetString(index++);
            model.Company = reader.GetString(index++);
            model.StatusId = reader.GetInt32(index++);
            model.Status = reader.GetString(index++);
            model.Modified = reader.GetDateTime(index++);
            return model;
        }
    }
}
