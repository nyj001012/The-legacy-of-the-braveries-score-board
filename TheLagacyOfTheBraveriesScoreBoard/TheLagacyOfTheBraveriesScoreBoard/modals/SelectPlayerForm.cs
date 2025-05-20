using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLagacyOfTheBraveriesScoreBoard.modals
{
    public partial class SelectPlayerForm : Form
    {
        public int SelectedPlayerId { get; private set; }

        public SelectPlayerForm()
        {
            InitializeComponent();
        }

        private void SelectPlayerForm_Load(object sender, EventArgs e)
        {

        }

        private void SelectPlayerForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
