using GISBase.UserControls;

namespace GISBase.Forms
{
    partial class FrmSelectField
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
            this.cb_Fields = new AEFieldComboBox();
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_Fields
            // 
            this.cb_Fields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Fields.FormattingEnabled = true;
            this.cb_Fields.Location = new System.Drawing.Point(12, 34);
            this.cb_Fields.Name = "cb_Fields";
            this.cb_Fields.Size = new System.Drawing.Size(260, 20);
            this.cb_Fields.TabIndex = 0;
            // 
            // bt_OK
            // 
            this.bt_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bt_OK.Location = new System.Drawing.Point(50, 75);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Size = new System.Drawing.Size(75, 23);
            this.bt_OK.TabIndex = 1;
            this.bt_OK.Text = "Ok";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Cancel.Location = new System.Drawing.Point(159, 75);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(75, 23);
            this.bt_Cancel.TabIndex = 2;
            this.bt_Cancel.Text = "Cancel";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            // 
            // FrmSelectField
            // 
            this.AcceptButton = this.bt_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bt_Cancel;
            this.ClientSize = new System.Drawing.Size(284, 113);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_OK);
            this.Controls.Add(this.cb_Fields);
            this.Name = "FrmSelectField";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSelectField";
            this.Load += new System.EventHandler(this.FrmSelectField_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AEFieldComboBox cb_Fields;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button bt_Cancel;
    }
}