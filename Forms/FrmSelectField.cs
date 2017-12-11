using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace GISBase.Forms
{
    public partial class FrmSelectField : DevComponents.DotNetBar.Office2007Form
    {
        private IFeatureLayer m_pFeatureLayer = null;
        private string m_strDefFieldName = null;
        public string strDefFieldName
        {
            get { return m_strDefFieldName; }
            set { m_strDefFieldName = value; }
        }
        public FrmSelectField(IFeatureLayer pFeatureLayer, string strDefFieldName)
        {
            m_strDefFieldName = strDefFieldName;
            m_pFeatureLayer = pFeatureLayer;
            InitializeComponent();
        }

        private void FrmSelectField_Load(object sender, EventArgs e)
        {
            cb_Fields.AddItems(m_pFeatureLayer);          
            cb_Fields.SelectedIndex = cb_Fields.FindString(m_strDefFieldName);
            if (cb_Fields.SelectedIndex != -1)
            {
                cb_Fields.SelectedIndex = 0;
            } 
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (cb_Fields.Items.Count == 0 || cb_Fields.SelectedIndex < 0)
            {
                m_strDefFieldName = null;
                return;
            }
            else
            {
                m_strDefFieldName = cb_Fields.SelectedItem.ToString();
            }
        }
    }
}
