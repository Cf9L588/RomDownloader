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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.cboSystems = new MetroFramework.Controls.MetroComboBox();
            this.msmStyler = new MetroFramework.Components.MetroStyleManager(this.components);
            this.lblLoadingRoms = new MetroFramework.Controls.MetroLabel();
            this.prgLoadingRoms = new MetroFramework.Controls.MetroProgressSpinner();
            this.lstRoms = new System.Windows.Forms.ListBox();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msmStyler)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Location = new System.Drawing.Point(23, 63);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(774, 434);
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.lstRoms);
            this.metroTabPage1.Controls.Add(this.prgLoadingRoms);
            this.metroTabPage1.Controls.Add(this.lblLoadingRoms);
            this.metroTabPage1.Controls.Add(this.metroLabel1);
            this.metroTabPage1.Controls.Add(this.cboSystems);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(766, 392);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Home";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 11);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(58, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Systems:";
            // 
            // cboSystems
            // 
            this.cboSystems.FormattingEnabled = true;
            this.cboSystems.ItemHeight = 23;
            this.cboSystems.Location = new System.Drawing.Point(3, 33);
            this.cboSystems.Name = "cboSystems";
            this.cboSystems.Size = new System.Drawing.Size(247, 29);
            this.cboSystems.TabIndex = 2;
            this.cboSystems.UseSelectable = true;
            this.cboSystems.SelectedIndexChanged += new System.EventHandler(this.cboSystems_SelectedIndexChanged);
            // 
            // msmStyler
            // 
            this.msmStyler.Owner = this;
            // 
            // lblLoadingRoms
            // 
            this.lblLoadingRoms.AutoSize = true;
            this.lblLoadingRoms.Location = new System.Drawing.Point(269, 42);
            this.lblLoadingRoms.Name = "lblLoadingRoms";
            this.lblLoadingRoms.Size = new System.Drawing.Size(93, 19);
            this.lblLoadingRoms.TabIndex = 5;
            this.lblLoadingRoms.Text = "Loading Roms";
            this.lblLoadingRoms.Visible = false;
            // 
            // prgLoadingRoms
            // 
            this.prgLoadingRoms.Location = new System.Drawing.Point(368, 45);
            this.prgLoadingRoms.Maximum = 100;
            this.prgLoadingRoms.Name = "prgLoadingRoms";
            this.prgLoadingRoms.Size = new System.Drawing.Size(16, 16);
            this.prgLoadingRoms.TabIndex = 6;
            this.prgLoadingRoms.UseSelectable = true;
            this.prgLoadingRoms.Value = 30;
            this.prgLoadingRoms.Visible = false;
            // 
            // lstRoms
            // 
            this.lstRoms.FormattingEnabled = true;
            this.lstRoms.Location = new System.Drawing.Point(4, 79);
            this.lstRoms.Name = "lstRoms";
            this.lstRoms.Size = new System.Drawing.Size(380, 316);
            this.lstRoms.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 520);
            this.Controls.Add(this.metroTabControl1);
            this.Name = "MainForm";
            this.Text = "Rom Downloader";
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.msmStyler)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox cboSystems;
        private MetroFramework.Components.MetroStyleManager msmStyler;
        private MetroFramework.Controls.MetroProgressSpinner prgLoadingRoms;
        private MetroFramework.Controls.MetroLabel lblLoadingRoms;
        private System.Windows.Forms.ListBox lstRoms;
    }
}

