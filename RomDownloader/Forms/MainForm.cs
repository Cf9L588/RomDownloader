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
            // Bind the list of all systems to the dropdown box
            cboSystems.DataSource = Globals.Core.GetSystemNames();
        }

        /// <summary>
        /// Handles what happens when a new dropdown selection is made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstRoms.Items.Clear();
            lblLoadingRoms.Visible = true;
            prgLoadingRoms.Visible = true;
            this.Refresh();
            Globals.Core.GetRomsListForSystem(cboSystems.SelectedItem as string).ForEach(rom => lstRoms.Items.Add(rom));
            lblLoadingRoms.Visible = false;
            prgLoadingRoms.Visible = false;
        }
    }
}
