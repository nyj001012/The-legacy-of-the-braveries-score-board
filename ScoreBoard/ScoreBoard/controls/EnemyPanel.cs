using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.controls
{
    public partial class EnemyPanel : UserControl
    {
        public EnemyPanel(string id, string name, ushort count)
        {
            InitializeComponent();
            lblName.Text = $"{name} ({count})";
        }
    }
}
