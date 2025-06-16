using ScoreBoard.controls;
using ScoreBoard.data;
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
            InitPlayerList();
            ScrollBarManager.SetScrollBar(playerContainer, playerList, playerScrollBar);
        }

        /*
         * playerList 컨트롤에 캐릭터 정보를 초기화
         */
        private void InitPlayerList()
        {
            int index = 1;
            playerList.Controls.Clear(); // 기존 컨트롤 제거
            foreach (var character in _characters.Values)
            {
                if (playerList.Controls.Find($"pn{index}PInfo", false)[0] is not DoubleBufferedPanel playerControl)
                {
                    throw new InvalidOperationException($"pn{index}PInfo 컨트롤을 찾지 못했습니다.");
                }
                if (playerControl.Controls.Find($"lbl{index}PName", false)[0] is not GradientLabel nameLabel
                    || playerControl.Controls.Find($"pb{index}PLv", false)[0] is not PictureBox levelPictureBox
                    || playerControl.Controls.Find($"fpn{index}PStatus", false)[0] is not CustomFlowLayoutPanel statusFlowLayoutPanel
                    || playerControl.Controls.Find($"fpn{index}PArtifact", false)[0] is not CustomFlowLayoutPanel artifactFlowLayoutPanel
                    || playerControl.Controls.Find($"hb{index}P", false)[0] is not HealthBar healthBar)
                {
                    throw new InvalidOperationException($"컨트롤 중 하나가 누락되었습니다: {playerControl.Name}");
                }
                nameLabel.Text = character.Name;
                InitLevel(levelPictureBox, character.Level);
                InitStatus(statusFlowLayoutPanel, character.Stat);
                InitArtifact(artifactFlowLayoutPanel, character.ArtifactSlot, character.MaxArtifactSlot);
                healthBar.SetValues(character.Stat.Hp, character.Stat.Shield, character.Stat.MaxHp);
                playerList.Controls.Add(playerControl);
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
            // TODO: stat에서 상태이상 정보를 가져와서 동적으로 추가
            statusFlowLayoutPanel.ResumeLayout();
        }

        /*
         * fpn{index}PArtifact 컨트롤에 유물 정보를 초기화
         */
        private void InitArtifact(CustomFlowLayoutPanel artifactFlowLayoutPanel, List<Artifact> artifacts, ushort maxSlots)
        {
            artifactFlowLayoutPanel.SuspendLayout();
            artifactFlowLayoutPanel.Controls.Clear(); // 기존 컨트롤 제거
            // TODO: 최대 슬롯 수에 따라 유물 아이콘을 동적으로 추가
            // TODO: 빈 슬롯은 빈 아이콘으로 표시
            artifactFlowLayoutPanel.ResumeLayout();
        }
    }
}
