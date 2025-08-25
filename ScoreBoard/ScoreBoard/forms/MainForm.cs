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
        private readonly Stack<UserControl> _history = new();
        private UserControl? _currentControl;

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
        private void ShowControl(UserControl control, bool addToHistory)
        {
            if (_currentControl != null && addToHistory)
                _history.Push(_currentControl);

            pbGoBack.Visible = _history.Count > 0;

            _containerPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            _containerPanel.Controls.Add(control);
            _currentControl = control;
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
                    ShowControl(new ScoreBoardControl(characters, monsters), true); // 점수판 컨트롤 표시
                };
                ShowControl(organisationControl, true); // 부대 편성 컨트롤 표시
            };

            ShowControl(mainControl, true);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            ShowMainControl();
        }

        private void pbGoBack_Click(object sender, EventArgs e)
        {
            UserControl oldControl = _history.Pop();
            if (oldControl != null)
            {
                ShowControl(oldControl, false);
            }
        }
    }
}
