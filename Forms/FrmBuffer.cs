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

namespace GISBase.Forms
{
    public partial class FrmBuffer : DevComponents.DotNetBar.Office2007Form
    {
        public AxMapControl m_axMapControl = null;
        public baseLayerType m_sltLayerType = baseLayerType.baseFeatureLayer;
        public ILayer m_pLayer = null;
        public double m_dRadius = 1.0;

        public FrmBuffer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeoBaseLib gb = new GeoBaseLib(m_axMapControl);
            string sLayer = comboBox1.SelectedItem.ToString();
            m_pLayer = gb.GetLayer(sLayer);
            m_dRadius = Convert.ToDouble(tb_Buffer.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FrmBuffer_Load(object sender, EventArgs e)
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";//IFeatureLayer
            IEnumLayer players = m_axMapControl.Map.get_Layers(uid, true);
            ILayer plyr = players.Next();
            while (plyr != null)
            {
                if (plyr is IFeatureLayer)
                {
                    comboBox1.Items.Add(plyr.Name);
                }
                plyr = players.Next();
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            tb_Buffer.Text = "100";
        }       
    }
}
