using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScoreBoard.modals;

namespace ScoreBoard.content
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

            var selectPlayerModal = new SelectPlayerForm();

            if (selectPlayerModal.ShowDialog(this) == DialogResult.OK)
            {
                string selectedData = selectPlayerModal.SelectedPlayerId;
                ApplyToSlot(playerNumber, selectedData);
            }
        }

        private void ApplyToSlot(string playerNumber, string selectedData)
        {
            // TODO => 슬롯에 선택된 데이터를 적용하는 로직을 구현합니다.
        }
    }
}
