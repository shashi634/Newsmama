using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NewsCaptain
{
    public class common : System.Web.UI.Page
    {
        public string _connectionString = ConfigurationManager.ConnectionStrings["newsConnectionString"].ConnectionString;
        protected string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        private bool _isAdminAuthorised = false;

        protected bool IsAdminLoggedIn
        {
            get
            {
                _isAdminAuthorised = Session["AdminUser"] != null;
                return _isAdminAuthorised;
            }

            set { _isAdminAuthorised = value; }
        }
        private bool _isEditorAuthorised = false;

        protected bool IsEditorLoggedIn
        {
            get
            {
                _isEditorAuthorised = Session["EditorUser"] != null;
                return _isEditorAuthorised;
            }

            set { _isEditorAuthorised = value; }
        }
        private bool _isWriterAuthorised = false;

        protected bool IsWriterLoggedIn
        {
            get
            {
                _isWriterAuthorised = Session["WriterUser"] != null;
                return _isWriterAuthorised;
            }

            set { _isWriterAuthorised = value; }
        }

        public DataTable GetData(string query)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query))
                {
                    using (var sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (var dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected int CompleteOrder(string query)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand(query, con))
                {

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        protected DataSet GetDataMultipleRow(string query)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query))
                {
                    using (var sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (var dt = new DataSet())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        protected int EnterSignleValue(string procedureName, string parametrisedValue)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(procedureName, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@value", SqlDbType.NVarChar).Value = parametrisedValue;
                        var obj = new SqlParameter("@msg", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        cmd.Parameters.Add(obj);
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                        return obj.Value.ToString() == "201" ? 201 : 200;
                    }
                }
            }
            catch (Exception)
            {
                return 203;
            }
        }
       
        protected int EnterOnlyOneValue(string procedureName, string parametrisedValue)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(procedureName, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@value", SqlDbType.NVarChar).Value = parametrisedValue;
                        var obj = new SqlParameter("@msg", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        cmd.Parameters.Add(obj);
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                        return obj.Value.ToString() == "201" ? 201 : 200;
                    }
                }
            }
            catch (Exception)
            {
                return 203;
            }
        }

        protected int UpdateSignleValue(string procedureName, string parametrisedValue, string id)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(procedureName, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.UniqueIdentifier).Value = id;
                        cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = parametrisedValue;

                        var obj = new SqlParameter("@msg", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        cmd.Parameters.Add(obj);
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                        return obj.Value.ToString() == "201" ? 201 : 200;
                    }
                }
            }
            catch (Exception)
            {
                return 203;
            }
        }

        protected int DeleteData(string procedureName, int id)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(procedureName, sqlConnection))
                    {
                        var userGuid = Session["TLogin"].ToString().Split('|');
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                        cmd.Parameters.AddWithValue("@userId", SqlDbType.UniqueIdentifier).Value = new Guid(userGuid[2]);
                        var obj = new SqlParameter("@msg", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        cmd.Parameters.Add(obj);
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                        return obj.Value.ToString() == "201" ? 201 : 200;
                    }
                }
            }
            catch (Exception)
            {
                return 203;
            }
        }

        protected int DeleteData(string procedureName, Guid id)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var cmd = new SqlCommand(procedureName, sqlConnection))
                    {
                        var userGuid = Session["TLogin"].ToString().Split('|');
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SqlDbType.UniqueIdentifier).Value = id;
                        cmd.Parameters.AddWithValue("@userId", SqlDbType.UniqueIdentifier).Value = userGuid[0].ToString();
                        var obj = new SqlParameter("@msg", SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        cmd.Parameters.Add(obj);
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                        int statusCode=0;
                        if (obj.Value.ToString() == "201") {
                            statusCode = 201;
                        }
                        if (obj.Value.ToString() == "200")
                        {
                            statusCode = 200;
                        }
                        if (obj.Value.ToString() == "403")
                        {
                            statusCode = 403;
                        }
                        return statusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return 203;
            }
        }
        protected void PopulateDropDown(string query, DropDownList dropDown, string valueField, string idField)
        {
            dropDown.DataSource = GetData(query);
            dropDown.DataTextField = valueField;
            dropDown.DataValueField = idField;
            dropDown.DataBind();
        }

        protected decimal GenerateRandonNumber()
        {
            var rm = new Random();
            return rm.Next();
        }
    }
}