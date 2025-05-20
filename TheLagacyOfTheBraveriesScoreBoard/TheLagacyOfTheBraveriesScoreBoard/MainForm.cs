using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLagacyOfTheBraveriesScoreBoard
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
            this.btnOrganise.Size = new Size(392, 104);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnOrganise_Click(object sender, EventArgs e)
        {

        }

        private void btnDataArchive_Click(object sender, EventArgs e)
        {

        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/nyj001012/The-lagacy-of-the-braveries-score-board");
        }

        private void btnSecretCode_Click(object sender, EventArgs e)
        {

        }
    }
}
