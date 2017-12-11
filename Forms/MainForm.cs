using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ESRI.ArcGIS.Controls;
using BaseLibs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.SystemUI;
using GISBase.Analysis;

using GISBase.Forms;

namespace GISBase
{
    public partial class MainForm : Office2007RibbonForm
    {
        private MapView m_MapView = null;
        private IMapDocument m_pMapDocument = null;
        //图层编辑对象
        private TOCEdit m_TOCEditor = null;
        public System.Windows.Forms.ToolStripMenuItem m_TM_LableLayer;

        bool m_bDocModified = false;
        public MainForm()
        {
            InitializeComponent();
        }
       
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;            
            eyeMap1.SetBuddyControl(axMapControl1);


            m_MapView = new MapView(this, axMapControl1);
            m_TOCEditor = new TOCEdit(this.axTOCControl1, this.axMapControl1,this);
            m_TM_LableLayer = TM_LableLayer;

        }

        private void AddLayer_btn_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ControlsAddDataCommandClass();
            pCommand.OnCreate(this.axMapControl1.Object);
            pCommand.OnClick();
            m_bDocModified = true;
        }

        private void ZoomIn_btn_Click(object sender, EventArgs e)
        {
            m_MapView.CurMapOperation = MapOperation.MapZoomByRect;  
        }
        private void ZoomOut_btn_Click(object sender, EventArgs e)
        {
            m_MapView.CurMapOperation = MapOperation.MapZoomOut;
        }

        private void Pan_btn_Click(object sender, EventArgs e)
        {
            m_MapView.CurMapOperation = MapOperation.MapPan;
        }

        private void Fullscope_btn_Click(object sender, EventArgs e)
        {
            m_MapView.CurMapOperation = MapOperation.MapFullScope;
        }
        private void OpenDoc_btn_Click(object sender, EventArgs e)
        {
             axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerHourglass;
            m_pMapDocument = new MapDocumentClass();
            try
            {
                OpenFileDialog dlgOpen = new OpenFileDialog();
                dlgOpen.Title = "打开文档";
                dlgOpen.Filter = "ArcMap Document(*.mxd)|*.mxd";
                string sFilePath = null;
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    sFilePath = dlgOpen.FileName;
                }
                else
                    return;

                if (sFilePath == "")
                {
                    MessageBox.Show("文件路径错误!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                //将数据载入pMapDocument并与map控件联系起来
                m_pMapDocument.Open(sFilePath, "");
                //IMapDocument对象中可能有多个Map对象，遍历每个map对象
                for (int i = 0; i < m_pMapDocument.MapCount; i++)
                {
                    axMapControl1.Map = m_pMapDocument.get_Map(i);
                }
                axMapControl1.DocumentFilename = m_pMapDocument.DocumentFilename;
                this.Text = this.Text + "  -  " + axMapControl1.DocumentFilename;
                axMapControl1.Refresh();
                FileInfo fi = new FileInfo(m_pMapDocument.DocumentFilename);
                Global.m_sMxdPath = fi.GetDirectory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }

        private void SaveMapAs()
        {
            if (m_bDocModified == false) return;
            try
            {
                //判断pMapDocument是否为空，
                if (axMapControl1.Map.LayerCount == 0) return;
                //获取pMapDocument对象
                IMxdContents pMxdC;
                pMxdC = axMapControl1.Map as IMxdContents;
                IMapDocument pMapDocument = new MapDocumentClass();
                //获取保存路径
                string saveURL = string.Empty;
                SaveFileDialog saveFileDialog;
                saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "保存地图";
                saveFileDialog.Filter = "Map Documents (*.mxd)|*.mxd";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveURL = saveFileDialog.FileName.ToString();
                }
                else
                {
                    MessageBox.Show("请选择文件路径!", "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                pMapDocument.New(saveURL);
                pMapDocument.ReplaceContents(pMxdC);
                pMapDocument.Save(true, true);
                this.Text = this.Text + "  -  " + saveURL;
                m_pMapDocument = pMapDocument;
                axMapControl1.DocumentFilename = m_pMapDocument.DocumentFilename;
                m_bDocModified = false;
                FileInfo fi = new FileInfo(m_pMapDocument.DocumentFilename);
                Global.m_sMxdPath = fi.GetDirectory();
                MessageBox.Show("保存完成!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            if (axMapControl1.DocumentFilename == null)
            {
                SaveMapAs();
            }
            if (m_bDocModified == false) return;
            try
            {
                if (axMapControl1.Map.LayerCount == 0) return;
                if (axMapControl1.DocumentFilename != null && axMapControl1.CheckMxFile(axMapControl1.DocumentFilename))
                {
                    if (m_pMapDocument != null)
                    {
                        m_pMapDocument.Close();
                        m_pMapDocument = null;
                    }
                    m_pMapDocument = new MapDocumentClass(); //实例化
                    m_pMapDocument.Open(axMapControl1.DocumentFilename, "");//必须的一步，用于将AxMapControl的实例的DocumentFileName传递给pMapDoc的
                    if (m_pMapDocument.get_IsReadOnly(m_pMapDocument.DocumentFilename))  //判断是否只读
                    {
                        MessageBox.Show("文件只读！", "失败", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        m_pMapDocument.Close();
                        return;
                    }
                    m_pMapDocument.ReplaceContents((IMxdContents)axMapControl1.Map); //重置
                    IObjectCopy lip_ObjCopy = new ObjectCopyClass(); //使用Copy，避免共享引用
                    axMapControl1.Map = (IMap)lip_ObjCopy.Copy(m_pMapDocument.get_Map(0));
                    lip_ObjCopy = null;
                    m_pMapDocument.Save(m_pMapDocument.UsesRelativePaths, true);
                    axMapControl1.DocumentFilename = m_pMapDocument.DocumentFilename;
                    this.Text = this.Text + "  -  " + axMapControl1.DocumentFilename;
                    m_bDocModified = false;

                    FileInfo fi = new FileInfo(m_pMapDocument.DocumentFilename);
                    Global.m_sMxdPath = fi.GetDirectory();
                    MessageBox.Show("保存完成!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Select_btn_Click(object sender, EventArgs e)
        {
            m_MapView.CurMapOperation = MapOperation.MapSelect;
        }

        private void Identify_btn_Click(object sender, EventArgs e)
        {
            m_MapView.CurMapOperation = MapOperation.MapIdentify;
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {

        }

        private void TM_DeleteLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.RemoveLayer;
        }

        private void TM_ZoomToLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.ZoomToLayer;
        }
               
        private void TB_CopyLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.CopyLayer;
        }

        private void TB_PasteLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.PasteLayer;
        }        

        private void TOC_SelectAllLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.SelectAllLayers;
        }

        private void TM_OpenAttribute_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.OpenAttributeTable;
        }

        private void TM_NewGroupLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.NewGroupLayer;
        }

        private void TM_NewLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.AddLayer;
        }

        private void TM_LableLayer_Click(object sender, EventArgs e)
        {
            m_TOCEditor.TOCEditType = TOCEditType.LableLayer;
        }


        //更新鹰眼控件
        public static void UpdateEagleMapControl()
        {
            //MainForm.g_eagleControl.Map = new MapClass();
            //FrmMain.g_eagleControl.ClearLayers();
            //for (int i = 1; i <= FrmMain.g_mapControl.LayerCount; i++)
            //{
            //    ILayer player = GeoBaseLib.LayerClone(FrmMain.g_mapControl.get_Layer(FrmMain.g_mapControl.LayerCount - i));
            //    if (player == null)
            //        continue;
            //    player.MaximumScale = FrmMain.g_mapControl.get_Layer(FrmMain.g_mapControl.LayerCount - i).MaximumScale;
            //    player.MinimumScale = FrmMain.g_mapControl.get_Layer(FrmMain.g_mapControl.LayerCount - i).MinimumScale;
            //    FrmMain.g_eagleControl.AddLayer(player);
            //}
            //FrmMain.g_eagleControl.Extent = FrmMain.g_mapControl.FullExtent;
            //FrmMain.g_eagleControl.Refresh();
        }

        //更新地图控件
        public static void UpdateMapControl()
        {
            //FrmMain.g_CMBLayerNames.Items.Clear();
            //FrmMain.g_eagleControl.ClearLayers();
            //if (FrmMain.g_mapControl.LayerCount > 0)
            //{
            //    ILayer pLayer = FrmMain.g_mapControl.get_Layer(0);
            //    FrmMain.g_CMBLayerNames.Text = pLayer.Name;
            //}
            //for (int i = 0; i < FrmMain.g_mapControl.LayerCount; i++)
            //{
            //    ILayer pLayer = FrmMain.g_mapControl.get_Layer(i);
            //    FrmMain.g_CMBLayerNames.Items.Add(pLayer.Name);
            //    ILayer pCloneLayer = GeoBaseLib.LayerClone(pLayer);
            //    if (pCloneLayer == null)
            //        continue;
            //    pCloneLayer.MaximumScale = pLayer.MaximumScale;
            //    pCloneLayer.MinimumScale = pLayer.MinimumScale;
            //    FrmMain.g_eagleControl.AddLayer(pCloneLayer);
            //}
            ////UpdateEagleMapControl();
            //FrmMain.g_eagleControl.Extent = FrmMain.g_mapControl.FullExtent;
            //FrmMain.g_eagleControl.Refresh();
            //FrmMain.g_LayerCount = FrmMain.g_mapControl.LayerCount;
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            try
            {
                esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap = null;
                ILayer layer = null;
                object unk = null;
                object data = null;
                this.axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
                if (e.button == 2)//鼠标右键
                {
                    switch (itemType)
                    {
                        case esriTOCControlItem.esriTOCControlItemLayer:
                            this.TOCMenu.Show(this.axTOCControl1, e.x, e.y);
                            break;
                        case esriTOCControlItem.esriTOCControlItemMap:
                            this.TOCMenu.Show(this.axTOCControl1, e.x, e.y);
                            break;
                        default:
                            break;
                    }
                }
                else if (e.button == 1) //鼠标左键
                {
                    switch (itemType)
                    {
                        case esriTOCControlItem.esriTOCControlItemLegendClass:
                            ILegendClass pLegendClass = ((ILegendGroup)unk).get_Class((int)data);
                            FrmSymbolSelector newSymbolSelectorFrm = new FrmSymbolSelector(pLegendClass, layer);
                            if (newSymbolSelectorFrm.ShowDialog() == DialogResult.OK)
                            {
                                this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                                pLegendClass.Symbol = newSymbolSelectorFrm.pSymbol;
                                this.axTOCControl1.Update();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (axMapControl1.LayerCount == 0) return;
            lbl_Coordinate.Text = "当前坐标：X:" + e.mapX.ToString() + " Y: " + e.mapY.ToString();
        }


        private void btn_SearchByAttr_Click(object sender, EventArgs e)
        {
            SearchByAttrFrm pSearchByAttrFrm = new SearchByAttrFrm(axMapControl1);
            pSearchByAttrFrm.Show(this);
        }

        private void btn_SearchBySpace_Click(object sender, EventArgs e)
        {
            FrmSelectLayer pLayerFrm = new FrmSelectLayer();
            pLayerFrm.m_axMapControl = axMapControl1;
            if (pLayerFrm.ShowDialog() == DialogResult.OK)
            {
                IArray aFeatures = GeoBaseLib.GetSelectedFeatures(axMapControl1);
                if (aFeatures.Count == 0)
                {
                    MessageBox.Show("没有选中目标！");
                    return;
                }
                IFeature pSelFeature = aFeatures.get_Element(0) as IFeature;
                try
                {
                    IArray pFeatures = GeoBaseLib.SelectFeatureByGeometry((IFeatureLayer)pLayerFrm.m_pLayer, pSelFeature.ShapeCopy);
                    for (int i = 0; i < pFeatures.Count; i++)
                    {
                        IFeature pFeature = pFeatures.get_Element(i) as IFeature;
                        IRgbColor pColor = new RgbColorClass();
                        pColor.RGB = 255;
                        GeoBaseLib.DrawElement(axMapControl1, pFeature.ShapeCopy, pColor);
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }            
            }
        }
            
        private void btn_UnSelect_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.SystemUI.ICommand pCommand = new ControlsClearSelectionCommandClass();
            pCommand.OnCreate(axMapControl1.Object);
            pCommand.OnClick();

            GeoBaseLib.ClearDrawItems(this.axMapControl1.ActiveView);
            axMapControl1.Refresh();
        }

        private void btn_DrawRect_Click(object sender, EventArgs e)
        {
            m_MapView.CurMapOperation = MapOperation.MapAddRectangleElement;
        }

        private void btn_Chart_Click(object sender, EventArgs e)
        {
            FrmSelectLayer pLayerFrm = new FrmSelectLayer();
            pLayerFrm.m_axMapControl = axMapControl1;
            if (pLayerFrm.ShowDialog() == DialogResult.OK)
            {
                ESRI.ArcGIS.SystemUI.ICommand cmd;
                cmd = new GISBase.Visualization.BarChart();
                cmd.OnCreate(axMapControl1.GetOcx());
                ((GISBase.Visualization.BarChart)cmd).pLayer = pLayerFrm.m_pLayer as IFeatureLayer;
                if (cmd.Enabled)
                {
                    cmd.OnClick();
                    this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                }
            }
        }

        private void btn_Buffer_Click(object sender, EventArgs e)
        {
            FrmBuffer frm = new FrmBuffer();
            frm.m_axMapControl = axMapControl1;
            if (frm.ShowDialog() == DialogResult.OK)
             {
                 SpatialAnalysis sa = new SpatialAnalysis();
                 sa.BufferOfFeatures(axMapControl1, (IFeatureLayer)frm.m_pLayer, frm.m_dRadius);
             }
        }

      
        private void btn_Test_Click(object sender, EventArgs e)
        {
            
        }
  
    }
}
