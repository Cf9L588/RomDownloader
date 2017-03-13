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
            cboSystems.DataSource = Globals.Core.GetSystemNames();
        }

        private void cboSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstRoms.Items.Clear();
            lblLoadingRoms.Visible = true;
            prgLoadingRoms.Visible = true;
            this.Refresh();
            Globals.Core.GetRomsListForSystem((string)cboSystems.SelectedItem).ForEach(rom => lstRoms.Items.Add(rom));
            lblLoadingRoms.Visible = false;
            prgLoadingRoms.Visible = false;
        }
    }
}
