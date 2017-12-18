using OSGSolutions.Models.Domain;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OSGSolutions.Services
{
    public class UsersService
    {
        public List<Users> SelectAll()
        {
            List<Users> usersList = new List<Users>();
            

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Users_SelectAll", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        Users model = Mapper(reader);
                        usersList.Add(model);
                    }
                }
                conn.Close();
            }
            return usersList;
        }

        public Users SelectByEmail(string email)
        {
            Users model = new Users();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Users_SelectByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        model = Mapper(reader);
                    }
                }
                conn.Close();
            }
            return model;
        }

        public int Insert(RegisterUser user)
        {
            int id = 0;
            string salt;
            string hashedPassword;
            string password = user.Password;

            CryptographyService svc = new CryptographyService();

            salt = svc.GenerateRandomString(15);
            hashedPassword = svc.Hash(password, salt);
            user.HashPassword = hashedPassword;
            user.Salt = salt;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Users_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Salt", user.Salt);
                    cmd.Parameters.AddWithValue("@HashPassword", user.HashPassword);
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

        public bool Login(LoginUser model)
        {
            bool res = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Users_SelectByEmail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Users responseModel = Mapper(reader);

                        int multOf4 = responseModel.Salt.Length % 4;
                        if (multOf4 > 0)
                        {
                            responseModel.Salt += new string('=', 4 - multOf4);
                        }
                        CryptographyService cSvc = new CryptographyService();

                        string passwordHash = cSvc.Hash(model.BasicPassword, responseModel.Salt);

                        if (passwordHash == responseModel.HashPassword)
                        {
                            res = true;
                        }
                    }
                };
                conn.Close();
            }

            return res;
        }

        private Users Mapper(SqlDataReader reader)
        {
            Users model = new Users();
            int index = 0;

            model.Id = reader.GetInt32(index++);
            model.Email = reader.GetString(index++);
            model.Salt = reader.GetString(index++);
            model.HashPassword = reader.GetString(index++);

            return model;
        }
    }
}
