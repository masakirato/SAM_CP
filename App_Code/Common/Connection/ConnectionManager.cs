using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

/// <summary>
/// Summary description for ConnectionManager
/// </summary>

[Serializable]
public class ConnectionManager
{
    private SqlConnection _sqlConnection;
    private string _connectionStrng;
    
	public ConnectionManager()
	{
        this._sqlConnection = null;
        this._connectionStrng = getConnectionString();
	}

    public ConnectionManager(string connectionString)
    {
        this._sqlConnection = null;
        this._connectionStrng = connectionString;
    }

    public string getConnectionString()
    {
        return ConfigurationSettings.AppSettings["DBConnectionString"];
    }

    public void setConnectionString(string connectionSring)
    {
        this._connectionStrng = connectionSring;
    }

    public SqlConnection getConnection()
    {
        return this._sqlConnection;
    }

    public void setConnection(SqlConnection sqlConnection)
    {
        this._sqlConnection = sqlConnection;
    }

    // Open Connection 
    // 
    public void openConnection()
    {
        if (this._sqlConnection == null)
        {

            try
            {
                this._sqlConnection = new SqlConnection(this._connectionStrng);
                this._sqlConnection.ConnectionString = this._connectionStrng;
                this._sqlConnection.Open();
            }
            catch (Exception ex)
            {
                //throw new SQLServerBaseException("OpenConnection error.", ex);
            }
        }
        else if (this._sqlConnection.State != ConnectionState.Open)
        {
            _sqlConnection.ConnectionString = this._connectionStrng;
            try
            {
                this._sqlConnection.Open();
            }
            catch (Exception ex)
            {
                //throw new SQLServerBaseException("OpenConnection error.", ex);
            }
        }
    }

    // Close Connection
    //
    public void closeConnection()
    {
        try
        {
            Debug.WriteLine("Close connection..");
            if (this._sqlConnection != null)
            {
                Debug.WriteLine("Connection not null...");
                if (this._sqlConnection.State == ConnectionState.Open)
                {
                    Debug.WriteLine("Connection open...");
                    Debug.WriteLine("Try to close connection...");
                    this._sqlConnection.Close();
                    Debug.WriteLine("Close connection success..");
                }
                Debug.WriteLine("Set connection to null..");
                this._sqlConnection = null;
                Debug.WriteLine("Close connection method finish..");
            }
        }
        catch (Exception ex)
        {
            Debug.Write("Error Conection : " + ex.ToString() + "\n" + ex.Message);
            //throw new SQLServerBaseException("CloseConnection error.", ex);
            //throw new Exception(ex.ToString() );

        }
        //			catch(SqlException ex)
        //			{
        //			    throw new SQLServerBaseException("CloseConnection error.", ex);  
        //			}
    } 
}