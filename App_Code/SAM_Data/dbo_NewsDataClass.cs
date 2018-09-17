using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class dbo_NewsDataClass
{
    //News_ID, News_Type, CV_Code, Subject, Content, Content_FileName, Content_File, VDO_Hyperlink, Start_Date, End_Date, Photo_Name, Photo, Created_Date, Created_By, Last_Modified_Date, Last_Modified_By

    public static DataSet GetNews(string searchSubject,string CV_Code)
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            string sqlStr = "GetNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[3];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetNews";

            spParameter[1] = new SqlParameter("@Search", SqlDbType.VarChar, 100);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = searchSubject;

            spParameter[2] = new SqlParameter("@CV_Code", SqlDbType.VarChar, 100);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = CV_Code;


            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);

            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    DataSet dsNews = new DataSet();
            //    DataTable dtNews = new DataTable();
            //    dtNews.Columns.Add("News_ID");
            //    dtNews.Columns.Add("News_Type");
            //    dtNews.Columns.Add("Agent_Name");
            //    dtNews.Columns.Add("Subject");
            //    dtNews.Columns.Add("Content");
            //    dtNews.Columns.Add("Content_FileName");
            //    dtNews.Columns.Add("Content_FileType");
            //    dtNews.Columns.Add("Content_File");
            //    dtNews.Columns.Add("VDO_Hyperlink");
            //    dtNews.Columns.Add("Start_Date");
            //    dtNews.Columns.Add("End_Date");
            //    dtNews.Columns.Add("Photo_Name");
            //    dtNews.Columns.Add("Photo");

            //    DataRow drNews = dtNews.NewRow();
            //    dtNews.Rows.Add(drNews);
            //    dsNews.Tables.Add(dtNews);

            //    ds = dsNews;
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static DataSet GetAgent()
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            string sqlStr = "GetNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[1];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetAgent";
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static DataSet GetNewsDetails(string newsID)
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            string sqlStr = "GetNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetNewsDetails";

            spParameter[1] = new SqlParameter("@newsID", SqlDbType.VarChar, 12);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = newsID;
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static Int32 GetNewsLastedID(string formatID)
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();
        int lastedID = 0;
        try
        {
            connDB.openConnection();
            string sqlStr = "GetNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[1];
            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetNewsIDLasted";
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);

            if (ds.Tables[0].Rows.Count != 0)
            {
                string newsID = ds.Tables[0].Rows[0]["News_ID"].ToString();
                string newsIDFormat = newsID.Substring(0, 8);

                if (formatID == newsIDFormat)
                {
                    lastedID = Convert.ToInt32(newsID.Substring(newsID.Length - 4));
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return lastedID;
    }

    public static DataSet GetDownloadFile(string newsID)
    {
        ConnectionManager connDB = new ConnectionManager();
        DataSet ds = new DataSet();

        try
        {
            connDB.openConnection();
            string sqlStr = "GetNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[2];

            spParameter[0] = new SqlParameter("@Mode", SqlDbType.VarChar, 100);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = "GetDownloadFile";

            spParameter[1] = new SqlParameter("@newsID", SqlDbType.VarChar, 12);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = newsID;
            #endregion

            ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return ds;
    }

    public static Boolean UpdateNews(string newsID, string newsType, string agentName, string subject, string content, string contentFilename, string contentFileType, Byte[] contentFile, string vdoLink, DateTime startDate, DateTime endDate, string photoName, string photoType, Byte[] photo, string userName)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "UpdateNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[15];

            spParameter[0] = new SqlParameter("@NewsID", SqlDbType.VarChar, 12);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = newsID;

            spParameter[1] = new SqlParameter("@NewsType", SqlDbType.VarChar, 7);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = newsType;

            spParameter[2] = new SqlParameter("@AgentName", SqlDbType.VarChar, 100);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = agentName.Replace("'", "''"); ;

            spParameter[3] = new SqlParameter("@Subject", SqlDbType.VarChar, 200);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = subject.Replace("'","''");

            spParameter[4] = new SqlParameter("@Content", SqlDbType.VarChar);
            spParameter[4].Direction = ParameterDirection.Input;
            spParameter[4].Value = content.Replace("'", "''");

            spParameter[5] = new SqlParameter("@ContentFileName", SqlDbType.VarChar, 200);
            spParameter[5].Direction = ParameterDirection.Input;
            spParameter[5].Value = contentFilename.Replace("'", "''");

            spParameter[6] = new SqlParameter("@ContentFile", SqlDbType.VarBinary);
            spParameter[6].Direction = ParameterDirection.Input;
            spParameter[6].Value = contentFile;

            spParameter[7] = new SqlParameter("@VDOHyperlink", SqlDbType.VarChar, 200);
            spParameter[7].Direction = ParameterDirection.Input;
            spParameter[7].Value = vdoLink.Replace("'", "''"); ;

            spParameter[8] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            spParameter[8].Direction = ParameterDirection.Input;
            spParameter[8].Value = startDate;

            spParameter[9] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            spParameter[9].Direction = ParameterDirection.Input;
            spParameter[9].Value = endDate;

            spParameter[10] = new SqlParameter("@PhotoName", SqlDbType.VarChar, 200);
            spParameter[10].Direction = ParameterDirection.Input;
            spParameter[10].Value = photoName;

            spParameter[11] = new SqlParameter("@Photo", SqlDbType.VarBinary);
            spParameter[11].Direction = ParameterDirection.Input;
            spParameter[11].Value = photo;

            spParameter[12] = new SqlParameter("@LastModifiedBy", SqlDbType.VarChar, 10);
            spParameter[12].Direction = ParameterDirection.Input;
            spParameter[12].Value = userName;

            spParameter[13] = new SqlParameter("@ContentFileType", SqlDbType.VarChar, 200);
            spParameter[13].Direction = ParameterDirection.Input;
            spParameter[13].Value = contentFileType;

            spParameter[14] = new SqlParameter("@PhotoType", SqlDbType.VarChar, 200);
            spParameter[14].Direction = ParameterDirection.Input;
            spParameter[14].Value = photoType;
            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean InsertNews(string newsID, string newsType, string agentName, string subject, string content, string contentFilename, string contentFileType, 
        Byte[] contentFile, string vdoLink, DateTime startDate, DateTime endDate, string photoName, string photoType, Byte[] photo, string userName,string CV_Code)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "InsertNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[17];

            spParameter[0] = new SqlParameter("@NewsID", SqlDbType.VarChar, 12);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = newsID;

            spParameter[1] = new SqlParameter("@NewsType", SqlDbType.VarChar, 7);
            spParameter[1].Direction = ParameterDirection.Input;
            spParameter[1].Value = newsType;

            spParameter[2] = new SqlParameter("@AgentName", SqlDbType.VarChar, 100);
            spParameter[2].Direction = ParameterDirection.Input;
            spParameter[2].Value = agentName.Replace("'", "''"); ;

            spParameter[3] = new SqlParameter("@Subject", SqlDbType.VarChar, 200);
            spParameter[3].Direction = ParameterDirection.Input;
            spParameter[3].Value = subject.Replace("'", "''"); ;

            spParameter[4] = new SqlParameter("@Content", SqlDbType.VarChar);
            spParameter[4].Direction = ParameterDirection.Input;
            spParameter[4].Value = content.Replace("'", "''"); ;

            spParameter[5] = new SqlParameter("@ContentFileName", SqlDbType.VarChar, 200);
            spParameter[5].Direction = ParameterDirection.Input;
            spParameter[5].Value = contentFilename.Replace("'", "''"); ;

            spParameter[6] = new SqlParameter("@ContentFile", SqlDbType.VarBinary);
            spParameter[6].Direction = ParameterDirection.Input;
            spParameter[6].Value = contentFile;

            spParameter[7] = new SqlParameter("@VDOHyperlink", SqlDbType.VarChar, 200);
            spParameter[7].Direction = ParameterDirection.Input;
            spParameter[7].Value = vdoLink.Replace("'", "''"); ;

            spParameter[8] = new SqlParameter("@StartDate", SqlDbType.DateTime);
            spParameter[8].Direction = ParameterDirection.Input;
            spParameter[8].Value = startDate;

            spParameter[9] = new SqlParameter("@EndDate", SqlDbType.DateTime);
            spParameter[9].Direction = ParameterDirection.Input;
            spParameter[9].Value = endDate;

            spParameter[10] = new SqlParameter("@PhotoName", SqlDbType.VarChar, 200);
            spParameter[10].Direction = ParameterDirection.Input;
            spParameter[10].Value = photoName.Replace("'", "''"); ;

            spParameter[11] = new SqlParameter("@Photo", SqlDbType.VarBinary);
            spParameter[11].Direction = ParameterDirection.Input;
            spParameter[11].Value = photo;

            spParameter[12] = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 10);
            spParameter[12].Direction = ParameterDirection.Input;
            spParameter[12].Value = userName;

            spParameter[13] = new SqlParameter("@LastModifiedBy", SqlDbType.VarChar, 10);
            spParameter[13].Direction = ParameterDirection.Input;
            spParameter[13].Value = userName;

            spParameter[14] = new SqlParameter("@ContentFileType", SqlDbType.VarChar, 200);
            spParameter[14].Direction = ParameterDirection.Input;
            spParameter[14].Value = contentFileType;

            spParameter[15] = new SqlParameter("@PhotoType", SqlDbType.VarChar, 200);
            spParameter[15].Direction = ParameterDirection.Input;
            spParameter[15].Value = photoType;


            spParameter[16] = new SqlParameter("@CV_Code", SqlDbType.VarChar, 10);
            spParameter[16].Direction = ParameterDirection.Input;
            spParameter[16].Value = CV_Code;

            #endregion

            int rowsAffected = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

    public static Boolean DeleteNews(string newsID)
    {
        ConnectionManager connDB = new ConnectionManager();
        bool status = false;

        try
        {
            connDB.openConnection();
            string sqlStr = "DeleteNews";

            #region SQL Parameter
            SqlParameter[] spParameter = new SqlParameter[1];
            spParameter[0] = new SqlParameter("@News_ID", SqlDbType.VarChar, 12);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = newsID;
            #endregion

            int rowsAffectedItem = SqlHelper.ExecuteNonQuery(connDB.getConnectionString(), CommandType.StoredProcedure, sqlStr, spParameter);
            status = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return status;
    }

}