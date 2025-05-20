using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLagacyOfTheBraveriesScoreBoard.content;

namespace TheLagacyOfTheBraveriesScoreBoard
{
    public partial class MainControl : UserControl
    {
        public event EventHandler OrganiseButtonClicked;

        public MainControl()
        {
            InitializeComponent();
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
    }
}
