using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;
using GISBase.UserControls;
using BaseLibs;

namespace GISBase.Forms
{
    public partial class SearchByAttrFrm : DevComponents.DotNetBar.Office2007Form
    {
        int m_iSelPos = -1;
        public SearchByAttrFrm(ESRI.ArcGIS.Controls.AxMapControl pMapControl)
        {
            InitializeComponent();
            m_MapControl = pMapControl;//得到当前AxMapControl，以便下一步操作
        }
        ESRI.ArcGIS.Controls.AxMapControl m_MapControl = null;
        IMap pMap;
        IFeatureLayer pFeatureLayer;
        ILayer pLayer;
        ILayerFields pLayerFields;
        IEnumLayer pEnumLayer;

        #region Mouse click/double click to create Whereclause
        private void listBoxValues_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (m_iSelPos < 0)
                {
                    m_iSelPos = textBoxWhereClause.SelectionStart;
                }
                else
                {
                    textBoxWhereClause.SelectionStart = m_iSelPos;
                    textBoxWhereClause.SelectionLength = textBoxWhereClause.Text.Length - m_iSelPos;
                }
                textBoxWhereClause.SelectedText = " " + listBoxValues.SelectedItem.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " = ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonNotEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " <> ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonBig_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " > ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonBigEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " >= ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonSmall_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " < ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonSmallEqual_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " <= ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonChars_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = "%";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonChar_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = "_";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonLike_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Like ";
            m_iSelPos = textBoxWhereClause.SelectionStart;

        }

        private void buttonAnd_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " And ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonOr_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Or ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }

        private void buttonNot_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Not ";
        }

        private void buttonIs_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " Is ";
            m_iSelPos = textBoxWhereClause.SelectionStart;
        }


        private void buttonBrace_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = "(  )";
            //让输入的位置恰好处在（）里面，就同arcmap的效果一样
            textBoxWhereClause.SelectionStart = textBoxWhereClause.Text.Length - 2;
        }
        #endregion
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectByAttrFrm_Load(object sender, EventArgs e)
        {
            //sxw-------------
            if (m_MapControl == null) return;
            pMap = m_MapControl.Object as IMap;
            //--------------
            if (pMap.LayerCount == 0) return;
            this.pEnumLayer = this.pMap.get_Layers(null, true);
            if (pEnumLayer == null)
            {
                return;
            }
            pEnumLayer.Reset();
            for (this.pLayer = pEnumLayer.Next(); this.pLayer != null; this.pLayer = pEnumLayer.Next())
            {
                if (this.pLayer is IFeatureLayer)
                {
                    this.comboBoxLayers.Items.Add(this.pLayer.Name);
                }
            }
            if (this.comboBoxLayers.Items.Count == 0)
            {
                MessageBox.Show("No layers existed to select!");
                this.Close();
                return;
            }
            this.comboBoxLayers.SelectedIndex = 0;
            this.comboBoxMethod.SelectedIndex = 0;
        }
        /// <summary>
        /// 选择了别的图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)//根据所选择的图层得到该图层的可查询字段
        {
            if (this.pEnumLayer == null) return;

            IField pField;
            int currentFieldType;
            this.pEnumLayer.Reset();
            this.listBoxFields.Items.Clear();
            for (this.pLayer = this.pEnumLayer.Next(); this.pLayer != null; this.pLayer = this.pEnumLayer.Next())
            {
                if (this.pLayer.Name != this.comboBoxLayers.Text) continue;
                this.pLayerFields = this.pLayer as ILayerFields;
                for (int i = 0; i < this.pLayerFields.FieldCount; i++)
                {
                    pField = this.pLayerFields.get_Field(i);
                    currentFieldType = (int)pField.Type;
                    //sxw
                    if (currentFieldType == 7 || currentFieldType == 8 || currentFieldType == 9 || currentFieldType == 10 || currentFieldType == 11 || currentFieldType == 12) continue;//不是可以查询的字段类型esriFieldTypeString
                    //sxw
                    this.listBoxFields.Items.Add(pField.Name);
                }
                break;
            }
            this.pFeatureLayer = this.pLayer as IFeatureLayer;
            this.labelLayer.Text = this.comboBoxLayers.Text;
            textBoxWhereClause.Text = "";
        }

        /// <summary>
        /// 双击字段名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFields_DoubleClick(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = listBoxFields.SelectedItem.ToString() + " ";
        }
        /// <summary>
        /// 获得当前字段的唯一值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void GetAttributeVale()
        {
            try
            {
                if (this.listBoxFields.SelectedIndex == -1) return;

                string currentFieldName = this.listBoxFields.Text;//当前字段名
                string currentLayerName = this.comboBoxLayers.Text;
                this.pEnumLayer.Reset();
                for (this.pLayer = this.pEnumLayer.Next(); this.pLayer != null; this.pLayer = this.pEnumLayer.Next())
                {
                    if (this.pLayer.Name == currentLayerName) break;
                }
                this.pLayerFields = this.pLayer as ILayerFields;
                IField pField = this.pLayerFields.get_Field(this.pLayerFields.FindField(currentFieldName));
                esriFieldType pFieldType = pField.Type;

                //对Table中当前字段进行排序,把结果赋给Cursor
                ITable pTable = this.pLayer as ITable;
                ITableSort pTableSort = new TableSortClass();
                pTableSort.Table = pTable;
                pTableSort.Fields = currentFieldName;
                pTableSort.set_Ascending(currentFieldName, true);
                pTableSort.set_CaseSensitive(currentFieldName, true);
                pTableSort.Sort(null);//排序
                ICursor pCursor = pTableSort.Rows;
                //IRow pRow = pCursor.NextRow();
                //int nSize = 0;
                //while (pRow != null)
                //{
                //    nSize++;
                //    pRow = pCursor.NextRow();
                //}
                //DevComponents.DotNetBar.MessageBox.Show(nSize.ToString());
                //return;

                //字段统计
                IDataStatistics pDataStatistics = new DataStatisticsClass();
                pDataStatistics.Cursor = pCursor;
                pDataStatistics.Field = currentFieldName;
                System.Collections.IEnumerator pEnumeratorUniqueValues = pDataStatistics.UniqueValues;//唯一值枚举
                int uniqueValueCount = pDataStatistics.UniqueValueCount;//唯一值的个数

                this.listBoxValues.Items.Clear();
                string currentValue = null;
                pEnumeratorUniqueValues.Reset();
                if (pFieldType == esriFieldType.esriFieldTypeString)
                {
                    while (pEnumeratorUniqueValues.MoveNext())
                    {
                        currentValue = pEnumeratorUniqueValues.Current.ToString();
                        this.listBoxValues.Items.Add("'" + currentValue + "'");
                    }
                }
                else
                {
                    while (pEnumeratorUniqueValues.MoveNext())
                    {
                        currentValue = pEnumeratorUniqueValues.Current.ToString();
                        this.listBoxValues.Items.Add(currentValue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonGetValue_Click(object sender, EventArgs e)
        {
            GetAttributeVale();
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.Clear();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView;
            pActiveView = m_MapControl.ActiveView;
            m_MapControl.Map.ClearSelection();
            if (textBoxWhereClause.Text == "")
            {
                MessageBox.Show("Please create query clause!");
                return;
            }
            try
            {
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = textBoxWhereClause.Text;
                IFeatureSelection pFeatureSelection = this.pFeatureLayer as IFeatureSelection;
                int iSelectedFeaturesCount = pFeatureSelection.SelectionSet.Count;
                esriSelectionResultEnum selectMethod;
                switch (comboBoxMethod.SelectedIndex)
                {
                    case 0: selectMethod = esriSelectionResultEnum.esriSelectionResultNew; break;
                    case 1: selectMethod = esriSelectionResultEnum.esriSelectionResultAdd; break;
                    case 2: selectMethod = esriSelectionResultEnum.esriSelectionResultSubtract; break;
                    case 3: selectMethod = esriSelectionResultEnum.esriSelectionResultAnd; break;
                    default: selectMethod = esriSelectionResultEnum.esriSelectionResultNew; break;
                }
                pFeatureSelection.SelectFeatures(pQueryFilter, selectMethod, false);//执行查询

                //如果本次查询后，查询的结果数目没有改变，则认为本次查询没有产生新的结果
                if (pFeatureSelection.SelectionSet.Count == 0)
                {
                    MessageBox.Show("No results!");
                    return;
                }
                //pFeatureSelection.SelectionSet.Count == iSelectedFeaturesCount || 
                IEnumFeature pEnumFeature = m_MapControl.Map.FeatureSelection as IEnumFeature;
                int nFeatureSize = 0;
                pEnumFeature.Reset();
                
                IFeature pFeature =pEnumFeature.Next();
                while (pFeature != null)
                {
                    nFeatureSize++;
                    pFeature = pEnumFeature.Next();
                }
                IEnvelope pEnvelope = new EnvelopeClass();

                IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
                double dScale = (this.pFeatureLayer.MinimumScale + this.pFeatureLayer.MaximumScale) / 2;
                ESRI.ArcGIS.Controls.IMapControl3 pMaps = (ESRI.ArcGIS.Controls.IMapControl3)m_MapControl.Object;

                bool bRec=true;
                while (bRec)
                {
                    pEnumFeature.Reset();
                    pFeature = pEnumFeature.Next();
                    int i = 0;
                    while (pFeature != null)//将目标中心显示
                    {
                        pEnvelope = pFeature.Extent;
                        IPoint pCenterPt = new PointClass();
                        pCenterPt.X = (pEnvelope.XMax + pEnvelope.XMin) / 2;
                        pCenterPt.Y = (pEnvelope.YMax + pEnvelope.YMin) / 2;

                        IEnvelope pMapEntent = pActiveView.Extent;
                        pMapEntent.CenterAt(pCenterPt);
                        pActiveView.Extent = pMapEntent;

                        pMaps.MapScale = dScale;
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, pActiveView.Extent);
                        pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, pActiveView.Extent);
                        Application.DoEvents();
                        MapView.FlashFeature(m_MapControl, pFeature.Shape);//目标闪烁
                        Application.DoEvents();
                        i++;
                        if (nFeatureSize >= 2 && i < nFeatureSize - 1)
                        {
                            this.Visible = false;
                            DialogResult pRes = MessageBox.Show("Is this feature?", "Sure", MessageBoxButtons.YesNoCancel);
                            if (pRes == DialogResult.Yes)
                            {
                                this.Visible = true;
                                return;
                            }
                            else if (pRes == DialogResult.Cancel)
                            {
                                return;
                            }
                        }
                        pFeature = pEnumFeature.Next();
                    }
                    if (nFeatureSize <= 1)
                    {
                        return;
                    }
                    DialogResult pDlgRes = MessageBox.Show("It is already last one, restart?", "Sure", MessageBoxButtons.YesNo);
                    if (pDlgRes == DialogResult.Yes)
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
                
                //for (int i = 0; i < pArray.Count; i++)
                //{
                //    int nOID = (int)pArray.get_Element(i);
                //    pFeature = pFeatureClass.GetFeature(nOID);
                //    pEnvelope = pFeature.Extent;
                //    IPoint pCenterPt = new PointClass();
                //    pCenterPt.X = (pEnvelope.XMax + pEnvelope.XMin) / 2;
                //    pCenterPt.Y = (pEnvelope.YMax + pEnvelope.YMin) / 2;

                //    IEnvelope pMapEntent = pActiveView.Extent;
                //    pMapEntent.CenterAt(pCenterPt);
                //    pActiveView.Extent = pMapEntent;

                //    pMaps.MapScale = dScale;
                //    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                //    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                //    Application.DoEvents();
                //    MapView.FlashFeature(m_MapControl, pFeature.Shape);
                //    Application.DoEvents();
                //    if (pArray.Count > 1 && i < pArray.Count - 1)
                //    {
                //        if (DevComponents.DotNetBar.MessageBox.Show("是否为这个目标", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //        {
                //            return;
                //        }
                //    }
                //}


                //double i = MainAxMapControl.Map.SelectionCount;
                //i = Math.Round(i, 0);//小数点后指定为０位数字
                //pMainform.toolStripStatusLabel1.Text = "当前共有" + i.ToString() + "个查询结果";
            }
            catch (Exception ex)
            {
                MessageBox.Show("The clause has errors, please double check!\n" + ex.Message);
                return;
            }
        }
        private void FlashFeature(IGeometry pGeometry)//目标闪烁
        {
            esriGeometryType pGeometryType = pGeometry.GeometryType;
            ISymbol pSymbol = null;
            IColor pColor = new RgbColorClass();
            pColor.RGB = 255;

            if (pGeometryType == esriGeometryType.esriGeometryPolygon)
            {
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                pSimpleFillSymbol.Color = pColor;
                pSymbol = pSimpleFillSymbol as ISymbol;
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPolyline || pGeometryType == esriGeometryType.esriGeometryLine)
            {
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                pSimpleLineSymbol.Color = pColor;
                pSimpleLineSymbol.Width = 2;
                pSymbol = pSimpleLineSymbol as ISymbol;
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPoint || pGeometryType == esriGeometryType.esriGeometryMultipoint)
            {
                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                pSimpleMarkerSymbol.OutlineColor = pColor;
                pSimpleMarkerSymbol.OutlineSize = 2;
                pSymbol = pSimpleMarkerSymbol as ISymbol;
            }
            m_MapControl.FlashShape(pGeometry, 4, 200, pSymbol);
        }
        private IEnvelope Getenvelop(IFeatureSelection pFeatureSelection)//得到选择集的包络矩形
        {
            //ISelection pSelection = pFeatureSelection as ISelection;
            IEnumFeature pEnumFeature = pFeatureSelection as IEnumFeature;

            IFeature pFeature = pEnumFeature.Next();
            IEnvelope pEnvelope = new EnvelopeClass();
            while (pFeature != null)
            {
                pEnvelope.Union(pFeature.Extent);
                pFeature = pEnumFeature.Next();
            }
            return pEnvelope;
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.buttonAnd_Click(sender, e);
            this.Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listBoxValues.Items.Clear();
            GetAttributeVale();
        }

        private void SearchByAttrFrm_Load(object sender, EventArgs e)
        {
            if (m_MapControl.Map.LayerCount == 0) return;
            pMap = m_MapControl.Map;

            if (pMap.LayerCount == 0) return;
            this.pEnumLayer = this.pMap.get_Layers(null, true);
            if (pEnumLayer == null)
            {
                return;
            }
            pEnumLayer.Reset();
            for (this.pLayer = pEnumLayer.Next(); this.pLayer != null; this.pLayer = pEnumLayer.Next())//添加图层
            {
                if (this.pLayer is IFeatureLayer)
                {
                    this.comboBoxLayers.Items.Add(this.pLayer.Name);
                }
            }
            if (this.comboBoxLayers.Items.Count == 0)
            {
                MessageBox.Show("No layers existed to select!");
                this.Close();
                return;
            }
            this.comboBoxLayers.SelectedIndex = 0;
            this.comboBoxMethod.SelectedIndex = 0;
        }

        private void SearchByAttrFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_MapControl==null) return;
            m_MapControl.Map.ClearSelection();
            m_MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, m_MapControl.Extent);
        }
    }
}