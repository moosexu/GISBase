namespace GISBase
{
    partial class FrmSymbolSelector
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSymbolSelector));
            this.axSymbologyControl = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.ptbPreview = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblOutlineColor = new System.Windows.Forms.Label();
            this.btnOutlineColor = new System.Windows.Forms.Button();
            this.lblAngle = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.nudAngle = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoreSymbols = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStripMoreSymbol = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            this.SuspendLayout();
            // 
            // axSymbologyControl
            // 
            this.axSymbologyControl.Location = new System.Drawing.Point(12, 12);
            this.axSymbologyControl.Name = "axSymbologyControl";
            this.axSymbologyControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl.OcxState")));
            this.axSymbologyControl.Size = new System.Drawing.Size(333, 444);
            this.axSymbologyControl.TabIndex = 0;
            this.axSymbologyControl.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.axSymbologyControl_OnDoubleClick);
            this.axSymbologyControl.OnStyleClassChanged += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnStyleClassChangedEventHandler(this.axSymbologyControl_OnStyleClassChanged);
            this.axSymbologyControl.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl_OnItemSelected);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(494, 450);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // ptbPreview
            // 
            this.ptbPreview.Location = new System.Drawing.Point(6, 11);
            this.ptbPreview.Name = "ptbPreview";
            this.ptbPreview.Size = new System.Drawing.Size(150, 102);
            this.ptbPreview.TabIndex = 2;
            this.ptbPreview.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ptbPreview);
            this.groupBox1.Location = new System.Drawing.Point(351, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 118);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.lblOutlineColor);
            this.groupBox2.Controls.Add(this.btnOutlineColor);
            this.groupBox2.Controls.Add(this.lblAngle);
            this.groupBox2.Controls.Add(this.lblWidth);
            this.groupBox2.Controls.Add(this.lblSize);
            this.groupBox2.Controls.Add(this.nudAngle);
            this.groupBox2.Controls.Add(this.nudWidth);
            this.groupBox2.Controls.Add(this.nudSize);
            this.groupBox2.Controls.Add(this.lblColor);
            this.groupBox2.Controls.Add(this.btnColor);
            this.groupBox2.Location = new System.Drawing.Point(351, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 246);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "Transparency:";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(4, 198);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(110, 45);
            this.trackBar1.TabIndex = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblOutlineColor
            // 
            this.lblOutlineColor.AutoSize = true;
            this.lblOutlineColor.Location = new System.Drawing.Point(7, 141);
            this.lblOutlineColor.Name = "lblOutlineColor";
            this.lblOutlineColor.Size = new System.Drawing.Size(53, 12);
            this.lblOutlineColor.TabIndex = 9;
            this.lblOutlineColor.Text = "Outline:";
            // 
            // btnOutlineColor
            // 
            this.btnOutlineColor.Location = new System.Drawing.Point(65, 136);
            this.btnOutlineColor.Name = "btnOutlineColor";
            this.btnOutlineColor.Size = new System.Drawing.Size(55, 24);
            this.btnOutlineColor.TabIndex = 8;
            this.btnOutlineColor.UseVisualStyleBackColor = true;
            this.btnOutlineColor.Click += new System.EventHandler(this.btnOutlineColor_Click);
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(19, 111);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(41, 12);
            this.lblAngle.TabIndex = 7;
            this.lblAngle.Text = "Angle:";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(19, 82);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(41, 12);
            this.lblWidth.TabIndex = 6;
            this.lblWidth.Text = "Width:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(25, 53);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(35, 12);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "Size:";
            // 
            // nudAngle
            // 
            this.nudAngle.Location = new System.Drawing.Point(66, 109);
            this.nudAngle.Name = "nudAngle";
            this.nudAngle.Size = new System.Drawing.Size(65, 21);
            this.nudAngle.TabIndex = 4;
            this.nudAngle.ValueChanged += new System.EventHandler(this.nudAngle_ValueChanged);
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(66, 80);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(65, 21);
            this.nudWidth.TabIndex = 3;
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // nudSize
            // 
            this.nudSize.Location = new System.Drawing.Point(66, 51);
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(65, 21);
            this.nudSize.TabIndex = 2;
            this.nudSize.Value = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(19, 20);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(41, 12);
            this.lblColor.TabIndex = 1;
            this.lblColor.Text = "Color:";
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(66, 13);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(55, 24);
            this.btnColor.TabIndex = 0;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(379, 426);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(55, 24);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(452, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMoreSymbols
            // 
            this.btnMoreSymbols.Location = new System.Drawing.Point(407, 390);
            this.btnMoreSymbols.Name = "btnMoreSymbols";
            this.btnMoreSymbols.Size = new System.Drawing.Size(71, 24);
            this.btnMoreSymbols.TabIndex = 11;
            this.btnMoreSymbols.Text = "More";
            this.btnMoreSymbols.UseVisualStyleBackColor = true;
            this.btnMoreSymbols.Click += new System.EventHandler(this.btnMoreSymbols_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "ESRI Style Set 文件 (*.ServerStyle)|*.ServerStyle";
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Title = "选择ESRI Style Set文件";
            // 
            // contextMenuStripMoreSymbol
            // 
            this.contextMenuStripMoreSymbol.Name = "contextMenuStripMoreSymbol";
            this.contextMenuStripMoreSymbol.Size = new System.Drawing.Size(61, 4);
            // 
            // FrmSymbolSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 480);
            this.Controls.Add(this.btnMoreSymbols);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axSymbologyControl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSymbolSelector";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择符号";
            this.Load += new System.EventHandler(this.SymbolSelectorFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.PictureBox ptbPreview;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblOutlineColor;
        private System.Windows.Forms.Button btnOutlineColor;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.NumericUpDown nudAngle;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoreSymbols;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMoreSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
    }
}