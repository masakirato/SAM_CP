using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class SAMDataClass
{
    public static string connectionString
            = "Data Source=DESKTOP-9Q7P0NB\\SOMPONG2008;Initial Catalog=SAM_DB;Integrated Security=SSPI;Pooling=False;";



    public static SqlConnection GetConnection()
    {
        //System.Configuration.Configuration config =
        //System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
        //if (config.ConnectionStrings.ConnectionStrings.Count > 0)
        //{
        //    connectionString = config.ConnectionStrings.ConnectionStrings[0].ToString();
        //}

        //System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
        //if (config.AppSettings.Settings > 0)
        //{
        connectionString = ConfigurationSettings.AppSettings["DBConnectionString"];


        //if (!string.IsNullOrEmpty(customSetting))
        //{
        //    return customSetting;
        //}
        // }

        SqlConnection connection = new SqlConnection(connectionString);
        return connection;
    }

    public int getIdent_Current(string Table)
    {
        string query = null;
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = default(SqlDataReader);
        int returnValue = 0;

        query = "SELECT IDENT_CURRENT('" + Table + "')";
        connection = GetConnection();
        command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;
        try
        {
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    returnValue = Convert.ToInt32(reader.GetValue(0));
                }
            }
            reader.Close();
            connection.Close();
        }
        catch //(SqlException ex)
        {
            //MessageBox.Show(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return returnValue;
    }

    public int getIdent_Incr(string Table)
    {
        string query = null;
        SqlConnection connection = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader reader = default(SqlDataReader);
        int returnValue = 0;

        query = "SELECT IDENT_INCR('" + Table + "')";
        connection = GetConnection();
        command = new SqlCommand(query, connection);
        command.CommandType = CommandType.Text;
        try
        {
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    returnValue = Convert.ToInt32(reader.GetValue(0));
                }
            }
            reader.Close();
            connection.Close();
        }
        catch //(SqlException ex)
        {
            //MessageBox.Show(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return returnValue;
    }

    public int getAutoID(string Mode, string Table)
    {
        int Ident_Current = getIdent_Current(Table);
        if (Mode == "Last")
        {
            return Ident_Current;
        }
        else if (Mode == "New")
        {
            return Ident_Current = Ident_Current + getIdent_Incr(Table);
        }
        return Ident_Current;
    }


}





