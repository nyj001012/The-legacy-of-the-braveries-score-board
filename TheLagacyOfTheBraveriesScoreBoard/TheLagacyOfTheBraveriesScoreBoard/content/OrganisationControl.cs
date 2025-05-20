using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLagacyOfTheBraveriesScoreBoard.content
{
    public partial class OrganisationControl : UserControl
    {
        public OrganisationControl()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        private void OrganisationControl_Load(object sender, EventArgs e)
        {

        }

        private void BtnSelectPlayer_Click(object sender, EventArgs e)
        {
            PictureBox selectedButton = sender as PictureBox;
            if (selectedButton != null)
            {
                string playerNumber = selectedButton.Tag.ToString();

                // TODO => 캐릭터 선택 모달 창(Form) 띄우기
                Console.WriteLine($"Player {playerNumber} selected.");
            }
        }
    }
}
