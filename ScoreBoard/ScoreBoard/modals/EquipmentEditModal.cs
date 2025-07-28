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
        private const int ICON_SIZE = 80; // 아이콘 크기 설정
        public Artifact? SelectedArtifact { get; private set; } = null; // 선택된 유물

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

        /*
         * ShowEquipmentIcons(ArtifactType type)
         * - 유물 아이콘과 착용 해제 아이콘을 리스트에 추가하는 메서드
         */
        private void ShowEquipmentIcons(ArtifactType type)
        {
            // 착용 해제하는 아이콘 추가
            ShowUnequipIcon();

            // 장비 아이콘을 불러와서 equipList에 추가
            ShowArtifactIcons(type);

            equipList.Height = Math.Max(equipContainer.Height, equipList.Controls.Cast<Control>().Where(c => c.Visible).Max(c => c.Bottom));
            ScrollBarManager.SetScrollBar(equipContainer, equipList, equipScrollbar);
        }

        /*
         * ShowArtifactIcons(ArtifactType type)
         * - 주어진 ArtifactType에 해당하는 유물 아이콘을 불러와서 equipList에 추가하는 메서드
         */
        private void ShowArtifactIcons(ArtifactType type)
        {
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
                    Width = ICON_SIZE,
                    Height = ICON_SIZE,
                    Margin = new Padding(10),
                    Cursor = Cursors.Hand,
                };
                pictureBox.Click += (s, e) => ShowArtifactDetails(artifact);
                equipList.Controls.Add(pictureBox);
            }
        }

        /*
         * ShowUnequipIcon()
         * - 착용 해제 아이콘을 equipList에 추가하는 메서드
         */
        private void ShowUnequipIcon()
        {
            var unequipIcon = new PictureBox
            {
                Image = Properties.Resources.BtnCross, // 착용 해제 아이콘 이미지
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = ICON_SIZE,
                Height = ICON_SIZE,
                Margin = new Padding(10),
                Cursor = Cursors.Hand,
            };
            unequipIcon.Click += (s, e) => SelectedArtifact = null;
            equipList.Controls.Add(unequipIcon);
        }

        /*
         * ShowArtifactDetails(Artifact artifact)
         * - 선택된 유물의 세부 정보를 표시하는 메서드
         */
        private void ShowArtifactDetails(Artifact artifact)
        {
            SelectedArtifact = artifact; // 선택된 유물 설정
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

        /*
         * equipList_MouseWheel(object? sender, MouseEventArgs e)
         * - 장비 리스트에서 마우스 휠 스크롤 이벤트를 처리하는 메서드
         */
        private void equipList_MouseWheel(object? sender, MouseEventArgs e)
        {
            // 스크롤 이벤트 처리
            if (e.Delta > 0) // 위로 스크롤
            {
                equipScrollbar.Value = Math.Max(equipScrollbar.Minimum, equipScrollbar.Value - equipScrollbar.SmallStep);
            }
            else if (e.Delta < 0) // 아래로 스크롤
            {
                equipScrollbar.Value = Math.Min(equipScrollbar.Maximum, equipScrollbar.Value + equipScrollbar.SmallStep);
            }
        }
    }
}
