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
    public partial class MainForm : Form
    {
        // 더블 버퍼링 적용
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        private void ShowControl(UserControl control)
        {
            this.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowMainControl();
        }

        private void ShowMainControl()
        {
            var mainControl = new MainControl();
            mainControl.OrganiseButtonClicked += (s, e) =>
            {
               ShowControl(new OrganisationControl());
            };

            ShowControl(mainControl);
        }
    }
}
