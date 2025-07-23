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
        public List<StatusEffect> NewStatusEffects = [];
        private StatusEffectType _currentType;

        public StatusEffectEditModal(List<StatusEffect> statusEffects)
        {
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 이벤트를 받을 수 있도록 설정
            NewStatusEffects = statusEffects ?? [];
            lblEffectName.Visible = lblEffectDescription.Visible = tbDuration.Visible = false; // 초기에는 상태이상 효과 정보 숨김
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
            ShowEffectIcons();
            effectList.MouseWheel += effectList_MouseWheel;
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
                pb.Click += (s, e) => ShowEffectDetails(type); // 클릭 시 상태이상 효과 세부 정보 표시
                effectList.Controls.Add(pb);
            }
            effectList.Height = effectList.Controls.Cast<Control>().Where(c => c.Visible).Max(c => c.Bottom); ;
            ScrollBarManager.SetScrollBar(pnEffectContainer, effectList, sbEffect); // 스크롤바 설정
        }

        private void effectList_MouseEnter(object sender, EventArgs e)
        {
            effectList.Focus(); // 마우스가 effectList에 들어오면 포커스를 줌
        }

        private void effectList_MouseWheel(object? sender, MouseEventArgs e)
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

        /*
         * ShowEffectDetails(StatusEffectType type)
         * - 선택한 상태이상 효과의 이름, 설명, 지속시간을 표시하는 메소드
         */
        private void ShowEffectDetails(StatusEffectType type)
        {
            lblEffectName.Visible = lblEffectDescription.Visible = tbDuration.Visible = true; // 상태이상 효과 정보 표시
            tbDuration.Focus(); // 지속시간 입력 필드에 포커스 설정
            _currentType = type; // 현재 선택된 상태이상 효과 타입 저장
            lblEffectName.Text = EnumHelper.GetEnumName(type);
            lblEffectDescription.Text = EnumHelper.GetEnumDescription(type);
            if (NewStatusEffects.Any(e => e.Type == type))
            {
                // 이미 추가된 상태이상 효과가 있다면 지속시간을 가져옴
                var existingEffect = NewStatusEffects.First(e => e.Type == type);
                tbDuration.Text = existingEffect.IsInfinite ? "-1" : existingEffect.Duration.ToString();
            }
            else
            {
                tbDuration.Text = "0"; // 새로 추가하는 상태이상 효과는 기본적으로 0으로 설정
            }
        }

        private void tbDuration_Leave(object sender, EventArgs e)
        {
            UpdateStatusEffect(); // 지속시간 입력 필드에서 포커스를 잃을 때 상태이상 효과 업데이트
        }

        private void tbDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter 키 입력을 무시
                UpdateStatusEffect(); // Enter 키를 누르면 상태이상 효과 업데이트
                this.DialogResult = DialogResult.OK; // 대화상자 닫기
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true; // Escape 키 입력을 무시
                this.Close(); // 대화상자 닫기
            }
        }

        /*
         * UpateStatusEffect()
         * - 상태이상 효과의 지속시간을 업데이트하는 메소드
         */
        private void UpdateStatusEffect()
        {
            tbDuration.Text = tbDuration.Text.Trim(); // 입력값의 앞뒤 공백 제거
            if (int.TryParse(tbDuration.Text, out int duration))
            {
                // 상태이상 효과를 새로 생성하거나 업데이트
                StatusEffect newEffect = new(_currentType, duration);
                NewStatusEffects.RemoveAll(e => e.Type == _currentType); // 기존 효과 제거
                if (duration != 0)
                    NewStatusEffects.Add(newEffect); // 새 효과 추가
            }
            else
            {
                MessageBox.Show("지속시간으로 유효한 숫자를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
