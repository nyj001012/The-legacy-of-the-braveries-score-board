using ScoreBoard.content;
using ScoreBoard.data.statusEffect;
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
    public partial class StatusEffectEditModal : Form
    {
        public List<StatusEffect> newStatusEffects = [];

        public StatusEffectEditModal()
        {
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 이벤트를 받을 수 있도록 설정
        }

        private void StatusEffectEditModal_KeyDown(object sender, KeyEventArgs e)
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

        private void StatusEffectEditModal_Load(object sender, EventArgs e)
        {
            // TODO => 상태이상 효과 아이콘 및 이름을 불러와서 effectList에 추가
            ShowEffectIcons();
            effectList.MouseWheel += effectList_MouseWheel;

            // TODO => 이미지를 picturebox로, 태그에 상태이상 id 저장
            // TODO => picturebox 클릭 시, 오른편에 상태이상 이름, 설명, 지속시간 표시
        }

        /*
         * ShowEffectIcons()
         * - 상태이상 효과 아이콘을 불러와서 effectList에 추가하는 메소드
         */
        private void ShowEffectIcons()
        {
            // 상태이상 효과 아이콘을 불러와서 effectList에 추가하는 로직 구현
            foreach (StatusEffectType type in Enum.GetValues(typeof(StatusEffectType)))
            {
                Image? icon = DataReader.GetStatusEffectImage(type);
                if (icon == null)
                {
                    MessageBox.Show($"상태이상 효과 아이콘을 불러오는 중 오류 발생: {type} 아이콘이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    effectList.Controls.Clear();
                    break;
                }
                PictureBox pb = new()
                {
                    Image = icon,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 64,
                    Height = 64,
                    Tag = type, // 상태이상 효과 타입을 태그에 저장
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand // 마우스 커서를 손 모양으로 변경
                };
                effectList.Controls.Add(pb);
            }
            effectList.Height = effectList.Controls.Cast<Control>().Where(c => c.Visible).Max(c => c.Bottom); ;
            ScrollBarManager.SetScrollBar(pnEffectContainer, effectList, sbEffect); // 스크롤바 설정
        }

        private void effectList_MouseEnter(object sender, EventArgs e)
        {
            effectList.Focus(); // 마우스가 effectList에 들어오면 포커스를 줌
        }

        private void effectList_MouseWheel(object sender, MouseEventArgs e)
        {
            // 마우스 휠 이벤트를 처리하여 스크롤바를 조정
            if (e.Delta > 0) // 위로 스크롤
            {
                sbEffect.Value = Math.Max(sbEffect.Minimum, sbEffect.Value - sbEffect.SmallStep);
            }
            else if (e.Delta < 0) // 아래로 스크롤
            {
                sbEffect.Value = Math.Min(sbEffect.Maximum, sbEffect.Value + sbEffect.SmallStep);
            }
        }
    }
}
