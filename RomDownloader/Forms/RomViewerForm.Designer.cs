namespace RomDownloader.Forms
{
    partial class RomViewerForm
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
            this.picBoxArt = new System.Windows.Forms.PictureBox();
            this.pnlHeader = new MetroFramework.Controls.MetroPanel();
            this.lblOverview = new MetroFramework.Controls.MetroLabel();
            this.lblRomTitle = new MetroFramework.Controls.MetroLabel();
            this.pnlScreenshots = new MetroFramework.Controls.MetroPanel();
            this.picScreenshot1 = new System.Windows.Forms.PictureBox();
            this.flpRomDescriptions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDownload = new MetroFramework.Controls.MetroTile();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxArt)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlScreenshots.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot1)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxArt
            // 
            this.picBoxArt.BackColor = System.Drawing.Color.White;
            this.picBoxArt.Location = new System.Drawing.Point(0, 62);
            this.picBoxArt.Name = "picBoxArt";
            this.picBoxArt.Size = new System.Drawing.Size(473, 398);
            this.picBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxArt.TabIndex = 0;
            this.picBoxArt.TabStop = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblOverview);
            this.pnlHeader.Controls.Add(this.lblRomTitle);
            this.pnlHeader.Controls.Add(this.picBoxArt);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHeader.HorizontalScrollbarBarColor = true;
            this.pnlHeader.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlHeader.HorizontalScrollbarSize = 10;
            this.pnlHeader.Location = new System.Drawing.Point(20, 60);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(473, 775);
            this.pnlHeader.Style = MetroFramework.MetroColorStyle.Black;
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.VerticalScrollbarBarColor = true;
            this.pnlHeader.VerticalScrollbarHighlightOnWheel = false;
            this.pnlHeader.VerticalScrollbarSize = 10;
            // 
            // lblOverview
            // 
            this.lblOverview.Location = new System.Drawing.Point(0, 463);
            this.lblOverview.Name = "lblOverview";
            this.lblOverview.Size = new System.Drawing.Size(473, 312);
            this.lblOverview.TabIndex = 3;
            this.lblOverview.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblOverview.WrapToLine = true;
            // 
            // lblRomTitle
            // 
            this.lblRomTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblRomTitle.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lblRomTitle.Location = new System.Drawing.Point(0, 9);
            this.lblRomTitle.Name = "lblRomTitle";
            this.lblRomTitle.Size = new System.Drawing.Size(473, 50);
            this.lblRomTitle.TabIndex = 2;
            this.lblRomTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlScreenshots
            // 
            this.pnlScreenshots.Controls.Add(this.picScreenshot1);
            this.pnlScreenshots.HorizontalScrollbarBarColor = true;
            this.pnlScreenshots.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlScreenshots.HorizontalScrollbarSize = 10;
            this.pnlScreenshots.Location = new System.Drawing.Point(499, 523);
            this.pnlScreenshots.Name = "pnlScreenshots";
            this.pnlScreenshots.Size = new System.Drawing.Size(690, 309);
            this.pnlScreenshots.TabIndex = 2;
            this.pnlScreenshots.VerticalScrollbarBarColor = true;
            this.pnlScreenshots.VerticalScrollbarHighlightOnWheel = false;
            this.pnlScreenshots.VerticalScrollbarSize = 10;
            // 
            // picScreenshot1
            // 
            this.picScreenshot1.Location = new System.Drawing.Point(0, 0);
            this.picScreenshot1.Name = "picScreenshot1";
            this.picScreenshot1.Size = new System.Drawing.Size(316, 312);
            this.picScreenshot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picScreenshot1.TabIndex = 2;
            this.picScreenshot1.TabStop = false;
            // 
            // flpRomDescriptions
            // 
            this.flpRomDescriptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpRomDescriptions.Location = new System.Drawing.Point(529, 122);
            this.flpRomDescriptions.Name = "flpRomDescriptions";
            this.flpRomDescriptions.Size = new System.Drawing.Size(660, 315);
            this.flpRomDescriptions.TabIndex = 3;
            // 
            // btnDownload
            // 
            this.btnDownload.ActiveControl = null;
            this.btnDownload.Location = new System.Drawing.Point(730, 459);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(85, 36);
            this.btnDownload.TabIndex = 5;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseSelectable = true;
            // 
            // RomViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 855);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.flpRomDescriptions);
            this.Controls.Add(this.pnlScreenshots);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RomViewerForm";
            this.Text = "RomViewerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RomViewerForm_FormClosing);
            this.Load += new System.EventHandler(this.RomViewerForm_Load);
            this.Shown += new System.EventHandler(this.RomViewerForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxArt)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlScreenshots.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picScreenshot1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxArt;
        private MetroFramework.Controls.MetroPanel pnlHeader;
        private MetroFramework.Controls.MetroLabel lblRomTitle;
        private MetroFramework.Controls.MetroLabel lblOverview;
        private MetroFramework.Controls.MetroPanel pnlScreenshots;
        private System.Windows.Forms.PictureBox picScreenshot1;
        private System.Windows.Forms.FlowLayoutPanel flpRomDescriptions;
        private MetroFramework.Controls.MetroTile btnDownload;
    }
}