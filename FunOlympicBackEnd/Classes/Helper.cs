using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Formats.Asn1;
using System.Text;

namespace FunOlympicBackEnd.Classes
{
    public class Helper
    {

        private IConfiguration configuration;
        public string connectionString;
        public Helper(IConfiguration _configuration)
        {
            IConfiguration configuration = _configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public string ReadDataToJson(string sql, SqlParameter[] parm, CommandType type)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.CommandType = type;
                cmd.CommandTimeout = 0;
                if (parm != null)
                {
                    cmd.Parameters.AddRange(parm);
                }
                con.Open();
                IDataReader dataReader = cmd.ExecuteReader();
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);

                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.WriteStartArray();
                    while (dataReader.Read())
                    {
                        jsonWriter.WriteStartObject();
                        int fields = dataReader.FieldCount;
                        for (int i = 0; i < fields; i++)
                        {
                            jsonWriter.WritePropertyName(dataReader.GetName(i));
                            if (dataReader[i] == System.DBNull.Value)
                            {
                                jsonWriter.WriteValue("");
                            }
                            else
                            {
                                jsonWriter.WriteValue(dataReader[i]);
                            }
                        }
                        jsonWriter.WriteEndObject();
                    }
                    jsonWriter.WriteEndArray();
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public int InsertUpdateCn(string sql, SqlParameter[] param, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = 0;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable ReadDataCn(string sql, SqlParameter[] parm, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;
                comm.CommandType = cmdType;
                comm.CommandTimeout = 0;
                if (parm != null)
                {
                    comm.Parameters.AddRange(parm);
                }
                conn.Open();
                SqlDataAdapter ada = new SqlDataAdapter(comm);
                DataTable ds = new DataTable();
                ada.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
