using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using stdole;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.BaseClasses;


namespace GISBase.Visualization
{
    class BarChart:BaseCommand
    {
        IHookHelper m_hookHelper = new HookHelperClass();
        IMap pMap ;
        IActiveView pActiveView;
        IFeatureLayer m_pLayer = null;
        public IFeatureLayer pLayer
        {
            set { m_pLayer = value; }
            get { return m_pLayer; }
        }
        public BarChart()
        {
            base.m_caption = "BarChartRenderer";
            base.m_message = "BarChartRenderer";
            base.m_toolTip = "BarChartRenderer";
            base.m_category = "Symbology";
            base.m_enabled = true;
            //base.m_bitmap = new 313
            //System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream(GetType(),"x.bmp"));
        }

        public override void OnCreate(object hook)
        {
            m_hookHelper.Hook = hook;
        }
        public override void OnClick()
        {
            IGeoFeatureLayer pGeoFeatureL;
            IFeatureLayer pFeatureLayer;
            ITable pTable;
            ICursor pCursor;
            IQueryFilter pQueryFilter;
            IRowBuffer pRowBuffer;
            int numFields=2;
            int[] fieldIndecies = new int[numFields];
            int lfieldIndex;
            double dmaxValue;
            bool firstValue;
            double dfieldValue;
            IChartRenderer pChartRenderer;
            IRendererFields pRendererFields;
            IFillSymbol pFillSymbol;
            IMarkerSymbol pMarkerSymbol;
            ISymbolArray pSymbolArray;
            IChartSymbol pChartSymbol;
            string strPopField1 = "population";
            string strPopField2 = "jingji";
            //pActiveView = m_hookHelper.ActiveView;
            //pMap = m_hookHelper.FocusMap;
            //pMap.ReferenceScale = pMap.MapScale ;
           // pFeatureLayer = (IGeoFeatureLayer) pMap.get_Layer(1);//Parameter!!!
            pGeoFeatureL = (IGeoFeatureLayer) m_pLayer;
            pTable = (ITable) pGeoFeatureL;
            pGeoFeatureL.ScaleSymbols = true;
            pChartRenderer = new ChartRendererClass();
            pRendererFields = (IRendererFields) pChartRenderer;
            pRendererFields.AddField(strPopField1,strPopField1);
            pRendererFields.AddField(strPopField2,strPopField2);
            pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField(strPopField1);
            pQueryFilter.AddField(strPopField2);
            pCursor = pTable.Search(pQueryFilter, true);
            fieldIndecies[0] = pTable.FindField(strPopField1);
            fieldIndecies[1] = pTable.FindField(strPopField2);
            firstValue = true;
            dmaxValue = 0;
            pRowBuffer = pCursor.NextRow();
            while (pRowBuffer != null)
            {
                for( lfieldIndex = 0 ; lfieldIndex<= numFields - 1;lfieldIndex++)
                {
                    dfieldValue=(double) Convert.ToInt32(pRowBuffer.get_Value(fieldIndecies[lfieldIndex]));
                    if (firstValue)
                    {
                        dmaxValue = dfieldValue;
                        firstValue=false;
                    }
                    else
                    {
                    if( dfieldValue > dmaxValue)
                    {
                        dmaxValue = dfieldValue;
                    }
                    //Set up the fields to draw charts of
                    //Make an array of the field numbers to iterate accross, this is to keep the code generic in the number of bars to draw.
                    //Iterate across each feature
                    // iterate through each data field and update the maxVal if needed
                    // Special case for the first value in a feature class
                    // we've got a new biggest value
                    }
                }
                pRowBuffer = pCursor.NextRow();
            }
            if (dmaxValue <= 0)
            {
                MessageBox.Show("Failed to gather stats on the feature class");
                Application.Exit();
            }
            IBarChartSymbol pBarChartSymbol;
            pBarChartSymbol = new BarChartSymbolClass();
            pChartSymbol = (IChartSymbol) pBarChartSymbol;
            pBarChartSymbol.Width = 12;
            pMarkerSymbol = (IMarkerSymbol) pBarChartSymbol;
            pChartSymbol.MaxValue = dmaxValue;
            pMarkerSymbol.Size = 80;
            pSymbolArray = (ISymbolArray) pBarChartSymbol;
            pFillSymbol = new SimpleFillSymbolClass();
            IRgbColor pRGB = new RgbColorClass();
            pRGB.Red = 213;
            pRGB.Green = 212;
            pRGB.Blue = 252;
            pFillSymbol.Color = pRGB;
            pSymbolArray.AddSymbol((ISymbol) pFillSymbol);
            pFillSymbol = new SimpleFillSymbolClass();
            pRGB.Red = 193;
            pRGB.Green = 252;
            pRGB.Blue = 179;
            pFillSymbol.Color = pRGB;
            pSymbolArray.AddSymbol( (ISymbol) pFillSymbol);
            pChartRenderer.ChartSymbol = (IChartSymbol) pBarChartSymbol;
            pChartRenderer.Label = "Population";
            pFillSymbol = new SimpleFillSymbolClass();
            pRGB.Red = 293;
            pRGB.Green = 228;
            pRGB.Blue = 190;
            pFillSymbol.Color = pRGB;
            pChartRenderer.BaseSymbol = (ISymbol) pFillSymbol;
            // Set up the chart marker symbol to use with the renderer
            //Finally we've got the biggest value, set this into the symbol
            //This is the maximum height of the bars
            //Now set up symbols for each bar
            // Add some colours in for each bar
            //This is a pastel green
            //Now set the barchart symbol into the renderer
            //set up the background symbol to use tan color
            //Disable overpoaster so that charts appear in the centre of polygons
            pChartRenderer.UseOverposter = false;
            pChartRenderer.CreateLegend();
            pGeoFeatureL.Renderer =(IFeatureRenderer) pChartRenderer;
            //pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
    }
}