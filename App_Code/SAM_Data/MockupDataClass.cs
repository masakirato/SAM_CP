using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace QueryMockupDataClass
{
    public class MockupDataClass
    {
        ConnectionManager connDB = new ConnectionManager();

        public MockupDataClass()
        {

        }

        public DataSet GetMockupData()
        {
            DataSet ds = new DataSet();
            string sqlStr = "GetMockupData";

            try
            {
                connDB.openConnection();
                ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), System.Data.CommandType.StoredProcedure, sqlStr);
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
    }
}