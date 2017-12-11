namespace GISBase
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
            this.ribbonControl2 = new DevComponents.DotNetBar.RibbonControl();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.OpenDoc_btn = new DevComponents.DotNetBar.ButtonItem();
            this.AddLayer_btn = new DevComponents.DotNetBar.ButtonItem();
            this.Save_btn = new DevComponents.DotNetBar.ButtonItem();
            this.ZoomIn_btn = new DevComponents.DotNetBar.ButtonItem();
            this.ratingItem1 = new DevComponents.DotNetBar.RatingItem();
            this.ZoomOut_btn = new DevComponents.DotNetBar.ButtonItem();
            this.Pan_btn = new DevComponents.DotNetBar.ButtonItem();
            this.Fullscope_btn = new DevComponents.DotNetBar.ButtonItem();
            this.Select_btn = new DevComponents.DotNetBar.ButtonItem();
            this.Identify_btn = new DevComponents.DotNetBar.ButtonItem();
            this.btn_SearchByAttr = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btn_SearchBySpace = new DevComponents.DotNetBar.ButtonItem();
            this.btn_UnSelect = new DevComponents.DotNetBar.ButtonItem();
            this.btn_DrawRect = new DevComponents.DotNetBar.ButtonItem();
            this.btn_Chart = new DevComponents.DotNetBar.ButtonItem();
            this.btn_Buffer = new DevComponents.DotNetBar.ButtonItem();
            this.btn_Test = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.qatCustomizeItem1 = new DevComponents.DotNetBar.QatCustomizeItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this.dotNetBarManager1 = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.statusBar = new DevComponents.DotNetBar.Bar();
            this.lbl_Coordinate = new DevComponents.DotNetBar.ButtonItem();
            this.progressBarItem1 = new DevComponents.DotNetBar.ProgressBarItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.TOCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TM_NewLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_DeleteLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TM_LableLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_ZoomToLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TM_OpenAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TM_NewGroupLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TB_CopyLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TB_PasteLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TOC_CutLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.TOC_SelectAllLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.eyeMap1 = new BaseLibs.EyeMap();
            this.ribbonControl2.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBar)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.TOCMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            // 
            // 
            // 
            this.ribbonControl1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.ribbonControl1.Size = new System.Drawing.Size(0, 0);
            this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl1.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.ribbonControl1.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.ribbonControl1.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.ribbonControl1.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.ribbonControl1.SystemText.QatDialogAddButton = "&Add >>";
            this.ribbonControl1.SystemText.QatDialogCancelButton = "Cancel";
            this.ribbonControl1.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.ribbonControl1.SystemText.QatDialogOkButton = "OK";
            this.ribbonControl1.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatDialogRemoveButton = "&Remove";
            this.ribbonControl1.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.ribbonControl1.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.ribbonControl1.TabGroupHeight = 14;
            this.ribbonControl1.TabIndex = 0;
            // 
            // ribbonControl2
            // 
            // 
            // 
            // 
            this.ribbonControl2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonControl2.CaptionVisible = true;
            this.ribbonControl2.Controls.Add(this.ribbonPanel1);
            this.ribbonControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonControl2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItem1});
            this.ribbonControl2.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.ribbonControl2.Location = new System.Drawing.Point(5, 1);
            this.ribbonControl2.Name = "ribbonControl2";
            this.ribbonControl2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.ribbonControl2.Size = new System.Drawing.Size(1031, 154);
            this.ribbonControl2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl2.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.ribbonControl2.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.ribbonControl2.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.ribbonControl2.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.ribbonControl2.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.ribbonControl2.SystemText.QatDialogAddButton = "&Add >>";
            this.ribbonControl2.SystemText.QatDialogCancelButton = "Cancel";
            this.ribbonControl2.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.ribbonControl2.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.ribbonControl2.SystemText.QatDialogOkButton = "OK";
            this.ribbonControl2.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl2.SystemText.QatDialogRemoveButton = "&Remove";
            this.ribbonControl2.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.ribbonControl2.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl2.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.ribbonControl2.TabGroupHeight = 14;
            this.ribbonControl2.TabIndex = 0;
            this.ribbonControl2.Text = "ribbonControl2";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.ribbonBar1);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 56);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(1031, 95);
            // 
            // 
            // 
            this.ribbonPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel1.TabIndex = 1;
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar1.ContainerControlProcessDialogKey = true;
            this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.OpenDoc_btn,
            this.AddLayer_btn,
            this.Save_btn,
            this.ZoomIn_btn,
            this.ZoomOut_btn,
            this.Pan_btn,
            this.Fullscope_btn,
            this.Select_btn,
            this.Identify_btn,
            this.btn_SearchByAttr,
            this.btn_SearchBySpace,
            this.btn_UnSelect,
            this.btn_DrawRect,
            this.btn_Chart,
            this.btn_Buffer,
            this.btn_Test});
            this.ribbonBar1.Location = new System.Drawing.Point(3, 0);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.Size = new System.Drawing.Size(1016, 92);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBar1.TabIndex = 0;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // OpenDoc_btn
            // 
            this.OpenDoc_btn.Image = ((System.Drawing.Image)(resources.GetObject("OpenDoc_btn.Image")));
            this.OpenDoc_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.OpenDoc_btn.Name = "OpenDoc_btn";
            this.OpenDoc_btn.SubItemsExpandWidth = 14;
            this.OpenDoc_btn.Text = "打开地图";
            this.OpenDoc_btn.Click += new System.EventHandler(this.OpenDoc_btn_Click);
            // 
            // AddLayer_btn
            // 
            this.AddLayer_btn.Image = ((System.Drawing.Image)(resources.GetObject("AddLayer_btn.Image")));
            this.AddLayer_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.AddLayer_btn.Name = "AddLayer_btn";
            this.AddLayer_btn.SubItemsExpandWidth = 14;
            this.AddLayer_btn.Text = "加载图层";
            this.AddLayer_btn.Click += new System.EventHandler(this.AddLayer_btn_Click);
            // 
            // Save_btn
            // 
            this.Save_btn.Image = ((System.Drawing.Image)(resources.GetObject("Save_btn.Image")));
            this.Save_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.SubItemsExpandWidth = 14;
            this.Save_btn.Text = "保存地图";
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // ZoomIn_btn
            // 
            this.ZoomIn_btn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomIn_btn.Image")));
            this.ZoomIn_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.ZoomIn_btn.Name = "ZoomIn_btn";
            this.ZoomIn_btn.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ratingItem1});
            this.ZoomIn_btn.SubItemsExpandWidth = 14;
            this.ZoomIn_btn.Text = "地图放大";
            this.ZoomIn_btn.Click += new System.EventHandler(this.ZoomIn_btn_Click);
            // 
            // ratingItem1
            // 
            // 
            // 
            // 
            this.ratingItem1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ratingItem1.Name = "ratingItem1";
            // 
            // ZoomOut_btn
            // 
            this.ZoomOut_btn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOut_btn.Image")));
            this.ZoomOut_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.ZoomOut_btn.Name = "ZoomOut_btn";
            this.ZoomOut_btn.SubItemsExpandWidth = 14;
            this.ZoomOut_btn.Text = "地图缩小";
            this.ZoomOut_btn.Click += new System.EventHandler(this.ZoomOut_btn_Click);
            // 
            // Pan_btn
            // 
            this.Pan_btn.Image = ((System.Drawing.Image)(resources.GetObject("Pan_btn.Image")));
            this.Pan_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.Pan_btn.Name = "Pan_btn";
            this.Pan_btn.SubItemsExpandWidth = 14;
            this.Pan_btn.Text = "地图移动";
            this.Pan_btn.Click += new System.EventHandler(this.Pan_btn_Click);
            // 
            // Fullscope_btn
            // 
            this.Fullscope_btn.Image = ((System.Drawing.Image)(resources.GetObject("Fullscope_btn.Image")));
            this.Fullscope_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.Fullscope_btn.Name = "Fullscope_btn";
            this.Fullscope_btn.SubItemsExpandWidth = 14;
            this.Fullscope_btn.Text = "全图显示";
            this.Fullscope_btn.Click += new System.EventHandler(this.Fullscope_btn_Click);
            // 
            // Select_btn
            // 
            this.Select_btn.Image = ((System.Drawing.Image)(resources.GetObject("Select_btn.Image")));
            this.Select_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.Select_btn.Name = "Select_btn";
            this.Select_btn.SubItemsExpandWidth = 14;
            this.Select_btn.Text = "选择目标";
            this.Select_btn.Click += new System.EventHandler(this.Select_btn_Click);
            // 
            // Identify_btn
            // 
            this.Identify_btn.Image = ((System.Drawing.Image)(resources.GetObject("Identify_btn.Image")));
            this.Identify_btn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.Identify_btn.Name = "Identify_btn";
            this.Identify_btn.SubItemsExpandWidth = 14;
            this.Identify_btn.Text = "标识目标";
            this.Identify_btn.Click += new System.EventHandler(this.Identify_btn_Click);
            // 
            // btn_SearchByAttr
            // 
            this.btn_SearchByAttr.Image = ((System.Drawing.Image)(resources.GetObject("btn_SearchByAttr.Image")));
            this.btn_SearchByAttr.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_SearchByAttr.Name = "btn_SearchByAttr";
            this.btn_SearchByAttr.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.btn_SearchByAttr.SubItemsExpandWidth = 14;
            this.btn_SearchByAttr.Text = "属性查询";
            this.btn_SearchByAttr.Click += new System.EventHandler(this.btn_SearchByAttr_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // btn_SearchBySpace
            // 
            this.btn_SearchBySpace.Image = ((System.Drawing.Image)(resources.GetObject("btn_SearchBySpace.Image")));
            this.btn_SearchBySpace.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_SearchBySpace.Name = "btn_SearchBySpace";
            this.btn_SearchBySpace.SubItemsExpandWidth = 14;
            this.btn_SearchBySpace.Text = "空间查询";
            this.btn_SearchBySpace.Click += new System.EventHandler(this.btn_SearchBySpace_Click);
            // 
            // btn_UnSelect
            // 
            this.btn_UnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btn_UnSelect.Image")));
            this.btn_UnSelect.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_UnSelect.Name = "btn_UnSelect";
            this.btn_UnSelect.SubItemsExpandWidth = 14;
            this.btn_UnSelect.Text = "清除选择";
            this.btn_UnSelect.Click += new System.EventHandler(this.btn_UnSelect_Click);
            // 
            // btn_DrawRect
            // 
            this.btn_DrawRect.Image = ((System.Drawing.Image)(resources.GetObject("btn_DrawRect.Image")));
            this.btn_DrawRect.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_DrawRect.Name = "btn_DrawRect";
            this.btn_DrawRect.SubItemsExpandWidth = 14;
            this.btn_DrawRect.Text = "绘制矩形";
            this.btn_DrawRect.Click += new System.EventHandler(this.btn_DrawRect_Click);
            // 
            // btn_Chart
            // 
            this.btn_Chart.Image = ((System.Drawing.Image)(resources.GetObject("btn_Chart.Image")));
            this.btn_Chart.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_Chart.Name = "btn_Chart";
            this.btn_Chart.SubItemsExpandWidth = 14;
            this.btn_Chart.Text = "专题图表";
            this.btn_Chart.Click += new System.EventHandler(this.btn_Chart_Click);
            // 
            // btn_Buffer
            // 
            this.btn_Buffer.Image = ((System.Drawing.Image)(resources.GetObject("btn_Buffer.Image")));
            this.btn_Buffer.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_Buffer.Name = "btn_Buffer";
            this.btn_Buffer.SubItemsExpandWidth = 14;
            this.btn_Buffer.Text = "缓冲区";
            this.btn_Buffer.Click += new System.EventHandler(this.btn_Buffer_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Image = global::GISBase.Properties.Resources.gps;
            this.btn_Test.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.SubItemsExpandWidth = 14;
            this.btn_Test.Text = "试验";
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Checked = true;
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "操作";
            // 
            // qatCustomizeItem1
            // 
            this.qatCustomizeItem1.Name = "qatCustomizeItem1";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite6.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite6.Location = new System.Drawing.Point(1036, 1);
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.Size = new System.Drawing.Size(0, 602);
            this.dockSite6.TabIndex = 6;
            this.dockSite6.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite5.Location = new System.Drawing.Point(5, 1);
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.Size = new System.Drawing.Size(0, 602);
            this.dockSite5.TabIndex = 5;
            this.dockSite5.TabStop = false;
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite8.Location = new System.Drawing.Point(5, 603);
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.Size = new System.Drawing.Size(1031, 0);
            this.dockSite8.TabIndex = 8;
            this.dockSite8.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite7.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite7.Location = new System.Drawing.Point(5, 1);
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.Size = new System.Drawing.Size(1031, 0);
            this.dockSite7.TabIndex = 7;
            this.dockSite7.TabStop = false;
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite2.Dock = System.Windows.Forms.DockStyle.Right;
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite2.Location = new System.Drawing.Point(1036, 155);
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.Size = new System.Drawing.Size(0, 448);
            this.dockSite2.TabIndex = 2;
            this.dockSite2.TabStop = false;
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite1.Location = new System.Drawing.Point(5, 155);
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.Size = new System.Drawing.Size(0, 448);
            this.dockSite1.TabIndex = 1;
            this.dockSite1.TabStop = false;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite4.Location = new System.Drawing.Point(5, 603);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.Size = new System.Drawing.Size(1031, 0);
            this.dockSite4.TabIndex = 4;
            this.dockSite4.TabStop = false;
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite3.Dock = System.Windows.Forms.DockStyle.Top;
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Location = new System.Drawing.Point(5, 1);
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.Size = new System.Drawing.Size(1031, 0);
            this.dockSite3.TabIndex = 3;
            this.dockSite3.TabStop = false;
            // 
            // dotNetBarManager1
            // 
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManager1.BottomDockSite = this.dockSite4;
            this.dotNetBarManager1.EnableFullSizeDock = false;
            this.dotNetBarManager1.LeftDockSite = this.dockSite1;
            this.dotNetBarManager1.ParentForm = this;
            this.dotNetBarManager1.RightDockSite = this.dockSite2;
            this.dotNetBarManager1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.dotNetBarManager1.ToolbarBottomDockSite = this.dockSite8;
            this.dotNetBarManager1.ToolbarLeftDockSite = this.dockSite5;
            this.dotNetBarManager1.ToolbarRightDockSite = this.dockSite6;
            this.dotNetBarManager1.ToolbarTopDockSite = this.dockSite7;
            this.dotNetBarManager1.TopDockSite = this.dockSite3;
            // 
            // statusBar
            // 
            this.statusBar.AntiAlias = true;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.statusBar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lbl_Coordinate,
            this.progressBarItem1});
            this.statusBar.Location = new System.Drawing.Point(5, 576);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1031, 27);
            this.statusBar.Stretch = true;
            this.statusBar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.statusBar.TabIndex = 9;
            this.statusBar.TabStop = false;
            this.statusBar.Text = "bar1";
            // 
            // lbl_Coordinate
            // 
            this.lbl_Coordinate.Name = "lbl_Coordinate";
            this.lbl_Coordinate.Text = "当前坐标";
            // 
            // progressBarItem1
            // 
            // 
            // 
            // 
            this.progressBarItem1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progressBarItem1.ChunkGradientAngle = 0F;
            this.progressBarItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.progressBarItem1.Name = "progressBarItem1";
            this.progressBarItem1.RecentlyUsed = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(5, 155);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1031, 421);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 10;
            this.panelEx1.Text = "panelEx1";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.axLicenseControl1);
            this.panelEx3.Controls.Add(this.axMapControl1);
            this.panelEx3.Controls.Add(this.expandableSplitter1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(244, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(787, 421);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            this.panelEx3.Text = "panelEx3";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(140, 86);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 2;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(10, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(777, 421);
            this.axMapControl1.TabIndex = 1;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.ExpandableControl = this.panelEx2;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(10, 421);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 0;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.eyeMap1);
            this.panelEx2.Controls.Add(this.axTOCControl1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(244, 421);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            this.panelEx2.Text = "panelEx2";
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(244, 369);
            this.axTOCControl1.TabIndex = 0;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            // 
            // TOCMenu
            // 
            this.TOCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TM_NewLayer,
            this.TM_DeleteLayer,
            this.toolStripSeparator1,
            this.TM_LableLayer,
            this.TM_ZoomToLayer,
            this.TM_OpenAttribute,
            this.toolStripSeparator2,
            this.TM_NewGroupLayer,
            this.toolStripSeparator3,
            this.TB_CopyLayer,
            this.TB_PasteLayer,
            this.TOC_CutLayer,
            this.TOC_SelectAllLayer});
            this.TOCMenu.Name = "CM_TocLayerRightMenu";
            this.TOCMenu.Size = new System.Drawing.Size(149, 242);
            // 
            // TM_NewLayer
            // 
            this.TM_NewLayer.Name = "TM_NewLayer";
            this.TM_NewLayer.Size = new System.Drawing.Size(148, 22);
            this.TM_NewLayer.Text = "增加图层";
            this.TM_NewLayer.Click += new System.EventHandler(this.TM_NewLayer_Click);
            // 
            // TM_DeleteLayer
            // 
            this.TM_DeleteLayer.Image = ((System.Drawing.Image)(resources.GetObject("TM_DeleteLayer.Image")));
            this.TM_DeleteLayer.ImageTransparentColor = System.Drawing.SystemColors.ButtonFace;
            this.TM_DeleteLayer.Name = "TM_DeleteLayer";
            this.TM_DeleteLayer.Size = new System.Drawing.Size(148, 22);
            this.TM_DeleteLayer.Text = "删除图层";
            this.TM_DeleteLayer.Click += new System.EventHandler(this.TM_DeleteLayer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // TM_LableLayer
            // 
            this.TM_LableLayer.Name = "TM_LableLayer";
            this.TM_LableLayer.Size = new System.Drawing.Size(148, 22);
            this.TM_LableLayer.Text = "标注图层";
            this.TM_LableLayer.Click += new System.EventHandler(this.TM_LableLayer_Click);
            // 
            // TM_ZoomToLayer
            // 
            this.TM_ZoomToLayer.Image = ((System.Drawing.Image)(resources.GetObject("TM_ZoomToLayer.Image")));
            this.TM_ZoomToLayer.ImageTransparentColor = System.Drawing.SystemColors.ButtonFace;
            this.TM_ZoomToLayer.Name = "TM_ZoomToLayer";
            this.TM_ZoomToLayer.Size = new System.Drawing.Size(148, 22);
            this.TM_ZoomToLayer.Text = "放大到图层";
            this.TM_ZoomToLayer.Click += new System.EventHandler(this.TM_ZoomToLayer_Click);
            // 
            // TM_OpenAttribute
            // 
            this.TM_OpenAttribute.Image = ((System.Drawing.Image)(resources.GetObject("TM_OpenAttribute.Image")));
            this.TM_OpenAttribute.ImageTransparentColor = System.Drawing.SystemColors.ButtonFace;
            this.TM_OpenAttribute.Name = "TM_OpenAttribute";
            this.TM_OpenAttribute.Size = new System.Drawing.Size(148, 22);
            this.TM_OpenAttribute.Text = "打开属性表";
            this.TM_OpenAttribute.Click += new System.EventHandler(this.TM_OpenAttribute_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // TM_NewGroupLayer
            // 
            this.TM_NewGroupLayer.Name = "TM_NewGroupLayer";
            this.TM_NewGroupLayer.Size = new System.Drawing.Size(148, 22);
            this.TM_NewGroupLayer.Text = "新建图组";
            this.TM_NewGroupLayer.Click += new System.EventHandler(this.TM_NewGroupLayer_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // TB_CopyLayer
            // 
            this.TB_CopyLayer.Name = "TB_CopyLayer";
            this.TB_CopyLayer.Size = new System.Drawing.Size(148, 22);
            this.TB_CopyLayer.Text = "拷贝图层";
            this.TB_CopyLayer.Click += new System.EventHandler(this.TB_CopyLayer_Click);
            // 
            // TB_PasteLayer
            // 
            this.TB_PasteLayer.Name = "TB_PasteLayer";
            this.TB_PasteLayer.Size = new System.Drawing.Size(148, 22);
            this.TB_PasteLayer.Text = "粘贴图层";
            this.TB_PasteLayer.Click += new System.EventHandler(this.TB_PasteLayer_Click);
            // 
            // TOC_CutLayer
            // 
            this.TOC_CutLayer.Name = "TOC_CutLayer";
            this.TOC_CutLayer.Size = new System.Drawing.Size(148, 22);
            this.TOC_CutLayer.Text = "剪切图层";
            // 
            // TOC_SelectAllLayer
            // 
            this.TOC_SelectAllLayer.Name = "TOC_SelectAllLayer";
            this.TOC_SelectAllLayer.Size = new System.Drawing.Size(148, 22);
            this.TOC_SelectAllLayer.Text = "选择所有图层";
            this.TOC_SelectAllLayer.Click += new System.EventHandler(this.TOC_SelectAllLayer_Click);
            // 
            // eyeMap1
            // 
            this.eyeMap1.BackColor = System.Drawing.Color.White;
            this.eyeMap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eyeMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eyeMap1.Location = new System.Drawing.Point(0, 369);
            this.eyeMap1.Name = "eyeMap1";
            this.eyeMap1.Size = new System.Drawing.Size(244, 52);
            this.eyeMap1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 605);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this.ribbonControl2);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "C# + AE 开发试验系统 V1.0";
            this.TopLeftCornerSize = 8;
            this.TopRightCornerSize = 8;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ribbonControl2.ResumeLayout(false);
            this.ribbonControl2.PerformLayout();
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBar)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.TOCMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl ribbonControl1;
        private DevComponents.DotNetBar.RibbonControl ribbonControl2;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.DotNetBar.QatCustomizeItem qatCustomizeItem1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite2;
        private DevComponents.DotNetBar.DockSite dockSite1;
        private DevComponents.DotNetBar.DockSite dockSite4;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;
        private DevComponents.DotNetBar.ButtonItem OpenDoc_btn;
        private DevComponents.DotNetBar.ButtonItem ZoomIn_btn;
        private DevComponents.DotNetBar.ButtonItem ZoomOut_btn;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Bar statusBar;
        private DevComponents.DotNetBar.ButtonItem lbl_Coordinate;
        private DevComponents.DotNetBar.ProgressBarItem progressBarItem1;
        private DevComponents.DotNetBar.ButtonItem AddLayer_btn;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private BaseLibs.EyeMap eyeMap1;
        private DevComponents.DotNetBar.RatingItem ratingItem1;
        private DevComponents.DotNetBar.ButtonItem Pan_btn;
        private DevComponents.DotNetBar.ButtonItem Fullscope_btn;
        private DevComponents.DotNetBar.ButtonItem Save_btn;
        private DevComponents.DotNetBar.ButtonItem Identify_btn;
        private DevComponents.DotNetBar.ButtonItem Select_btn;
        private System.Windows.Forms.ContextMenuStrip TOCMenu;
        private System.Windows.Forms.ToolStripMenuItem TM_DeleteLayer;
        private System.Windows.Forms.ToolStripMenuItem TM_ZoomToLayer;
        private System.Windows.Forms.ToolStripMenuItem TM_OpenAttribute;
        private System.Windows.Forms.ToolStripMenuItem TM_NewGroupLayer;
        private System.Windows.Forms.ToolStripMenuItem TB_CopyLayer;
        private System.Windows.Forms.ToolStripMenuItem TB_PasteLayer;
        private System.Windows.Forms.ToolStripMenuItem TOC_CutLayer;
        private System.Windows.Forms.ToolStripMenuItem TOC_SelectAllLayer;
        private System.Windows.Forms.ToolStripMenuItem TM_NewLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TM_LableLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private DevComponents.DotNetBar.ButtonItem btn_SearchByAttr;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btn_SearchBySpace;
        private DevComponents.DotNetBar.ButtonItem btn_UnSelect;
        private DevComponents.DotNetBar.ButtonItem btn_DrawRect;
        private DevComponents.DotNetBar.ButtonItem btn_Chart;
        private DevComponents.DotNetBar.ButtonItem btn_Buffer;
        private DevComponents.DotNetBar.ButtonItem btn_Test;

    }
}