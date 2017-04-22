using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RomDownloader.Models;

namespace RomDownloader.Forms
{
    public partial class RomViewerForm : MetroForm
    {
        string romName, systemName;
        GameInfo info;

        public RomViewerForm(string romName, string systemName)
        {
            InitializeComponent();
            this.romName = romName;
            this.systemName = systemName;
            lblInfo.Text = "Information: " + romName;
        }

        public RomViewerForm(GameInfo info)
        {
            InitializeComponent();
            this.info = info;
            lblInfo.Text = "Information: " + info.Title;
        }

        private void RomViewerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private async void RomViewerForm_Shown(object sender, EventArgs e)
        {
            if (info == null)
            {
                var getInfo = TheGamesDB.GetGame(romName, systemName);
                info = await getInfo;
                lblInfo.Text = "Information: " + info?.Title ?? romName;
            }
        }

        private  void RomViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
