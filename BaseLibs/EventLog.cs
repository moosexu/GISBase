using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BaseLibs
{
    enum EnventType
    {
        etError = 0,
        etWarning = 1,
        etInformation = 2,
    }    
    struct EnventInfo
    {
        public string sSource;
        public string sStartTime;
        public string sEndTime;
        public string sDiscription;
    }
    class EnventLog
    {
        public static void WriteEventLog(EnventType nType, string sSource, string sDiscription,string sStartTime, string sEndTime)
        {
            string strFile = Global.GetWorkspacePath() +("\\Log.ini");            
            System.IO.FileStream fs = new System.IO.FileStream(strFile, System.IO.FileMode.Append);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            string sType="Information";

            switch (nType)
            {
                case EnventType.etError:
                    sType = "Error";
                    break;
                case EnventType.etWarning:
                    sType = "Warning";
                    break;
                case EnventType.etInformation:
                    sType = "Information";
                    break;
                default:
                    sType = "Other";
                    break;
            }
            string errInfo= "--------------------------------------------------------------------------------------------\r\n"
                    + "        Envent ID£º" + DateTime.Now.ToString() + "\r\n"
                    + "      Envent Type£º" + sType + "\r\n"
                    + "    Envent Source£º" + sSource + "\r\n"
                    + "       Start Time£º" + sStartTime + "\r\n"
                    + "         End Time£º" + sEndTime + "\r\n"
                    + "Event Discription£º" + sDiscription + "\r\n";
            sw.Write(errInfo);
            sw.Close();
            fs.Close();
        }
    }
}

