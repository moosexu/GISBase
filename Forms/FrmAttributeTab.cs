using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;


namespace GISBase.Forms
{
    public partial class FrmAttributeTab : DevComponents.DotNetBar.Office2007Form
    {
        private IFeatureLayer pFeatureLayer=null;
        int m_nMode = 0;//0--for a layer, 1-for array
        IArray m_aFeatures = null;
        public IArray aFeatures
        {
            set { m_aFeatures = value; }
        }
        public int nMode
        {
            set { m_nMode = value; }
        }

        public FrmAttributeTab(IFeatureLayer FeatureLayer)
        {
            pFeatureLayer = FeatureLayer;
            InitializeComponent();
        }

        private void FrmAttributeTab_Load(object sender, EventArgs e)
        {
            int nFeatureCount = 0;
            try
            {
                if (pFeatureLayer == null) return;
                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                if (pFeatureLayer == null)
                {
                    return;
                }
                ILayerFields pLayerFields = pFeatureLayer as ILayerFields;
                DataSet ds = new DataSet("dsTest");
                DataTable dt = new DataTable(pFeatureLayer.Name);
                DataColumn dc = null;
                for (int k = 0; k < pLayerFields.FieldCount; k++)
                {
                    dc = new DataColumn(pLayerFields.get_Field(k).Name);
                    dt.Columns.Add(dc);
                    dc = null;
                }
                if (m_nMode == 0)
                {
                    IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    while (pFeature != null)
                    {
                        nFeatureCount++;
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < pLayerFields.FieldCount; j++)
                        {
                            if (pLayerFields.FindField(pFeatureClass.ShapeFieldName) == j)
                            {
                                dr[j] = pFeatureClass.ShapeType.ToString();
                            }
                            else
                            {
                                dr[j] = pFeature.get_Value(j).ToString();
                            }
                        }
                        dt.Rows.Add(dr);
                        pFeature = pFeatureCursor.NextFeature();
                    }
                    ds.Tables.Add(dt);
                    dataGridView1.DataSource = ds.Tables[pFeatureLayer.Name];
                }
                else if (m_nMode == 1)
                {
                    nFeatureCount = m_aFeatures.Count;
                    for (int i = 0; i < m_aFeatures.Count; i++)
                    {
                        IFeature pFeature = m_aFeatures.get_Element(i) as IFeature;                       
                        DataRow dr = dt.NewRow();
                        for (int j = 0; j < pLayerFields.FieldCount; j++)
                        {
                            if (pLayerFields.FindField(pFeatureClass.ShapeFieldName) == j)
                            {
                                dr[j] = pFeatureClass.ShapeType.ToString();
                            }
                            else
                            {
                                dr[j] = pFeature.get_Value(j).ToString();
                            }
                        }                         
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                    dataGridView1.DataSource = ds.Tables[pFeatureLayer.Name];
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed in read attributes: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            label1.Text = nFeatureCount.ToString() + " features in this table.";
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
           // Export.DataGridView2Excel(dataGridView1);
        }
    }
}