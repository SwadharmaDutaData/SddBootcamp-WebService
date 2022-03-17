using System.Data;
using System.Data.SqlClient;

namespace SDD_Bootcamp.RestApi.ApiHelpers
{
    public class DatabaseConfig
    {
        public static SqlConnection GetOpenConnection()
        {
            try
            {
                string connStrings = ConstantsHelper.CONNECTION_STRINGS + ";pooling=true";
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connStrings);
                SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return connection;
            }
            catch (SqlException MyEx)
            {
                throw new Exception(MyEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static SqlCommand GetCommand(string query, SqlConnection connection, CommandType commandType = CommandType.Text, SqlParameter[]? parameters = null)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (string.IsNullOrEmpty(query))
                    throw new ArgumentNullException(nameof(query));

                cmd.Connection = connection;
                cmd.CommandType = commandType;
                cmd.CommandText = query;
                cmd.CommandTimeout = 15;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return cmd;
        }
    }
}
