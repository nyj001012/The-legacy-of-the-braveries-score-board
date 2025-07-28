using ScoreBoard.controls;
using ScoreBoard.data.artifact;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.modals
{
    public partial class EquipmentEditModal : Form
    {
        private readonly ArtifactType _type = ArtifactType.Weapon; // 기본값은 Weapon으로 설정

        public EquipmentEditModal(ArtifactType type)
        {
            InitializeComponent();
            this.KeyPreview = true;
            _type = type;
        }

        private void EquipmentEditModal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void EquipmentEditModal_Load(object sender, EventArgs e)
        {
            ShowEquipmentIcons(_type);
            equipList.MouseWheel += equipList_MouseWheel;
        }

        private void ShowEquipmentIcons(ArtifactType type)
        {
            equipList.SuspendLayout();
            // 장비 아이콘을 불러와서 equipList에 추가하는 로직 구현
            var items = DataReader.GetEquipments(type);
            if (items == null)
            {
                MessageBox.Show($"장비 아이콘을 불러오는 중 오류 발생.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var item in items)
            {
                string id = item.Key;
                var (artifact, icon) = item.Value;
                var pictureBox = new PictureBox
                {
                    Image = icon,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 100,
                    Height = 100,
                    Margin = new Padding(10),
                    Cursor = Cursors.Hand,
                };
                pictureBox.Click += (s, e) => ShowArtifactDetails(artifact);
                equipList.Controls.Add(pictureBox);
            }
            equipList.PerformLayout();
            equipList.Height = Math.Max(equipContainer.Height, equipList.Controls.Cast<Control>().Where(c => c.Visible).Max(c => c.Bottom));
            ScrollBarManager.SetScrollBar(equipContainer, equipList, equipScrollbar);
            equipList.ResumeLayout();
        }

        private void ShowArtifactDetails(Artifact artifact)
        {
            fpnDetails.SuspendLayout();
            lblName.Text = artifact.Name;
            fpnDescription.Controls.Clear(); // 기존 설명 제거
            foreach (var text in artifact.Description)
            {
                var label = new TransparentTextLabel
                {
                    Text = text,
                    AutoSize = true,
                    ForeColor = Color.WhiteSmoke,
                    Font = new Font("Danjo-bold", 18),
                    Margin = new Padding(0, 0, 0, 0)
                };
                fpnDescription.Controls.Add(label);
            }
            fpnDetails.ResumeLayout();
        }

        private void equipList_MouseWheel(object? sender, MouseEventArgs e)
        {
            // 스크롤 이벤트 처리
            if (e.Delta > 0)
            {
                equipScrollbar.Value = Math.Max(equipScrollbar.Value - 1, equipScrollbar.Minimum);
            }
            else if (e.Delta < 0)
            {
                equipScrollbar.Value = Math.Min(equipScrollbar.Value + 1, equipScrollbar.Maximum);
            }
        }
    }
}
