using ScoreBoard.controls;
using ScoreBoard.data.character;
using ScoreBoard.data.monster;
using ScoreBoard.modals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.content
{
    public partial class OrganisationControl : UserControl
    {
        public event EventHandler<(Dictionary<string, CorpsMember> characters, List<Monster> monsters)>? RequestScoreBoard; // 점수판 요청 이벤트
        Dictionary<string, CorpsMember> selectedCharacters = [];
        List<(string id, string name, ushort count)> selectedMonsters = [];

        public OrganisationControl()
        {
            InitializeComponent();
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

        /*
         * ApplyToSlot(string playerNumber, CorpsMember selectedData)
         * 선택된 멤버의 정보를 해당 플레이어 슬롯에 적용하는 메서드
         * - playerNumber: 플레이어 번호 (예: "1", "2", "3", "4")
         * - selectedData: 선택된 CorpsMember 객체
         */
        private void ApplyToSlot(string playerNumber, CorpsMember selectedData)
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "image", "character", selectedData.Id + ".png");
            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"이미지 파일이 존재하지 않습니다: {imagePath}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.Controls.Find($"btnSelect{playerNumber}P", true).FirstOrDefault() is not PictureBox pictureBox
                || this.Controls.Find($"lbl{playerNumber}P", true).FirstOrDefault() is not GradientLabel label
                || this.Controls.Find($"btnCancel{playerNumber}P", true).FirstOrDefault() is not PictureBox cancelButton)
            {
                MessageBox.Show($"UI 요소를 찾을 수 없습니다: btnSelect{playerNumber}P, lbl{playerNumber}P, btnCancel{playerNumber}P", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pictureBox.BackgroundImage = Image.FromFile(imagePath);
            label.Text = selectedData.Name;
            label.Invalidate();
            cancelButton.Visible = true;
            btnJoin.Visible = btnJoin.Enabled = selectedCharacters.Count == 4;
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var selectMonsterModal = new SelectMonsterForm(selectedMonsters);
            if (selectMonsterModal.ShowDialog(this) == DialogResult.OK)
            {
                selectedMonsters = selectMonsterModal.currentSelectedMonsters;
                selectMonsterModal.Close();
                List<Monster> selectedMonsterList = new();
                foreach (var (id, name, count) in selectedMonsters)
                {
                    Monster monster = id switch
                    {
                        "2_01_white_soldier" => new WhiteSoldier(id, 0),// 스폰 턴은 0으로 설정
                        "2_02_black_knight" => new BlackKnight(id, 0),
                        _ => throw new ArgumentException($"알 수 없는 몬스터 ID: {id}"),
                    };
                    monster.Count = count;
                    selectedMonsterList.Add(monster);
                }
                RequestScoreBoard?.Invoke(this, (selectedCharacters, selectedMonsterList));
            }
        }

        /*
         * btnCancel_Click(object sender, EventArgs e)
         * 선택된 플레이어를 취소하는 메서드
         * - sender: 취소 버튼 (예: btnCancel1P, btnCancel2P 등)
         */
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (sender is not PictureBox { Tag: string playerNumber }) return;
            if (!selectedCharacters.Remove(playerNumber)) return;

            // 선택된 멤버를 제거합니다.
            selectedCharacters.Remove(playerNumber);

            // 해당 슬롯의 UI를 초기화합니다.
            GradientLabel? label = this.Controls.Find($"lbl{playerNumber}P", true).FirstOrDefault() as GradientLabel;
            PictureBox? button = this.Controls.Find($"btnSelect{playerNumber}P", true).FirstOrDefault() as PictureBox;
            if (label is not null)
            {
                label.Text = "";
                label.Invalidate();
            }
            if (button is not null)
            {
                button.BackgroundImage = Properties.Resources.BtnSelectCharacter;
            }
            // 취소 버튼을 숨깁니다.
            ((PictureBox)sender).Visible = false;
            // Join 버튼의 가시성과 활성화 상태를 업데이트합니다.
            btnJoin.Visible = btnJoin.Enabled = selectedCharacters.Count == 4;
        }
    }
}
