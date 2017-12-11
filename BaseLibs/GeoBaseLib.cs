using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesRaster;
using System.Text.RegularExpressions;

namespace BaseLibs
{

    /// The different layer GUID's and Interface's are:
    /// "{AD88322D-533D-4E36-A5C9-1B109AF7A346}" = IACFeatureLayer
    /// "{74E45211-DFE6-11D3-9FF7-00C04F6BC6A5}" = IACLayer
    /// "{495C0E2C-D51D-4ED4-9FC1-FA04AB93568D}" = IACImageLayer
    /// "{65BD02AC-1CAD-462A-A524-3F17E9D85432}" = IACAcetateLayer
    /// "{4AEDC069-B599-424B-A374-49602ABAD308}" = IAnnotationLayer
    /// "{DBCA59AC-6771-4408-8F48-C7D53389440C}" = IAnnotationSublayer
    /// "{E299ADBC-A5C3-11D2-9B10-00C04FA33299}" = ICadLayer
    /// "{7F1AB670-5CA9-44D1-B42D-12AA868FC757}" = ICadastralFabricLayer
    /// "{BA119BC4-939A-11D2-A2F4-080009B6F22B}" = ICompositeLayer
    /// "{9646BB82-9512-11D2-A2F6-080009B6F22B}" = ICompositeGraphicsLayer
    /// "{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E}" = ICoverageAnnotationLayer
    /// "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}" = IDataLayer
    /// "{0737082E-958E-11D4-80ED-00C04F601565}" = IDimensionLayer
    /// "{48E56B3F-EC3A-11D2-9F5C-00C04F6BC6A5}" = IFDOGraphicsLayer
    /// "{40A9E885-5533-11D0-98BE-00805F7CED21}" = IFeatureLayer
    /// "{605BC37A-15E9-40A0-90FB-DE4CC376838C}" = IGdbRasterCatalogLayer
    /// "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer
    /// "{34B2EF81-F4AC-11D1-A245-080009B6F22B}" = IGraphicsLayer
    /// "{EDAD6644-1810-11D1-86AE-0000F8751720}" = IGroupLayer
    /// "{D090AA89-C2F1-11D3-9FEF-00C04F6BC6A5}" = IIMSSubLayer
    /// "{DC8505FF-D521-11D3-9FF4-00C04F6BC6A5}" = IIMAMapLayer
    /// "{34C20002-4D3C-11D0-92D8-00805F7C28B0}" = ILayer
    /// "{E9B56157-7EB7-4DB3-9958-AFBF3B5E1470}" = IMapServerLayer
    /// "{B059B902-5C7A-4287-982E-EF0BC77C6AAB}" = IMapServerSublayer
    /// "{82870538-E09E-42C0-9228-CBCB244B91BA}" = INetworkLayer
    /// "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}" = IRasterLayer
    /// "{AF9930F0-F61E-11D3-8D6C-00C04F5B87B2}" = IRasterCatalogLayer
    /// "{FCEFF094-8E6A-4972-9BB4-429C71B07289}" = ITemporaryLayer
    /// "{5A0F220D-614F-4C72-AFF2-7EA0BE2C8513}" = ITerrainLayer
    /// "{FE308F36-BDCA-11D1-A523-0000F8774F0F}" = ITinLayer
    /// "{FB6337E3-610A-4BC2-9142-760D954C22EB}" = ITopologyLayer
    /// "{005F592A-327B-44A4-AEEB-409D2F866F47}" = IWMSLayer
    /// "{D43D9A73-FF6C-4A19-B36A-D7ECBE61962A}" = IWMSGroupLayer
    /// "{8C19B114-1168-41A3-9E14-FC30CA5A4E9D}" = IWMSMapLayer
    // for example
    //UID uid = new UIDClass();
    //uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";
    //IEnumLayer players = axMapControl1.Map.get_Layers(uid, true);
    //ILayer plyr = players.Next();
    //while (plyr != null)
    //{
    //     plyr.Next();
    //}
    public enum baseLayerType
    {
        baseGroupLayer = 0,
        baseFeatureLayer = 1,
        baseRasterLayer = 2,
        basePointLayer = 3,
        basePolylineLayer = 4,
        basePolygonLayer =5,
    }
    public enum baseColorRamp
    {
        Green2Red = 0,
        Red2Green = 1,
        Gray2Black =3,
        Black2Gray =4,
        LightBlue2DeepBlue  =5,
        DeepBlue2LightBlue = 6,
        LightGreen2DeepGreen =7,
        DeepGreen2LightGreen =8,
    }
    public struct baseField
    {
        public string strFieldName;
        public string strFieldAliasName;
        public esriFieldType ftFieldType;
        public int nFieldLen;
    }

    public class DataSetInfo
    {
        public string Name = null;
        public IArray pSubSetArray = null;
    }

    public class baseFieldList
    {
        public List<baseField> list = new List<baseField>();

        bool Exists(baseField field)
        {
            foreach (baseField item in list)
            {
                if (item.strFieldName.Equals(field.strFieldName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }          
                return false;
        }
        bool Exists(string sFieldName)
        {
            foreach (baseField item in list)
            {
                if (item.strFieldName.Equals(sFieldName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        public int Count
        {
            get { return list.Count; }
        }
        public void Add(baseField field)
        {
            if(!Exists(field))
                list.Add(field);
        }
        public void Add(string sFieldName, esriFieldType fieldType,int nFieldLen=0)
        {
            if (!Exists(sFieldName))
            {
                baseField field = new baseField();
                field.strFieldName = sFieldName;
                field.strFieldAliasName = sFieldName;
                field.ftFieldType = fieldType;
                field.nFieldLen = nFieldLen;
                list.Add(field);
            }
        }
        public baseField get_Element(int i)
        {           
            return list[i];
        }  
    }
    public class baseFeatureList
    {
        public List<IFeature> list = new List<IFeature>();
        private bool m_bSort = false;
        private bool m_bAscending = true;
        public int Count
        {
            get { return list.Count; }
        }
        public void Add(IFeature pFeature)
        {
            list.Add(pFeature);
            m_bSort = false;
        }
        public IFeature get_Element(int i)
        {
            if (list.Count == 0) return null;
            return list[i];
        }
        public IFeature get_Max()
        {
            if (list.Count == 0) return null;
            if (m_bAscending) return list[0];
            else return list[list.Count - 1];
        }
        public IFeature get_Min()
        {
            if (list.Count == 0) return null;
            if (m_bAscending) return list[list.Count - 1];
            else return list[0];
        }
        public void Sort(int idxSortField, bool bAscending = true)
        {
            m_bAscending = bAscending;
            for (int i = 0; i < list.Count; i++)
            {
                IFeature pFeature_i = list[i];
                double dValue_i = Convert.ToDouble(pFeature_i.get_Value(idxSortField));
                for (int j = i + 1; j < list.Count; j++)
                {
                    IFeature pFeature_j = list[j];
                    double dValue_j = Convert.ToDouble(pFeature_j.get_Value(idxSortField));
                    if (bAscending)
                    {
                        if (dValue_i < dValue_j)
                        {
                            IFeature pFeature_tmp = list[i];
                            list[i] = list[j];
                            list[j] = pFeature_tmp;                           
                            dValue_i = Convert.ToDouble(list[i].get_Value(idxSortField));//Being careful!!!!!
                        }
                    }
                    else
                    {
                        if (dValue_i > dValue_j)
                        {
                            IFeature pFeature_tmp = list[i];
                            list[i] = list[j];
                            list[j] = pFeature_tmp;
                            dValue_i = Convert.ToDouble(list[i].get_Value(idxSortField));//Being careful!!!!!
                        }
                    }
                }
            }
            m_bSort = true;
        }
    }

   
    class GeoBaseLib
    {
        //private IArray m_pLayersArray = null;
        private int m_nDataType = 1; //1---SHAPE, 2---GDB 
        private AxMapControl m_axMapControl = null;
        private bool m_bCreateZGCLine = false;
        IMapDocument m_pMapDocument = null;
        
        public bool bCreateZGCLine
        {
            set { m_bCreateZGCLine = value; }
            get { return m_bCreateZGCLine; }
        }
        public int nDataType
        {
            set { m_nDataType = value; }
            get { return m_nDataType; }
        }
        public GeoBaseLib(AxMapControl mapControl=null,IMapDocument mdoc=null)
        {
            m_axMapControl = mapControl;
            m_pMapDocument = mdoc;
        }
        public int GetMapCount()
        {
            if (m_pMapDocument != null)
            {
                return m_pMapDocument.MapCount;
            }
            return 0;
        }
        public static bool IsSysField(string sFieldName)
        {
            if (sFieldName.Equals("FID", StringComparison.OrdinalIgnoreCase) || sFieldName.Equals("OBJECTID", StringComparison.OrdinalIgnoreCase)
                || sFieldName.Equals("SHAPE", StringComparison.OrdinalIgnoreCase) || sFieldName.Equals("SHAPE_Length", StringComparison.OrdinalIgnoreCase)
                || sFieldName.Equals("SHAPE_Area", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        public int GetLayerCount()
        {
            if (m_axMapControl == null) return 0;
            int nAllLayerCount = 0;
            UID uid = new UIDClass();
            //IRasterlayers
            //uid.Value = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}";
            //IFeatureLayers
            //uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";
            //Ilayer
            uid.Value = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}";
            IEnumLayer players = m_axMapControl.Map.get_Layers(uid, true);
            ILayer plyr = players.Next();
            while (plyr != null)
            {
                nAllLayerCount++;
                plyr = players.Next();
            }
            //for (int iMap = 0; iMap < GetMapCount(); iMap++)
            //{
            //    IMap pMap = m_pMapDocument.get_Map(iMap);
            //    if(pMap!=null)
            //    {
            //        nAllLayerCount += pMap.LayerCount;               
            //    }            
            //}
            return nAllLayerCount;
        }

        public static List<IFeature> IArray2IList(IArray aFeauters)
        {
            List<IFeature> pList = new List<IFeature>();
            for (int i = 0; i < aFeauters.Count;i++ )
            {
                IFeature pFeature = aFeauters.get_Element(i) as IFeature;
                pList.Add(pFeature);
            }
            return pList;
        }

        public static void ListSort(ref List<IFeature> pList, int idxSortField, bool bAscending = true)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                IFeature pFeature_i = pList[i];
                double dValue_i = Convert.ToDouble(pFeature_i.get_Value(idxSortField));
                for (int j = i + 1; j < pList.Count; j++)
                {
                    IFeature pFeature_j = pList[j];
                    double dValue_j = Convert.ToDouble(pFeature_j.get_Value(idxSortField));
                    if (bAscending)
                    {
                        if (dValue_i < dValue_j)
                        {
                            IFeature pFeature_tmp = pList[i];
                            pList[i] = pList[j];
                            pList[j] = pFeature_tmp;
                            dValue_i = Convert.ToDouble(pList[i].get_Value(idxSortField));//Being careful!!!!!
                        }
                    }
                    else
                    {
                        if (dValue_i > dValue_j)
                        {
                            IFeature pFeature_tmp = pList[i];
                            pList[i] = pList[j];
                            pList[j] = pFeature_tmp;
                            dValue_i = Convert.ToDouble(pList[i].get_Value(idxSortField));//Being careful!!!!!
                        }
                    }
                }
            }            
        }

        public static IEnvelope LayerEnvelope(ILayer pLayer)
        {
            if (pLayer is IRasterLayer)
            {
                IGeoDataset pGeoDataset = ((IRasterLayer)pLayer).Raster as IGeoDataset;
                if (pGeoDataset != null)
                {
                    return pGeoDataset.Extent;
                }
            }
            else if (pLayer is IFeatureLayer)
            {
                IGeoDataset pGeoDataset = ((IFeatureLayer)pLayer).FeatureClass as IGeoDataset;
                if (pGeoDataset != null)
                {
                    return pGeoDataset.Extent;
                }
            }
            return null;
        }
        //Need Test!
        public static bool MergeLayers(AxMapControl axMapControl)
        {
             //合并图层的集合
            try
            {
                IArray pArray = new ArrayClass();
                UID uid = new UIDClass();
                uid.Value = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}";
                IEnumLayer pLayers = axMapControl.Map.get_Layers(uid, true);
                ILayer pLayer = pLayers.Next();
                while (pLayer != null)
                {
                    pLayer = pLayers.Next();
                    pArray.Add(pLayer);
                }
                if (pArray.Count < 2) return false;
                //定义输出图层的fields表
                pLayer = axMapControl.get_Layer(0);
                ITable pTable = (ITable)pLayer;
                IFeatureLayer pFeatureLayer = (IFeatureLayer)pLayer;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                //判断图层是否大于2个
                
                //输出文件类型
                IFeatureClassName pFeatureClassName = new FeatureClassNameClass();
                pFeatureClassName.FeatureType = esriFeatureType.esriFTSimple;
                pFeatureClassName.ShapeFieldName = "Shape";
                pFeatureClassName.ShapeType = pFeatureClass.ShapeType;
                //输出shapefile的名称和位置
                IWorkspaceName pNewWSName = new WorkspaceNameClass();
                pNewWSName.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
                pNewWSName.PathName = Global.GetWorkspacePath();
                IDatasetName pDatasetName = (IDatasetName)pFeatureClassName;
                pDatasetName.Name = "Union";
                pDatasetName.WorkspaceName = pNewWSName;

                //合并图层
                IBasicGeoprocessor pBasicGeop = new BasicGeoprocessorClass();
                IFeatureClass pOutputFeatClass = pBasicGeop.Merge(pArray, pTable, pFeatureClassName);
                //Add the output layer to the map
                IFeatureLayer pOutputFeatLayer = new FeatureLayerClass();
                pOutputFeatLayer.FeatureClass = pOutputFeatClass;
                pOutputFeatLayer.Name = pOutputFeatClass.AliasName;
                axMapControl.AddLayer(pOutputFeatLayer as ILayer, 0);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in merging layers with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return true;
        }
        public bool IsLayerExisted(string sLayerName)
        {
            if (m_axMapControl == null) return false;
            UID uid = new UIDClass();          
            uid.Value = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}";
            IEnumLayer pLayers = m_axMapControl.Map.get_Layers(uid, true);
            ILayer pLayer = pLayers.Next();
            while (pLayer != null)
            {
                if (pLayer != null && pLayer.Name == sLayerName)
                {
                    return true;
                }
                pLayer = pLayers.Next();
            }
            //for (int iMap = 0; iMap < GetMapCount(); iMap++)
            //{
            //    IMap pMap = m_pMapDocument.get_Map(iMap);
            //    for (int i = 0; i < pMap.LayerCount; i++)
            //    {
            //        ILayer pLayer = pMap.get_Layer(i);
            //        if (pLayer != null && pLayer.Name == sLayerName)
            //        {
            //            return true;
            //        }
            //    }
            //}
            return false;
        }
        public ILayer GetLayer(string sLayerName)
        {
            if (m_axMapControl == null) return null;
            UID uid = new UIDClass();
            uid.Value = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}";
            IEnumLayer pLayers = m_axMapControl.Map.get_Layers(uid, true);
            ILayer pLayer = pLayers.Next();
            while (pLayer != null)
            {
                if (pLayer != null && pLayer.Name == sLayerName)
                {
                    return pLayer;
                }
                pLayer = pLayers.Next();
            }        
            return null;
        }
        public static IFeatureLayer GetFeatureLayer(AxMapControl mapControl, IFeature pFeature)
        {
            if (pFeature == null || pFeature.Class == null) return null;
            IFeatureClass pFeatureClass = pFeature.Class as IFeatureClass;
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";
            IEnumLayer players = mapControl.Map.get_Layers(uid, true);
            ILayer plyr = players.Next();
            while (plyr != null)
            {
                IFeatureLayer pLayer = plyr as IFeatureLayer;
                IFeatureClass pTmpFeatureClass = pLayer.FeatureClass;
                if (pTmpFeatureClass == pFeatureClass)
                {
                    return pLayer;
                }
                plyr = players.Next();
            }
            return null;
        }

        public static List<double> Object2DoubleList(List<object> pValues)
        {
            List<double> dTmps = new List<double>();
            foreach (object obj in pValues)
            {
                dTmps.Add(Convert.ToDouble(obj));
            }
            return dTmps;
        }

        public static List<double> Integer2DoubleList(List<int> pValues)
        {
            List<double> dTmps = new List<double>();
            foreach (int obj in pValues)
            {
                dTmps.Add(Convert.ToDouble(obj));
            }
            return dTmps;
        }
        public static esriGeometryType GeometryType(IFeatureLayer pFeatureLayer)
        {
            if (pFeatureLayer == null) return esriGeometryType.esriGeometryAny;
            return pFeatureLayer.FeatureClass.ShapeType;
        }

        public static esriGeometryType GeometryType(IFeature pFeature)
        {
            if (pFeature == null) return esriGeometryType.esriGeometryAny;
             return pFeature.Shape.GeometryType;
        }

        public static bool GetFeatureValues(AxMapControl mapControl, IFeature pFeature,List<string> pFieldNames,out List<object> pValues)
        {
            pValues = new List<object>();
            if (pFeature == null || mapControl == null) return false;
            IFeatureLayer pLayer = GetFeatureLayer(mapControl, pFeature);
            if (pLayer == null || pLayer.FeatureClass == null) return false;
            for (int i = 0; i < pFieldNames.Count; i++)
            {
                int index = pLayer.FeatureClass.Fields.FindField(pFieldNames[i]);
                if ( index != -1)
                {
                    object obj = pFeature.get_Value(index);
                    pValues.Add(obj);
                }
            } 
            return true;
        }


        public void RemoveLayer(string sLayerName)        
        {
            if (m_axMapControl == null) return;
            for (int iMap = 0; iMap < GetMapCount(); iMap++)
            {
                IMap pMap = m_pMapDocument.get_Map(iMap);
                for (int i = 0; i < pMap.LayerCount; i++)
                {
                    ILayer pLayer = pMap.get_Layer(i);
                    if (pLayer != null && pLayer.Name == sLayerName)
                    {
                        pMap.DeleteLayer(pLayer);
                        m_axMapControl.Refresh();
                        return;
                    }
                }
            }
        }

        public static void RemoveLayer(AxMapControl axMapControl,string sLayerName)
        {
            if (axMapControl == null) return;          
            UID uid = new UIDClass();
            uid.Value = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}";
            IEnumLayer pLayers = axMapControl.Map.get_Layers(uid, true);
            ILayer pLayer = pLayers.Next();
            while (pLayer != null)
            {
                if (pLayer != null && pLayer.Name == sLayerName)
                {
                    axMapControl.Map.DeleteLayer(pLayer);
                    axMapControl.Refresh();
                    return;
                }
                pLayer = pLayers.Next();
            }          
        }

        public static bool IsLayerExisted(AxMapControl axMapControl, string sLayerName)
        {
            if (axMapControl == null) return false;
            UID uid = new UIDClass();
            uid.Value = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}";
            IEnumLayer pLayers = axMapControl.Map.get_Layers(uid, true);
            ILayer pLayer = pLayers.Next();
            while (pLayer != null)
            {
                if (pLayer != null && pLayer.Name == sLayerName)
                {
                    return true;
                }
                pLayer = pLayers.Next();
            }           
            return false;
        }

        public bool IsFeatureExisted(IFeatureLayer pLayer,string sWhereClause)
        {
            IArray aRes = Search(pLayer, sWhereClause);
            return (aRes.Count > 0) ? true : false;
        }
        public bool IsFieldExisted(IFeatureLayer pLayer, string sField)
        {
            if (pLayer == null) return false;
            if (pLayer.FeatureClass.FindField(sField) < 0) return false;
            return true;
        }
       
        //Get feature by ID
        public bool GetFeature(IFeatureLayer pLayer, int nFeatureID,out IFeature pFeature)
        {
            pFeature = null;
            if (pLayer == null) return false;
            try
            {
                IFeatureClass pFeatureclass = pLayer.FeatureClass;
                IFeatureCursor pfeatureCursor = pFeatureclass.Search(null, false);
                IFeature newpfeature = pFeatureclass.GetFeature(nFeatureID);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pfeatureCursor);
            }
            catch (Exception exception)
            {
               MessageBox.Show("Failed in getting feature with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return false;
        }
        //Get nearest feature
        public bool GetNearestFeature(IPoint pLocation, IFeatureLayer pLayer, out int nFeatureID, out double dDistance)
        {
            nFeatureID = -1;
            dDistance = -0.0;
            if (pLayer == null) return false;
            try
            {
                IFeatureClass pFeatureclass = pLayer.FeatureClass;
                IFeatureCursor pfeatureCursor = pFeatureclass.Search(null, true);
                IGeoDataset geodataset = pFeatureclass as IGeoDataset;
                ITrackCancel trackCancel = new TrackCancelClass();
                IFeatureIndex2 pFIndex2 = new FeatureIndexClass();
                pFIndex2.FeatureClass = pFeatureclass;
                pFIndex2.FeatureCursor = pfeatureCursor;
                pFIndex2.set_OutputSpatialReference(pFeatureclass.ShapeFieldName, geodataset.SpatialReference);
                pFIndex2.Index(trackCancel, geodataset.Extent);
                IIndexQuery2 pIndexQuery = pFIndex2 as IIndexQuery2;
                pIndexQuery.NearestFeature(pLocation, out nFeatureID, out dDistance);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pfeatureCursor);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searcin nearest feature with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return false;
        }
        //Get nearest feature
        public bool GetNearestFeature(IPoint pLocation, IFeatureLayer pLayer, out IFeature pFeature)
        {
            pFeature = null;    
            if (pLayer == null) return false;
            try
            {
                IFeatureClass pFeatureclass = pLayer.FeatureClass;
                IFeatureCursor pfeatureCursor = pFeatureclass.Search(null, false);
                IGeoDataset geodataset = pFeatureclass as IGeoDataset;
                ITrackCancel trackCancel = new TrackCancelClass();
                IFeatureIndex2 pFIndex2 = new FeatureIndexClass();
                pFIndex2.FeatureClass = pFeatureclass;
                pFIndex2.FeatureCursor = pfeatureCursor;
                pFIndex2.set_OutputSpatialReference(pFeatureclass.ShapeFieldName, geodataset.SpatialReference);
                pFIndex2.Index(trackCancel, geodataset.Extent);
                IIndexQuery2 pIndexQuery = pFIndex2 as IIndexQuery2;
                int nFeatureID = -1;
                double dDistance = -0.0; 
                pIndexQuery.NearestFeature(pLocation, out nFeatureID, out dDistance);
                pFeature = pFeatureclass.GetFeature(nFeatureID);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pfeatureCursor);
                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searcin nearest feature with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return false;
        }
        //Not suit to point!!
        public IPointCollection GetCoords(IFeature pFeature, esriGeometryType pType)
        {
            IPointCollection points = null;
            switch(pType)
            {
                case esriGeometryType.esriGeometryPoint:
                    IPoint point = pFeature.Shape as IPoint;                   
                    break;
                case esriGeometryType.esriGeometryLine:
                    points =  pFeature.Shape as IPointCollection;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    points =  pFeature.Shape as IPointCollection;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    points = pFeature.Shape as IPointCollection;
                    break;
                default:
                    break;
            }
            return points;
        }

        /*public IPoint GetOnePoint(IFeature pFeature, esriGeometryType pType,bool bEliminateEnvelopePoint = false, IGeometry pGeometry=null)
        {
            IPointCollection points = null;
            IPoint point = null;
            if (bEliminateEnvelopePoint && pGeometry == null)
            {
                return null;
            }
            switch (pType)
            {
                case esriGeometryType.esriGeometryPoint:
                    point = pFeature.Shape as IPoint;
                    break;
                case esriGeometryType.esriGeometryLine:
                    points = pFeature.Shape as IPointCollection;
                    if (!bEliminateEnvelopePoint && points.PointCount > 0)//First point if NOT need to eliminate the points on the boundary
                    {
                        point = points.get_Point(0);
                    }
                    else//Find first point which is NOT on the boundary
                    {
                        for (int i = 0; i < points.PointCount; i++)
                        {
                            IPoint pointTmp = points.get_Point(i);
                            if (!SpatialOperator.OnBoundaryExt(pointTmp, pGeometry))
                            {
                                point = pointTmp;
                                break;
                            }
                        }
                    }
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    points = pFeature.Shape as IPointCollection;
                    if (!bEliminateEnvelopePoint && points.PointCount > 0)//First point if NOT need to eliminate the points on the boundary
                    {
                        point = points.get_Point(0);
                    }
                    else//Find first point which is NOT on the boundary
                    {
                        for (int i = 0; i < points.PointCount; i++)
                        {
                            IPoint pointTmp = points.get_Point(i);
                            if (!SpatialOperator.OnBoundaryExt(pointTmp, pGeometry))
                            {
                                point = pointTmp;
                                break;
                            }
                        }
                    }
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    points = pFeature.Shape as IPointCollection;
                    if (!bEliminateEnvelopePoint && points.PointCount > 0)//First point if NOT need to eliminate the points on the boundary
                    {
                        point = points.get_Point(0);
                    }
                    else//Find first point which is NOT on the boundary
                    {
                        for (int i = 0; i < points.PointCount; i++)
                        {
                            IPoint pointTmp = points.get_Point(i);
                            if (!SpatialOperator.OnBoundaryExt(pointTmp, pGeometry))
                            {
                                point = pointTmp;
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return point;
        }*/
        //Get the physical location of layer
        public string GetLayerPhysicalLocation(string lyrname)
        {
            if(m_axMapControl==null) return null;
            ILayer pLayer = GetLayer(lyrname);
            if (pLayer == null) return null;
            IDataLayer datalayer = (IDataLayer)pLayer;
            IDatasetName datasetName = (IDatasetName)datalayer.DataSourceName;
            string sDataSourceName = datasetName.WorkspaceName.PathName.ToString();
            return sDataSourceName;
        }
        //Get the physical location of layer, path without "\\" at the end
        public string GetLayerPhysicalLocation(ILayer pLayer)
        {
            if (pLayer ==null) return null;           
            IDataLayer datalayer = (IDataLayer)pLayer;
            IDatasetName datasetName = (IDatasetName)datalayer.DataSourceName;
            string sDataSourceName = datasetName.WorkspaceName.PathName.ToString();
            return sDataSourceName;
        }
        public bool DeleteWorkSpaceIfExist(IWorkspace pWS,string sDatasetName)
        {
            bool bSucceed = false;
            if (pWS != null)
            {
                if (pWS.Exists())
                {
                    IEnumDataset pEnumDataset = pWS.get_Datasets(esriDatasetType.esriDTAny);
                    IDataset pDS = pEnumDataset.Next();
                    while (pDS != null)
                    {
                        if (pDS.Name.ToLower() == sDatasetName.ToLower())
                        {
                            if (pDS.CanDelete())
                            {
                                pDS.Delete();
                                bSucceed = true;
                                break;
                            }
                        }
                        pDS = pEnumDataset.Next();
                    }
                }
            }
            string[] strFiles1 = Directory.GetFiles(pWS.PathName, sDatasetName + ".*");
            foreach (string strFile1 in strFiles1)
            {
                System.IO.File.Delete(strFile1);
                bSucceed = true;
            }
            return bSucceed;           
        }
        public static IArray GetSelectedFeatures(AxMapControl mapControl)
        {
            IArray pFeatureArray = new ArrayClass();
            ISelection pFeatureSelction = mapControl.Map.FeatureSelection;
            IEnumFeature pEnumFeature = pFeatureSelction as IEnumFeature;
            IEnumFeatureSetup pEnumFeatureSetup = pEnumFeature as IEnumFeatureSetup;
            pEnumFeatureSetup.AllFields = true;
            IFeature pFeature = pEnumFeature.Next();
            while (pFeature != null)
            {
                if (!pFeature.Shape.IsEmpty)
                {
                    pFeatureArray.Add(pFeature);
                }
                pFeature = pEnumFeature.Next();
            }
            return pFeatureArray;
        }
        public IFeatureSelection GetSelectFeatures(IFeatureLayer pFeatureLayer, IGeometry pGeometry)
        {
            if (m_axMapControl == null || GetLayerCount() == 0) return null;
            m_axMapControl.Map.FeatureSelection.Clear();
            IFeatureSelection pFeatureSelection = pFeatureLayer as IFeatureSelection;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.SearchOrder = esriSearchOrder.esriSearchOrderSpatial;     
            pSpatialFilter.Geometry = pGeometry;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            pFeatureSelection.SelectFeatures(pSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
            if (pFeatureSelection.SelectionSet.Count == 0) return null;
            return pFeatureSelection;
        }
        public static IArray SelectFeatureByGeometry(IFeatureLayer pLayer, IGeometry pGeometry)
        {
            if (pLayer == null) return null; 
            IArray pFeatureArray = new ArrayClass();
            try
            {
                IFeatureClass pFeatureClass = pLayer.FeatureClass;
                ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                pSpatialFilter.Geometry = pGeometry;
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    if (pFeature.Shape == null)
                    {
                        pFeature = pFeatureCursor.NextFeature();
                        continue;
                    }
                    if (!pFeature.Shape.IsEmpty)
                    {
                        pFeatureArray.Add(pFeature);
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                //If using Search many times, the following statement must be called! czl 2012,TXSTATE
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                return pFeatureArray;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searching with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return null; 
        }

        public static baseFeatureList SelectFeatureByGeometryExt(IFeatureLayer pLayer, IGeometry pGeometry)
        {            
            baseFeatureList pFeatureList = new baseFeatureList();
            if (pLayer == null) return pFeatureList;
            try
            {
                IFeatureClass pFeatureClass = pLayer.FeatureClass;
                ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                pSpatialFilter.Geometry = pGeometry;
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    if (pFeature.Shape == null)
                    {
                        pFeature = pFeatureCursor.NextFeature();
                        continue;
                    }
                    if (!pFeature.Shape.IsEmpty)
                    {
                        pFeatureList.Add(pFeature);
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                //If using Search many times, the following statement must be called! czl 2012,TXSTATE
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                return pFeatureList;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searching with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return null;
        }
        public IFeature FindAnyOneFeature(IFeatureLayer pLayer)
        {
            if (pLayer == null) return null;
            try
            {
                IFeatureClass pFeatureClass = pLayer.FeatureClass;
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    if (!pFeature.Shape.IsEmpty)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                        return pFeature;
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                //If using Search many times, the following statement must be called! czl 2012,TXSTATE
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                return null;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searching with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return null;
        }
        public void DeleteFeatures(IFeatureLayer pLayer, IArray pDeleteFeatureIDs)
        {
            if (pLayer == null) return;
            try
            {
                IFeatureClass pFeatureClass = pLayer.FeatureClass;
                IDataset dataset = pLayer.FeatureClass as IDataset;
                IWorkspaceEdit pWorkspaceEdit = (IWorkspaceEdit)dataset.Workspace;
                //pWorkspaceEdit.StartEditing(true);
                //pWorkspaceEdit.StartEditOperation();

                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = "";
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    for (int i = 0; i < pDeleteFeatureIDs.Count; i++)
                    {
                        int nOID = Convert.ToInt32(pDeleteFeatureIDs.get_Element(i));
                        if (pFeature.OID == nOID)
                        {
                            //pFeatureCursor.DeleteFeature();
                            pFeature.Delete();
                            pDeleteFeatureIDs.Remove(i);
                            break;
                        }     
                    }                                      
                    pFeature = pFeatureCursor.NextFeature();
                }
                //pWorkspaceEdit.StopEditOperation();
                //pWorkspaceEdit.StopEditing(true);

                //If using Search many times, the following statement must be called! czl 2012,TXSTATE
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searching with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public static bool CreateNormalField(IFeatureLayer pLayer,string strFieldName, string strFieldAliasName, esriFieldType ftFieldType, int nFieldLen)
        {
            if (strFieldName.Length == 0)
            {
                return false;
            }        
            if (pLayer != null && pLayer.FeatureClass != null)
            {
                if (pLayer.FeatureClass.Fields.FindField(strFieldName) != -1) return true;             
                try
                {
                    IFeatureClass fc = pLayer.FeatureClass;
                    IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                    //w.StartEditing(true);
                    //w.StartEditOperation(); 

                    IField pField;
                    pField = new FieldClass();
                    IFieldEdit pFieldEdit;
                    pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Type_2 = ftFieldType;
                    pFieldEdit.Name_2 = strFieldName;
                    pFieldEdit.AliasName_2 = strFieldAliasName;
                    if (ftFieldType == esriFieldType.esriFieldTypeString)
                    {
                        pFieldEdit.Length_2 = nFieldLen;
                    }
                    pLayer.FeatureClass.AddField(pField);

                   // w.StopEditOperation();
                    //w.StopEditing(true);
                     
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
            return false;
        }
        public static bool CreateNormalFields(IFeatureLayer pLayer, baseFieldList pNewFields)
        {
            if (pLayer == null || pLayer.FeatureClass == null || pNewFields.Count == 0) return false;            
            try
            {
                IFeatureClass fc = pLayer.FeatureClass;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                //w.StartEditing(true);
                //w.StartEditOperation();
                for (int i = 0; i < pNewFields.Count; i++)
                {
                    baseField pNewField = pNewFields.get_Element(i);
                    if (pNewField.strFieldName.Length == 0 || pLayer.FeatureClass.Fields.FindField(pNewField.strFieldName) != -1) continue; 
                    IField pField;
                    pField = new FieldClass();
                    IFieldEdit pFieldEdit;
                    pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Type_2 = pNewField.ftFieldType;
                    pFieldEdit.Name_2 = pNewField.strFieldName;
                    pFieldEdit.AliasName_2 = pNewField.strFieldAliasName;
                    if (pNewField.ftFieldType == esriFieldType.esriFieldTypeString)
                    {
                        pFieldEdit.Length_2 = pNewField.nFieldLen;
                    }
                    pLayer.FeatureClass.AddField(pField);
                }
                //w.StopEditOperation();
                //w.StopEditing(true);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static double DisPRJtoGCS(double dDisInMeter, esriSRProjCSType pFromPRJ = esriSRProjCSType.esriSRProjCS_World_Mercator, esriSRGeoCSType pToGCS = esriSRGeoCSType.esriSRGeoCS_WGS1984)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(dDisInMeter, 0);
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem((int)pFromPRJ);
            pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)pToGCS));
            return pPoint.X;
        }
        public static IPoint PRJtoGCS(double x, double y,esriSRProjCSType pFromPRJ = esriSRProjCSType.esriSRProjCS_World_Mercator, esriSRGeoCSType pToGCS =esriSRGeoCSType.esriSRGeoCS_WGS1984)
        { 
            IPoint pPoint = new PointClass(); 
            pPoint.PutCoords(x, y); 
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateProjectedCoordinateSystem((int)pFromPRJ); 
            pPoint.Project(pSRF.CreateGeographicCoordinateSystem((int)pToGCS)); 
            return pPoint; 
        }

        public static IPoint GCStoPRJ(double lon, double lat, esriSRGeoCSType pFromGCS = esriSRGeoCSType.esriSRGeoCS_WGS1984, esriSRProjCSType pToPRJ = esriSRProjCSType.esriSRProjCS_World_Mercator)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(lon, lat);
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateGeographicCoordinateSystem((int)pFromGCS);
            pPoint.Project(pSRF.CreateProjectedCoordinateSystem((int)pToPRJ));
            return pPoint;
        }

        public static double DisGCStoPRJ(double dDisInDeg, esriSRGeoCSType pFromGCS = esriSRGeoCSType.esriSRGeoCS_WGS1984, esriSRProjCSType pToPRJ = esriSRProjCSType.esriSRProjCS_World_Mercator)
        {
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(dDisInDeg, 0);
            ISpatialReferenceFactory pSRF = new SpatialReferenceEnvironmentClass();
            pPoint.SpatialReference = pSRF.CreateGeographicCoordinateSystem((int)pFromGCS);
            pPoint.Project(pSRF.CreateProjectedCoordinateSystem((int)pToPRJ));
            return pPoint.X;
        }


        public static void InitLabel(IGeoFeatureLayer pGeoFeaturelayer, string sFieldName)
        {
            /*IAnnotateLayerPropertiesCollection作用于一个要素图层的所有注记设置的集合，控制要素图层的一系列注记对象*/
            IAnnotateLayerPropertiesCollection pAnnoLayerPropsCollection;
            //定义标注类
            pAnnoLayerPropsCollection = pGeoFeaturelayer.AnnotationProperties;
            /*将要素图层注记集合中的所有项都移除*/
            pAnnoLayerPropsCollection.Clear();

            IBasicOverposterLayerProperties pBasicOverposterlayerProps = new BasicOverposterLayerPropertiesClass();
            switch (pGeoFeaturelayer.FeatureClass.ShapeType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    pBasicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    pBasicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPoint;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    pBasicOverposterlayerProps.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
                    break;
            }
            ITextSymbol pTextSymbol = new TextSymbolClass();
            stdole.IFontDisp pFont = (stdole.IFontDisp)new stdole.StdFont();
            IRgbColor pRGB = null;
            pFont.Name = "Arial";
            pFont.Size = 9;
            pFont.Bold = false;
            pFont.Italic = false;
            pFont.Underline = false;
            pTextSymbol.Font = pFont;
            if (pRGB == null)
            {
                pRGB = new RgbColorClass();
                pRGB.Red = 0;
                pRGB.Green = 0;
                pRGB.Blue = 0;
                pTextSymbol.Color = (IColor)pRGB;
            }

            ILabelEngineLayerProperties pLabelEnginelayerProps = new LabelEngineLayerPropertiesClass();
            pLabelEnginelayerProps.Expression = "[" + sFieldName + "]";
            pLabelEnginelayerProps.Symbol = pTextSymbol;
            pLabelEnginelayerProps.BasicOverposterLayerProperties = pBasicOverposterlayerProps;
            /*将一项标注属性(LayerEngineLayerProperties对象)增加到要素图层的注记集合当中*/
            /*IAnnotateLayerProperties接口用于获取和修改要素图层注记类的注记属性，定义要素图层动态注记（文本）的显示*/
            pAnnoLayerPropsCollection.Add((IAnnotateLayerProperties)pLabelEnginelayerProps);

        }
        public IArray Search(IFeatureLayer pLayer, string sWhereClause)
        {
            IArray pFeatureArray = new ArrayClass();
            if (pLayer == null) return pFeatureArray;            
            try
            {
                IFeatureClass pFeatureClass = pLayer.FeatureClass;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = sWhereClause;
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);                
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    if (pFeature.Shape == null)
                    {
                        pFeature = pFeatureCursor.NextFeature();
                        continue;
                    }
                    if (!pFeature.Shape.IsEmpty)
                    {
                        pFeatureArray.Add(pFeature);
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                //If using Search many times, the following statement must be called! czl 2012,TXSTATE
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                return pFeatureArray;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searching with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return pFeatureArray; 
        }

        public static baseFeatureList SearchExt(IFeatureLayer pLayer, string sWhereClause)
        {
            baseFeatureList pFeatureList = new baseFeatureList();
            if (pLayer == null) return pFeatureList;
            try
            {
                IFeatureClass pFeatureClass = pLayer.FeatureClass;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = sWhereClause;
                IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    if (pFeature.Shape == null)
                    {
                        pFeature = pFeatureCursor.NextFeature();
                        continue;
                    }
                    if (!pFeature.Shape.IsEmpty)
                    {
                        pFeatureList.Add(pFeature);
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                //If using Search many times, the following statement must be called! czl 2012,TXSTATE
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
                return pFeatureList;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in searching with error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return pFeatureList;
        }
        //Create a new layer which it has the same fields as pfromLayer
        //ShapeFile : pQueryFilter.WhereClause = "\"CONTOUR\" = 0"; 
        //            pQueryFilter.WhereClause = "\"" + fldName + "\"" + " like '%" + sConditons + "%'";  // ONLY for string type Field
        //GDB: sWhereClause="\["CONTOUR]" = 0"
        //             pQueryFilter.WhereClause =  "[" + fldName + "]" + " like '*" + sConditons + "*'";  
        //      
        public IFeatureLayer CreateShapefile(IFeatureLayer pFromLayer, string sNewLayerName)
        {
            if (m_axMapControl == null && pFromLayer == null) return null;
            string MapPath = Global.GetWorkspacePath();          
            bool bFileExisted = System.IO.File.Exists(MapPath + "\\" + sNewLayerName + ".shp");
            if(bFileExisted && MessageBox.Show("The file is already existed, and do you want to create new one?", "Select one", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".shp");
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".dbf");
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".shx");
                RemoveLayer(sNewLayerName);
            }
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFWS = pWorkspaceFactory.OpenFromFile(MapPath, 0) as IFeatureWorkspace;
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            if (System.IO.File.Exists(MapPath + "\\" + sNewLayerName + ".shp"))
            {
                
                pFeatureLayer.FeatureClass = pFWS.OpenFeatureClass(sNewLayerName);
                pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                if (!IsLayerExisted(sNewLayerName))
                {
                    m_axMapControl.AddLayer(pFeatureLayer);
                    m_axMapControl.Refresh();
                }
                return pFeatureLayer;             
            }         
            try
            {
                IFields fields = new FieldsClass();
                IFieldsEdit fieldsEdit = fields as IFieldsEdit;
                 //Shape field
                IField fd1 = new FieldClass();
                IFieldEdit fiEdit1 = fd1 as IFieldEdit;
                fiEdit1.Name_2 = "Shape";
                fiEdit1.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDef pGeomDef = new GeometryDefClass();
                IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;
                ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();
                pGeomDefEdit.GeometryType_2 = pFromLayer.FeatureClass.ShapeType;
                pGeomDefEdit.SpatialReference_2 =  pSpatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984) as ISpatialReference;
                fiEdit1.GeometryDef_2 = pGeomDef;
                fieldsEdit.AddField(fd1);
                for (int i = 0; i < pFromLayer.FeatureClass.Fields.FieldCount; i++)
                {
                    IField pField = pFromLayer.FeatureClass.Fields.get_Field(i);
                    if (IsSysField(pField.Name)) continue;
                    IField pNewField = new FieldClass(); 
                    IFieldEdit pFieldEdit = pNewField as IFieldEdit;
                    pFieldEdit.Type_2 = pField.Type;
                    pFieldEdit.Name_2 = pField.Name;
                    pFieldEdit.AliasName_2 = pField.AliasName;
                    fieldsEdit.AddField(pFieldEdit);
                }
                pFeatureLayer.FeatureClass = pFWS.CreateFeatureClass(sNewLayerName, fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                pFeatureLayer.Name = sNewLayerName;
                if (!IsLayerExisted(sNewLayerName))
                {
                    m_axMapControl.AddShapeFile(MapPath, sNewLayerName + ".shp");
                    m_axMapControl.Refresh();
                }
            }          
            catch (Exception exception)
            {
                MessageBox.Show("Create" + sNewLayerName + " shapefile error:" + exception.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return pFeatureLayer;
        }

        //Only geometry field
        public IFeatureLayer CreateShapefile(string sNewLayerName, IArray pNewGeometries, esriGeometryType nNewGeometryType)
        {
            if (m_axMapControl == null ||  pNewGeometries.Count == 0) return null;
            string MapPath = Global.GetWorkspacePath();
            bool bFileExisted = System.IO.File.Exists(MapPath + "\\" + sNewLayerName + ".shp");
            if (bFileExisted && MessageBox.Show("The file is already existed, and do you want to create new one?", "Select one", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".shp");
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".dbf");
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".shx");
                RemoveLayer(sNewLayerName);
            }
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFWS = pWorkspaceFactory.OpenFromFile(MapPath, 0) as IFeatureWorkspace;
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            //If file existed, directly load
            if (System.IO.File.Exists(MapPath + "\\" + sNewLayerName + ".shp"))
            {

                pFeatureLayer.FeatureClass = pFWS.OpenFeatureClass(sNewLayerName);
                pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                if (!IsLayerExisted(sNewLayerName))
                {
                    m_axMapControl.AddLayer(pFeatureLayer);
                    m_axMapControl.Refresh();
                }
                return pFeatureLayer;
            }         
            try
            {
                IFields fields = new FieldsClass();
                IFieldsEdit fieldsEdit = fields as IFieldsEdit;

                //Shape field
                IField fd1 = new FieldClass();
                IFieldEdit fiEdit1 = fd1 as IFieldEdit;
                fiEdit1.Name_2 = "Shape";
                fiEdit1.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDef pGeomDef = new GeometryDefClass();
                IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;
                ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();
                pGeomDefEdit.GeometryType_2 = nNewGeometryType;
                pGeomDefEdit.SpatialReference_2 = pSpatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984) as ISpatialReference;
                fiEdit1.GeometryDef_2 = pGeomDef;
                fieldsEdit.AddField(fd1);              

                //Create feature class
                pFeatureLayer.FeatureClass = pFWS.CreateFeatureClass(sNewLayerName, fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                pFeatureLayer.Name = sNewLayerName;
                //start editing
                IFeatureClass fc = pFeatureLayer.FeatureClass;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                //w.StartEditing(true);
                //w.StartEditOperation();
                IFeatureBuffer f = fc.CreateFeatureBuffer();
                IFeatureCursor cur = fc.Insert(true);

                for (int i = 0; i < pNewGeometries.Count; i++)
                {                    
                    f.Shape = pNewGeometries.get_Element(i) as IGeometry;
                    cur.InsertFeature(f);
                    //flush per 1000 loops
                    if (i % 1000 == 0)
                    {
                        cur.Flush();
                    }
                }
                cur.Flush();
                //w.StopEditOperation();
                //w.StopEditing(true);
                m_axMapControl.AddShapeFile(MapPath, sNewLayerName + ".shp");
                m_axMapControl.Refresh();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Create \"" + sNewLayerName + "\" shapefile error:" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return pFeatureLayer;
        }
        public static IPoint Centroid(IPolygon pPolygon)
        {
            if (pPolygon == null || pPolygon.IsEmpty) return null;
            IArea pArea = pPolygon as IArea;
            return pArea.Centroid;
        }
        //
        public IFeatureLayer CreateShapefile(string sNewLayerName, IArray pOldFeatures,IArray pNewGeometries,esriGeometryType nNewGeometryType)
        {
            if (m_axMapControl == null || pOldFeatures.Count == 0 || pNewGeometries.Count==0) return null;
            string MapPath = Global.GetWorkspacePath(); 
            bool bFileExisted = System.IO.File.Exists(MapPath + "\\" + sNewLayerName + ".shp");
            if (bFileExisted && MessageBox.Show("The file is already existed, and do you want to create new one?", "Select one", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".shp");
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".dbf");
                System.IO.File.Delete(MapPath + "\\" + sNewLayerName + ".shx");
                RemoveLayer(sNewLayerName);
            }
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFWS = pWorkspaceFactory.OpenFromFile(MapPath, 0) as IFeatureWorkspace;
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            //If file existed, directly load
            if (System.IO.File.Exists(MapPath + "\\" + sNewLayerName + ".shp"))
            {
                pFeatureLayer.FeatureClass = pFWS.OpenFeatureClass(sNewLayerName);
                pFeatureLayer.Name = pFeatureLayer.FeatureClass.AliasName;
                if (!IsLayerExisted(sNewLayerName))
                {
                    m_axMapControl.AddLayer(pFeatureLayer);
                    m_axMapControl.Refresh();
                }
                return pFeatureLayer;
            }
            //Find fields from old feature
            IFeature pTmpFt = pOldFeatures.get_Element(0) as IFeature;
            IFields pTmpFields = pTmpFt.Fields;          
            try
            {
                IFields fields = new FieldsClass();
                IFieldsEdit fieldsEdit = fields as IFieldsEdit;

                //Shape field
                IField fd1 = new FieldClass();
                IFieldEdit fiEdit1 = fd1 as IFieldEdit;
                fiEdit1.Name_2 = "Shape";
                fiEdit1.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDef pGeomDef = new GeometryDefClass();
                IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;
                ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();
                pGeomDefEdit.GeometryType_2 = nNewGeometryType;
                //pGeomDefEdit.SpatialReference_2 = pSpatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984) as ISpatialReference;
                fiEdit1.GeometryDef_2 = pGeomDef;
                fieldsEdit.AddField(fd1);

                for (int index = 0; index < pTmpFields.FieldCount;index++ )
                {
                    IField pField = pTmpFields.get_Field(index);
                    if (IsSysField(pField.Name)) continue;
                    IField fd = new FieldClass();
                    IFieldEdit fiEdit2 = fd as IFieldEdit;
                    fiEdit2.Name_2 = pField.Name;
                    fiEdit2.Type_2 = pField.Type;
                    fiEdit2.Length_2 = pField.Length;
                    fiEdit2.AliasName_2 = pField.AliasName;                    
                    fieldsEdit.AddField(fd);                 
                }               

                //Create feature class
                pFeatureLayer.FeatureClass = pFWS.CreateFeatureClass(sNewLayerName, fields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                pFeatureLayer.Name = sNewLayerName;
                //start editing
                IFeatureClass fc = pFeatureLayer.FeatureClass;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
                //w.StartEditing(true);
                //w.StartEditOperation();
                IFeatureBuffer f = fc.CreateFeatureBuffer();
                IFeatureCursor cur = fc.Insert(true);

                pTmpFields = fc.Fields;
                for (int i = 0; i < pOldFeatures.Count; i++)
                {
                    IFeature pOldFeature = pOldFeatures.get_Element(0) as IFeature;
                    f.Shape = pNewGeometries.get_Element(i) as IGeometry;
                    for (int index = 0; index < pTmpFields.FieldCount; index++)
                    {
                        IField pField = pTmpFields.get_Field(index);
                        if (IsSysField(pField.Name)) continue;
                        int nOldIdx = pOldFeature.Fields.FindField(pField.Name);
                        if (nOldIdx != -1)
                        {
                            object obj = pOldFeature.get_Value(nOldIdx);
                            f.set_Value(index, obj);
                        }
                    }
                    cur.InsertFeature(f);                
                    //flush per 1000 loops
                    if (i % 1000 == 0)
                    {
                        cur.Flush();
                    }
                }
                cur.Flush();
                //w.StopEditOperation();
                //w.StopEditing(true);
                m_axMapControl.AddShapeFile(MapPath, sNewLayerName + ".shp");
                m_axMapControl.Refresh();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Create" + sNewLayerName + " shapefile error:" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return pFeatureLayer;
        }

      
        //Before using this function, MUST make sure that the featureclass of pFeatures is the SAME as pTargetLayer 
        public IFeatureLayer CopyToLayer(IArray pFeatures,IFeatureLayer pFromLayer, string sToLayerName,IFeatureLayer pToLayer=null)
        {
            if (pFeatures== null || pFeatures.Count == 0) return null;
            IFeatureLayer pResultLayer = pToLayer;
            if (pToLayer == null)
            {
                if(pFromLayer == null) return null;
                else
                {
                    string sLayerName = sToLayerName;
                    pToLayer = CreateShapefile(pFromLayer, sLayerName);
                    if (pToLayer == null) return null;
                    pResultLayer = pToLayer;
                }               
            }
            try
            {
                IFeatureClass pFeatureClass = pToLayer.FeatureClass;
                for (int i = 0; i < pFeatures.Count; i++)
                {
                    IFeature pFeature = pFeatures.get_Element(i) as IFeature;
                    IFeature pNewFeature = pFeatureClass.CreateFeature();
                    IFields pFields = pFeature.Fields;
                    for (int j = 0; j < pFields.FieldCount; j++)
                    {
                        IField pField = pFields.get_Field(j);
                        if (IsSysField(pField.Name)) continue;
                        int nIndex = pNewFeature.Fields.FindField(pField.Name);
                        if (nIndex != -1)
                        {
                            pNewFeature.set_Value(nIndex, pFeature.get_Value(j));
                        }
                    }
                    pNewFeature.Shape = pFeature.Shape;
                    pNewFeature.Store();
                }
            }
            catch (Exception exception)
            {                
                MessageBox.Show( "Failed in copying feature with error:" + exception.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if (m_axMapControl != null) m_axMapControl.Refresh();
            return pResultLayer;
        }


        
        //=======================================================================================================
        //For some basic Functions
        public static void FlashPolygon(IScreenDisplay pDisplay, IGeometry pGeometry, int nTimer, int time)
        {
            ISimpleFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            IRgbColor pRGBColor = new RgbColorClass();
            pRGBColor.Green = 60;
            pRGBColor.Red = 255;
            pRGBColor.Blue = 0;
            pFillSymbol.Outline = null;
            pFillSymbol.Color = pRGBColor;
            ISymbol pSymbol = (ISymbol)pFillSymbol;
            pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            pDisplay.StartDrawing(0, (short)esriScreenCache.esriNoScreenCache);
            pDisplay.SetSymbol(pSymbol);
            for (int i = 0; i < nTimer; i++)
            {
                pDisplay.DrawPolygon(pGeometry);
                System.Threading.Thread.Sleep(time);
            }
            pDisplay.FinishDrawing();
        }

        //闪烁目标
        public static void FlashFeature(AxMapControl mapControl, IFeature iFeature, IMap iMap)
        {
            IActiveView iActiveView = iMap as IActiveView;
            if (iActiveView != null)
            {
                iActiveView.ScreenDisplay.StartDrawing(0, (short)esriScreenCache.esriNoScreenCache);
                //根据几何类型调用不同的过程
                switch (iFeature.Shape.GeometryType)
                {
                    case esriGeometryType.esriGeometryPolyline:
                        FlashLine(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        FlashPolygon(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                        break;
                    case esriGeometryType.esriGeometryPoint:
                        FlashPoint(mapControl, iActiveView.ScreenDisplay, iFeature.Shape);
                        break;
                    default:
                        break;
                }
                iActiveView.ScreenDisplay.FinishDrawing();
            }
        }

        //闪烁线
        public static void FlashLine(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleLineSymbol iLineSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;

            iLineSymbol = new SimpleLineSymbol();
            iLineSymbol.Width = 4;
            iRgbColor = new RgbColor();
            iRgbColor.Red = 255;
            iLineSymbol.Color = iRgbColor;
            iSymbol = (ISymbol)iLineSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            mapControl.FlashShape(iGeometry, 5, 300, iSymbol);
        }

        //闪烁面
        public static void FlashPolygon(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleFillSymbol iFillSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;

            iFillSymbol = new SimpleFillSymbol();
            iFillSymbol.Style = esriSimpleFillStyle.esriSFSSolid;
            iFillSymbol.Outline.Width = 12;

            iRgbColor = new RgbColor();
            iRgbColor.RGB = System.Drawing.Color.FromArgb(100, 180, 180).ToArgb();
            iFillSymbol.Color = iRgbColor;

            iSymbol = (ISymbol)iFillSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            iScreenDisplay.SetSymbol(iSymbol);
            mapControl.FlashShape(iGeometry, 5, 300, iSymbol);
        }

        //闪烁点
        public static void FlashPoint(AxMapControl mapControl, IScreenDisplay iScreenDisplay, IGeometry iGeometry)
        {
            ISimpleMarkerSymbol iMarkerSymbol;
            ISymbol iSymbol;
            IRgbColor iRgbColor;

            iMarkerSymbol = new SimpleMarkerSymbol();
            iMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            iRgbColor = new RgbColor();
            iRgbColor.RGB = System.Drawing.Color.FromArgb(0, 0, 0).ToArgb();
            iMarkerSymbol.Color = iRgbColor;
            iSymbol = (ISymbol)iMarkerSymbol;
            iSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            mapControl.FlashShape(iGeometry, 5, 300, iSymbol);
        }

        //=======================================================================================================
        // functons below for GDB
        public  IArray GetLayersFromGDB(string strDBPath)
        {            
            try
            {
                IArray pLayersArray = new ArrayClass();
                IWorkspaceFactory ipWorkspaceFactory = new AccessWorkspaceFactory();
                IWorkspace ipWorkspace = ipWorkspaceFactory.OpenFromFile(strDBPath, 0);
                IFeatureWorkspace ipFeatureWorkspace = ipWorkspace as IFeatureWorkspace;
                IEnumDataset pEnumDataSet = ipWorkspace.get_Datasets(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureDataset);
                IDataset pDataset = pEnumDataSet.Next();
                IArray pFeatureLayerArray = new ArrayClass();
                while (pDataset != null)
                {
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        IFeatureLayer pFeaturelayer = new FeatureLayerClass();
                        IFeatureClass pFeatureClass = ipFeatureWorkspace.OpenFeatureClass(pDataset.Name);
                        pFeaturelayer.FeatureClass = pFeatureClass;
                        pFeaturelayer.Name = pDataset.Name;
                        pFeatureLayerArray.Add(pFeaturelayer);
                    }
                    else if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset pEnumSubDataset = pDataset.Subsets;
                        pEnumSubDataset.Reset();
                        IDataset pSubDataSet = pEnumSubDataset.Next();
                        while (pSubDataSet != null)
                        {
                            if (pSubDataSet.Type == esriDatasetType.esriDTFeatureClass)
                            {
                                IFeatureLayer pFeaturelayer = new FeatureLayerClass();
                                IFeatureClass pFeatureClass = ipFeatureWorkspace.OpenFeatureClass(pSubDataSet.Name);
                                pFeaturelayer.FeatureClass = pFeatureClass;
                                pFeaturelayer.Name = pSubDataSet.Name;
                                pFeatureLayerArray.Add(pFeaturelayer);
                            }
                            pSubDataSet = pEnumSubDataset.Next();
                        }
                    }
                    pDataset = pEnumDataSet.Next();
                }
                //为各层排序，按照Mutipatch,Polygon,polyLine,Point
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryMultiPatch)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryPolygon)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryPolyline)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryPoint)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                return pLayersArray;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }
        }
        public static void ClearDrawItems(IActiveView pActiveView)
        {
            if (pActiveView != null)
            {
                IGraphicsContainer pGra = pActiveView as IGraphicsContainer;
                IActiveView pAv = pGra as IActiveView;
                // clear all draw item in container
                pGra.DeleteAllElements();                
            }
        }

        public static void DrawElement(AxMapControl mapControl, IGeometry pGeometry, IRgbColor pColor, bool bCleanLastDraw = false)
        {
            if (pGeometry == null || mapControl == null) return;
            IElement pElement = null;
            IGraphicsContainer pGra = mapControl.ActiveView as IGraphicsContainer;
            IActiveView pAv = pGra as IActiveView;
            if (bCleanLastDraw)
            {
                pGra.DeleteAllElements();
            }
            if (pGeometry.Dimension == esriGeometryDimension.esriGeometry0Dimension)
            {
                IMarkerElement pMarkerElement = new MarkerElementClass();
                pElement = pMarkerElement as IElement;
                pElement.Geometry = pGeometry;

                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                pSimpleMarkerSymbol.Color = pColor;
                pSimpleMarkerSymbol.Size = 3;
                (pElement as IMarkerElement).Symbol = pSimpleMarkerSymbol;
                pGra.AddElement((IElement)pElement, 0);
            }
            else if (pGeometry.Dimension == esriGeometryDimension.esriGeometry1Dimension)
            {
                ILineElement pLineElement = new LineElementClass();
                pElement = pLineElement as IElement;
                pElement.Geometry = pGeometry;
                ILineSymbol pOutline = new SimpleLineSymbolClass();
                pOutline.Width = 1;
                pOutline.Color = pColor;
                (pElement as ILineElement).Symbol = pOutline;
                pGra.AddElement((IElement)pElement, 0);
            }
            else if (pGeometry.Dimension == esriGeometryDimension.esriGeometry2Dimension)
            {
                IPolygonElement pPolygonElement = new PolygonElementClass();
                pElement = pPolygonElement as IElement;
                pElement.Geometry = pGeometry;

                IRgbColor pOutlineColor = new RgbColorClass();
                pOutlineColor.Red = 0;
                pOutlineColor.Green = 0;
                pOutlineColor.Blue = 0;
                pOutlineColor.Transparency = 40;
                ILineSymbol pOutline = new SimpleLineSymbolClass();
                pOutline.Width = 1;
                pOutline.Color = pOutlineColor;
                IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                pFillSymbol.Color = pColor;
                pFillSymbol.Outline = pOutline;
                pFillSymbol.Color.Transparency = 60;
                IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
                pFillShapeEle.Symbol = pFillSymbol;
                pGra.AddElement((IElement)pFillShapeEle, 0);
            }
            pAv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public static IRgbColor GetColor(int r, int g, int b)
        {
            RgbColor color = new RgbColor();
            color.Red = r;
            color.Green = g;
            color.Blue = b;
            return color;
        }

        public static ISymbol CreateSimpleMarkSymbol(Color color, int size, esriSimpleMarkerStyle style)
        {  
            ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
            pSimpleMarkerSymbol.Color = GetColor(color.R, color.G, color.B);
            pSimpleMarkerSymbol.Size = size;
            pSimpleMarkerSymbol.Style = style;
            return (ISymbol)pSimpleMarkerSymbol;
        }

        public static ISymbol CreateSimpleLineSymbol(Color color, int width, esriSimpleLineStyle style)
        {
            ISimpleLineSymbol pSimpleLineSymbol;
            pSimpleLineSymbol = new SimpleLineSymbol();
            pSimpleLineSymbol.Width = width;
            pSimpleLineSymbol.Color = GetColor(color.R, color.G, color.B);
            pSimpleLineSymbol.Style = style;
            return (ISymbol)pSimpleLineSymbol;

        }
        public static ISymbol CreateSimpleFillSymbol(Color fillColor, int oLineWidth, esriSimpleFillStyle fillStyle)
        {
            ISimpleFillSymbol pSimpleFillSymbol;
            pSimpleFillSymbol = new SimpleFillSymbol();
            pSimpleFillSymbol.Style = fillStyle;
            pSimpleFillSymbol.Color = GetColor(fillColor.R, fillColor.G, fillColor.B);
            pSimpleFillSymbol.Outline = (ILineSymbol)CreateSimpleLineSymbol(fillColor, 1, esriSimpleLineStyle.esriSLSSolid);
            return (ISymbol)pSimpleFillSymbol;

        }
        public static IElement AddElement(IActiveView pActiveView,IGeometry pGeometry, ISymbol pSymbol, string key="1")
        {
            if (pGeometry == null || pActiveView == null) return null;
            try
            {  
                IGraphicsContainer pGraphicsContainer = pActiveView.GraphicsContainer;
                IElement pElement = null;
                ILineElement pLineElement = null;
                IFillShapeElement pFillShapeElement = null;
                IMarkerElement pMarkerElement = null;
                ICircleElement pCircleElement = null;
                IElementProperties pElmentProperties = null;
                switch (pGeometry.GeometryType)
                {
                    case esriGeometryType.esriGeometryEnvelope:
                        {
                            pElement = new RectangleElement();
                            pElement.Geometry = pGeometry;
                            pFillShapeElement = (IFillShapeElement)pElement;
                            pFillShapeElement.Symbol = (IFillSymbol)pSymbol;
                            break;
                        }
                    case esriGeometryType.esriGeometryLine://ILine error! ???
                    //{
                    //    pElement = new LineElement();
                    //    pElement.Geometry = pGeometry;
                    //    pLineElement = (ILineElement)pElement;
                    //    pLineElement.Symbol = (ILineSymbol)pSymbol;
                    //    break;
                    //}         
                    case esriGeometryType.esriGeometryPolyline:
                        {
                            if (pGeometry.GeometryType == esriGeometryType.esriGeometryLine)
                                pGeometry = LineToPolyline(pGeometry as ILine);
                            pElement = new LineElement();
                            pElement.Geometry = pGeometry;
                            pLineElement = (ILineElement)pElement;
                            pLineElement.Symbol = (ILineSymbol)pSymbol;
                            break;
                        }
                                  
                    case esriGeometryType.esriGeometryPolygon:
                        {
                            pElement = new PolygonElement();
                            pElement.Geometry = pGeometry;
                            pFillShapeElement = (IFillShapeElement)pElement;
                            pFillShapeElement.Symbol = (IFillSymbol)pSymbol;
                            break;
                        }

                    case esriGeometryType.esriGeometryMultipoint:
                    case esriGeometryType.esriGeometryPoint:
                        {
                            pElement = new MarkerElement();
                            pElement.Geometry = pGeometry;
                            pMarkerElement = (IMarkerElement)pElement;
                            pMarkerElement.Symbol = (IMarkerSymbol)pSymbol;
                            break;
                        }

                    case esriGeometryType.esriGeometryCircularArc:
                        {
                            pElement = new CircleElementClass();
                            pElement.Geometry = pGeometry;
                            pCircleElement = (ICircleElement)pElement;
                            break;
                        }

                    default:
                        pElement = null;
                        break;
                }

                if (pElement != null)
                {
                    pElmentProperties = pElement as IElementProperties;
                    pElmentProperties.Name = key;
                    pGraphicsContainer.AddElement(pElement, 0);
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics,null,null);
                    return pElement;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                return null;
            }
        }

        public static IPolygon ConstructPolygonFromPolyline(IPolyline pPolyline)
        {
            IGeometryCollection pPolygonGeoCol = new PolygonClass();
            if ((pPolyline != null) && (!pPolyline.IsEmpty))
            {
                IGeometryCollection pPolylineGeoCol = pPolyline as IGeometryCollection;
                ISegmentCollection pSegCol = new RingClass();
                ISegment pSegment = null;
                object missing = Type.Missing;

                for (int i = 0; i < pPolylineGeoCol.GeometryCount; i++)
                {
                    ISegmentCollection pPolylineSegCol = pPolylineGeoCol.get_Geometry(i) as ISegmentCollection;
                    for (int j = 0; j < pPolylineSegCol.SegmentCount; j++)
                    {
                        pSegment = pPolylineSegCol.get_Segment(j);
                        pSegCol.AddSegment(pSegment, ref missing, ref missing);
                    }
                    pPolygonGeoCol.AddGeometry(pSegCol as IGeometry, ref missing, ref missing);
                }
            }
            return pPolygonGeoCol as IPolygon;
        }
        public static IPolyline ConstructPolylineFromPoints(IPointCollection pPointCollection)
        {
            if (pPointCollection == null) return null;
            IPolyline pPolyline = new PolylineClass();
            pPolyline = pPointCollection as IPolyline;
            return pPolyline;             
        }

        public static IPolyline ConstructPolylineFromPoints(IPoint pFrom, IPoint pTo)
        {
            IPointCollection pLine = new PolylineClass();
            object missing = Type.Missing;
            pLine.AddPoint(pFrom, ref missing, ref missing);
            pLine.AddPoint(pTo, ref missing, ref missing);
            return (pLine as IPolyline);
        }

        public static IPolyline ConstructPolylineFromEnvelope(IEnvelope pEnvelope)
        {
            if (pEnvelope == null) return null;
            object misssing = Type.Missing;
            IPolyline pPolyline = new PolylineClass();
            IPointCollection pPointCollection = new PolylineClass(); 
            pPointCollection.AddPoint(pEnvelope.LowerLeft, ref misssing, ref misssing);
            pPointCollection.AddPoint(pEnvelope.LowerRight, ref misssing, ref misssing);
            pPointCollection.AddPoint(pEnvelope.UpperRight, ref misssing, ref misssing);
            pPointCollection.AddPoint(pEnvelope.UpperLeft, ref misssing, ref misssing);
            pPointCollection.AddPoint(pEnvelope.LowerLeft, ref misssing, ref misssing);
            pPolyline = pPointCollection as IPolyline;
            return pPolyline;
        }
        public static IPolygon ConstructPolygonFromEnvelope(IEnvelope pEnvelope)
        {
            if (pEnvelope == null) return null;           
            ISegmentCollection pSegCol = new PolygonClass();
            pSegCol.SetRectangle(pEnvelope);
            return pSegCol as IPolygon;
        }
        public static ILine PolylineToLine(IPolyline pPloyline)
        {
            if (pPloyline == null) return null;
            ISegmentCollection pSegCol = pPloyline as ISegmentCollection;
            if (pSegCol.SegmentCount > 0) return pSegCol.get_Segment(0) as ILine;
            return null;
        }

        public static IPolyline LineToPolyline(ILine pLine)
        {
            object misssing = Type.Missing;
            IPolyline pPolyline = new PolylineClass();     
            IPointCollection pPointCollection = new PolylineClass();
            pPointCollection.AddPoint(pLine.FromPoint, ref misssing, ref misssing);
            pPointCollection.AddPoint(pLine.ToPoint, ref misssing, ref misssing);          
            pPolyline = pPointCollection as IPolyline;
            return pPolyline;
        }

      /*  public static IPolygon ConstructPolygonFromCircle(ILine pLine)
        {
            if (pLine == null) return null;
            IPoint pCenter = SpatialOperator.MiddlePoint(pLine);
            return ConstructPolygonFromCircle(pCenter, pLine.Length / 2);
        }
       * */
        public static IPolygon ConstructPolygonFromCircle(IPoint ptCenter, double dRadius)
        {
            ISegmentCollection pSegCol = new PolygonClass();
            pSegCol.SetCircle(ptCenter, dRadius);
            return pSegCol as IPolygon;
        }
        public static IPointCollection ConstructPointCollectionFromPoints(IArray pPoints)
        {
            if (pPoints == null) return null;
            object misssing = Type.Missing;
            IPointCollection pPointCollection = new PolylineClass();
            for (int i = 0; i < pPoints.Count; i++)
            {
                IPoint point = pPoints.get_Element(i) as IPoint;
                pPointCollection.AddPoint(point,ref misssing, ref misssing);
            }
            return pPointCollection;           
        }

        public static double Area(IPolygon pPolygon)
        {
            if (pPolygon == null || pPolygon.IsEmpty) return double.NaN;
            ESRI.ArcGIS.Geometry.IArea areaOper = pPolygon as ESRI.ArcGIS.Geometry.IArea;
            return areaOper.Area;
        }
        public static double Area(IEnvelope pEnvelope)
        {
            if (pEnvelope == null || pEnvelope.IsEmpty) return double.NaN;
            ESRI.ArcGIS.Geometry.IArea areaOper = pEnvelope as ESRI.ArcGIS.Geometry.IArea;
            return areaOper.Area;
        }


        public static IEnumColors CustomColorRamp(baseColorRamp nSeismClrRamp, int nColorNum, esriColorRampAlgorithm rampAlgorithm = esriColorRampAlgorithm.esriCIELabAlgorithm)
        {   
            IAlgorithmicColorRamp pColorRamp = new AlgorithmicColorRampClass();
            IRgbColor pFromColor = new RgbColorClass();
            IRgbColor pToColor = new RgbColorClass();
            switch (nSeismClrRamp)
            {
                case baseColorRamp.Green2Red:                            
                            pFromColor.Red = 16;
                            pFromColor.Green = 245;
                            pFromColor.Blue = 0;                          
                            pToColor.Red = 245;
                            pToColor.Green = 20;
                            pToColor.Blue = 0;
                            break;
                case baseColorRamp.Red2Green:
                            pFromColor.Red = 245;
                            pFromColor.Green = 20;
                            pFromColor.Blue = 0;
                            pToColor.Red = 16;
                            pToColor.Green = 245;
                            pToColor.Blue = 0;
                            break;
                case baseColorRamp.Gray2Black:
                            pFromColor.Red = 255;
                            pFromColor.Green = 255;
                            pFromColor.Blue = 255;
                            pToColor.Red = 0;
                            pToColor.Green = 0;
                            pToColor.Blue = 0;
                            break;
                case baseColorRamp.Black2Gray:
                            pFromColor.Red = 0;
                            pFromColor.Green = 0;
                            pFromColor.Blue = 0;
                            pToColor.Red = 255;
                            pToColor.Green = 255;
                            pToColor.Blue = 255;
                            break;
                case baseColorRamp.LightBlue2DeepBlue:
                            pFromColor.Red = 202;
                            pFromColor.Green = 201;
                            pFromColor.Blue = 255;
                            pToColor.Red = 0;
                            pToColor.Green = 0;
                            pToColor.Blue = 225;
                            break;
                case baseColorRamp.DeepBlue2LightBlue:
                            pFromColor.Red = 0;
                            pFromColor.Green = 0;
                            pFromColor.Blue = 225;
                            pToColor.Red = 202;
                            pToColor.Green = 201;
                            pToColor.Blue = 255;
                            break;
                case baseColorRamp.LightGreen2DeepGreen:
                            pFromColor.Red = 213;
                            pFromColor.Green = 237;
                            pFromColor.Blue = 173;
                            pToColor.Red = 96;
                            pToColor.Green = 107;
                            pToColor.Blue = 45;
                            break;
                case baseColorRamp.DeepGreen2LightGreen:
                            pFromColor.Red = 96;
                            pFromColor.Green = 107;
                            pFromColor.Blue = 45;
                            pToColor.Red = 213;
                            pToColor.Green = 237;
                            pToColor.Blue = 45;
                            break;
            }
            pColorRamp.FromColor = (IColor)pFromColor;
            pColorRamp.ToColor = (IColor)pToColor;
            pColorRamp.Size = nColorNum;
            pColorRamp.Algorithm = rampAlgorithm;
            bool ok = true;
            pColorRamp.CreateRamp(out ok);
            return pColorRamp.Colors;    
        }

        public static double MathStatMedian(List<double> dataContainer)
        {
            //get median
            dataContainer.Sort();
            double median = 0.0;
            if ((dataContainer.Count % 2) == 0)
            {
                median = (dataContainer[dataContainer.Count / 2] + dataContainer[dataContainer.Count / 2 - 1]) / 2;
            }
            else
            {
                median = dataContainer[(dataContainer.Count - 1) / 2];
            }
            return median;           
        }
        public static double MathStatMean(List<double> dataContainer)
        {
            double sum = 0;
            for (int i = 0; i < dataContainer.Count; i++)
            {
                sum += dataContainer[i];
            }
            //get mean
            double mean = sum / dataContainer.Count;
            return mean;
        }
        public static double MathStatVariance(List<double> dataContainer)
        {
            double sum = 0;
            for (int i = 0; i < dataContainer.Count; i++)
            {
                sum += dataContainer[i];
            }
            //get mean
            double mean = sum / dataContainer.Count;
            double variance = 0.0;
            for (int i = 0; i < dataContainer.Count; i++)
            {
                variance += Math.Pow(dataContainer[i] - mean, 2);
            }
            variance = variance / (dataContainer.Count - 1);
            return variance;
        }
        public static double MathStatStandradDeviation(List<double> dataContainer)
        {           
            dataContainer.Sort();     
            double sum = 0;
            for (int i = 0; i < dataContainer.Count; i++)
            {
                sum += dataContainer[i];
            }
            //get mean
            double mean = sum / dataContainer.Count;
            double variance =0.0;
            for (int i = 0; i < dataContainer.Count; i++)
            {
                variance += Math.Pow(dataContainer[i] - mean, 2);
            }
            variance = variance / (dataContainer.Count - 1);
            double standdev = Math.Pow(variance, 0.5);
            return standdev;
        }
        public static void MathStat(List<double> dataContainer, ref double mean, ref double median, ref double variance, ref double standdev)
        {
            //get median
            dataContainer.Sort();
            if ((dataContainer.Count%2)==0)
            {
                median = (dataContainer[dataContainer.Count / 2] + dataContainer[dataContainer.Count / 2 - 1]) / 2;
            }
            else
            {
                median = dataContainer[(dataContainer.Count - 1) / 2];
            }
            //get standard deviation
            double sum = 0;
            for (int i = 0; i < dataContainer.Count; i++)
            {
                sum += dataContainer[i];
            }
            //get mean
            mean = sum / dataContainer.Count;        
            for (int i = 0; i < dataContainer.Count; i++)
            {
                variance += Math.Pow(dataContainer[i] - mean, 2);
            }
            variance = variance / (dataContainer.Count - 1);
            standdev = Math.Pow(variance, 0.5);
        }
    //end of geobaselib class

      
        //
        //获取Geodatabase（mdb）的所有图层，按照mutipatch，polygon，polyline，point；
        //
        public static IArray GetLayersFromGDB(string strDBPath, ref IArray pDatasets)
        {
            try
            {
                IArray pLayersArray = new ArrayClass();
                IWorkspaceFactory ipWorkspaceFactory = new AccessWorkspaceFactory();
                IWorkspace ipWorkspace = ipWorkspaceFactory.OpenFromFile(strDBPath, 0);
                IFeatureWorkspace ipFeatureWorkspace = ipWorkspace as IFeatureWorkspace;
                IEnumDataset pEnumDataSet = ipWorkspace.get_Datasets(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureDataset);
                IDataset pDataset = pEnumDataSet.Next();
                IArray pFeatureLayerArray = new ArrayClass();

                while (pDataset != null)
                {
                    DataSetInfo pDataSetInfo = new DataSetInfo();
                    pDataSetInfo.Name = pDataset.Name;
                    pDataSetInfo.pSubSetArray = new ArrayClass();
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        IFeatureLayer pFeaturelayer = new FeatureLayerClass();
                        IFeatureClass pFeatureClass = ipFeatureWorkspace.OpenFeatureClass(pDataset.Name);
                        pFeaturelayer.FeatureClass = pFeatureClass;
                        pFeaturelayer.Name = pDataset.Name;
                        pFeatureLayerArray.Add(pFeaturelayer);
                    }
                    else if (pDataset.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        IEnumDataset pEnumSubDataset = pDataset.Subsets;
                        pEnumSubDataset.Reset();
                        IDataset pSubDataSet = pEnumSubDataset.Next();
                        while (pSubDataSet != null)
                        {
                            pDataSetInfo.pSubSetArray.Add(pSubDataSet.Name);

                            if (pSubDataSet.Type == esriDatasetType.esriDTFeatureClass)
                            {
                                IFeatureLayer pFeaturelayer = new FeatureLayerClass();
                                IFeatureClass pFeatureClass = ipFeatureWorkspace.OpenFeatureClass(pSubDataSet.Name);
                                pFeaturelayer.FeatureClass = pFeatureClass;
                                pFeaturelayer.Name = pSubDataSet.Name;
                                pFeatureLayerArray.Add(pFeaturelayer);
                            }
                            pSubDataSet = pEnumSubDataset.Next();
                        }
                    }
                    if (pDatasets != null)
                    {
                        pDatasets.Add(pDataSetInfo);
                    }
                    pDataset = pEnumDataSet.Next();
                }
                pEnumDataSet = ipWorkspace.get_Datasets(ESRI.ArcGIS.Geodatabase.esriDatasetType.esriDTFeatureClass);
                pDataset = pEnumDataSet.Next();
                while (pDataset != null)
                {
                    if (pDataset.Type == esriDatasetType.esriDTFeatureClass)
                    {
                        if (pDatasets != null)
                        {
                            DataSetInfo pDataSetInfo = new DataSetInfo();
                            pDataSetInfo.Name = pDataset.Name;
                            pDatasets.Add(pDataSetInfo);
                        }
                        IFeatureLayer pFeaturelayer = new FeatureLayerClass();
                        IFeatureClass pFeatureClass = ipFeatureWorkspace.OpenFeatureClass(pDataset.Name);
                        pFeaturelayer.FeatureClass = pFeatureClass;
                        pFeaturelayer.Name = pDataset.Name;
                        pFeatureLayerArray.Add(pFeaturelayer);
                    }
                    pDataset = pEnumDataSet.Next();
                }

                //为各层排序，按照Mutipatch,Polygon,polyLine,Point
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryMultiPatch)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryPolygon)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryPolyline)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                for (int i = 0; i < pFeatureLayerArray.Count; i++)
                {
                    IFeatureLayer pFeatureLayer = pFeatureLayerArray.get_Element(i) as IFeatureLayer;
                    IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                    esriGeometryType gType = pFeatureClass.ShapeType;
                    if (gType == esriGeometryType.esriGeometryPoint)
                    {
                        pLayersArray.Add(pFeatureLayer);
                    }
                }
                return pLayersArray;
            }
            catch (Exception Ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(Ex.Message);
                return null;
            }
            return null;
        }
    
    
    }
}
