using OSGSolutions.Models.Domain;
using OSGSolutions.Models.Responses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;

namespace OSGSolutions.Services
{
    public class FileUploadService
    {
        public int Insert(UploadFile model)
        {
            int id = 0;
            ItemResponse<int> resp = new ItemResponse<int>();
            string systemFileName = string.Empty;

            if (model.ByteArray != null)
            {
                systemFileName = string.Format("{0}_{1}{2}",
                    model.FileUploadName,
                    Guid.NewGuid().ToString(),
                    model.Extension);

                SaveBytesFile(model.Location, systemFileName, model.ByteArray);
            };

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.Files_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FileUploadName", model.FileUploadName);
                    cmd.Parameters.AddWithValue("@SystemFileName", systemFileName);
                    cmd.Parameters.AddWithValue("@Location", model.Location);

                    SqlParameter parm = new SqlParameter("@Id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(parm);

                    cmd.ExecuteNonQuery();

                    id = (int)cmd.Parameters["@Id"].Value;
                }
                conn.Close();
            }
            return id;
        }



        private void SaveBytesFile(string location, string systemFileName, byte[] Bytes)
        {
            string fileBase = "~/images";
            var filePath = HttpContext.Current.Server.MapPath(fileBase + "/" + location + "/" + systemFileName);
            File.WriteAllBytes("C:/repos/github/OSGSolutions/Web/app/public/images/" + systemFileName, Bytes);
        }
    }

}
