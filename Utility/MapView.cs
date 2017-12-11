using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.DataSourcesRaster;
using System.Data;
using System.Drawing;
using GISBase;

//����:������ ��ͼ�����.
//Modified by czl,
namespace BaseLibs
{
    //��λ��ͼ�������ö��
    public enum MapOperation { DoNothing = 0, MapZoomByRect = 1, MapZoomOut = 2, MapCenterZoomIn = 3, MapCenterZoomOut = 4, MapPan = 5, MapFullScope = 6, MapSelect = 7, MapIdentify = 8, MapPrevious = 9, MapNext = 10, Map_PointSel = 11, MapCircleSelect = 12, MapPolygonSelect = 13, MapUserIdentify = 14, MapSelectPrint = 15, MapMeasure = 16, MapSelectByRect = 17, MapCreateLine=18,MapAddRectangleElement=19,MapAddCircleElemnt=20,MapAddEllipseElemnt=21,MapAddPolygonElement=22 };

    class FeatureAttributeEventArgs
    {
        public IArray SelectFeatures = null;
        public IPolygon pPolygon = null;
    }
    class MapView
    {
        public delegate void SelectFeatureEventHandler(object sender, FeatureAttributeEventArgs e);
        public event SelectFeatureEventHandler SelectFeaturEvent;
        public IMapDocument m_MapDocument = null;//��ͼ�ĵ�
        string m_strCurSelLayer = "";//��ǰ�ɲ���ͼ��
        private MapOperation m_CurMapOperation;//��ǰ��ͼ�����������
        ESRI.ArcGIS.Controls.AxMapControl axMapControl1 = null;
        public IEnvelope pEnv = new EnvelopeClass();
        //IPointCollection myPCol = new PolygonClass();  //����ȥ��ʼ��һ��IPointCollection
        IPolygon polygon = new PolygonClass();
        public IFeatureLayer m_CurSelLayer = null;
        private MainForm m_pMainform = null;
        //private IdentifyForm m_IdentifyForm = null;//Ŀ���ʾ����
        /// <summary>
        /// //��λת���������ѵ�������ϵ��λ"��"ת��Ϊƽ������ϵ�ĵ�λ���ס�����ǧ�ס���
        /// </summary>
        /// <param name="aim_value">��Ҫת������ֵ</param>
        /// <param name="geography_units">�������굥λ</param>
        /// <param name="plane_units">ƽ�����굥λ</param>
        public static void m_UnitConverter(double aim_value, esriUnits geography_units, esriUnits plane_units)
        {
            IUnitConverter myUnit = new UnitConverterClass();
            double PlaneLength = myUnit.ConvertUnits(aim_value, geography_units, plane_units);
        }

        public MapView(MainForm pMainform,ESRI.ArcGIS.Controls.AxMapControl pMapControl1)
        {
            m_pMainform = pMainform;
            axMapControl1 = pMapControl1;
            axMapControl1.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(axMapControl1_OnMouseDown);
            axMapControl1.OnMouseUp += new IMapControlEvents2_Ax_OnMouseUpEventHandler(axMapControl1_OnMouseUp);
            axMapControl1.OnKeyDown += new IMapControlEvents2_Ax_OnKeyDownEventHandler(axMapControl1_OnKeyDown);
            axMapControl1.OnDoubleClick += new IMapControlEvents2_Ax_OnDoubleClickEventHandler(axMapControl1_OnDoubleClick);
           // m_strCurSelLayer = sCurEditLayer;
        }

        void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
           /* IGraphicsContainerSelect graphicsContainerSelect = axMapControl1.ActiveView as IGraphicsContainerSelect;
            IEnumElement enumElements = graphicsContainerSelect.SelectedElements;
            if (enumElements == null) return;
            enumElements.Reset();
            IElement pElement = enumElements.Next();
            enumElements.Reset();
            pElement = enumElements.Next();
            if (pElement is IFillShapeElement)
            {
                IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
                IFillSymbol pFillSymbol = pFillShapeElement.Symbol;
                ElementSymbolFrm pElementSymbolFrm = new ElementSymbolFrm();
                pElementSymbolFrm.FillSymbol = pFillSymbol;
                if (pElementSymbolFrm.ShowDialog(axMapControl1) != DialogResult.OK) return;
                pFillSymbol = pElementSymbolFrm.FillSymbol;
                pFillShapeElement.Symbol = pFillSymbol;
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
            */
        }

        public MapView( ESRI.ArcGIS.Controls.AxMapControl pMapControl1)
        {
            axMapControl1 = pMapControl1;
            //Override the mouse message for new task, they are executed after the corresponding messages of MAIN FRAME! czl 2012, Texas State
            axMapControl1.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(axMapControl1_OnMouseDown);
            axMapControl1.OnMouseUp += new IMapControlEvents2_Ax_OnMouseUpEventHandler(axMapControl1_OnMouseUp);
            axMapControl1.OnKeyDown += new IMapControlEvents2_Ax_OnKeyDownEventHandler(axMapControl1_OnKeyDown);
            // m_strCurSelLayer = sCurEditLayer;
        }

        void axMapControl1_OnKeyDown(object sender, IMapControlEvents2_OnKeyDownEvent e)
        {
            if (e.keyCode == 27)//Esc
            {
                CurMapOperation = MapOperation.DoNothing;
            }
        }

        void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            switch (m_CurMapOperation)
            {
                case MapOperation.MapSelectByRect:
                    //Map_SearchByShape();
                    break;
                default:
                    break;
            }
        }

        void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (axMapControl1.CurrentTool != null) return;
            axMapControl1.Focus();
            switch (m_CurMapOperation)
            {
                case MapOperation.MapSelectByRect:
                    //Map_Select(e.mapX, e.mapY);Map_PointSelMapCircleSelect MapPolygonSelect
                    //MapSelectByRect();
                    MapSelectByGeometry();
                    break;
                case MapOperation.Map_PointSel:
                    //Map_PointSel(e.mapX, e.mapY);
                    Map_PointSel(e.mapX, e.mapY);
                    break;
                case MapOperation.MapCircleSelect:
                    //MapCircleSelect();
                    MapSelectByGeometry();
                    break;
                case MapOperation.MapPolygonSelect:
                    //MapPolygonSelect();
                    MapSelectByGeometry();
                    break;
                case MapOperation.MapUserIdentify:
                    MapUserIdentify(e.mapX, e.mapY);

                    break;
                case MapOperation.MapSelectPrint:
                    MapSelectPrint();
                    break;
                case MapOperation.MapCreateLine:
                    CreateLine();
                    break;
                case MapOperation.MapAddRectangleElement:
                    AddRectangleElement();
                    break;
                case MapOperation.MapAddCircleElemnt:
                    AddCircleElement();
                    break;
                case MapOperation.MapAddEllipseElemnt:
                    AddEllipseElement();
                    break;
                case MapOperation.MapAddPolygonElement:
                    AddPolygonElement();
                    break;
                default:
                    break;
            }
        }

        private void AddRectangleElement()
        {
            IEnvelope pEnvelope = axMapControl1.TrackRectangle();
            if (pEnvelope == null) return;
            IRectangleElement pRectangleElement = new RectangleElementClass();
            IElement pElement = pRectangleElement as IElement;
            pElement.Geometry = pEnvelope;
            AddElement(pElement);
        }

        private void AddCircleElement()
        {
            IGeometry pGeometry = axMapControl1.TrackCircle();
            if (pGeometry == null) return;
            ICircleElement pCircleElement = new CircleElementClass();
            IElement pElement = pCircleElement as IElement;
            pElement.Geometry = pGeometry;
            AddElement(pElement);
        }

        private void AddEllipseElement()
        {
            IEnvelope pEnvelope = axMapControl1.TrackRectangle();
            if (pEnvelope == null) return;

            IConstructEllipticArc pConstructEllipticArc = new  EllipticArcClass() ;   //�����E�A��Geometry
            pConstructEllipticArc.ConstructEnvelope(pEnvelope) ;
            IEllipticArc pEllipticArc = (IEllipticArc)pConstructEllipticArc ;
            ISegmentCollection pSegmentCollection=new PolygonClass();
            object Missing =Type.Missing;
            pSegmentCollection.AddSegment((ISegment)pEllipticArc,ref Missing,ref Missing);
            IGeometry pGeometry = pSegmentCollection as IGeometry;
            IEllipseElement pEllipseElement = new EllipseElementClass();
            IElement pElement = pEllipseElement as IElement;
            pElement.Geometry = pGeometry;
            AddElement(pElement);
        }

        private void AddPolygonElement()
        {
            IGeometry pGeometry = axMapControl1.TrackPolygon();
            if (pGeometry == null) return;
            IPolygonElement pPolygonElement = new PolygonElementClass();
            IElement pElement = pPolygonElement as IElement;
            pElement.Geometry = pGeometry;
            AddElement(pElement);
        }

        private void AddElement(IElement pElement)
        {
            IGraphicsContainer pGra = axMapControl1.ActiveView as IGraphicsContainer;
            IActiveView pAv = pGra as IActiveView;
            
            // �ڻ���ǰ����� axMapControl2 �е��κ�ͼ��Ԫ�� 
            pGra.DeleteAllElements();
            // ����ӥ��ͼ�еĺ��߿� 
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            // ����һ���߷��Ŷ��� 
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 1;
            pOutline.Color = pColor;
            // ������ɫ���� 
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 255;
            pColor.Blue = 255;
            pColor.Transparency = 0;
            // ���������ŵ����� 
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGra.AddElement((IElement)pFillShapeEle, 0);
            // ˢ�� 
            pAv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void CreateLine()
        {
            //m_pMainform.ClearSel();
            IGeometry pGeometry = axMapControl1.TrackLine();
            if (pGeometry == null) return;

            IObjectCopy pObjectCopy;
            pObjectCopy = new ObjectCopyClass();
            //�õ���ͼ�Ŀ���
            System.Object pCopiedGeometry;
            pCopiedGeometry = pObjectCopy.Copy(pGeometry);
            IPolyline pPolyline = new PolylineClass();
            System.Object pObj;
            pObj = (System.Object)pPolyline;
            pObjectCopy.Overwrite(pCopiedGeometry, ref pObj);
            //m_pMainform.ShowInfo(pPolyline, false);
        }
        

        public string CurSelectLayerName
        {
            get
            {
                return m_strCurSelLayer;
            }
            set
            {
                m_strCurSelLayer = value;
                if (axMapControl1 == null) return;
                IMap pMap = axMapControl1.Map;
                if (pMap.LayerCount == 0) return;
                IEnumLayer pEnumLayer;
                ILayer pLayer;
                pEnumLayer = pMap.get_Layers(null, true);
                if (pEnumLayer == null)
                {
                    return;
                }
                pEnumLayer.Reset();
                for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
                {
                    if (pLayer.Name == m_strCurSelLayer)
                    {
                        m_CurSelLayer = pLayer as IFeatureLayer;
                        break;
                    }
                }
            }
        }

        public IFeatureLayer GetCurSelectLayer()
        {
            return m_CurSelLayer;
        }

        public MapOperation CurMapOperation
        {
            get
            {
                return m_CurMapOperation;
            }
            set
            {
                ICommand pCommand = null;
                m_CurMapOperation = value;
                axMapControl1.CurrentTool = null;
                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                switch (m_CurMapOperation)
                {
                    case MapOperation.MapZoomByRect:
                        MapZoomByRect();
                        break;
                    case MapOperation.MapZoomOut:
                        MapZoomOut();
                        break;
                    case MapOperation.MapCenterZoomIn:
                        Map_CenterZoomIn();
                        break;
                    case MapOperation.MapCenterZoomOut:
                        Map_CenterZoomOut();
                        break;
                    case MapOperation.MapPan:
                        MapPan();
                        break;
                    case MapOperation.MapPrevious:
                        MapPreviousExtent();
                        break;
                    case MapOperation.MapNext:
                        MapNextExtent();
                        break;
                    case MapOperation.MapFullScope:
                        Map_FullScope();
                        break;
                    case MapOperation.MapSelect:
                        pCommand = new ControlsSelectToolClass(); //ѡ��Ҫ��
                        pCommand.OnCreate(axMapControl1.Object);     //ʹ��PageLayoutContro�ж�����Ա༭
                        axMapControl1.CurrentTool = (ITool)pCommand;
                        MapSelectFeature();
                        break;
                    case MapOperation.Map_PointSel:
                        pCommand = new ControlsSelectToolClass(); //ѡ��Ҫ��
                        pCommand.OnCreate(axMapControl1.Object);     //ʹ��PageLayoutContro�ж�����Ա༭
                        axMapControl1.CurrentTool = (ITool)pCommand;
                        break;
                    case MapOperation.MapUserIdentify:                        
                        axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                        break;
                    case MapOperation.DoNothing:
                        axMapControl1.CurrentTool = null;
                        break;
                    case MapOperation.MapIdentify:
                        pCommand = new ControlsMapIdentifyToolClass();
                        pCommand.OnCreate(axMapControl1.Object);
                        axMapControl1.CurrentTool = (ITool)pCommand;
                        break;
                    default:
                        break;

                }
            }
            
        }

        private void MapPreviousExtent()
        {
            IExtentStack pExtentStack = axMapControl1.ActiveView.ExtentStack;
            if (pExtentStack.CanUndo())
            {
                pExtentStack.Undo();
            }
        }

        private void MapNextExtent()
        {
            IExtentStack pExtentStack2 = axMapControl1.ActiveView.ExtentStack;
            if (pExtentStack2.CanRedo())
            {
                pExtentStack2.Redo();
            }
        }

        private void MapZoomByRect()
        {
            ICommand pCommand = new ControlsMapZoomInToolClass();
            ITool pTool = pCommand as ITool;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pTool;
        }//����ѡ��

        private void MapSelectFeature()
        {
            ICommand pCommand = new ControlsSelectFeaturesToolClass();
            ITool pTool = pCommand as ITool;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pTool;
        }//ѡ���ͼҪ��

        private void MapZoomOut()
        {
            ICommand pCommand = new ControlsMapZoomOutToolClass();
            ITool pTool = pCommand as ITool;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pTool;
        }//�Ŵ�

        private void MapPan()
        {
            ICommand pCommand = new ControlsMapPanToolClass();
            ITool pTool = pCommand as ITool;
            pCommand.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = pTool;
        }//����

        private void Map_ZoomByRect()
        {
            axMapControl1.Extent = axMapControl1.TrackRectangle();
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.ActiveView.Extent);
        }//���ηŴ�

        private void Map_ZoomOut(double mapX, double mapY)
        {
            IEnvelope pEnv = axMapControl1.ActiveView.Extent;
            pEnv.Expand(2, 2, true);
            double width = pEnv.Width, height = pEnv.Height;
            pEnv.XMin = mapX - width; pEnv.XMax = mapX + width;
            pEnv.YMin = mapY - height; pEnv.YMax = mapY + height;
            axMapControl1.ActiveView.Extent = pEnv;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.ActiveView.Extent);
        }

        private void Map_CenterZoomIn()
        {
            IEnvelope pEnv = axMapControl1.ActiveView.Extent;
            pEnv.Expand(0.8, 0.8, true);
            axMapControl1.ActiveView.Extent = pEnv;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.ActiveView.Extent);
        }//���ķŴ�

        private void Map_CenterZoomOut()
        {
            IEnvelope pEnv = axMapControl1.ActiveView.Extent;
            pEnv.Expand(1.6, 1.6, true);
            axMapControl1.ActiveView.Extent = pEnv;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.ActiveView.Extent);
        }//������С

        private void Map_Pan()
        {
            axMapControl1.Pan();
        }//����

        private void Map_FullScope()
        {
            axMapControl1.Extent = axMapControl1.FullExtent;
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, axMapControl1.ActiveView.Extent);
        }//ȫ����ʾ

        private void Map_PointSel(double mapX, double mapY)//��ѡҪ��
        {
            axMapControl1.Map.ClearSelection();
            IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
            pPoint.X = mapX; pPoint.Y = mapY;
            //ISelectionEnvironment pSelEnv = new SelectionEnvironment();
            //IRgbColor pRGBColor = new RgbColorClass();
            //pRGBColor.Red = 255;
            //pSelEnv.PointSelectionMethod = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            //pSelEnv.DefaultColor = pRGBColor;
            axMapControl1.Map.SelectByShape(pPoint, null, true);
            axMapControl1.ActiveView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, null, axMapControl1.Extent);
            Map_SearchByShape();
        }

        public IFeature GetSelFeature()
        {
            IMap pMap = axMapControl1.Map;
            if (pMap == null) return null;
            ISelection pFeatureSelction = pMap.FeatureSelection;
            if (pFeatureSelction == null) return null;
            IEnumFeature pEnumFeature = pFeatureSelction as IEnumFeature;
            IEnumFeatureSetup pEnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
            pEnumFeatureSetup.AllFields = true;
            FeatureAttributeEventArgs FeatureAttributes = new FeatureAttributeEventArgs();
            pEnumFeature.Reset();
            IFeature pFeature = pEnumFeature.Next();
            return pFeature;
        }

        private void MapSelectPrint()//�����ӡ���ڵ�ͼ
        {
            try
            {
                IEnvelope pEnv = new EnvelopeClass();
                pEnv = axMapControl1.TrackRectangle();
                SaveFileDialog exportJPGDialog = new SaveFileDialog();
                exportJPGDialog.Title = "����JPEGͼ��";
                exportJPGDialog.Filter = "Jpeg Files(*.jpg,*.jpeg)|*.jpg,*.jpeg";
                exportJPGDialog.RestoreDirectory = true;
                exportJPGDialog.ValidateNames = true;
                exportJPGDialog.OverwritePrompt = true;
                exportJPGDialog.DefaultExt = "jpg";

                if (exportJPGDialog.ShowDialog() == DialogResult.OK)
                {
                    double lScreenResolution;
                    lScreenResolution = axMapControl1.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;
                    // lScreenResolution =  axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;
                    IExport pExporter = new ExportJPEGClass() as IExport;
                    //IExport pExporter = new ExportPDFClass() as IExport;//ֱ�ӿ����ã���
                    pExporter.ExportFileName = exportJPGDialog.FileName;
                    pExporter.Resolution = lScreenResolution;

                    tagRECT deviceRECT;
                    //�����Ļ�ִ�е����µģ�������������ʱ�ͻ���ִ���Not enough memory to create requested bitmap
                    //MainaxMapControl.ActiveView.ScreenDisplay.DisplayTransformation.set_DeviceFrame(ref deviceRECT);
                    deviceRECT = axMapControl1.ActiveView.ExportFrame;
                    ////////////////////////////
                    //deviceRECT.left = 0;
                    //deviceRECT.top = 0;
                    // deviceRECT.right = (int)pActiveView.Extent.Width;
                    //deviceRECT.bottom = (int)pActiveView.Extent.Height;
                    ///////////////////////////////////

                    IEnvelope pDriverBounds = new EnvelopeClass();
                    //pDriverBounds = MainaxMapControl.ActiveView.FullExtent;

                    pDriverBounds.PutCoords(deviceRECT.left, deviceRECT.bottom, deviceRECT.right, deviceRECT.top);

                    pExporter.PixelBounds = pDriverBounds;

                    ITrackCancel pCancel = new CancelTrackerClass();
                    axMapControl1.ActiveView.Output(pExporter.StartExporting(), (int)lScreenResolution, ref deviceRECT, pEnv, pCancel);
                    pExporter.FinishExporting();
                    pExporter.Cleanup();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); return; }
        }

        private void MapSelectByGeometry()
        {
           // axMapControl1.Map.ClearSelection();
            IMap pMap = axMapControl1.Map;
            if (axMapControl1.Map.LayerCount == 0) return;
            if (pMap.LayerCount == 0) return;
           // pMap.FeatureSelection.Clear();
            //IFeatureSelection pFeatureSelection = m_CurSelLayer as IFeatureSelection;
            //ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            //pSpatialFilter.SearchOrder = esriSearchOrder.esriSearchOrderSpatial;
            
            IPolygon pGeometry = null;
            switch (m_CurMapOperation)
            {
                case MapOperation.MapSelectByRect:
                    {
                        //pGeometry = axMapControl1.TrackRectangle();  
                        IEnvelope env = axMapControl1.TrackRectangle();//Track����envelope
                        if (env.Width > 0.5)
                        {
                            axMapControl1.Map.ClearSelection();
                            EnvelopeToPolygon(env);  //��Envelope��ΪPolygon
                            pGeometry = polygon;
                        }
                        else//���������ж�Ϊ�ǵ������
                        { return; }
                    }
                    break;
                case MapOperation.MapPolygonSelect:
                    {
                        axMapControl1.Map.ClearSelection();
                        pGeometry = axMapControl1.TrackPolygon() as IPolygon;  //Track����Polygon
                    }
                    break;
                case MapOperation.MapCircleSelect:
                    {
                        axMapControl1.Map.ClearSelection();
                        pGeometry = axMapControl1.TrackCircle() as IPolygon;  //Track����Circle
                    }
                    break;
            }
            if (pGeometry == null || pGeometry.Length == 0) return;
            //m_pMainform.ShowInfo(pGeometry,false);
            ////MainForm.g_pTrackGeometry = pGeometry as IPolygon;   //����ק������ͼ�δ���CustomStatistics����
            //pSpatialFilter.Geometry = pGeometry;
            //pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            //pFeatureSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
            //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, axMapControl1.Extent);
            ////axMapControl1.ActiveView.Refresh();
        }

        private void MapSelectByRect()
        {
            IMap pMap = axMapControl1.Map;
            if (axMapControl1.Map.LayerCount == 0) return;
            if (pMap.LayerCount == 0) return;
            pMap.FeatureSelection.Clear();
            IFeatureSelection pFeatureSelection = m_CurSelLayer as IFeatureSelection;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.SearchOrder = esriSearchOrder.esriSearchOrderSpatial;
            IEnvelope pEnvelope = axMapControl1.TrackRectangle();  //Track����envelope
            IGeometry pGeometry = axMapControl1.TrackPolygon();    //��ȡ����ͳ�ƿھ�������Σ�
            //IPolygon pPolygon = axMapControl1.TrackPolygon() as IPolygon;
            //MainForm.g_pTrackGeometry = pPolygon;    //�����ק

            pSpatialFilter.Geometry = pEnvelope;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            pFeatureSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
            if (pFeatureSelection.SelectionSet.Count == 0) return;
            //IEnumFeature pEnumFeature = pMap.FeatureSelection as IEnumFeature;
            //IEnumFeatureSetup pEnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
            //pEnumFeatureSetup.AllFields = true;
            //pEnumFeature.Reset();
            //IFeature pFeature = pEnumFeature.Next();
            //IArray pFArray = new ArrayClass();
            //while (pFeature != null)
            //{
            //    IObjectClass pObjectClass = pFeature.Class;
            //    IFeatureClass pFeatureClass = pObjectClass as IFeatureClass;
            //    if (pFeatureClass.AliasName == m_strCurSelLayer)
            //    {
            //        pFArray.Add(pFeature);
            //    }
            //    pFeature = pEnumFeature.Next();
            //}
            //FeatureAttributes.SelectFeatures = pFArray;
            //SelectFeaturEvent(this, FeatureAttributes);
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, axMapControl1.Extent);

        }

        private void MapCircleSelect()//Բ��ѡ��
        {
            try
            {
                IMap pMap = axMapControl1.Map;
                if (axMapControl1.Map.LayerCount == 0) return;
                if (pMap.LayerCount == 0) return;
                ISelectionEnvironment pSelEnvi = new SelectionEnvironmentClass();
                // IEnvelope pEnv = new EnvelopeClass();pEnv = axMapControl1.TrackCircle();Dim pTopo As  
                IPolygon pPolygon = axMapControl1.TrackCircle() as IPolygon;   //��ȡ����ͳ�ƿھ���Բ��
                //MainForm.g_pTrackGeometry = pPolygon;    //�����קԲ
                ITopologicalOperator pTopo = pPolygon as ITopologicalOperator;
                pTopo.Simplify();
                pMap.SelectByShape(pPolygon, null, false);
                IEnumLayer pEnumLayer;
                ILayer pLayer;
                pEnumLayer = pMap.get_Layers(null, true);
                if (pEnumLayer == null)
                {
                    return;
                }
                pEnumLayer.Reset();
                for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
                {


                    if (pLayer is IFeatureLayer)
                    {
                        IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                        if (pFeatureLayer.Name == m_strCurSelLayer) continue;
                        IFeatureSelection ppFeatureSelction = pFeatureLayer as IFeatureSelection;
                        ppFeatureSelction.Clear();
                    }
                }

                ISelection pFeatureSelction = pMap.FeatureSelection;
                IEnumFeature pEnumFeature = pFeatureSelction as IEnumFeature;
                IFeature pFeature = pEnumFeature.Next();
                IArray pFArray = new ArrayClass();
                while (pFeature != null)
                {
                    IObjectClass pObjectClass = pFeature.Class;
                    IFeatureClass pFeatureClass = pObjectClass as IFeatureClass;
                    if (pFeatureClass.AliasName == m_strCurSelLayer)
                    {
                        pFArray.Add(pFeature);
                    }


                    pFeature = pEnumFeature.Next();
                }
                axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, axMapControl1.Extent);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); return; }
        }

        private void MapPolygonSelect()//��������ѡ��
        {
            IMap pMap = axMapControl1.Map;
            if (axMapControl1.Map.LayerCount == 0) return;
            if (pMap.LayerCount == 0) return;
            ISelectionEnvironment pSelEnvi = new SelectionEnvironmentClass();
            IPolygon pPolygon = axMapControl1.TrackPolygon() as IPolygon;
           // MainForm.g_pTrackGeometry = pPolygon;   //�����ק�������
            ITopologicalOperator pTopo = pPolygon as ITopologicalOperator;
            pTopo.Simplify();
            pMap.SelectByShape(pPolygon, null, false);
            IEnumLayer pEnumLayer;
            ILayer pLayer;
            pEnumLayer = pMap.get_Layers(null, true);
            if (pEnumLayer == null)
            {
                return;
            }
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (pLayer is IFeatureLayer)
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    if (pFeatureLayer.Name == m_strCurSelLayer) continue;
                    IFeatureSelection ppFeatureSelction = pFeatureLayer as IFeatureSelection;
                    ppFeatureSelction.Clear();
                }
            }

            ISelection pFeatureSelction = pMap.FeatureSelection;            
            IEnumFeature pEnumFeature = pFeatureSelction as IEnumFeature;
            IFeature pFeature = pEnumFeature.Next();
            IArray pFArray = new ArrayClass();
            while (pFeature != null)
            {
                IObjectClass pObjectClass = pFeature.Class;
                IFeatureClass pFeatureClass = pObjectClass as IFeatureClass;
                if (pFeatureClass.AliasName == m_strCurSelLayer)
                {
                    pFArray.Add(pFeature);
                }


                pFeature = pEnumFeature.Next();
            }

            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, axMapControl1.Extent);
        }

        private void Map_SearchByShape()
        {
            IMap pMap = axMapControl1.Map;
            ISelection pFeatureSelction = pMap.FeatureSelection;

            IEnumFeature pEnumFeature = pFeatureSelction as IEnumFeature;
            IEnumFeatureSetup pEnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
            pEnumFeatureSetup.AllFields = true;
            FeatureAttributeEventArgs FeatureAttributes = new FeatureAttributeEventArgs();
            IArray pFArray = new ArrayClass();
            pEnumFeature.Reset();
            IFeature pFeature = pEnumFeature.Next();

            while (pFeature != null)
            {

                pFArray.Add(pFeature);
                pFeature = pEnumFeature.Next();
            }
            FeatureAttributes.SelectFeatures = pFArray;
            if (SelectFeaturEvent != null)
            {
                SelectFeaturEvent(this, FeatureAttributes);
            }
        }
        /// <summary>
        /// ��ѯ������bug��lxk�޸���
        /// </summary>
        /// <param name="mapX"></param>
        /// <param name="mapY"></param>
        private void Map_IDentify(double mapX, double mapY)//���Բ�ѯ��ʾ
        {
            try
            {
                axMapControl1.Map.ClearSelection();
                IMap pMap = axMapControl1.Map;
                IGroupLayer pGroupLayer = new GroupLayer();
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    ILayer pLayer = pMap.get_Layer(i);

                    pGroupLayer.Add(pMap.get_Layer(i));
                }
                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.X = mapX; pPoint.Y = mapY;
                IIdentifyObj pIdObj;
                IIdentify pIdentify = pGroupLayer as IIdentify;
                IArray pIDArray;
                IFeatureIdentifyObj pFeatIdObj;
                IEnvelope pEnv = axMapControl1.ActiveView.Extent;
                pEnv.Height /= 100;
                pEnv.Width /= 100;
                pEnv.CenterAt(pPoint);
                pIDArray = pIdentify.Identify(pEnv);

                if (pIDArray != null)
                {
                    FeatureAttributeEventArgs FeatureAttributes = new FeatureAttributeEventArgs();
                    IArray pFArray = new ArrayClass();
                    for (int i = 0; i < pIDArray.Count; i++)
                    {
                        pFeatIdObj = pIDArray.get_Element(i) as IFeatureIdentifyObj;
                        pIdObj = pFeatIdObj as IIdentifyObj;
                        //pIdObj.Flash(axMapControl1.ActiveView.ScreenDisplay);

                        //pIdObj.Flash(axMapControl1.ActiveView.ScreenDisplay); 
                        IRowIdentifyObject pRowIdentifyObj = pFeatIdObj as IRowIdentifyObject;
                        IFeature pFeature = pRowIdentifyObj.Row as IFeature;
                        pFArray.Add(pFeature as object);

                        axMapControl1.Map.SelectByShape(pFeature.Shape.Envelope, null, false);
                        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, axMapControl1.Extent);

                    }

                    // FeatureAttributes.SelectFeatures=pFArray;
                    // SelectFeaturEvent(this, FeatureAttributes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void MapUserIdentify(double mapX, double mapY)
        {
            try
            {
                //axMapControl1.Map.ClearSelection();
                ESRI.ArcGIS.Controls.IMapControl3 m_mapControl = (IMapControl3)axMapControl1.Object;
                IMap pMap = axMapControl1.Map;
                IGroupLayer pGroupLayer = new GroupLayer();

                if (pMap.LayerCount == 0) return;
                IEnumLayer pEnumLayer;
                ILayer pLayer;
                pEnumLayer = pMap.get_Layers(null, true);
                if (pEnumLayer == null)
                {
                    return;
                }
                pEnumLayer.Reset();
                double dCurrScale = m_mapControl.MapScale;
                for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
                {
                    if (pLayer.Visible)
                    {
                        if (pLayer is IGroupLayer)
                        {
                            continue;
                        }
                        if (pLayer.MinimumScale != 0 && dCurrScale > pLayer.MinimumScale)
                        {
                            continue;
                        }
                        if (pLayer.MaximumScale != 0 && dCurrScale < pLayer.MaximumScale)
                        {
                            continue;

                        }
                        pGroupLayer.Add(pLayer);
                    }
                }

                IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.X = mapX; pPoint.Y = mapY;
                IIdentifyObj pIdObj;
                IIdentify pIdentify = pGroupLayer as IIdentify;
                IArray pIDArray;
                IFeatureIdentifyObj pFeatIdObj;
                
                IEnvelope pEnv = pPoint.Envelope;
                IDisplayTransformation pDT = m_mapControl.ActiveView.ScreenDisplay.DisplayTransformation;
                pEnv.Expand(pDT.VisibleBounds.Width / 200, pDT.VisibleBounds.Height / 200, false);

                pIDArray = pIdentify.Identify(pEnv);

                if (pIDArray == null||pIDArray.Count==0)return;

                pFeatIdObj = pIDArray.get_Element(0) as IFeatureIdentifyObj;
                pIdObj = pFeatIdObj as IIdentifyObj;

                IRowIdentifyObject pRowIdentifyObj = pFeatIdObj as IRowIdentifyObject;
                IFeature pFeature = pRowIdentifyObj.Row as IFeature;
                if (pFeature != null && pFeature.Shape != null && !pFeature.Shape.IsEmpty)
                {
                    //axMapControl1.FlashShape(pFeature.Shape);
                    axMapControl1.Map.SelectFeature(pIdObj.Layer, pFeature);
                    FlashFeature(axMapControl1, pFeature.Shape);
                }

              //  pIdObj.Flash(m_mapControl.ActiveView.ScreenDisplay);//The Flash method is not supported with ArcGIS Engine, use the IHookActions.DoActions() method with the esriHookActionsFlash for this functionality.
                ILayer pIdentiyLayer = pIdObj.Layer;

                DataTable pTable = new DataTable();
                DataColumn pDataColumn = pTable.Columns.Add();
                pDataColumn.ColumnName = "����";
                pDataColumn = pTable.Columns.Add();
                pDataColumn.ColumnName = "ֵ";

                DataRow pFirestDataRow=pTable.Rows.Add();
                pFirestDataRow[0] = "���ڲ�";
                pFirestDataRow[1] = pIdentiyLayer.Name;

                if (pIdentiyLayer is IFeatureLayer)
                {
                    IRowIdentifyObject pRowObj = pIdObj as IRowIdentifyObject;
                 //   IRow pRow = pRowObj.Row;

                    IFeature pRow = pRowObj.Row as IFeature;
                    IFields pFields = pRow.Fields;

                    for (int i = 0; i < pFields.FieldCount; i++)
                    {
                        IField pField = pFields.get_Field(i);
                        DataRow pDataRow = null;

                        /*
                        switch (pField.Type)
                        {
                            case esriFieldType.esriFieldTypeOID:
                                pDataRow = pTable.Rows.Add();
                                pDataRow[0] = pField.Name;
                                pDataRow[1] = pRow.OID.ToString();
                                break;
                            case esriFieldType.esriFieldTypeGeometry:
                                //pDataRow[0] = "Geometry";
                                //pDataRow[1] = QueryShapeType(pField.GeometryDef.GeometryType);;
                                break;
                            default:
                                pDataRow = pTable.Rows.Add();
                                pDataRow[0] = pField.Name;
                                pDataRow[1] = pRow.get_Value(i).ToString();
                                break;
                        }
                         * * */

                        //////////////////////////////////////////////////
                        string sValue = pRow.get_Value(i).ToString();
                        string strFName = pField.AliasName.ToUpper();
                        string strUName = strFName.ToUpper();
                        if (strUName == "SHAPE" || strUName == "LENGTH" || strUName == "OBJECTID" || strUName == "ID" || strUName == "FNODE_" || strUName == "TNODE_" || strUName == "LPOLY_" || strUName == "RPOLY_" || strUName == "SDXL_" || strUName == "SDXL_ID" || strUName == "OBJECTID_1" || strUName == "FID")
                        {
                            continue;
                        }
                        else if (strUName == "SHAPE.LEN" || strUName == "SHAPE_LENG")
                        {
                            strFName = "���γ���";
                        }
                        else if (strUName == "SHAPE_AREA" || strUName == "SHAPE.AREA")
                        {
                            strFName = "��������";
                        }
                        else if (strUName == "HEIGHT")
                        {
                            strFName = "�߳�";
                        }
                        else if (strUName == "NAME")
                        {
                            strFName = "����";
                        }
                        else if (strUName == "TYPE")
                        {
                            strFName = "����";
                        }
                        else if (strUName == "SUBTYPE")
                        {
                            strFName = "������";
                        }

                        if (strUName == "LENGTH" || strUName == "SHAPE.LEN" )
                        {
                            IUnitConverter myUnit = new UnitConverterClass();
                            sValue = Math.Round(myUnit.ConvertUnits((double)pRow.get_Value(i), esriUnits.esriMeters, esriUnits.esriKilometers), 2).ToString();
                            sValue = sValue.ToString() + "ǧ��";
                        }
                        if (strUName == "SHAPE_AREA" || strUName == "SHAPE.AREA")
                        {
                            IGeometry pGeo = pRow.ShapeCopy;
                            pGeo.Project(axMapControl1.Map.SpatialReference);
                            double strValue = GetAreaValue(pGeo)*0.000001;
                            //// double strValue = Math.Abs((double)pFeature.get_Value(j)) * 10585;
                            strValue = Math.Round(strValue, 2);
                            sValue = strValue.ToString() + "ƽ��ǧ��";
                        }
                        esriFieldType tFieldTypy = pField.Type;
                        if (tFieldTypy == esriFieldType.esriFieldTypeGeometry)
                        {
                            sValue = pField.GeometryDef.GeometryType.ToString();
                            sValue = sValue.Substring(12, sValue.Length - 12);
                        }

                        pDataRow = pTable.Rows.Add();
                        pDataRow[0] = strFName;
                        pDataRow[1] = sValue;
                       
                        //////////////////////////////////////////////////
                        
                    }
                }
                else if (pIdentiyLayer is ITinLayer)
                {
                    ITinLayer pTinLayer=(ITinLayer)pIdentiyLayer;
                    ITinSurface pTinSurface = (ITinSurface)pTinLayer.Dataset;
                    if (pTinSurface == null) return;
                    ITinSurfaceElement pTinSurfaceElement =  pTinSurface.GetSurfaceElement(pPoint);

                    if(pTinSurfaceElement==null)return;
                    IFields pFields = pTinLayer.Dataset.Fields;
                    DataRow pDataRow = null;

                    pDataRow = pTable.Rows.Add();
                    pDataRow[0] = "�߶�";
                    pDataRow[1] = pTinSurfaceElement.Elevation.ToString();

                    pDataRow = pTable.Rows.Add();
                    pDataRow[0] = "�¶�";
                    pDataRow[1] = pTinSurfaceElement.SlopeDegrees.ToString();

                    pDataRow = pTable.Rows.Add();
                    pDataRow[0] = "����";
                    pDataRow[1] = pTinSurfaceElement.AspectDegrees.ToString();
                }

                if (pIdentiyLayer is IRasterLayer)
                {
                    MessageBox.Show("aa");
                }
                //ShowIndetify(pTable);
                //m_pMainform.ShowIndetify(pTable);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


        }

        private double GetAreaValue(IGeometry pGeometry)
        {
            
            IPolygon pPolygon = (IPolygon)pGeometry;
            IArea pArea = (IArea)pPolygon;
            return pArea.Area;
        }
      /*
        private void ShowIndetify(DataTable pTable)
        {
            if (m_IdentifyForm == null || m_IdentifyForm.IsDisposed)
            {
                m_IdentifyForm = new IdentifyForm();
            }

            if (!m_IdentifyForm.Visible)
            {
                m_IdentifyForm.Show(axMapControl1);
            }
            m_IdentifyForm.ShowInfo(pTable);
        }
        */
        public static void FlashFeature(ESRI.ArcGIS.Controls.AxMapControl pMapControl1, IGeometry pGeometry)//Ҫ����˸
        {
            esriGeometryType pGeometryType = pGeometry.GeometryType;
            ISymbol pSymbol = null;
            IColor pColor = new RgbColorClass();
            pColor.RGB = 255;

            if (pGeometryType == esriGeometryType.esriGeometryPolygon)
            {
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                pSimpleFillSymbol.Color = pColor;
                pSymbol = pSimpleFillSymbol as ISymbol;
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPolyline || pGeometryType == esriGeometryType.esriGeometryLine)
            {
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                pSimpleLineSymbol.Color = pColor;
                pSimpleLineSymbol.Width = 5;
                pSymbol = pSimpleLineSymbol as ISymbol;
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPoint || pGeometryType == esriGeometryType.esriGeometryMultipoint)
            {
                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                pSimpleMarkerSymbol.OutlineColor = pColor;
                pSimpleMarkerSymbol.OutlineSize = 5;
                pSymbol = pSimpleMarkerSymbol as ISymbol;
            }
            else
            {
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                pSimpleFillSymbol.Color = pColor;
                pSymbol = pSimpleFillSymbol as ISymbol;
            }
            pMapControl1.FlashShape(pGeometry, 5, 200, pSymbol);
        }

        private void EnvelopeToPolygon(IEnvelope ev)  //��envelope��Ϊpolygon
        {
            IRing ring = new RingClass();
            object missing1 = Type.Missing;
            object missing2 = Type.Missing;
            (ring as IPointCollection).AddPoint(ev.LowerLeft, ref missing1, ref missing2);
            (ring as IPointCollection).AddPoint(ev.UpperLeft, ref missing1, ref missing2);
            (ring as IPointCollection).AddPoint(ev.UpperRight, ref missing1, ref missing2);
            (ring as IPointCollection).AddPoint(ev.LowerRight, ref missing1, ref missing2);
            (ring as IPointCollection).AddPoint(ev.LowerLeft, ref missing1, ref missing2);
            ring.Close();

            IPolygon p = new PolygonClass();
            (p as IGeometryCollection).AddGeometry(ring, ref missing1, ref missing2);
            p.SimplifyPreserveFromTo();
            polygon = p as IPolygon;
        }

        public static double ConvertDegreeToMeter(double dDegreeUint)
        {
            IUnitConverter myUnit = new UnitConverterClass();
            double dMeterUnit = myUnit.ConvertUnits(dDegreeUint, esriUnits.esriDecimalDegrees, esriUnits.esriMeters);
            return dMeterUnit;
        }

        public static double ConvertKMToDegree(double pKM,IMap pMap)
        {
            IUnitConverter myUnit = new UnitConverterClass();
            ///////�趨ͶӰ����ϵ֮��ͼ�εĳ��ȵ�λΪ��
            double len1 = 0.0;
            if (pMap.DistanceUnits == esriUnits.esriMeters)
            {
                len1 = myUnit.ConvertUnits(pKM, esriUnits.esriKilometers, esriUnits.esriMeters);
            } ////////////////
            else
            {
                len1 = myUnit.ConvertUnits(pKM, esriUnits.esriKilometers, esriUnits.esriDecimalDegrees);
            }
            return len1;
        }

        public static  IFeatureLayer GetLayerByName(IMap pMap, string strName)
        {
            if (pMap.LayerCount == 0) return null;
            IEnumLayer pEnumLayer;
            IFeatureLayer pFeatureLayer = null;
            ILayer pLayer = null;
            pEnumLayer = pMap.get_Layers(null, true);
            if (pEnumLayer == null)
            {
                return null;
            }
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (!(pLayer is IFeatureLayer)) continue;
                if (pLayer.Name == strName)
                {
                    pFeatureLayer = pLayer as IFeatureLayer;
                    break;
                }
            }
            return pFeatureLayer;
        }
        //��ȡͬ����ͼ��
        public static IArray GetLayersByName(IMap pMap, string strName)
        {
            if (pMap.LayerCount == 0) return null;
            IEnumLayer pEnumLayer;
            IFeatureLayer pFeatureLayer = null;
            ILayer pLayer = null;
            IArray pArray = new ArrayClass();
            pEnumLayer = pMap.get_Layers(null, true);
            if (pEnumLayer == null)
            {
                return null;
            }
            pEnumLayer.Reset();
            for (pLayer = pEnumLayer.Next(); pLayer != null; pLayer = pEnumLayer.Next())
            {
                if (!(pLayer is IFeatureLayer)) continue;
                if (pLayer.Name == strName)
                {
                    pFeatureLayer = pLayer as IFeatureLayer;
                   // break;
                    pArray.Add(pFeatureLayer);
                }
            }
            return pArray;
        }

        //��ȡͬ����ͼ���Լ���һ����grouplayer ����
        public static IArray GetLayersByName2(IMap pMap, string strName)
        {
            if (pMap.LayerCount == 0) return null;
            IArray pArray = new ArrayClass();
            string Name = string.Empty;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                ILayer pLayer = pMap.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    AddGroupLayer(pLayer, ref pArray,strName);
                }
                else
                {
                    if (pLayer.Name == strName && pLayer is IFeatureLayer)
                    {
                        AddLayer(pLayer, ref pArray, Name,null);
                    }
                }
            }
            return pArray;
        }

        private static void AddGroupLayer(ILayer mLayer, ref IArray mArray,string strname)
        {
            ICompositeLayer pComLayer = (ICompositeLayer)mLayer;
            IGroupLayer pGrouplayer=(IGroupLayer)mLayer;
            int ComCount = pComLayer.Count;
            string name = mLayer.Name;
            for (int i = 0; i < ComCount; i++)
            {
                ILayer pLayer = pComLayer.get_Layer(i);
                if (pLayer is IGroupLayer)
                {
                    AddGroupLayer(pLayer, ref mArray,strname);
                }
                else
                {
                    if (pLayer.Name == strname && pLayer is IFeatureLayer)
                    {
                        AddLayer(pLayer, ref mArray, name, pGrouplayer);
                    }
                }
            }

        }

        private static void AddLayer(ILayer mLayer, ref IArray mArray, string name,IGroupLayer mGroupLayer)
        {
            LayerAndGrouplayerName Layerinfo = new LayerAndGrouplayerName();
            Layerinfo.grouplayer = mGroupLayer;
            Layerinfo.GrouplayerName = name;
            Layerinfo.layer = mLayer;
            Layerinfo.layername = mLayer.Name;
            mArray.Add(Layerinfo);
        }       
    }

    class LayerAndGrouplayerName
    {
        private ILayer mlayer;
        private IGroupLayer mgrouplayer;
        private string mName = string.Empty;
        private string mGrouplayerName = string.Empty;

        public string layername
        {
            get { return mName; }
            set { mName = value; }
        }

        public string GrouplayerName
        {
            get { return mGrouplayerName; }
            set { mGrouplayerName = value; }
        }

        public ILayer layer
        {
            get { return mlayer; }
            set { mlayer = value; }
        }

        public IGroupLayer grouplayer
        {
            get { return mgrouplayer; }
            set { mgrouplayer = value; }
        }

        public override string ToString()
        {
            string result = string.Empty;
            if (GrouplayerName.Length > 0)
            {
                result = mGrouplayerName + "-" + mName;
            }
            else
            {
                result = mName;
            }
            return result;
        }
    }
       
}
