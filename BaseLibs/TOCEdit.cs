using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using System.Windows.Forms;
using GISBase;
using GISBase.Forms;

namespace BaseLibs
{
    enum TOCEditType
    {
        DoNothing=0,
        AddLayer=1,       //添加图层
        NewGroupLayer=2,//添加组图层
        CopyLayer=3,    //拷贝图层
        PasteLayer=4,   //粘贴图层
        RemoveLayer=5,  //移除图层
        TurnAllLayersOn=6, //打开所有图层
        TurnAllLayersOff=7,  //关闭所有图层
        SelectAllLayers=8,   //选择所有图层
        ExpandAllLayers=9,   //展开所有图层
        CollapseAllLayers=10,  //折叠所有图层
        ZoomToLayer=11,     //缩放到图层
        OpenAttributeTable=12,  //打开图层属性表
        CutLayer=13,   //剪切图层
        LableLayer=14, //标注图层
    }
    class TOCEdit
    {
        private ESRI.ArcGIS.Controls.AxTOCControl m_pTOCControl;
        private ESRI.ArcGIS.Controls.AxMapControl m_pMapControl;
        private ILayer m_pTOCLayer;
        private TOCEditType m_TOCEditType;  //当前编辑类型
        private IList<ILayer> m_CopyedLayers=new List<ILayer>();   //当前拷贝的图层
        int m_nKeyDownCode;             //当前按下的键
        private MainForm m_pMainForm = null;

        //当前操作类型
        public TOCEditType TOCEditType
        {
            get { return m_TOCEditType; }
            set 
            { 
                m_TOCEditType = value;
                switch (m_TOCEditType)
                {
                    case TOCEditType.AddLayer:
                        TOC_AddLayer();
                        break;
                    case TOCEditType.ExpandAllLayers:
                        TOC_ExpandAllLayers();
                        break;
                    case TOCEditType.CollapseAllLayers:
                        TOC_CollapseAllLayers();
                        break;
                    case TOCEditType.CopyLayer:
                        TOC_CopyLayer();
                        break;
                    case TOCEditType.PasteLayer:
                        TOC_PasteLayers(get_LayIndex(m_pTOCLayer)+1);
                        break;
                    case TOCEditType.NewGroupLayer:
                        TOC_NewGroupLayer();
                        break;
                    case TOCEditType.ZoomToLayer:
                        TOC_ZoomToLayer(m_pTOCLayer);
                        break;
                    case TOCEditType.RemoveLayer:
                        TOC_RemoveLayer(m_pTOCLayer);
                        break;
                    case TOCEditType.TurnAllLayersOff:
                        TOC_TurnAllLayersOFF();
                        break;
                    case TOCEditType.TurnAllLayersOn:
                        TOC_TurnAllLayersOn();
                        break;
                    case TOCEditType.CutLayer:
                        TOC_CutLayer();
                        break;
                    case TOCEditType.SelectAllLayers:
                        TOC_SelectAllLayers();
                        break;
                    case TOCEditType.OpenAttributeTable:
                        TOC_OpenAttributeTable();
                        break;
                    case TOCEditType.LableLayer:
                        TOC_LabelLayer(m_pTOCLayer);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pTOCControl"></param>
        /// <param name="pMapControl"></param>
        public TOCEdit(ESRI.ArcGIS.Controls.AxTOCControl pTOCControl, ESRI.ArcGIS.Controls.AxMapControl pMapControl, MainForm pMain)
        {
            m_pMainForm = pMain;
            m_pMapControl = pMapControl;
            m_pTOCControl = pTOCControl;
            m_pTOCControl.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(m_pTOCControl_OnMouseDown);
            m_pTOCControl.OnKeyDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnKeyDownEventHandler(m_pTOCControl_OnKeyDown);
            m_pTOCControl.OnKeyUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnKeyUpEventHandler(m_pTOCControl_OnKeyUp);
            m_pMapControl.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(m_pMapControl_OnMapReplaced);
            m_pMapControl.OnAfterDraw+=new IMapControlEvents2_Ax_OnAfterDrawEventHandler(m_pMapControl_OnAfterDraw);
        }

        private void m_pMapControl_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {
            //if (this.m_pMapControl.LayerCount != MainForm.g_LayerCount)
            //{
            //    MainForm.UpdateMapControl();
            //}
        }


        //地图更新时关联事件
        private void m_pMapControl_OnMapReplaced(object sender,IMapControlEvents2_OnMapReplacedEvent e)
        {
 
        }


        private void m_pTOCControl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            try
            {
                esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap basicMap = null;
                object unk = null;
                object data = null;
                this.m_pTOCControl.HitTest(e.x, e.y, ref itemType, ref basicMap, ref m_pTOCLayer, ref unk, ref data);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }

        private void m_pTOCControl_OnKeyDown(object sender,ITOCControlEvents_OnKeyDownEvent e)
        {
            switch(e.keyCode)
            {
                case (int)Keys.ControlKey:
                    break;
                case (int)Keys.C:
                    if(m_nKeyDownCode==(int)Keys.ControlKey)   //Ctrl+C键
                    {
                        TOC_CopyLayer();
                    }
                    break;
                case (int)Keys.V:
                    if (m_nKeyDownCode == (int)Keys.ControlKey)   //Ctrl+C键
                    {
                        TOC_PasteLayers(get_LayIndex(m_pTOCLayer));
                    }
                    break;
                default:
                    break;
            }
            m_nKeyDownCode = e.keyCode;
        }

        private void m_pTOCControl_OnKeyUp(object sender,ITOCControlEvents_OnKeyUpEvent e)
        {

        }

        //打开属性表
        private void TOC_OpenAttributeTable()
        {
            FrmAttributeTab frm = new FrmAttributeTab(m_pTOCLayer as IFeatureLayer);
            frm.ShowDialog();
        }


        //**添加图层**//
        private void TOC_AddLayer()
        {
            ICommand pCommand = new ControlsAddDataCommandClass();
            pCommand.OnCreate(this.m_pMapControl.Object);
            pCommand.OnClick();
        }
        //**展开所有图层**//
        private void TOC_ExpandAllLayers()
        {
            IEnumLayer pEnumLayer = this.m_pMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            ILegendInfo pLengendInfo;
            ILegendGroup pLengendGroup;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (pLayer is ILegendInfo)
                {
                    pLengendInfo = pLayer as ILegendInfo;
                    for (int i = 0; i < pLengendInfo.LegendGroupCount; i++)
                    {
                        pLengendGroup = pLengendInfo.get_LegendGroup(i);
                        pLengendGroup.Visible = true;
                    }
                }
            }
            this.m_pTOCControl.Update();
        }
        //**折叠所有图层**//
        private void TOC_CollapseAllLayers()
        {
            IEnumLayer pEnumLayer = this.m_pMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            ILegendInfo pLengendInfo;
            ILegendGroup pLengendGroup;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (pLayer is ILegendInfo)
                {
                    pLengendInfo = pLayer as ILegendInfo;
                    for (int i = 0; i < pLengendInfo.LegendGroupCount; i++)
                    {
                        pLengendGroup = pLengendInfo.get_LegendGroup(i);
                        pLengendGroup.Visible = false;
                    }
                }
            }
            this.m_pTOCControl.Update();
        }
        //**打开所有图层**//
        private void TOC_TurnAllLayersOn()
        {
            //打开地图图层
            IEnumLayer pEnumLayer = this.m_pMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                pLayer.Visible = true;
            }
            this.m_pMapControl.Refresh();
            this.m_pTOCControl.Update();
        }
        //**关闭所有图层**//
        private void TOC_TurnAllLayersOFF()
        {
            IEnumLayer pEnumLayer = this.m_pMapControl.Map.get_Layers(null, false);
            if (pEnumLayer == null) return;
            ILayer pLayer;
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                pLayer.Visible = false;
            }
            this.m_pMapControl.Refresh();
            this.m_pTOCControl.Update();
        }
        //**移除图层**//
        private void TOC_RemoveLayer(ILayer pLayer)
        {
            this.m_pMapControl.Map.DeleteLayer(pLayer);
            this.m_pMapControl.Refresh();
        }
        //**缩放到图层**//
        private void TOC_ZoomToLayer(ILayer pLayer)
        {
            this.m_pMapControl.Extent = pLayer.AreaOfInterest;
            this.m_pMapControl.ActiveView.Refresh();
        }
        //**新建图层组**//
        private void TOC_NewGroupLayer()
        {
            IGroupLayer pGroupLayer=new GroupLayerClass();
            this.m_pMapControl.AddLayer(pGroupLayer, 0);
        }
        //**拷贝图层**//
        private void TOC_CopyLayer()
        {
            IBasicMap basicMap = null;
            object unk = null;
            object data = null;
            ILayer pLayer = null;
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            this.m_pTOCControl.GetSelectedItem(ref itemType, 
                ref basicMap, ref pLayer, ref unk, ref data);
            m_CopyedLayers.Add(pLayer);

        }
        //**获取图层索引**//
        private int get_LayIndex(ILayer pLayer)
        {
            for(int i=0;i<this.m_pMapControl.LayerCount;i++)
            {
                if(pLayer==this.m_pMapControl.get_Layer(i))
                {
                    return i;
                }
            }
            return 0;
        }

        //**粘贴图层**//
        private void TOC_PasteLayers(int toIndex)
        {
            for (int i=0;i<m_CopyedLayers.Count;i++)
            {
                if(m_pTOCLayer is IGroupLayer)
                {
                    (m_pTOCLayer as IGroupLayer).Add(m_CopyedLayers[i]);
                }
                else
                {
                    this.m_pMapControl.AddLayer(m_CopyedLayers[i], toIndex);
                }
            }
            this.m_pTOCControl.Update();
            this.m_pMapControl.Refresh();
            m_CopyedLayers.Clear();
        }
        //**剪切图层**//
        private void TOC_CutLayer()
        {
            TOC_CopyLayer();
            TOC_RemoveLayer(m_pTOCLayer);
        }

        //**选择所有图层**//
        private void TOC_SelectAllLayers()
        {
            IGroupLayer pGroupLayer = new GroupLayerClass();
            for(int i=0;i<this.m_pMapControl.LayerCount;i++)
            {
                ILayer pLayer=this.m_pMapControl.get_Layer(i);
                pGroupLayer.Add(pLayer);
                //this.m_pTOCControl.SelectItem(pLayer,null);
            }
            this.m_pTOCControl.SelectItem(pGroupLayer);
        }
        private void TOC_LabelLayer(ILayer pLayer)
        {
            if (pLayer == null) return;
            if (!(pLayer is IFeatureLayer)) return;

            IGeoFeatureLayer pGeoFeaturelayer = (IGeoFeatureLayer)pLayer;
            bool boolKG = pGeoFeaturelayer.DisplayAnnotation;
            m_pMainForm.m_TM_LableLayer.Checked = !boolKG;

            if (m_pMainForm.m_TM_LableLayer.Checked == true)
            {
                //Select Field Name from Current Layers
                FrmSelectField frm = new FrmSelectField((IFeatureLayer)pLayer, "name");
                string sFieldName = "NAME";
                if (frm.ShowDialog() == DialogResult.Cancel) return;
                sFieldName = frm.strDefFieldName;
                GeoBaseLib.InitLabel(pGeoFeaturelayer, sFieldName);
                pGeoFeaturelayer.DisplayAnnotation = true;
            }
            else
            {
                pGeoFeaturelayer.DisplayAnnotation = false;
            }
            m_pMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, m_pMapControl.Extent);
        }
    }
    
}
