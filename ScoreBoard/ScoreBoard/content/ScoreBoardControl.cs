using ScoreBoard.controls;
using ScoreBoard.data;
using ScoreBoard.data.character;
using ScoreBoard.data.stat;
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

namespace ScoreBoard.content
{
    public partial class ScoreBoardControl : UserControl
    {
        private readonly Dictionary<string, CorpsMember> _characters;
        private readonly List<(string id, string name, ushort count)> _monsters;

        public ScoreBoardControl(Dictionary<string, CorpsMember> characters, List<(string id, string name, ushort count)> monsters)
        {
            _characters = characters ?? throw new ArgumentNullException(nameof(characters), "캐릭터는 비어있을 수 없습니다.");
            _monsters = monsters ?? throw new ArgumentNullException(nameof(monsters), "몬스터를 선택해야 합니다.");
            InitializeComponent();
        }

        private void ScoreBoardControl_Load(object sender, EventArgs e)
        {
            BeginInvoke(() => InitPlayerList());
            BeginInvoke(() => InitEnemyList());
            ScrollBarManager.SetScrollBar(playerContainer, playerList, playerScrollBar);
        }

        /*
         * enemyList 컨트롤에 적 정보를 초기화
         */
        private void InitEnemyList()
        {
            enemyList.SuspendLayout();
            enemyList.Controls.Clear(); // 기존 컨트롤 제거

            foreach (var (id, name, count) in _monsters)
            {
                // EnemyPanel 생성자에 id, name, count 받기
                EnemyPanel enemyControl = new EnemyPanel(id, name, count);
                enemyControl.Name = $"pn{id}";
                enemyList.Controls.Add(enemyControl);
            }

            enemyList.ResumeLayout();
        }

        /*
         * playerList 컨트롤에 캐릭터 정보를 초기화
         */
        private void InitPlayerList()
        {
            int index = 1;
            foreach (var character in _characters.Values)
            {
                if (playerList.Controls.Find($"pn{index}P", true)[0] is not DoubleBufferedPanel playerControl)
                {
                    throw new InvalidOperationException($"pn{index}P 컨트롤을 찾지 못했습니다.");
                }
                if (playerControl.Controls.Find($"lbl{index}PName", true)[0] is not GradientLabel nameLabel
                    || playerControl.Controls.Find($"pb{index}PLv", true)[0] is not PictureBox levelPictureBox
                    || playerControl.Controls.Find($"fpn{index}PStatus", true)[0] is not CustomFlowLayoutPanel statusFlowLayoutPanel
                    || playerControl.Controls.Find($"fpn{index}PArtifact", true)[0] is not CustomFlowLayoutPanel artifactFlowLayoutPanel
                    || playerControl.Controls.Find($"hb{index}P", true)[0] is not HealthBar healthBar)
                {
                    throw new InvalidOperationException($"컨트롤 중 하나가 누락되었습니다: {playerControl.Name}");
                }
                nameLabel.Text = character.Name;
                InitLevel(levelPictureBox, character.Level);
                InitStatus(statusFlowLayoutPanel, character.Stat);
                InitArtifact(artifactFlowLayoutPanel, character.ArtifactSlot, character.MaxArtifactSlot);
                healthBar.SetValues(character.Stat.Hp, character.Stat.Shield, character.Stat.MaxHp);
                playerList.Controls.Add(playerControl);
                index++;
            }
        }

        /*
         * picturebox에 레벨 이미지를 초기화
         */
        private void InitLevel(PictureBox levelPictureBox, ushort level)
        {
            if (level < 0 || level > 3) throw new ArgumentOutOfRangeException(nameof(level), "레벨은 0에서 3 사이여야 합니다.");
            levelPictureBox.Image = level switch
            {
                0 => null, // 레벨 0은 이미지 없음
                1 => Properties.Resources.Lv1, // 레벨 1 이미지
                2 => Properties.Resources.Lv2, // 레벨 2 이미지
                3 => Properties.Resources.Lv3, // 레벨 3 이미지
                _ => throw new NotImplementedException($"{level}은(는) 유효한 레벨이 아닙니다."),
            };
        }

        /*
         * fpn{index}PStatus 컨트롤에 상태이상 정보를 초기화
         */
        private void InitStatus(CustomFlowLayoutPanel statusFlowLayoutPanel, Stat stat)
        {
            statusFlowLayoutPanel.SuspendLayout();
            statusFlowLayoutPanel.Controls.Clear(); // 기존 컨트롤 제거
            if (stat.StatusEffects.Count > 0)
            {
                statusFlowLayoutPanel.Visible = true; // 상태이상 정보가 있으면 보임 처리
                // TODO: stat에서 상태이상 정보를 가져와서 동적으로 추가
            }
            else
            {
                statusFlowLayoutPanel.Visible = false; // 상태이상 정보가 없으면 숨김 처리
            }
            statusFlowLayoutPanel.ResumeLayout();
        }

        /*
         * fpn{index}PArtifact 컨트롤에 유물 정보를 초기화
         */
        private void InitArtifact(CustomFlowLayoutPanel artifactFlowLayoutPanel, List<Artifact> artifacts, ushort maxSlots)
        {
            int size = artifactFlowLayoutPanel.Size.Height;
            artifactFlowLayoutPanel.SuspendLayout();
            artifactFlowLayoutPanel.Controls.Clear(); // 기존 컨트롤 제거
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
                    artifactFlowLayoutPanel.Controls.Add(emptySlot);
                }
            }
            artifactFlowLayoutPanel.ResumeLayout();
        }
    }
}
