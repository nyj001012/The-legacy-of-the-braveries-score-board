using ScoreBoard.content;
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

            // AutoSize 체인 설정 (스크롤 없이 키우기)
            SetAutoSize();

            // 플레이어 정보 초기화
            LblName.Text = player.Name;
            LblName.AutoSize = true;
            LblOrder.Text = $"{order}P";
            InitLevel(player.Level);
            InitStatus(FpnStatus, player.Stat);
            InitArtifact(FpnArtifact, player.ArtifactSlot, player.MaxArtifactSlot);
            InitSummon(player.Minions);
            HbPlayer.SetValues(player.Stat.Hp, player.Stat.Shield, player.Stat.MaxHp);

            // 클릭 이벤트를 패널 전체에 걸치도록 설정
            RegisterClickRecursive(this);

            // pnInfo 높이가 바뀔 때 전체 높이 동기화
            PnInfo.SizeChanged += (_, __) => SyncHeights();
            PnInfo.PerformLayout(); // 강제 레이아웃 갱신
            SyncHeights();
        }

        /*
         * SetAutoSize()
         * - AutoSize 체인 설정 (스크롤 없이 키우기)
         */
        private void SetAutoSize()
        {
            PnPlayer.Dock = DockStyle.None; // Dock.Left 끊기
            PnPlayer.AutoSize = true;
            PnPlayer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PnPlayer.Location = new Point(0, 0);

            // 왼쪽 번호 라벨(1P~4P): 고정 폭, 자동 높이 동기화 대상
            LblOrder.Dock = DockStyle.None;
            LblOrder.AutoSize = false;
            LblOrder.Width = 88;
            LblOrder.Location = new Point(0, 0);

            // 오른쪽 정보 패널(pnInfo): 세로로만 쌓이고, AutoSize로 높이 계산
            PnInfo.AutoSize = true;
            PnInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            PnInfo.WrapContents = false;
            PnInfo.AutoScroll = false;
            PnInfo.Margin = new Padding(0);
            PnInfo.Location = new Point(LblOrder.Width, 0); // 왼쪽 라벨 옆에 고정 배치
        }

        /*
         * RegisterClickRecursive(Control control)
         * - 지정한 컨트롤과 자식 컨트롤에 클릭 이벤트 핸들러 등록
         * - 단, Tag가 "minion"인 컨트롤은 제외 (미니언 클릭 이벤트 방지)
         */
        private void RegisterClickRecursive(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c.Tag as string == "minion") continue; // 미니언은 클릭 이벤트 제외

                c.Click -= (s, e) => this.OnClick(e); // 중복 등록 방지
                c.Click += (s, e) => this.OnClick(e);

                if (c.HasChildren)
                    RegisterClickRecursive(c);
            }
        }

        /*
         * RegisterClickRecursive(Control root, EventHandler handler)
         * - 지정한 컨트롤과 자식 컨트롤에 클릭 이벤트 핸들러 등록
         */
        private void RegisterClickRecursive(Control root, EventHandler handler)
        {
            foreach (Control c in root.Controls)
            {
                c.Click -= handler; // 중복 방지
                c.Click += handler;
                if (c.HasChildren) RegisterClickRecursive(c, handler);
            }
        }

        /*
         * InitLevel(ushort level)
         * - 레벨 아이콘 초기화 메서드
         * - level: 캐릭터의 레벨 (0~3)
         */
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

        /*
         * InitStatus(CustomFlowLayoutPanel panel, Stat stat)
         * - 상태이상 패널 초기화 메서드
         * - panel: 상태이상 아이콘을 표시할 패널
         * - stat: 캐릭터의 Stat 객체
         */
        protected void InitStatus(CustomFlowLayoutPanel panel, Stat stat)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            if (stat.StatusEffects.Count > 0)
            {
                panel.Visible = true;
                AddStatusEffectIcons(panel, stat.StatusEffects);
            }
            else
            {
                panel.Visible = false;
            }

            panel.ResumeLayout();
        }

        /*
         * AddStatusEffectIcons(CustomFlowLayoutPanel panel, List<StatusEffect> statusEffects)
         * - 상태이상 아이콘을 패널에 추가하는 메서드
         * - 최대 3개 아이콘을 표시하고, 4개 이상일 경우 +N 표시
         * - panel: 상태이상 아이콘을 표시할 패널
         * - statusEffects: 캐릭터의 상태이상 리스트
         */
        private void AddStatusEffectIcons(CustomFlowLayoutPanel panel, List<StatusEffect> statusEffects)
        {
            panel.Controls.Clear(); // 기존 아이콘 제거

            int iconSize = panel.Height;
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
                        panel.Controls.Add(label);
                    }
                    break;
                }

                CreateStatusEffectIcon(panel, effect, iconSize, margin);
                count++;
            }
        }

        /*
         * CreateStatusEffectIcon(CustomFlowLayoutPanel panel, StatusEffect effect, int size, int margin)
         * - 상태이상 아이콘을 생성하고 패널에 추가하는 메서드
         * - panel: 상태이상 아이콘을 표시할 패널
         * - effect: 상태이상 객체
         * - size: 아이콘 크기 (정사각형)
         * - margin: 아이콘 오른쪽 마진
         */
        private void CreateStatusEffectIcon(CustomFlowLayoutPanel panel, StatusEffect effect, int size, int margin)
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
            panel.Controls.Add(pb);
        }

        /*
         * InitArtifact(CustomFlowLayoutPanel panel, List<Artifact?> artifacts, ushort maxSlots)
         * - 유물 패널 초기화 메서드
         * - panel: 유물 아이콘을 표시할 패널
         * - artifacts: 캐릭터의 유물 리스트
         */
        protected void InitArtifact(CustomFlowLayoutPanel panel, List<Artifact?> artifacts, ushort maxSlots)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

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

            panel.ResumeLayout();
        }

        /*
         * SetArtifactImage(Artifact artifact, int i)
         * - 유물 아이콘을 패널에 추가하는 메서드
         * - artifact: 유물 객체
         * - i: 유물 슬롯 인덱스 (0: 무기, 1: 방어구, 2~3: 액세서리)
         */
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

        /*
         * SetDefaultArtifactSlot(int index)
         * - 유물 슬롯이 비어있을 때 기본 아이콘을 표시하는 메서드
         * - index: 유물 슬롯 인덱스 (0: 무기, 1: 방어구, 2~3: 액세서리)
         */
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
                CustomFlowLayoutPanel fpnMinion = CreateMinionPanel();
                TransparentTextLabel lblMinionName = CreateMinionLabel(m.Name);
                CustomFlowLayoutPanel fpnStatusInfo = CreateMinionStatusPanel(m.Stat, m.ArtifactSlot, m.MaxArtifactSlot);
                HealthBar hbMinion = CreateMinionHealthBar(m.Id, m.Stat);

                fpnMinion.Controls.Add(lblMinionName);
                fpnMinion.Controls.Add(fpnStatusInfo);
                fpnMinion.Controls.Add(hbMinion);
                PnInfo.Controls.Add(fpnMinion);

                // 소환수 클릭 이벤트 할당
                EventHandler minionClick = (s, e) => FindAncestor<ScoreBoardControl>(this)?.ShowDetail(m);
                fpnMinion.Click += (s, e) => minionClick(s, e);
                RegisterClickRecursive(fpnMinion, minionClick);
            }
        }

        /*
         * CreateMinionHealthBar(string minionId)
         * - 소환수 체력바 생성 메서드
         * - minionId: 소환수 ID
         */
        private CustomFlowLayoutPanel CreateMinionPanel()
        {
            return new()
            {
                Width = (int)(PnPlayer.Width * 0.8),
                AutoSize = true,
                Margin = new Padding(0, 15, 0, 0),
                FlowDirection = FlowDirection.TopDown,
                Tag = "minion",
            };
        }

        /*
         * CreateMinionLabel(string name)
         * - 소환수 이름 라벨 생성 메서드
         * - name: 소환수 이름
         */
        private TransparentTextLabel CreateMinionLabel(string name)
        {
            return new()
            {
                Text = name,
                Font = new Font("Danjo-bold", (int)(LblName.Font.Size * 0.8)),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                Tag = "minion"
            };
        }

        /*
         * CreateMinionStatusPanel(List<StatusEffect> statusEffects, List<Artifact?> artifacts, ushort maxSlots)
         * - 소환수 상태이상 및 유물 패널 생성 메서드
         * - statusEffects: 소환수의 상태이상 리스트
         * - artifacts: 소환수의 유물 리스트
         * - maxSlots: 소환수의 최대 유물 슬롯 수
         */
        private CustomFlowLayoutPanel CreateMinionStatusPanel(Stat stat, List<Artifact?> artifacts, ushort maxSlots)
        {
            CustomFlowLayoutPanel fpnStatusInfo = new()
            {
                Width = (int)(PnInfo.Width * 0.8),
                Height = (int)(FpnStatus.Height * 0.8),
                AutoSize = false,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Tag = "minion",
            };
            if (stat.StatusEffects.Count == 0 && maxSlots == 0)
            {
                fpnStatusInfo.Visible = false;
                return fpnStatusInfo;
            }
            if (stat.StatusEffects.Count > 0)
                fpnStatusInfo.Controls.Add(CreateMinionStatusEffect(stat));
            if (maxSlots > 0)
                fpnStatusInfo.Controls.Add(CreateMinionArtifactPanel(artifacts, maxSlots));
            return fpnStatusInfo;
        }

        /*
         * CreateMinionStatusEffect(List<StatusEffect> statusEffects)
         * - 소환수 상태이상 패널 생성 메서드
         * - statusEffects: 소환수의 상태이상 리스트
         */
        private CustomFlowLayoutPanel CreateMinionStatusEffect(Stat stat)
        {
            CustomFlowLayoutPanel fpnStatus = new()
            {
                Width = (int)(FpnStatus.Width * 0.8),
                Height = (int)(FpnStatus.Height * 0.8),
                AutoSize = false,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Tag = "minion",
            };

            InitStatus(fpnStatus, stat);
            return fpnStatus;
        }

        /*
         * CreateMinionArtifactPanel(List<Artifact?> artifacts, ushort maxSlots)
         * - 소환수 유물 패널 생성 메서드
         * - artifacts: 소환수의 유물 리스트
         * - maxSlots: 소환수의 최대 유물 슬롯 수
         */
        private CustomFlowLayoutPanel CreateMinionArtifactPanel(List<Artifact?> artifacts, ushort maxSlots)
        {
            CustomFlowLayoutPanel fpnArtifact = new()
            {
                Width = (int)(FpnArtifact.Width * 0.8),
                Height = (int)(FpnArtifact.Height * 0.8),
                AutoSize = false,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = new Padding(8, 0, 0, 0),
                Tag = "minion",
            };

            InitArtifact(fpnArtifact, artifacts, maxSlots);
            return fpnArtifact;
        }

        /*
         * CreateMinionHealthBar(string minionId, Stat stat)
         * - 소환수 체력바 생성 메서드
         * - minionId: 소환수 ID
         * - stat: 소환수의 Stat 객체
         */
        private HealthBar CreateMinionHealthBar(string name, Stat stat)
        {
            HealthBar hb = new()
            {
                Name = $"hb{name}",
                Width = (int)(HbPlayer.Width * 0.8),
                Height = (int)(HbPlayer.Height * 0.8),
                Margin = new Padding(0, 15, 0, 0),
                BackColor = Color.Transparent,
                Font = new Font("Danjo-bold", (int)(HbPlayer.Font.Size * 0.8)),
                Tag = "minion"
            };
            hb.SetValues(stat.Hp, stat.Shield, stat.MaxHp);

            return hb;
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

        /*
         * FindAncestor<T>(Control start) where T : Control
         * - 지정한 컨트롤의 조상 중 T 타입인 첫 번째 컨트롤을 반환
         */
        protected static T? FindAncestor<T>(Control start) where T : Control
        {
            for (Control? p = start.Parent; p != null; p = p.Parent)
                if (p is T t) return t;
            return null;
        }
    }
}
