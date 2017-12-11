using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;


//�û��ؼ�:ʵ�ֵ�ͼӥ��ͼ����
//author ������
//Modified by czl,

namespace BaseLibs
{
    struct POINT
    {
        public double X;
        public double Y;
    }
    struct SIZE
    {
        public double cX;
        public double cY;
    }
    public partial class EyeMap : UserControl
    {
        ESRI.ArcGIS.Controls.AxMapControl m_MapControl = null;
        System.Drawing.Image m_pMapImage = null;
        IEnvelope m_pMapExtent = null;
        POINT m_pStratPt=new POINT();
        Rectangle m_rRect=new Rectangle();//�ڵ�ͼ����ʾ�ĵ�ǰ��Χ���ο��С
        Rectangle m_rStartRect = new Rectangle();//�ڵ�ͼ����ʾ�������ʼ��Χ���ο��С
        public EyeMap()
        {
            InitializeComponent();
        }

        private void EyeMap_Load(object sender, EventArgs e)
        {

        }

        public void SetBuddyControl(object pCtrl)//���ð󶨵ĵ�ͼ�ؼ�
        {
            if (!(pCtrl is ESRI.ArcGIS.Controls.AxMapControl)) return;
            m_MapControl = pCtrl as ESRI.ArcGIS.Controls.AxMapControl;
            m_MapControl.OnMapReplaced += new IMapControlEvents2_Ax_OnMapReplacedEventHandler(Ctrl_MapReplace);
            m_MapControl.OnExtentUpdated += new IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(Ctrl_ExtentUpdated);

        }

        public void MapRefresh()
        {
            if (m_MapControl == null) return;
            if (m_MapControl.LayerCount == 0)
            {
                m_pMapImage = null;
                this.Refresh();
                return;
            }
            RefreshMap();
        }

        //���ص�ͼ���ص��¼�
        private void Ctrl_MapReplace(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            if (m_pMapImage != null) return;
            m_pMapExtent = m_MapControl.Extent;
            m_rRect = MapExtent2Rect(m_pMapExtent);
            RefreshMap();
        }

        //���ص�ͼ��ǰ��Χ�仯���¼�
        private void Ctrl_ExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            m_pMapExtent = e.newEnvelope as IEnvelope;
            m_rRect = MapExtent2Rect(m_pMapExtent);
            this.Refresh();
            //RefreshMap();
        }
        //���ؿؼ�����仯�¼�
        private void EyeMap_Resize(object sender, EventArgs e)
        {
            if(this.ClientSize.Width==0||this.ClientSize.Height==0)
            {
                return;
            }
            Application.DoEvents();
            RefreshMap();
        }

        //���»��Ƶ�ͼ
        private void RefreshMap()
        {
            Image pImage = GetEyeImage(this.ClientSize.Width, this.ClientSize.Height);
            //if (pImage == null) return;
            m_pMapImage = pImage;
            this.Refresh();
        }

        //���ص�ͼ�����¼�
        private void EyeMap_Paint(object sender, PaintEventArgs e)
        {
            DrawEyeMap(e.Graphics);
        }
        //����ǰ��ͼ��Χ���㵽ӥ��ͼ��ָ����Χ
        private Rectangle MapExtent2Rect(IEnvelope pEnvelope)
        {
            Rectangle rect=new Rectangle();
            IEnvelope pFullExtent = m_MapControl.FullExtent;
            IPoint pCenterLogPoint = new PointClass();
            pCenterLogPoint.X = (pFullExtent.XMin + pFullExtent.XMax) / 2;
            pCenterLogPoint.Y = (pFullExtent.YMin + pFullExtent.YMax) / 2;

            IPoint pCenterDevPt = new PointClass();
            pCenterDevPt.X = (this.ClientSize.Width) / 2;
            pCenterDevPt.Y = (this.ClientSize.Height) / 2;

            double dxScale = (double)(this.ClientSize.Width) / (double)(pFullExtent.Width);
            double dyScale = (double)(this.ClientSize.Height) / (double)(pFullExtent.Height);
            double dSacle = Math.Min(dxScale, dyScale);
            double pX = (m_pMapExtent.XMin - pCenterLogPoint.X);
            double pY = -(m_pMapExtent.YMax - pCenterLogPoint.Y);
            int nX = (int)(pX * dSacle);
            int nY = (int)(pY * dSacle);
            int nWidth = (int)(m_pMapExtent.Width * dSacle);
            int nHeight = (int)(m_pMapExtent.Height * dSacle);
            
            rect.X = (int)(nX + pCenterDevPt.X); rect.Y =(int)(nY + pCenterDevPt.Y);
            if (rect.X < 0) rect.X = 0;
            if (rect.Y < 0) rect.Y = 0;
            if (nWidth + rect.X > this.ClientSize.Width) nWidth = this.ClientSize.Width - rect.X;
            if (nHeight + rect.Y > this.ClientSize.Height) nHeight = this.ClientSize.Height - rect.Y;
            rect.Width=nWidth; rect.Height = nHeight;
            return rect;
        }
        //��ӥ��ͼ�����㻻�㵽��ǰ��ͼ�����
        private IPoint EyeMap2Ctrl(POINT point)
        {
            IEnvelope pFullExtent = m_MapControl.FullExtent;
            IPoint pCenterLogPoint = new PointClass();
            pCenterLogPoint.X = (pFullExtent.XMin + pFullExtent.XMax) / 2;
            pCenterLogPoint.Y = (pFullExtent.YMin + pFullExtent.YMax) / 2;
            IPoint pCenterDevPt = new PointClass();
            pCenterDevPt.X = (this.ClientSize.Width) / 2;
            pCenterDevPt.Y = (this.ClientSize.Height) / 2;
            IPoint pPoint = new PointClass();
            Size rSize = new Size();
            rSize.Width = (int)(point.X - pCenterDevPt.X);
            rSize.Height = (int)(point.Y - pCenterDevPt.Y);
            SIZE dMapSize = MapExtent2Rect(rSize);
            pPoint.X = pCenterLogPoint.X + dMapSize.cX;
            pPoint.Y = pCenterLogPoint.Y - dMapSize.cY;
            return pPoint;
        }

        //��ӥ��ͼ������ƫ�ƻ��㵽��ǰ��ͼ����ƫ��
        private SIZE MapExtent2Rect(Size rSize)
        {
            SIZE dMapSize = new SIZE();
            IEnvelope pFullExtent = m_MapControl.FullExtent;
            double dxScale = (double)(this.ClientSize.Width) / (double)(pFullExtent.Width);
            double dyScale = (double)(this.ClientSize.Height) / (double)(pFullExtent.Height);
            double dSacle = Math.Min(dxScale, dyScale);
            dMapSize.cX = (double)rSize.Width / dSacle;
            dMapSize.cY = (double)rSize.Height / dSacle;
            return dMapSize;
        }

        //����ӥ��ͼ��
        private void DrawEyeMap(Graphics g)
        {
            if (m_pMapImage == null) return;
            PointF point = new PointF();
            point.X = 0; point.Y = 0;
            g.DrawImage(m_pMapImage, point);

            if (m_pMapExtent == null) return;
            
            Pen pPen = new Pen(Color.Red,2);
            g.DrawRectangle(pPen, m_rRect);
            //g.DrawRectangle(
        }

        //�ӵ�ͼ�ϻ��ӥ��ͼƬ
        private Image GetEyeImage(int nWidth, int hHeight)
        {
            if (m_MapControl == null) return null;
            if (m_MapControl.Map.LayerCount == 0) return null;
            IActiveView pActiveView = m_MapControl.ActiveView;
            if (pActiveView == null) return null;
            IExport pExport = new ExportBMPClass();

            tagRECT bmpFrame;
            bmpFrame.left = 0; bmpFrame.top = 0;
            bmpFrame.right = nWidth; bmpFrame.bottom = hHeight;
            int iOutputResolution = 20;
            pExport.Resolution = iOutputResolution;

            IEnvelope pPixelBoundsEnv = new EnvelopeClass();
            pPixelBoundsEnv.PutCoords(bmpFrame.left, bmpFrame.top, bmpFrame.right, bmpFrame.bottom);
            
            pExport.PixelBounds = pPixelBoundsEnv;
            tagRECT exportRECT = bmpFrame;
            //��ʼ��������ȡDC  
            int hDC = pExport.StartExporting();
            //����
            IEnvelope pMapEnvelope = pActiveView.FullExtent;
            pActiveView.Output(hDC, (int)pExport.Resolution, ref exportRECT, pMapEnvelope, null);
            //��������
            pExport.FinishExporting();
            IExportBMP pExportBmp = pExport as IExportBMP;
            System.Drawing.Image pImage= System.Drawing.Image.FromHbitmap((System.IntPtr)pExportBmp.Bitmap);
            //��������
            pExport.Cleanup();

            return pImage;
        }

        private void EyeMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_MapControl == null) return ;
            m_pStratPt.X = e.X; m_pStratPt.Y = e.Y;
            m_rStartRect = m_rRect;
        }

        private void EyeMap_MouseMove(object sender, MouseEventArgs e) 
        {
            if (m_MapControl == null) return;
            if (e.Button != MouseButtons.Left) return;
            Size size = new Size();
            size.Width = (int)(e.X - m_pStratPt.X);
            size.Height = (int)(e.Y - m_pStratPt.Y);
            m_rRect.X =m_rStartRect.X+ size.Width;
            m_rRect.Y =m_rStartRect.Y+size.Height;
            this.Refresh();
        }

        private void EyeMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_MapControl == null) return;
            if (e.Button != MouseButtons.Left) return;
            IEnvelope pEnvelope = m_MapControl.Extent;
            Size size = new Size();
            size.Width = (int)(e.X - m_pStratPt.X);
            size.Height = (int)(e.Y - m_pStratPt.Y);
            if (Math.Abs(size.Width) < 2 && Math.Abs(size.Height) < 2)//�������ƶ���Χ��С�����������λ
            {
                POINT pt = new POINT();
                pt.X = (int)e.X; pt.Y = (int)e.Y;
                IPoint point = EyeMap2Ctrl(pt);
                pEnvelope.CenterAt(point);
            }
            else//��Ӧ�϶�����¼�
            {
                POINT pt = new POINT();
                pt.X = (int)e.X; pt.Y = (int)e.Y;
                IPoint point = EyeMap2Ctrl(pt);
                pEnvelope.CenterAt(point);
                //SIZE dMapSize = MapExtent2Rect(size);
               // pEnvelope.Offset(dMapSize.cX, dMapSize.cY);
            }
            
            m_MapControl.Extent = pEnvelope;
        }


    }
}
