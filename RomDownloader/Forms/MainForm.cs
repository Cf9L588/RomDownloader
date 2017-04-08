using MetroFramework.Forms;
using RomDownloader.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RomDownloader.Forms
{
    public partial class MainForm : MetroForm
    {
        List<string> romList;

        public MainForm()
        {
            romList = new List<string>();
            InitializeComponent();
            this.StyleManager = msmStyler;
            msmStyler.Theme = MetroFramework.MetroThemeStyle.Dark;
            msmStyler.Style = MetroFramework.MetroColorStyle.Purple;
        }

        /// <summary>
        /// Handles what happens when a new dropdown selection is made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cboSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstRoms.Items.Clear();
            this.Refresh();

            lblLoading.Text = $"Loading {cboSystems.SelectedItem as string} Roms";
            lblLoading.Visible = true;
            prgLoadingRoms.Visible = true;
            cboSystems.Enabled = false;
            var gettingTask = Core.GetSystemRomsAsync(cboSystems.SelectedItem as string);
            foreach (var rom in await gettingTask)
            {
                lstRoms.Items.Add(rom);
            }

            lblLoading.Visible = false;
            prgLoadingRoms.Visible = false;
            cboSystems.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private async void lstRoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lstRoms.SelectedIndex;
            
            if (index != -1)
            {
                btnOpenRomInfo.Visible = true;
            }
            else
            {
                btnOpenRomInfo.Visible = false;
            }
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            lblLoading.Text = "Loading Platforms";
            lblLoading.Visible = true;
            prgLoadingRoms.Visible = true;
            cboSystems.Enabled = false;
            await Core.GetSystemsListAsync();
            // Bind the list of all systems to the dropdown box
            romList = Core.GetSystemNames();
            lblLoading.Visible = false;
            prgLoadingRoms.Visible = false;
            cboSystems.Enabled = true;
            cboSystems.DataSource = romList;
        }

        private void btnOpenRomInfo_Click(object sender, EventArgs e)
        {
            NewFormHandler();
        }

        private async void NewFormHandler()
        {
            btnOpenRomInfo.Enabled = false;
            var getInfo = TheGamesDB.GetGame(lstRoms.SelectedItem as string, cboSystems.SelectedItem as string);
            GameInfo info = await getInfo;
            this.Hide();
            RomViewerForm InfoViewer = new RomViewerForm(info);
            InfoViewer.FormClosing += (o, e) => this.Show();
            InfoViewer.FormClosing += this.EnableButton;
            InfoViewer.Show();
        }
        private void EnableButton (object sender, FormClosingEventArgs e)
        {
            btnOpenRomInfo.Enabled = true;
        }
    }
}
