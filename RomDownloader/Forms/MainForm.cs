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

        public MainForm()
        {
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
        private void cboSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstRoms.Items.Clear();
            this.Refresh();

            lblLoadingRoms.Visible = true;
            prgLoadingRoms.Visible = true;

            var list = Core.GetSystemRoms(cboSystems.SelectedItem as string);
            foreach (var rom in list)
            {
                lstRoms.Items.Add(rom);
            }
            
            lblLoadingRoms.Visible = false;
            prgLoadingRoms.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Bind the list of all systems to the dropdown box
            cboSystems.DataSource = Core.GetSystemNames();
        }
    }
}
