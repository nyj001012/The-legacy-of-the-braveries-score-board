using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScoreBoard.content;
using ScoreBoard.controls;

namespace ScoreBoard.forms
{
    public partial class MainForm : Form
    {
        private readonly Panel _containerPanel;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // 더블 버퍼링 활성화
            this.ClientSize = new Size(1920, 1080);
            _containerPanel = new DoubleBufferedPanel
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_containerPanel);
        }

        /*
         * ShowControl(UserControl control)
         * - control: 표시할 UserControl 객체
         * - 기능: 현재 폼에 UserControl을 표시하는 메서드
         */
        private void ShowControl(UserControl control)
        {
            _containerPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            _containerPanel.Controls.Add(control);
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
                ShowControl(organisationControl); // 부대 편성 컨트롤 표시
            };

            ShowControl(mainControl);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ShowMainControl();
        }
    }
}
