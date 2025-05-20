using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScoreBoard.content;

namespace ScoreBoard
{
    public partial class MainControl : UserControl
    {
        public event EventHandler OrganiseButtonClicked = delegate { }; // Initialize with an empty delegate to avoid null

        public MainControl()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        private void btnOrganise_Click(object sender, EventArgs e)
        {
            OrganiseButtonClicked.Invoke(this, EventArgs.Empty);
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

        private void MainControl_Load(object sender, EventArgs e)
        {

        }
    }
}
