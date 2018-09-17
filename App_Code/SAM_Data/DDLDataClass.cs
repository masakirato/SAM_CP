using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public class DDLDataClass
{
    ConnectionManager connDB = new ConnectionManager();
    string ProductGroup = "นมสดพาสเจอร์ไรส์,นมเปรี้ยว,โยเกิร์ตเมจิ,นมเปรี้ยวไพเกน,อื่นๆ";
    string Unit = "จำนวนเงิน,จำนวนหน่วย";
    string DayGroup = "วันอาทิตย์,วันจันทร์,วันอังคาร,วันพุธ,วันพฤหัสบดี,วันศุกร์,วันเสาร์";
    string MonthGroup = "มกราคม,กุมภาพันธ์,มีนาคม,เมษายน,พฤษภาคม,มิถุนายน,กรกฎาคม,สิงหาคม,กันยายน,ตุลาคม,พฤศจิกายน,ธันวาคม";
    string SizeDemo = "200,300";
    string QuarterlyGroup = "Q1,Q2,Q3,Q4";

    public DDLDataClass()
    {

    }

    //Todo: DDL get agent
    public DataTable ddlGetAgent()
    {
        DataTable dt = new DataTable();
        string sqlStr = "SELECT CV_Code, First_Name FROM Agent (nolock)";

        try
        {
            connDB.openConnection();
            DataSet ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), System.Data.CommandType.Text, sqlStr);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
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

        return dt;
    }

    //Todo: DDL get item value 
    public Dictionary<string, string> ddlGetItemValue(string item_id)
    {
        #region Query store procedure
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string sqlStr = "GetItemValue";

        try
        {
            connDB.openConnection();
            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@item_id", SqlDbType.VarChar, 4);
            spParameter[0].Direction = ParameterDirection.Input;
            spParameter[0].Value = item_id;

            DataSet ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), System.Data.CommandType.StoredProcedure, sqlStr, spParameter);
            if (ds.Tables.Count > 0)
                foreach (DataRow row in ds.Tables[0].Rows)
                    dict.Add(row[0].ToString(), row[1].ToString());
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            connDB.closeConnection();
        }

        return dict;
        #endregion

        #region Query command string
        //DataTable dt = new DataTable();
        //string sqlStr = "SELECT Item_Value_ID, Item_ID, Item_Value FROM ItemValue WHERE Item_ID = '" + id + "' ORDER BY Item_Value_ID";

        //try
        //{
        //    connDB.openConnection();
        //    DataSet ds = SqlHelper.ExecuteDataset(connDB.getConnectionString(), System.Data.CommandType.Text, sqlStr);

        //    if (ds.Tables.Count > 0)
        //    {
        //        dt = ds.Tables[0];
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //finally
        //{
        //    connDB.closeConnection();
        //}

        //return dt;
        #endregion
    }

    //Todo: DDL item fix value
    public Dictionary<string, string> ddlFixValue(Common.Common.DDLFieldType fieldType)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        switch (fieldType)
        {
            case Common.Common.DDLFieldType.ProductGroup:
                {
                    string[] pg = ProductGroup.Split(',');
                    int index = 0;
                    foreach (string word in pg)
                    {
                        dict.Add(word, index.ToString());
                        index++;
                    }
                    break;
                }
            case Common.Common.DDLFieldType.Unit:
                {
                    string[] un = Unit.Split(',');
                    int index = 0;
                    foreach (string word in un)
                    {
                        dict.Add(word, index.ToString());
                        index++;
                    }
                    break;
                }
            case Common.Common.DDLFieldType.DayGroup:
                {
                    string[] dg = DayGroup.Split(',');
                    int index = 0;
                    foreach (string word in dg)
                    {
                        dict.Add(word, index.ToString());
                        index++;
                    }
                    break;
                }
            case Common.Common.DDLFieldType.MonthGroup:
                {
                    string[] mg = MonthGroup.Split(',');
                    int index = 0;
                    foreach (string word in mg)
                    {
                        dict.Add(word, index.ToString());
                        index++;
                    }
                    break;
                }
            case Common.Common.DDLFieldType.Size:
                {
                    string[] sd = SizeDemo.Split(',');
                    int index = 0;
                    foreach (string word in sd)
                    {
                        dict.Add(word, index.ToString());
                        index++;
                    }
                    break;
                }
            case Common.Common.DDLFieldType.Year:
                {
                    var currentYear = DateTime.Today.Year;
                    for (int i = 3; i >= 0; i--)
                    {
                        int Year = currentYear - i;
                        dict.Add(Year.ToString(), Year.ToString());
                    }
                    break;
                }
            case Common.Common.DDLFieldType.Quarter:
                {
                    string[] qg = QuarterlyGroup.Split(',');
                    int index = 0;
                    foreach (string word in qg)
                    {
                        dict.Add(word, index.ToString());
                        index++;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }

        return dict;
    }
}