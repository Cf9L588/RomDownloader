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
using RomDownloader.Properties;

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
        }

        public RomViewerForm(GameInfo info)
        {
            InitializeComponent();
            this.info = info;
            
        }

        private void RomViewerForm_Load(object sender, EventArgs e)
        {
            
        }

        private async void RomViewerForm_Shown(object sender, EventArgs e)
        {
            if (info == null)
            {
                var getInfo = TheGamesDB.GetGame(romName, systemName);
                info = await getInfo;
                
            }
            var boxArt = info.Images.Where(img => img.Style == GameInfo.Image.ImageStyle.BoxArt).Select(img=> img.Url);
            lblRomTitle.Text = info.Title;

            if (boxArt.Any())
            {
                picBoxArt.Load(boxArt.First());
            }
            else
            {
                picBoxArt.Image = Resources.NoRomImg;
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RomViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
