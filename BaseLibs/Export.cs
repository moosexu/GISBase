using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Data;


using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using Microsoft.Office.Interop.Excel;

namespace BaseLibs
{
    public partial class Dataset4Excel
    {
               //Headers of Sheet
        DataSet m_pDataSet = null;
        
        public Dataset4Excel(string datasetName)
        {
            m_pDataSet = new System.Data.DataSet(datasetName);
        }
        private System.Data.DataTable FindTable(string tabName)
        {
            if (m_pDataSet == null) return null;
            foreach (System.Data.DataTable dt in m_pDataSet.Tables)
            {
                if (dt.TableName == tabName)
                {
                    return dt;
                }
            }
            return null;
        }

        public System.Data.DataTable AddTable(string tabName)
        {
            if(m_pDataSet==null) return null;
            System.Data.DataTable dt = new System.Data.DataTable(tabName);
            m_pDataSet.Tables.Add(dt);
            return dt;
        }

        public void AddColumns(string tabName,string[] columns)
        {
            foreach (System.Data.DataTable dt in m_pDataSet.Tables)
            {
                if(dt.TableName==tabName)
                {
                    for(int i=0;i<columns.Length;i++)
                    {
                        DataColumn dc = new DataColumn(columns[i]);
                        dt.Columns.Add(dc);
                        dc = null;
                    }
                }
            }            
        }
        public void AddRecord(string tabName, object[] columns)
        {
            System.Data.DataTable dt = FindTable(tabName);
            if (dt == null) return;
            if (columns == null || columns.Length != dt.Columns.Count) return;
            DataRow dr = dt.NewRow();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dr[i] = columns[i];
            }
            dt.Rows.Add(dr);
        }
        public void Export2Excel(string sFullName)
        {
            Export.ExportToExcel(m_pDataSet, sFullName);
        }
    }

    class Export
    {
        /// <summary>
        /// 输出图片
        /// </summary>
        /// <param name="MapCtrl">AxMapControl控件</param>
        public static void ExportFile(AxMapControl MapCtrl)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "(*.tif)|*tif|(*jpg)|*jpg|(*.pdf)|*.pdf|(*.bmp)|*.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    IExport pExport = null;
                    if (1 == sfd.FilterIndex)
                    {
                        pExport = new ExportTIFFClass();
                        pExport.ExportFileName = sfd.FileName + ".tif";
                    }
                    else if (2 == sfd.FilterIndex)
                    { pExport = new ExportJPEGClass(); pExport.ExportFileName = sfd.FileName + ".jpeg"; }
                    else if (3 == sfd.FilterIndex)
                    { pExport = new ExportPDFClass(); pExport.ExportFileName = sfd.FileName + ".pdf"; }
                    else if (4 == sfd.FilterIndex)
                    { pExport = new ExportBMPClass(); pExport.ExportFileName = sfd.FileName + ".bmp"; }
                    //pExport.ExportFileName = sfd.FileName; 
                    int res = 96;
                    pExport.Resolution = res;
                    tagRECT exportRECT = MapCtrl.ActiveView.ExportFrame;
                    IEnvelope pENV = new EnvelopeClass();
                    pENV.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
                    pExport.PixelBounds = pENV;
                    int Hdc = pExport.StartExporting();
                    IEnvelope pVisibleBounds = null;
                    ITrackCancel pTrack = null;
                    MapCtrl.ActiveView.Output(Hdc, (int)pExport.Resolution, ref exportRECT, pVisibleBounds, pTrack);
                    System.Windows.Forms.Application.DoEvents();
                    pExport.FinishExporting();
                    pExport.Cleanup();
                }
            }
            catch { }
        }

        //////////////////////////输出当前视图
        public static void ExportWindow(AxMapControl MapCtrl)
        {

            SaveFileDialog pSaveDialog = new SaveFileDialog();
            pSaveDialog.FileName = "";
            pSaveDialog.Filter = "JPG图片(*.JPG)|*.jpg|tif图片(*.tif)|*.tif";
            if (pSaveDialog.ShowDialog() == DialogResult.OK)
            {
                double iScreenDispalyResolution = MapCtrl.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;
                IExporter pExporter = null;
                if (pSaveDialog.FilterIndex == 1)
                {
                    pExporter = new JpegExporterClass();
                }
                else if (pSaveDialog.FilterIndex == 2)
                {
                    pExporter = new TiffExporterClass();
                }
                pExporter.ExportFileName = pSaveDialog.FileName;
                pExporter.Resolution = (short)iScreenDispalyResolution;
                tagRECT deviceRect = MapCtrl.ActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                IEnvelope pDeviceEnvelope = new EnvelopeClass();
                pDeviceEnvelope.PutCoords(deviceRect.left, deviceRect.bottom, deviceRect.right, deviceRect.top);
                pExporter.PixelBounds = pDeviceEnvelope;
                ITrackCancel pCancle = new CancelTrackerClass();
                MapCtrl.ActiveView.Output(pExporter.StartExporting(), pExporter.Resolution, ref deviceRect,MapCtrl.ActiveView.Extent, pCancle);
                pExporter.FinishExporting();
            }
        }
             

        /// <summary>
        /// Export DataGridView data to Excel file
        /// </summary>
        /// <param name="datagridview">DataGridView</param>
        /// <param name="SheetName">Excel sheet title</param>
        public static void DataGridView2Excel(DataGridView datagridview)
        {
            object missing = System.Reflection.Missing.Value;
            int nRowCount = datagridview.Rows.Count;
            int nDispalyColumnCount = 0;
            for (int i = 0; i <= datagridview.ColumnCount - 1; i++)
            {
                if (datagridview.Columns[i].Visible == true)
                {
                    nDispalyColumnCount++;
                }
            }  
            
            if (nRowCount <= 0 || nDispalyColumnCount <= 0 || nDispalyColumnCount > 255) return;
            Microsoft.Office.Interop.Excel.Application objExcel = null;
            Workbook workbook = null;
            Worksheet worksheet = null;
            try
            {
                objExcel = new Microsoft.Office.Interop.Excel.Application();
                workbook = objExcel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                worksheet = (Worksheet)workbook.Worksheets[1];//取得sheet1

                if (nRowCount > 65535)
                {
                    long pageRows = 60000;//定义每页显示的行数
                    int pageCount = (int)(nRowCount / pageRows);
                    if (pageCount * pageRows < nRowCount)//当总行数不被pageRows整除时，经过四舍五入可能页数不准
                    {
                        pageCount = pageCount + 1;
                    }
                    for (int sc = 1; sc <= pageCount; sc++)
                    {
                        if (sc > 1)
                        {
                            worksheet = (Worksheet)workbook.Worksheets.Add(missing, missing, missing, missing);//添加一个sheet
                        }
                        else
                        {
                            worksheet = (Worksheet)workbook.Worksheets[sc];//取得sheet1
                        }
                        string[,] datas = new string[pageRows + 1, nDispalyColumnCount + 1];
                        int displayColumnsCount = 1;
                        for (int i = 0; i <= datagridview.ColumnCount - 1; i++)
                        {
                            if (datagridview.Columns[i].Visible == true)
                            {
                                datas[0, i] = datagridview.Columns[i].HeaderText.Trim();
                                displayColumnsCount++;
                            }
                        }
                        Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, nDispalyColumnCount]];
                        range.Interior.ColorIndex = 15;//15代表灰色
                        range.Font.Bold = true;
                        range.Font.Size = 9;

                        int init = int.Parse(((sc - 1) * pageRows).ToString());
                        int r = 0;
                        int index = 0;
                        int result;
                        if (pageRows * sc >= nRowCount)
                        {
                            result = nRowCount;
                        }
                        else
                        {
                            result = int.Parse((pageRows * sc).ToString());
                        }
                        for (r = init; r < result; r++)
                        {
                            index = index + 1;
                            for (int i = 0; i < datagridview.ColumnCount; i++)
                            {
                                if (datagridview.Columns[i].Visible == true)
                                {
                                    if (datagridview.Columns[i].ValueType == typeof(string) || datagridview.Columns[i].ValueType == typeof(Decimal) ||
                                        datagridview.Columns[i].ValueType == typeof(DateTime) || datagridview.Columns[i].ValueType == typeof(Double) ||
                                        datagridview.Columns[i].ValueType == typeof(int))
                                    {
                                        object obj = datagridview.Rows[r].Cells[datagridview.Columns[i].Name].Value;
                                        datas[index, i] = obj == null ? "" : "'" + obj.ToString().Trim();//在obj.ToString()前加单引号是为了防止自动转化格式
                                    }
                                }
                            }
                            System.Windows.Forms.Application.DoEvents();
                        }
                        Range fchR = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[index + 2, nDispalyColumnCount + 1]];
                        fchR.Value2 = datas;

                        worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。 
                        range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[index + 1, nDispalyColumnCount]];
                        //15代表灰色
                        range.Font.Size = 9;
                        range.RowHeight = 14.25;
                        range.Borders.LineStyle = 1;
                        range.HorizontalAlignment = 1;
                    }
                }
                else
                {
                    string[,] datas = new string[nRowCount + 2, nDispalyColumnCount + 1];
                    int displayColumnsCount = 1;
                    for (int i = 0; i <= datagridview.ColumnCount - 1; i++)
                    {
                        if (datagridview.Columns[i].Visible == true)
                        {
                            datas[0, i] = datagridview.Columns[i].HeaderText.Trim();
                            displayColumnsCount++;
                        }
                    }
                    Range range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, nDispalyColumnCount]];
                    range.Interior.ColorIndex = 15;//15代表灰色
                    range.Font.Bold = true;
                    range.Font.Size = 9;
                    int r = 0;
                    for (r = 0; r < nRowCount; r++)
                    {
                        for (int i = 0; i < datagridview.ColumnCount; i++)
                        {
                            if (datagridview.Columns[i].Visible == true)
                            {
                                if (datagridview.Columns[i].ValueType == typeof(string) || datagridview.Columns[i].ValueType == typeof(Decimal) ||
                                    datagridview.Columns[i].ValueType == typeof(DateTime) || datagridview.Columns[i].ValueType == typeof(Double) ||
                                    datagridview.Columns[i].ValueType == typeof(int))
                                {
                                    object obj = datagridview.Rows[r].Cells[datagridview.Columns[i].Name].Value;
                                    datas[r + 1, i] = obj == null ? "" : "'" + obj.ToString().Trim();//在obj.ToString()前加单引号是为了防止自动转化格式
                                }
                            }
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }
                    Range fchR = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[nRowCount + 2, nDispalyColumnCount + 1]];
                    fchR.Value2 = datas;
                    worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。
                    range = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[nRowCount + 1, nDispalyColumnCount]];
                    //15代表灰色
                    range.Font.Size = 9;
                    range.RowHeight = 14.25;
                    range.Borders.LineStyle = 1;
                    range.HorizontalAlignment = 1;
                }

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = "xls";
                dlg.Filter = "Excel File(*.XLS)|*.xls";
                dlg.InitialDirectory = Global.GetWorkspacePath();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string sFilePath = dlg.FileName;
                    if (sFilePath != "")
                    {
                        try
                        {
                            workbook.SaveAs(sFilePath, 56, missing, missing, missing, missing, XlSaveAsAccessMode.xlShared, missing, missing, missing, missing, missing);
                            workbook.Saved = true;
                            //workbook.SaveCopyAs(m_FilePath);
                            MessageBox.Show("Succeeding in exporting excel file!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error happened when exporting excel file!\n" + ex.Message);
                        }
                    }
                }
            }
            finally
            {
                //关闭Excel应用
                if (workbook != null)
                    workbook.Close(missing, missing, missing);
                if (objExcel.Workbooks != null)
                    objExcel.Workbooks.Close();
                if (objExcel != null)
                    objExcel.Quit();
                worksheet = null;
                workbook = null;
                objExcel = null;
                GC.Collect();//强行销毁
            }      
        }

        public static void ExportToExcel(DataSet dataSet, string outputPath)
        {
            // Create the Excel Application object
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            // Create a new Excel Workbook
            Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
            int sheetIndex = 0;
            // Copy each DataTable
            foreach (System.Data.DataTable dt in dataSet.Tables)
            {
                // Copy the DataTable to an object array
                object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];
                // Copy the column names to the first row of the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    rawData[0, col] = dt.Columns[col].ColumnName;
                }
                // Copy the values to the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        rawData[row + 1, col] = dt.Rows[row].ItemArray[col];
                    }
                }
                // Calculate the final column letter
                string finalColLetter = string.Empty;
                string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int colCharsetLen = colCharset.Length;
                if (dt.Columns.Count > colCharsetLen)
                {
                    finalColLetter = colCharset.Substring(
                        (dt.Columns.Count - 1) / colCharsetLen - 1, 1);
                }
                finalColLetter += colCharset.Substring(
                        (dt.Columns.Count - 1) % colCharsetLen, 1);
                // Create a new Sheet
                Worksheet excelSheet = (Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, XlSheetType.xlWorksheet);
                excelSheet.Name = dt.TableName;
                // Fast data export to Excel
                string excelRange = string.Format("A1:{0}{1}",
                    finalColLetter, dt.Rows.Count + 1);
                excelSheet.Range[excelRange, Type.Missing].Value2 = rawData;
                // Mark the first row as BOLD
                ((Range)excelSheet.Rows[1, Type.Missing]).Font.Bold = true;
                excelSheet.Columns.EntireColumn.AutoFit();//列宽自适应。 
            }
            // Save and Close the Workbook
            excelWorkbook.SaveAs(outputPath, XlFileFormat.xlWorkbookNormal, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            excelWorkbook.Close(true, Type.Missing, Type.Missing);
            excelWorkbook = null;
            // Release the Application object
            excelApp.Quit();
            excelApp = null;
            // Collect the unreferenced objects
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        //end class
    }
}
