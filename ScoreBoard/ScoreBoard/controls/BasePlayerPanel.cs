using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.stat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScoreBoard.controls
{
    public class BasePlayerPanel : UserControl
    {
        protected GradientLabel LblName { get; set; } = null!;
        protected GradientLabel LblOrder { get; set; } = null!;
        protected PictureBox PbLv { get; set; } = null!;
        protected CustomFlowLayoutPanel FpnStatus { get; set; } = null!;
        protected CustomFlowLayoutPanel FpnArtifact { get; set; } = null!;
        protected HealthBar HbPlayer { get; set; } = null!;

        protected void InitBase(CorpsMember player, int order)
        {
            this.Cursor = Cursors.Hand;
            LblName.Text = player.Name;
            LblOrder.Text = $"{order}P";
            InitLevel(player.Level);
            InitStatus(player.Stat);
            InitArtifact(player.ArtifactSlot, player.MaxArtifactSlot);
            HbPlayer.SetValues(player.Stat.Hp, player.Stat.Shield, player.Stat.MaxHp);

            RegisterClickRecursive(this);
        }

        private void RegisterClickRecursive(Control control)
        {
            foreach (Control c in control.Controls)
            {
                c.Click += (s, e) => this.OnClick(e);
                if (c.HasChildren)
                    RegisterClickRecursive(c);
            }
        }

        protected void InitLevel(ushort level)
        {
            if (level < 0 || level > 3)
                throw new ArgumentOutOfRangeException(nameof(level), "레벨은 0에서 3 사이여야 합니다.");

            PbLv.Image = level switch
            {
                0 => null,
                1 => Properties.Resources.Lv1,
                2 => Properties.Resources.Lv2,
                3 => Properties.Resources.Lv3,
                _ => throw new NotImplementedException($"{level}은(는) 유효한 레벨이 아닙니다."),
            };
        }

        protected void InitStatus(Stat stat)
        {
            FpnStatus.SuspendLayout();
            FpnStatus.Controls.Clear();

            if (stat.StatusEffects.Count > 0)
            {
                FpnStatus.Visible = true;
                // TODO: 동적 상태이상 표시
            }
            else
            {
                FpnStatus.Visible = false;
            }

            FpnStatus.ResumeLayout();
        }

        protected void InitArtifact(List<Artifact> artifacts, ushort maxSlots)
        {
            FpnArtifact.SuspendLayout();
            FpnArtifact.Controls.Clear();

            for (int i = 0; i < maxSlots; i++)
            {
                if (artifacts.ElementAtOrDefault(i) != null)
                {
                    // TODO: 착용 중인 유물 아이콘 동적 추가
                }
                else
                {
                    SetDefaultArtifactSlot(i);
                }
            }

            FpnArtifact.ResumeLayout();
        }

        protected void SetDefaultArtifactSlot(int index)
        {
            int size = FpnArtifact.Size.Height;

            Image image = index switch
            {
                0 => Properties.Resources.EmptyWeaponSlot,
                1 => Properties.Resources.EmptyArmourSlot,
                _ => Properties.Resources.EmptyAccessorySlot,
            };

            PictureBox pb = new PictureBox
            {
                Name = $"pbArtifact{index}",
                Size = new Size(size, size),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = image,
                Tag = index
            };

            FpnArtifact.Controls.Add(pb);
        }
    }
}
