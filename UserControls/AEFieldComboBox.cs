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
using ESRI.ArcGIS.Geodatabase;

using BaseLibs;

namespace GISBase.UserControls
{
    public partial class AEFieldComboBox :  ComboBox
    {
        public AEFieldComboBox()
        {            
        }
        public void AddItems(IFeatureLayer m_pFeatureLayer)
        {
            Items.Clear();
            if (m_pFeatureLayer == null || m_pFeatureLayer.FeatureClass == null) return;
            IFeatureClass pFeatureClass = m_pFeatureLayer.FeatureClass;
               
            ILayerFields pLayerFields = m_pFeatureLayer as ILayerFields;
            for (int k = 0; k < pLayerFields.FieldCount; k++)
            {
                IField pField = pLayerFields.get_Field(k);
                if (GeoBaseLib.IsSysField(pField.Name)) continue;
                Items.Add(pField.Name);
            }
            if (Items.Count > 0)
            {
                SelectedIndex = 0;
            }
        }
    }
}
