using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScoreBoard.data;
using ScoreBoard.modals;

namespace ScoreBoard.content
{
    public partial class OrganisationControl : UserControl
    {
        Dictionary<string, CorpsMember> selectedCharacters = [];

        public OrganisationControl()
        {
            InitializeComponent();
            this.Size = new Size(1920, 1080);
        }

        private void OrganisationControl_Load(object sender, EventArgs e)
        {

        }

        /*
         * BtnSelectPlayer_Click(object sender, EventArgs e)
         * 플레이어 선택 버튼 클릭 시 플레이어 선택 모달을 표시하는 메서드
         */
        private void BtnSelectPlayer_Click(object sender, EventArgs e)
        {
            if (sender is not PictureBox selectedButton || selectedButton.Tag is null)
            {
                return;
            }

            string? playerNumber = selectedButton.Tag.ToString();
            if (playerNumber is null)
            {
                return;
            }

            var selectPlayerModal = new SelectPlayerForm(selectedCharacters);

            if (selectPlayerModal.ShowDialog(this) == DialogResult.OK)
            {
                CorpsMember? selectedMember = selectPlayerModal.SelectedMember;
                if (selectedMember is not null)
                {
                    // 선택된 멤버를 리스트에 추가합니다.
                    selectedCharacters[playerNumber] = selectedMember;
                    // 선택된 멤버의 정보를 UI에 표시합니다.
                    ApplyToSlot(playerNumber, selectedMember);
                }
            }
        }

        private void ApplyToSlot(string playerNumber, CorpsMember selectedData)
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "image", "character", selectedData.Id + ".png");
            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"이미지 파일이 존재하지 않습니다: {imagePath}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (playerNumber)
            {
                case "1":
                    btnSelect1P.BackgroundImage = Image.FromFile(imagePath);
                    break;
                case "2":
                    btnSelect2P.BackgroundImage = Image.FromFile(imagePath);
                    break;
                case "3":
                    btnSelect3P.BackgroundImage = Image.FromFile(imagePath);
                    break;
                case "4":
                    btnSelect4P.BackgroundImage = Image.FromFile(imagePath);
                    break;
                default:
                    break;
            }
            if (selectedCharacters.Count == 4)
            {
                btnJoin.Visible = true;
                btnJoin.Enabled = true;
            }
            else
            {
                btnJoin.Visible = false;
                btnJoin.Enabled = false;
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {

        }
    }
}
