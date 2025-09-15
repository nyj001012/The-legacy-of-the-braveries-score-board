using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.minion;
using ScoreBoard.data.stat;
using ScoreBoard.data.statusEffect;
using ScoreBoard.utils;
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
        protected DoubleBufferedPanel PnPlayer { get; set; } = null!;
        protected CustomFlowLayoutPanel PnInfo { get; set; } = null!;

        protected void InitBase(CorpsMember player, int order)
        {
            this.Cursor = Cursors.Hand;

            PnPlayer.Dock = DockStyle.None;                     // ← Dock.Left 끊기
            PnPlayer.AutoSize = true;
            PnPlayer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PnPlayer.Location = new Point(0, 0);

            // 왼쪽 번호 라벨(1P~4P): 고정 폭, 자동 높이 동기화 대상
            LblOrder.Dock = DockStyle.None;
            LblOrder.AutoSize = false;
            LblOrder.Width = 88;                                // 네가 쓰던 폭
            LblOrder.Location = new Point(0, 0);

            // 오른쪽 정보 패널(pnInfo): 세로로만 쌓이고, AutoSize로 높이 계산
            PnInfo.AutoSize = true;
            PnInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PnInfo.WrapContents = false;
            PnInfo.AutoScroll = false;
            PnInfo.Margin = new Padding(0);
            PnInfo.Location = new Point(LblOrder.Width, 0);     // ← 왼쪽 라벨 옆에 고정 배치

            LblName.Text = player.Name;
            LblName.AutoSize = true;
            LblOrder.Text = $"{order}P";
            InitLevel(player.Level);
            InitStatus(player.Stat);
            InitArtifact(player.ArtifactSlot, player.MaxArtifactSlot);
            InitSummon(player.Minions);
            HbPlayer.SetValues(player.Stat.Hp, player.Stat.Shield, player.Stat.MaxHp);

            RegisterClickRecursive(this);
            // pnInfo 높이가 바뀔 때 전체 높이 동기화
            PnInfo.SizeChanged += (_, __) => SyncHeights();
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

            PbLv.BackgroundImage = level switch
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
                AddStatusEffectIcons(stat.StatusEffects);
            }
            else
            {
                FpnStatus.Visible = false;
            }

            FpnStatus.ResumeLayout();
        }

        /*
         * AddStatusEffectIcons(List<StatusEffect> statusEffects)
         * - 상태이상 아이콘을 패널에 추가하는 메서드
         * - 최대 3개 아이콘을 표시하고, 4개 이상일 경우 +N 표시
         */
        private void AddStatusEffectIcons(List<StatusEffect> statusEffects)
        {
            FpnStatus.Controls.Clear(); // 기존 아이콘 제거

            int iconSize = FpnStatus.Height;
            int margin = 3; // 기본 Margin값
            int count = 0;

            foreach (var effect in statusEffects)
            {
                // 다음 아이콘까지 포함했을 때 공간이 부족하면 +N 표시
                if (count == 3)
                {
                    int remaining = statusEffects.Count - count;
                    if (remaining > 0)
                    {
                        TransparentTextLabel label = new()
                        {
                            Text = $"+{remaining}",
                            ForeColor = Color.WhiteSmoke,
                            Font = new Font("Danjo-bold", iconSize / 2),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Size = new Size(iconSize, iconSize),
                            Margin = new Padding(0, margin, 0, 0)
                        };
                        FpnStatus.Controls.Add(label);
                    }
                    break;
                }

                CreateStatusEffectIcon(effect, iconSize, margin);
                count++;
            }
        }

        /*
         * CreateStatusEffectIcon(StatusEffect effect, int size)
         * - 상태이상 아이콘을 생성하고 패널에 추가하는 메서드
         */
        private void CreateStatusEffectIcon(StatusEffect effect, int size, int margin)
        {
            PictureBox pb = new()
            {
                Name = $"pbStatus{effect.Type}",
                Size = new Size(size, size),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = DataReader.GetStatusEffectImage(effect.Type), // 상태이상 아이콘을 설정
                Margin = new Padding(0, 0, margin, 0), // 오른쪽에만 마진을 줌
                Tag = effect.Type // 상태이상 타입을 태그로 설정
            };

            // 툴팁 설정: 상태이상 이름과 설명을 툴팁으로 표시
            ToolTip toolTip = new()
            {
                AutomaticDelay = 0, // 툴팁 표시 지연 시간 (ms)
                AutoPopDelay = 0, // 툴팁 자동 사라지는 시간 (ms)
                InitialDelay = 0, // 툴팁 초기 지연 시간 (ms)
                ReshowDelay = 0, // 툴팁 다시 표시 지연 시간 (ms)
            };

            string caption = $"{EnumHelper.GetEnumName(effect.Type)}:" +
                             $"{(effect.IsInfinite ? "∞" : $"{effect.Duration}턴")}";
            toolTip.SetToolTip(pb, caption);
            FpnStatus.Controls.Add(pb);
        }

        protected void InitArtifact(List<Artifact?> artifacts, ushort maxSlots)
        {
            FpnArtifact.SuspendLayout();
            FpnArtifact.Controls.Clear();

            for (int i = 0; i < maxSlots; i++)
            {
                if (artifacts.ElementAtOrDefault(i) != null)
                {
                    SetArtifactImage(artifacts[i]!, i);
                }
                else
                {
                    SetDefaultArtifactSlot(i);
                }
            }

            FpnArtifact.ResumeLayout();
        }

        protected void SetArtifactImage(Artifact artifact, int i)
        {
            Image? artifactImage = DataReader.GetArtifactImage(artifact.Id);
            if (artifactImage == null)
            {
                MessageBox.Show($"유물 이미지가 없습니다: {artifact.Id}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PictureBox pb = new()
            {
                Name = $"pbArtifact{i}",
                Size = new Size(FpnArtifact.Size.Height - 5, FpnArtifact.Size.Height - 5),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = artifactImage,
            };
            FpnArtifact.Controls.Add(pb);
        }

        protected void SetDefaultArtifactSlot(int index)
        {
            int size = FpnArtifact.Size.Height - 5;

            Image image = index switch
            {
                0 => Properties.Resources.EmptyHeadgearSlot,
                1 => Properties.Resources.EmptyArmourSlot,
                _ => Properties.Resources.EmptyAccessorySlot,
            };

            PictureBox pb = new()
            {
                Name = $"pbArtifact{index}",
                Size = new Size(size, size),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = image,
            };

            FpnArtifact.Controls.Add(pb);
        }

        /*
         * InitSummon(List<Minion> minions)
         * - 소환수 요약 정보를 pnInfo에 표시
         * - minions: 소환수 리스트
         */
        protected void InitSummon(List<Minion> minions)
        {
            List<Minion> aliveMinions = minions.Where(m => m.Stat.Hp > 0).ToList();
            if (aliveMinions.Count < 0)
                return;

            foreach (Minion m in aliveMinions)
            {
                CustomFlowLayoutPanel fpnMinion = new()
                {
                    Width = PnInfo.Width,
                    AutoSize = true,
                    Margin = new Padding(0, 15, 0, 0),
                    FlowDirection = FlowDirection.TopDown,
                };
                TransparentTextLabel lblMinionName = new()
                {
                    Text = m.Name,
                    Font = new Font("Danjo-bold", (int)(LblName.Font.Size * 0.8)),
                    ForeColor = Color.WhiteSmoke,
                    AutoSize = true,
                };
                HealthBar hbMinion = new()
                {
                    Name = $"hb{m.Id}",
                    Width = (int)(HbPlayer.Width * 0.8),
                    Height = (int)(HbPlayer.Height * 0.8),
                    Margin = new Padding(0, 15, 0, 0),
                    BackColor = Color.Transparent,
                    Font = new Font("Danjo-bold", (int)(HbPlayer.Font.Size * 0.8)),
                };
                fpnMinion.Controls.Add(lblMinionName);
                fpnMinion.Controls.Add(hbMinion);
                PnInfo.Controls.Add(fpnMinion);
            }
        }

        private void SyncHeights()
        {
            // pnInfo는 FlowLayoutPanel이라 PreferredSize가 정확함
            var infoH = PnInfo.PreferredSize.Height;
            var orderH = LblOrder.PreferredSize.Height; // GradientLabel이면 Height 써도 무방
            var h = Math.Max(Math.Max(infoH, orderH), this.MinimumSize.Height);

            if (LblOrder.Height != h) LblOrder.Height = h;
            if (PnPlayer.Height != h) PnPlayer.Height = h;
            if (this.Height != h) this.Height = h;

            // 좌우 배치 유지
            if (PnInfo.Left != LblOrder.Width) PnInfo.Left = LblOrder.Width;

            // 부모가 FlowLayoutPanel 등일 경우, 레이아웃 갱신
            this.Parent?.PerformLayout();
        }
    }
}
