using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.IO;

using System.Configuration;
//using System.Data.OracleClient;
//using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using ESRI.ArcGIS.Controls;

namespace BaseLibs
{
    class Global
    {
        public const int EngineVersion = 100;//9.3, 100£¨10.0£©
        public static string m_sMxdPath = "";

        public static double m_dMinZGCLineLength = 1.0;
        public static string m_sLongitudeFieldName = "Lon";
        public static string m_sLatitudeFieldName = "Lat";
        public static string m_sRadiusFieldName = "Radius";
        public static string m_sDeltaDCFieldName = "deltaGC";
        public static string m_sGCFieldName = "gra_chg";
        public static string m_sGCPositiveFieldName = "p_gra";
        public static string m_sGCNegativeFieldName = "n_gra";
        public static string m_sGCCurFieldName = "YRS9805";
        public static string m_sGCEnergyFieldName = "engy";
        public static string m_sGCVarianceFieldName = "var";
        public static string m_sEqkFieldName = "Mag";
        public static string m_sLevelFieldName = "GRIDCODE";
        public static string m_sMinGCFieldName = "min_gra";
        public static string m_sMaxGCFieldName = "max_gra";
        public static string m_sAreaFieldName = "Area";
        public static string m_sMaxPositiveGCFieldName = "p_max_gra";
        public static string m_sMinPositiveGCFieldName = "p_min_gra";
        public static string m_sMaxNegativeGCFieldName = "n_max_gra";
        public static string m_sMinNegativeGCFieldName = "n_min_gra";     

        //e.g. c:\temp
        public static string GetMxdPath()
        {
            return m_sMxdPath;
        }
        //e.g. c:\temp\WSData
        public static string GetWorkspacePath()
        {
            string sPath = m_sMxdPath + "\\WSData";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            return sPath;
        }
        //public static Form g_pBaseForm = null;
        //public static ProgressFrm g_pProgressForm = null;
        //public static void ShowProgrss(int nMode)
        //{
        //    System.Windows.Forms.Application.DoEvents();
        //    if (g_pProgressForm != null)
        //    {
        //        g_pProgressForm = null;
        //    }
        //    g_pProgressForm = new ProgressFrm();
        //    g_pProgressForm.SetMode(nMode);
        //    g_pProgressForm.Show(g_pBaseForm);
        //    System.Windows.Forms.Application.DoEvents();
        //}

        //public static void HideProgrss()
        //{
        //    System.Windows.Forms.Application.DoEvents();
        //    if (g_pProgressForm == null || g_pProgressForm.IsDisposed)
        //    {
        //        return;
        //    }
        //    g_pProgressForm.Close();
        //    g_pProgressForm = null;
        //    System.Windows.Forms.Application.DoEvents();
        //}

        //public static bool InitGloalParameter()
        //{
        //    try
        //    {
        //        return true;
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //        return false;
        //    }
        //}

        //public static bool DisposeGloalResource()
        //{
        //    try
        //    {
                
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //        return false;
        //    }

        //}       
    }
}
