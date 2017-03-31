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

            lblLoadingRoms.Visible = true;
            prgLoadingRoms.Visible = true;
            cboSystems.Enabled = false;
            var gettingTask = Core.GetSystemRomsAsync(cboSystems.SelectedItem as string);
            foreach (var rom in await gettingTask)
            {
                lstRoms.Items.Add(rom);
            }

            lblLoadingRoms.Visible = false;
            prgLoadingRoms.Visible = false;
            cboSystems.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Bind the list of all systems to the dropdown box
            cboSystems.DataSource = Core.GetSystemNames();
        }

        private void bgwGetRoms_DoWork(object sender, DoWorkEventArgs e)
        {
            romList = Core.GetSystemRoms(cboSystems.SelectedItem as string);
        }

        private void bgwGetRoms_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (var rom in romList)
            {
                lstRoms.Items.Add(rom);
            }

            lblLoadingRoms.Visible = false;
            prgLoadingRoms.Visible = false;
            cboSystems.Enabled = true;
        }

        private void lstRoms_SelectedIndexChanged(object sender, EventArgs e)
        {
            TheGamesDB.GetGame(lstRoms.SelectedItem as string, cboSystems.SelectedItem as string); 
        }
    }
}
