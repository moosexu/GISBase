using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using BaseLibs;
    

namespace GISBase.Analysis
{
    class SpatialAnalysis
    {
        public void BufferOfFeatures(AxMapControl axMap,IFeatureLayer pFeatureLayer, double bRadiusInKm)
        {
            if (pFeatureLayer == null) return;
            IPoint pTransform = GeoBaseLib.PRJtoGCS(bRadiusInKm * 1000.0, 0);
            //double bRadiusInDegree = pTransform.X;
            double bRadiusInDegree = bRadiusInKm;

            GeoBaseLib gb = new GeoBaseLib(axMap);
            IArray pFaultFeatureArray = gb.Search(pFeatureLayer, null);
            IArray pGeometries = new ArrayClass();
            esriGeometryType geoType = GeoBaseLib.GeometryType(pFeatureLayer);
            for (int i = 0; i < pFaultFeatureArray.Count; i++)
            {
                IFeature pFaultFeature = pFaultFeatureArray.get_Element(i) as IFeature;
                if (pFaultFeature.Shape.IsEmpty || pFaultFeature.Extent.Height == 0.0 || pFaultFeature.Extent.Width == 0.0)
                {
                    pFaultFeatureArray.Remove(i);
                    i--;
                    continue;
                }
                IGeometry pBufferGeometry = Buffer(pFaultFeature.Shape, bRadiusInDegree);
                pGeometries.Add(pBufferGeometry);
            }
            gb.CreateShapefile(pFeatureLayer.Name + ("_") + bRadiusInKm.ToString(), pFaultFeatureArray, pGeometries, esriGeometryType.esriGeometryPolygon);
        }
        public static IGeometry Buffer(IGeometry pGeometry, double dDistance)
        {
            if (pGeometry == null) return null;
            ITopologicalOperator pTopologicalOperator = pGeometry as ITopologicalOperator;
            if (pTopologicalOperator == null) return null;
            if (!pTopologicalOperator.IsSimple)
                pTopologicalOperator.Simplify();
            IGeometry pBufferGeomtry = pTopologicalOperator.Buffer(dDistance);
            return pBufferGeomtry;
        }
    }
}
