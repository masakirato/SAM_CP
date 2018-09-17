using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    public class Common
    {
        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum DDLFieldType
        {
            ProductGroup,
            Unit,
            DayGroup,
            MonthGroup,
            Year,
            Quarter,
            Size
        }

        public static String ContentType(string filename)
        {
            string contenttype = string.Empty;

            switch (filename)
            {
                case ".doc":
                    contenttype = "application/vnd.ms-word";
                    break;
                case ".docx":
                    contenttype = "application/vnd.ms-word";
                    break;
                case ".xls":
                    contenttype = "application/vnd.ms-excel";
                    break;
                case ".xlsx":
                    contenttype = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                    contenttype = "image/jpg";
                    break;
                case ".jpeg":
                    contenttype = "image/jpg";
                    break;
                case ".png":
                    contenttype = "image/png";
                    break;
                case ".gif":
                    contenttype = "image/gif";
                    break;
                case ".pdf":
                    contenttype = "application/pdf";
                    break;
                case ".txt":
                    contenttype = "textfile/txt";
                    break;
                default:
                    contenttype = "Undefine";
                    break;
            }

            return contenttype;
        }
    }
}