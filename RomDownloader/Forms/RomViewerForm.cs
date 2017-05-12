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
using MetroFramework.Controls;
using System.Net;

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
            romName = info.Title;
            systemName = info.SystemName;
            this.info = info;
            
        }

        private void RomViewerForm_Load(object sender, EventArgs e)
        {
            
        }

        private async void RomViewerForm_Shown(object sender, EventArgs e)
        {
            if (info == null)
            {
                var getInfo = Core.GetGameInfo(romName, systemName);
                //var getInfo = TheGamesDB.GetGame(romName, systemName);
                info = await getInfo;
                
            }
            var boxArt = info.Images.Where(img => img.Style == GameInfo.Image.ImageStyle.BoxArt_Front)?.Select(img => img.Url)
                ?? info.Images.Where(img => img.Style == GameInfo.Image.ImageStyle.BoxArt_Back).Select(img => img.Url);
            var screenshots = info.Images.Where(img => img.Style == GameInfo.Image.ImageStyle.ScreenShot).Select(img => img.Url);

            lblRomTitle.Text = info.Title;
            lblOverview.Text = info.Overview;

            if (boxArt.Any())
            {
                picBoxArt.Load(boxArt.First());
            }
            else
            {
                picBoxArt.Image = Resources.NoRomImg;
            }
            if (screenshots.Any())
            {
                picScreenshot1.Load(screenshots.First());
            }
            if (info.CoOp != null)
            {
                MetroLabel lblCoOp = new MetroLabel();
                lblCoOp.AutoSize = true;
                lblCoOp.Text = "Co-Op: ";
                lblCoOp.Text += info.CoOp == true ? "yes": "no";
                flpRomDescriptions.Controls.Add(lblCoOp);
            }
            if (info.Genres.Count > 0)
            {
                MetroLabel lblGenres = new MetroLabel();
                lblGenres.AutoSize = true;
                lblGenres.Text = "Genres: ";
                lblGenres.Text += info.Genres[0];

                for (int i = 1; i < info.Genres.Count; i++)
                {
                    lblGenres.Text += $", {info.Genres[i]}";
                }
                flpRomDescriptions.Controls.Add(lblGenres);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            List<Rom> roms = Core.GetRomsByName(info.SystemName, info.Title);
            List<string> urls = roms.Select(rom => rom.DownloadUrl).ToList();

            

            using (var client = new WebClient())
            {
                client.DownloadFile(urls[0], urls[0].Split(new char[] { '/' }).Last());
            }
        }

        private void RomViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
