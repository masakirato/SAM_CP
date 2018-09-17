using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Messages
/// </summary>
public static class Messages
{

    public static string MessageBox()
    {
        return @"swal(""บันทึกสำเร็จ!"", """", ""success"")";
    }

    public static string DuplicateKeyAlert(string key)
    {
        return string.Format(@"swal(""ไม่สามารถบันทึกได้เนื่องจาก {0} ซ้ำ!"", """", ""error"")", key);
    }

    public static string UsedKeyAlert(string key)
    {
        return string.Format(@"swal(""ไม่สามารถลบได้เนื่องจาก {0} ใช้งาน!"", """", ""error"")", key);
    }

    public static string ExceptionAlert()
    {
        return @"swal(""error!"", """", ""error"")";
    }

    public static string SuccessAlert()
    {
        return @"swal(""บันทึกสำเร็จ!"", """", ""success"")";
    }

    public static string SuccessDeleteAlert()
    {
        return @"swal(""ลบสำเร็จ!"", """", ""success"")";
    }

    public static void Show(string message, Page _page)
    {
        try
        {
            string cleanMessage = message.Replace("'", "\'");
            string script = string.Format("alert('{0}');", cleanMessage);

            ScriptManager.RegisterStartupScript(_page, _page.GetType(), "SAM", script, true);
        }
        catch (Exception ex)
        {

        }
    }
}