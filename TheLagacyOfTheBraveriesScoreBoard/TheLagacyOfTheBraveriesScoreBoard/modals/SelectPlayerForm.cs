using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Deactivate += (s, e) => this.Close();
        }
    }
}
