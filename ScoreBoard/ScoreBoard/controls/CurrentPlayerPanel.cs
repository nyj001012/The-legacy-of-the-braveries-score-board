using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.stat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ScoreBoard.controls
{
    public partial class CurrentPlayerPanel : UserControl
    {
        public CurrentPlayerPanel(CorpsMember player, int order)
        {
            InitializeComponent();
            lblName.Text = player.Name;
            lblOrder.Text = $"{order.ToString()}P";
            InitLevel(player.Level);
            InitStatus(player.Stat);
            InitArtifact(player.ArtifactSlot, player.MaxArtifactSlot);
            hbPlayer.SetValues(player.Stat.Hp, player.Stat.Shield, player.Stat.MaxHp);
        }

        /*
         * pbLv의 레벨 이미지를 초기화
         */
        private void InitLevel(ushort level)
        {
            if (level < 0 || level > 3) throw new ArgumentOutOfRangeException(nameof(level), "레벨은 0에서 3 사이여야 합니다.");
            pbLv.Image = level switch
            {
                0 => null, // 레벨 0은 이미지 없음
                1 => Properties.Resources.Lv1, // 레벨 1 이미지
                2 => Properties.Resources.Lv2, // 레벨 2 이미지
                3 => Properties.Resources.Lv3, // 레벨 3 이미지
                _ => throw new NotImplementedException($"{level}은(는) 유효한 레벨이 아닙니다."),
            };
        }

        /*
         * fpnStatus 컨트롤에 상태이상 정보를 초기화
         */
        private void InitStatus(Stat stat)
        {
            fpnStatus.SuspendLayout();
            fpnStatus.Controls.Clear(); // 기존 컨트롤 제거
            if (stat.StatusEffects.Count > 0)
            {
                fpnStatus.Visible = true; // 상태이상 정보가 있으면 보임 처리
                // TODO: stat에서 상태이상 정보를 가져와서 동적으로 추가
            }
            else
            {
                fpnStatus.Visible = false; // 상태이상 정보가 없으면 숨김 처리
            }
            fpnStatus.ResumeLayout();
        }

        /*
         * fpnArtifact 컨트롤에 유물 정보를 초기화
         */
        private void InitArtifact(List<Artifact> artifacts, ushort maxSlots)
        {
            int size = fpnArtifact.Size.Height;
            fpnArtifact.SuspendLayout();
            fpnArtifact.Controls.Clear(); // 기존 컨트롤 제거
            for (int i = 0; i < maxSlots; i++)
            {
                if (artifacts.ElementAtOrDefault(i) != null)
                {
                    // TODO: 유물 아이콘 동적 추가
                }
                else
                {
                    // 빈 슬롯 아이콘 추가 (예: Properties.Resources.EmptyArtifactSlot)
                    PictureBox emptySlot = new PictureBox
                    {
                        Name = $"EmptyArtifactSlot{i + 1}",
                        Size = new Size(size, size), // 아이콘 크기 조정
                        Image = Properties.Resources.EmptyArtifactSlot, // 빈 슬롯 아이콘 이미지
                        SizeMode = PictureBoxSizeMode.StretchImage,
                    };
                    fpnArtifact.Controls.Add(emptySlot);
                }
            }
            fpnArtifact.ResumeLayout();
        }
    }
}
