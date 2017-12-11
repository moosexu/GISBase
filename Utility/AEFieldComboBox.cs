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


namespace Seismography
{
    public partial class AEFieldComboBox :  ComboBox
    {
        seismicGCBaseRelativeYearFieldFormat m_nFieldFormat;
        public AEFieldComboBox()
        {            
        }
        public void AddItems(IFeatureLayer m_pFeatureLayer, seismicGCBaseRelativeYearFieldFormat format = seismicGCBaseRelativeYearFieldFormat.seismicGCBRYFTypeYRS)
        {
            m_nFieldFormat = format;
            Items.Clear();
            if (m_pFeatureLayer == null || m_pFeatureLayer.FeatureClass == null) return;
            IFeatureClass pFeatureClass = m_pFeatureLayer.FeatureClass;
               
            ILayerFields pLayerFields = m_pFeatureLayer as ILayerFields;
            for (int k = 0; k < pLayerFields.FieldCount; k++)
            {
                IField pField = pLayerFields.get_Field(k);
                if (GeoBaseLib.IsSysField(pField.Name)) continue;
                if (m_nFieldFormat == seismicGCBaseRelativeYearFieldFormat.seismicGCBRYFTypeYRS)
                {
                    if (pField.Name.Contains("YRS"))
                        Items.Add(pField.Name);
                }
                else if (m_nFieldFormat == seismicGCBaseRelativeYearFieldFormat.seismicGCBRYFTypeNone)
                {
                     Items.Add(pField.Name);
                }
            }
            if (Items.Count > 0)
            {
                SelectedIndex = 0;
            }
        }
    }
}
