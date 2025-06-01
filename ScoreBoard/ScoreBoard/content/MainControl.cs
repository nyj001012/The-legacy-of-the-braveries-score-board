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
        public event EventHandler OrganiseButtonClicked = delegate { }; // null 방지를 위한 기본값 설정

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

        }

        /*
         * btnGuide_Click(object sender, EventArgs e)
         * 훈련 교본 버튼 클릭 시 호출되는 메서드
         */
        private void btnGuide_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/nyj001012/The-lagacy-of-the-braveries-score-board");
        }

        /*
         * btnSecretCode_Click(object sender, EventArgs e)
         * 비밀 코드 버튼 클릭 시 호출되는 메서드
         */
        private void btnSecretCode_Click(object sender, EventArgs e)
        {

        }

        private void MainControl_Load(object sender, EventArgs e)
        {

        }
    }
}
