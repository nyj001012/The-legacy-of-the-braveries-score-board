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

namespace ScoreBoard.forms
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

        /*
         * ShowControl(UserControl control)
         * - control: 표시할 UserControl 객체
         * - 기능: 현재 폼에 UserControl을 표시하는 메서드
         */
        private void ShowControl(UserControl control)
        {
            this.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
        }

        /*
         * ShowMainControl()
         * - 기능: 메인 컨트롤을 표시하는 메서드
         */
        private void ShowMainControl()
        {
            var mainControl = new MainControl();
            mainControl.OrganiseButtonClicked += (s, e) =>
            {
                var organisationControl = new OrganisationControl();
                organisationControl.RequestScoreBoard += (_, data) =>
                {
                    var (characters, monsters) = data;
                    ShowControl(new ScoreBoardControl(characters, monsters)); // 점수판 컨트롤 표시
                };
                ShowControl(new OrganisationControl()); // 부대 편성 컨트롤 표시
            };

            ShowControl(mainControl);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowMainControl();
        }
    }
}
