using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheLagacyOfTheBraveriesScoreBoard.modals;

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
            if (selectedButton == null) { return; }

            string playerNumber = selectedButton.Tag.ToString();
            var selectPlayerModal = new SelectPlayerForm();

            if (selectPlayerModal.ShowDialog(this) == DialogResult.OK)
            {
                int selectedData = selectPlayerModal.SelectedPlayerId;
                ApplyToSlot(playerNumber, selectedData);
            }
        }

        private void ApplyToSlot(string playerNumber, int selectedData)
        {
            // TODO => 슬롯에 선택된 데이터를 적용하는 로직을 구현합니다.
        }
    }
}
