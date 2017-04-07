namespace RomDownloader.Forms
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
            this.lstRoms = new System.Windows.Forms.ListBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.msmStyler = new MetroFramework.Components.MetroStyleManager(this.components);
            this.prgLoadingRoms = new MetroFramework.Controls.MetroProgressSpinner();
            this.lblLoading = new MetroFramework.Controls.MetroLabel();
            this.cboSystems = new MetroFramework.Controls.MetroComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.msmStyler)).BeginInit();
            this.SuspendLayout();
            // 
            // lstRoms
            // 
            this.lstRoms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstRoms.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstRoms.FormattingEnabled = true;
            this.lstRoms.Location = new System.Drawing.Point(24, 128);
            this.lstRoms.Name = "lstRoms";
            this.lstRoms.Size = new System.Drawing.Size(297, 405);
            this.lstRoms.TabIndex = 12;
            this.lstRoms.SelectedIndexChanged += new System.EventHandler(this.lstRoms_SelectedIndexChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(58, 19);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Systems:";
            // 
            // msmStyler
            // 
            this.msmStyler.Owner = this;
            // 
            // prgLoadingRoms
            // 
            this.prgLoadingRoms.Location = new System.Drawing.Point(303, 63);
            this.prgLoadingRoms.Maximum = 100;
            this.prgLoadingRoms.Minimum = 1;
            this.prgLoadingRoms.Name = "prgLoadingRoms";
            this.prgLoadingRoms.Size = new System.Drawing.Size(16, 16);
            this.prgLoadingRoms.TabIndex = 11;
            this.prgLoadingRoms.UseSelectable = true;
            this.prgLoadingRoms.Value = 30;
            this.prgLoadingRoms.Visible = false;
            // 
            // lblLoading
            // 
            this.lblLoading.Location = new System.Drawing.Point(91, 60);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(206, 19);
            this.lblLoading.TabIndex = 10;
            this.lblLoading.Text = "Loading";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblLoading.Visible = false;
            // 
            // cboSystems
            // 
            this.cboSystems.FormattingEnabled = true;
            this.cboSystems.ItemHeight = 23;
            this.cboSystems.Location = new System.Drawing.Point(24, 82);
            this.cboSystems.Name = "cboSystems";
            this.cboSystems.Size = new System.Drawing.Size(297, 29);
            this.cboSystems.TabIndex = 8;
            this.cboSystems.UseSelectable = true;
            this.cboSystems.SelectedIndexChanged += new System.EventHandler(this.cboSystems_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 561);
            this.Controls.Add(this.lstRoms);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.prgLoadingRoms);
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.cboSystems);
            this.Name = "MainForm";
            this.Text = "Rom Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.msmStyler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstRoms;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Components.MetroStyleManager msmStyler;
        private MetroFramework.Controls.MetroProgressSpinner prgLoadingRoms;
        private MetroFramework.Controls.MetroLabel lblLoading;
        private MetroFramework.Controls.MetroComboBox cboSystems;
    }
}

