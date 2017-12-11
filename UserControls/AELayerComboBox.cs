using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using BaseLibs;

namespace GISBase.UserControls
{    
    public partial class AELayerComboBox : ComboBox
    {
        baseLayerType m_sltLayerType;
        public AELayerComboBox()
        {
            InitializeComponent();
        }
        public void AddItems(AxMapControl mapControl, baseLayerType sltLayerType)
        {
            if (mapControl == null) return;
            Items.Clear();
            m_sltLayerType = sltLayerType;
            if (m_sltLayerType == baseLayerType.baseRasterLayer)
            {
                UID uid = new UIDClass();
                uid.Value = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}";//IRasterLayer
                IEnumLayer players = mapControl.Map.get_Layers(uid, true);
                ILayer plyr = players.Next();
                while (plyr != null)
                {               
                    if (plyr is IRasterLayer)
                    {
                        Items.Add(plyr.Name);
                    }
                    plyr = players.Next();
                }
            }
            else if (m_sltLayerType == baseLayerType.baseFeatureLayer)
            {
                UID uid = new UIDClass();
                uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";//IFeatureLayer
                IEnumLayer players = mapControl.Map.get_Layers(uid, true);
                ILayer plyr = players.Next();
                while (plyr != null)
                {
                    if (plyr is IFeatureLayer)
                    {
                        Items.Add(plyr.Name);
                    }
                    plyr = players.Next();
                }
            }
            else if (m_sltLayerType == baseLayerType.baseGroupLayer)
            {
                UID uid = new UIDClass();
                uid.Value = "{EDAD6644-1810-11D1-86AE-0000F8751720}";//IGroupLayer
                IEnumLayer players = mapControl.Map.get_Layers(uid, true);
                ILayer plyr = players.Next();
                while (plyr != null)
                {
                    if (plyr is IGroupLayer)
                    {
                        Items.Add(plyr.Name);
                    }
                    plyr = players.Next();
                }
            }
            else if (m_sltLayerType == baseLayerType.basePointLayer)
            {
                UID uid = new UIDClass();
                uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";//IFeatureLayer
                IEnumLayer players = mapControl.Map.get_Layers(uid, true);
                ILayer plyr = players.Next();
                while (plyr != null)
                {                   
                    if (plyr is IFeatureLayer)
                    {
                        IFeatureLayer pLyrTmp = plyr as IFeatureLayer;
                        if (pLyrTmp.FeatureClass != null && pLyrTmp.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint)
                        {
                            Items.Add(plyr.Name);
                        }
                    }
                    plyr = players.Next();
                }
            }
            else if (m_sltLayerType == baseLayerType.basePolylineLayer)
            {
                UID uid = new UIDClass();
                uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";//IFeatureLayer
                IEnumLayer players = mapControl.Map.get_Layers(uid, true);
                ILayer plyr = players.Next();
                while (plyr != null)
                {
                    if (plyr is IFeatureLayer)
                    {
                        IFeatureLayer pLyrTmp = plyr as IFeatureLayer;
                        if (pLyrTmp.FeatureClass!=null && pLyrTmp.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline)
                        {
                            Items.Add(plyr.Name);
                        }
                    }
                    plyr = players.Next();
                }
            }
            else if (m_sltLayerType == baseLayerType.basePolygonLayer)
            {
                UID uid = new UIDClass();
                uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";//IFeatureLayer
                IEnumLayer players = mapControl.Map.get_Layers(uid, true);
                ILayer plyr = players.Next();
                while (plyr != null)
                {
                    if (plyr is IFeatureLayer)
                    {
                        IFeatureLayer pLyrTmp = plyr as IFeatureLayer;
                        if (pLyrTmp.FeatureClass != null && pLyrTmp.FeatureClass.ShapeType == ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon)
                        {
                            Items.Add(plyr.Name);
                        }
                    }
                    plyr = players.Next();
                }
            }            
        }
        
    }
}
