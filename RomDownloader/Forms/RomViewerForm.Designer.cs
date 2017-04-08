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
            this.btnDone = new MetroFramework.Controls.MetroTile();
            this.lblInfo = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.ActiveControl = null;
            this.btnDone.Location = new System.Drawing.Point(23, 215);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 0;
            this.btnDone.Text = "Done!";
            this.btnDone.UseSelectable = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(24, 74);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(80, 38);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Information:\r\n";
            // 
            // RomViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnDone);
            this.Name = "RomViewerForm";
            this.Text = "RomViewerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RomViewerForm_FormClosing);
            this.Load += new System.EventHandler(this.RomViewerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTile btnDone;
        private MetroFramework.Controls.MetroLabel lblInfo;
    }
}