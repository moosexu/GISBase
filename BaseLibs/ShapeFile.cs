using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BaseLibs
{
    class ShapeFile
    {
        AxMapControl m_axMapControl =null;
        IFeatureLayer m_pFeatureLayer = null;
        esriGeometryType m_nGeometryType = esriGeometryType.esriGeometryPoint;
        string m_sMapPath = null;
        string m_sLayerName = null;
        IFeature m_pCurFeature = null;
     
        public ShapeFile(AxMapControl axMapControl)
        {
            m_axMapControl = axMapControl;
        }
        IFields CreateFields(baseFieldList pNewFields)
        {
            IFields pFields = new FieldsClass();
            IFieldsEdit fieldsEdit = pFields as IFieldsEdit;
            //Shape Field
            IField fd1 = new FieldClass();
            IFieldEdit fde1 = fd1 as IFieldEdit;
            fde1.Name_2 = "Shape";
            fde1.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeomDef = new GeometryDefClass();
            IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;
            ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();
            pGeomDefEdit.GeometryType_2 = m_nGeometryType;
            pGeomDefEdit.SpatialReference_2 = pSpatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984) as ISpatialReference;
            fde1.GeometryDef_2 = pGeomDef;
            fieldsEdit.AddField(fde1);

            for (int i = 0; i < pNewFields.Count; i++)
            {
                baseField pNewField = pNewFields.get_Element(i);             
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
                fieldsEdit.AddField(pField);
            }
            return pFields;
        }
        public bool Create(string sLayerName,esriGeometryType nGeometryType,baseFieldList pNewFields,bool bAddIntoMapContol=false)
        {            
            if (m_axMapControl == null) return false;
            m_nGeometryType = nGeometryType;
            m_sMapPath = Global.GetWorkspacePath();
            m_sLayerName = sLayerName;
            bool bFileExisted = System.IO.File.Exists(m_sMapPath + "\\" + m_sLayerName + ".shp");
            if (bFileExisted && MessageBox.Show("The file is already existed, and do you want to create new one?", "Select one", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.IO.File.Delete(m_sMapPath + "\\" + m_sLayerName + ".shp");
                System.IO.File.Delete(m_sMapPath + "\\" + m_sLayerName + ".dbf");
                System.IO.File.Delete(m_sMapPath + "\\" + m_sLayerName + ".shx");
                GeoBaseLib.RemoveLayer(m_axMapControl, m_sLayerName);
            }
            IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace pFWS = pWorkspaceFactory.OpenFromFile(m_sMapPath, 0) as IFeatureWorkspace;
            m_pFeatureLayer = new FeatureLayerClass();
            //If file existed, directly load
            if (System.IO.File.Exists(m_sMapPath + "\\" + m_sLayerName + ".shp"))
            {
                m_pFeatureLayer.FeatureClass = pFWS.OpenFeatureClass(m_sLayerName);
                m_pFeatureLayer.Name = m_pFeatureLayer.FeatureClass.AliasName;
                if (bAddIntoMapContol && !GeoBaseLib.IsLayerExisted(m_axMapControl, m_sLayerName))
                {
                    m_axMapControl.AddLayer(m_pFeatureLayer);
                    m_axMapControl.Refresh();
                }
                return true;
            }
            try
            {
                IFields pFields = CreateFields(pNewFields);
                //Create feature class
                m_pFeatureLayer.FeatureClass = pFWS.CreateFeatureClass(m_sLayerName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                m_pFeatureLayer.Name = m_sLayerName;
                if (bAddIntoMapContol)
                {
                    m_axMapControl.AddShapeFile(m_sMapPath, m_sLayerName + ".shp");
                    m_axMapControl.Refresh();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Create \"" + m_sLayerName + "\" shapefile error:" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            return true;
        }

        public void AddIntoMapControl()
        {
            m_axMapControl.AddShapeFile(m_sMapPath, m_sLayerName + ".shp");
            m_axMapControl.Refresh();
        }

        public IFeature CreateFeature()
        {
            //start adding
            IFeatureClass fc = m_pFeatureLayer.FeatureClass;
            IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;
            m_pCurFeature =  fc.CreateFeature();
            return m_pCurFeature;
        }
        public void set_Shape(IGeometry pShape)
        {
            if (m_pCurFeature != null)
                m_pCurFeature.Shape = pShape;
            
        }
        public void copy_Value(IFeature pFromFeature)
        {
            if (pFromFeature == null || m_pCurFeature==null) return;
            for (int i = 0; i < pFromFeature.Fields.FieldCount; i++)
            {
                IField pFromField = pFromFeature.Fields.get_Field(i);
                if (GeoBaseLib.IsSysField(pFromField.Name)) continue;
                int index = m_pFeatureLayer.FeatureClass.Fields.FindField(pFromField.Name);
                if (index != -1)
                {
                    object value = pFromFeature.get_Value(i);
                    m_pCurFeature.set_Value(index, value);
                }
            }
        }
        public void set_Value(string FieldName, object value)
        {
            if (m_pCurFeature != null)
            {
                int index = m_pFeatureLayer.FeatureClass.Fields.FindField(FieldName);
                if (index != -1)
                {
                    m_pCurFeature.set_Value(index, value);
                }
            }
        }
        public void Store()
        {
            if (m_pCurFeature != null)
                m_pCurFeature.Store();
        }
        
        public static baseFieldList get_Fields(IFeatureLayer pLayer)
        {
            baseFieldList pFields = new baseFieldList();
            if (pLayer == null || pLayer.FeatureClass == null) return null;
            for (int i = 0; i < pLayer.FeatureClass.Fields.FieldCount; i++)
            {
                IField pField = pLayer.FeatureClass.Fields.get_Field(i);
                if (GeoBaseLib.IsSysField(pField.Name)) continue;
                baseField smField = new baseField();
                smField.strFieldName = pField.Name;
                smField.strFieldAliasName = pField.Name;
                smField.ftFieldType = pField.Type;
                smField.nFieldLen = pField.Length;
                pFields.Add(smField);
            }
            return pFields;
        }

        public static baseFieldList get_Fields(IFeature pFeature)
        {
            baseFieldList pFields = new baseFieldList();
            if (pFeature == null) return null;
            for (int i = 0; i < pFeature.Fields.FieldCount; i++)
            {
                IField pField = pFeature.Fields.get_Field(i);
                if (GeoBaseLib.IsSysField(pField.Name)) continue;
                baseField smField = new baseField();
                smField.strFieldName = pField.Name;
                smField.strFieldAliasName = pField.Name;
                smField.ftFieldType = pField.Type;
                smField.nFieldLen = pField.Length;
                pFields.Add(smField);
            }
            return pFields;
        }

        public bool AddFields(baseFieldList pNewFields)
        {
            if (m_pFeatureLayer == null || m_pFeatureLayer.FeatureClass == null || pNewFields.Count == 0) return false;
            try
            {
                IFeatureClass fc = m_pFeatureLayer.FeatureClass;
                IWorkspaceEdit w = (fc as IDataset).Workspace as IWorkspaceEdit;               
                for (int i = 0; i < pNewFields.Count; i++)
                {
                    baseField pNewField = pNewFields.get_Element(i);
                    if (pNewField.strFieldName.Length == 0 || m_pFeatureLayer.FeatureClass.Fields.FindField(pNewField.strFieldName) != -1) continue;
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
                    m_pFeatureLayer.FeatureClass.AddField(pField);
                }             
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }





    }
}
