using ScoreBoard.content;
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

namespace ScoreBoard
{
    public partial class MainControl : UserControl
    {
        public event EventHandler OrganiseButtonClicked = delegate { }; // null 방지를 위한 기본값 설정
        public event EventHandler ShowScoreBoard = delegate { }; // null 방지를 위한 기본값 설정

        public MainControl()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        // 더블 버퍼링 적용
        private void btnOrganise_Click(object sender, EventArgs e)
        {
            OrganiseButtonClicked.Invoke(this, EventArgs.Empty);
        }

        /*
         * btnDataArchive_Click(object sender, EventArgs e)
         * 기록 저장소 버튼 클릭 시 호출되는 메서드
         */
        private void btnDataArchive_Click(object sender, EventArgs e)
        {
            MessageBox.Show("지원하지 않는 기능입니다.");
        }

        /*
         * btnGuide_Click(object sender, EventArgs e)
         * 훈련 교본 버튼 클릭 시 호출되는 메서드
         */
        private void btnGuide_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/nyj001012/The-lagacy-of-the-braveries-score-board/blob/main/README.md");
        }

        /*
         * OpenUrl(string url)
         * - 지정된 주소로 웹사이트를 여는 메서드
         * - url: 웹사이트 url
         */
        private static void OpenUrl(string url)
        {
            try
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                {
                    MessageBox.Show("유효한 URL이 아닙니다.");
                    return;
                }

                Process.Start(new ProcessStartInfo(uri.ToString())
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"브라우저를 여는 중 오류: {ex.Message}");
            }
        }

        /*
         * btnSecretCode_Click(object sender, EventArgs e)
         * 비밀 코드 버튼 클릭 시 호출되는 메서드
         */
        private void btnSecretCode_Click(object sender, EventArgs e)
        {
            MessageBox.Show("지원하지 않는 기능입니다.");
        }

        private void MainControl_Load(object sender, EventArgs e)
        {

        }
    }
}
