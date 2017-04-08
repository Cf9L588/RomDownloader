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
        public RomViewerForm(GameInfo info)
        {
            InitializeComponent();
            lblInfo.Text += info.Title;
        }

        private void RomViewerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void RomViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
