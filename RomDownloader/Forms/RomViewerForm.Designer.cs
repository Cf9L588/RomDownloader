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
            this.lblRomTitle = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxArt)).BeginInit();
            this.pnlHeader.SuspendLayout();
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
            this.pnlHeader.Controls.Add(this.lblRomTitle);
            this.pnlHeader.Controls.Add(this.picBoxArt);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHeader.HorizontalScrollbarBarColor = true;
            this.pnlHeader.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlHeader.HorizontalScrollbarSize = 10;
            this.pnlHeader.Location = new System.Drawing.Point(20, 60);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(473, 460);
            this.pnlHeader.Style = MetroFramework.MetroColorStyle.Black;
            this.pnlHeader.TabIndex = 1;
            this.pnlHeader.VerticalScrollbarBarColor = true;
            this.pnlHeader.VerticalScrollbarHighlightOnWheel = false;
            this.pnlHeader.VerticalScrollbarSize = 10;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
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
            // RomViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 540);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RomViewerForm";
            this.Text = "RomViewerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RomViewerForm_FormClosing);
            this.Load += new System.EventHandler(this.RomViewerForm_Load);
            this.Shown += new System.EventHandler(this.RomViewerForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxArt)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxArt;
        private MetroFramework.Controls.MetroPanel pnlHeader;
        private MetroFramework.Controls.MetroLabel lblRomTitle;
    }
}