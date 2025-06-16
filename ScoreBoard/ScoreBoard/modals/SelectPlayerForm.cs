using ScoreBoard.controls;
using ScoreBoard.data.character;
using ScoreBoard.data.Skill;
using ScoreBoard.Properties;
using ScoreBoard.utils;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace ScoreBoard.modals
{
    public partial class SelectPlayerForm : Form
    {
        private int labelHeight = 0; // 레이블의 총 높이. 동적 레이블 높이를 계산하기 위해 사용
        private readonly int verticalSpace = 20; // 레이블 간의 수직 여백. 동적으로 레이블을 생성할 때 사용
        internal CorpsMember? SelectedMember = null; // 선택된 병사 객체. 폼이 닫힐 때 반환됨
        private readonly Dictionary<string, CorpsMember> _selectedCharacters = []; // 선택된 병사들을 저장하는 딕셔너리

        internal SelectPlayerForm(Dictionary<string, CorpsMember> selectedCharacters)
        {
            this.DoubleBuffered = true; // 폼의 더블 버퍼링 활성화
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 입력을 우선하여 받을 수 있도록 설정
            this._selectedCharacters = selectedCharacters; // 선택된 병사들을 저장하는 딕셔너리 초기화
            this.corpsScrollBar.Enabled = false;
            this.membersScrollBar.Enabled = false;
            this.statScrollBar.Enabled = false;
        }

        private void SelectPlayerForm_Load(object sender, EventArgs e)
        {
            ShowCorps(); // 군단 리스트 표시
        }

        /*
         * ShowCorps()
         * 군단 JSON 파일을 읽어와서 레이블로 표시하는 메서드
         */
        private void ShowCorps()
        {
            this.SuspendLayout(); // 폼 로드 중 레이아웃 업데이트 일시 중지
            // 군단 JSON 파일 읽고 리스트에 표시
            string corpsJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "corps.json");
            Dictionary<string, string> corpsMap = DataReader.ReadCorpsData(corpsJsonPath);

            if (corpsMap.Count == 0)
            {
                MessageBox.Show("군단 정보가 없습니다.");
                return;
            }
            foreach (var unit in corpsMap)
            {
                var label = CreateListItem(unit.Value);

                label.MouseEnter += (s, e) => label.ForeColor = Color.FromArgb(255, 245, 245, 245);
                label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
                label.Click += (s, e) => ShowCorpsMembers(unit.Key);
                corpsList.Controls.Add(label);
                labelHeight += label.Height + verticalSpace * 2; // 레이블 높이 + 여백
            }
            ScrollBarManager.SetScrollBar(corpsListContainer, corpsList, corpsScrollBar); // 스크롤바 설정
            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
        }

        /*
         * CreateListItem(string text)
         * - text: 레이블에 표시할 텍스트
         * - return: 반투명한 레이블 객체
         */
        private TransparentTextLabel CreateListItem(string text)
        {
            return new TransparentTextLabel
            {
                Text = text,
                AutoSize = true,
                Cursor = Cursors.Hand,
                Font = new Font("나눔고딕코딩", 25, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 245, 245, 245),
                Margin = new Padding(0, verticalSpace, 0, verticalSpace),
            };
        }

        /*
         * ShowCorpsMembers(string corpsId)
         * - corpsId: 군단 ID
         * - 해당 군단의 병사 리스트를 표시하는 메서드
         */
        private void ShowCorpsMembers(string corpsId)
        {
            this.SuspendLayout(); // 폼 로드 중 레이아웃 업데이트 일시 중지
            Dictionary<string, string> membersMap = DataReader.ReadCorpsMembersData(corpsId);
            if (membersMap.Count == 0)
            {
                MessageBox.Show("해당 군단의 병사 정보가 없습니다.");
                return;
            }
            membersList.Controls.Clear(); // 이전 병사 리스트 초기화
            foreach (var member in membersMap)
            {
                var label = CreateListItem(member.Value);

                label.MouseEnter += (s, e) => label.ForeColor = Color.FromArgb(255, 245, 245, 245);
                label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
                label.Click += (s, e) => ShowMemberStat(member.Key);
                membersList.Controls.Add(label);
                labelHeight += label.Height + verticalSpace * 2; // 레이블 높이 + 여백
            }
            ScrollBarManager.SetScrollBar(MembersListContainer, membersList, membersScrollBar); // 병사 리스트 스크롤바 설정
            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
        }

        /*
         * ShowMemberStat(string memberId)
         * - memberId: 병사 ID
         * - 해당 병사의 정보를 표시하는 메서드
         */
        private void ShowMemberStat(string memberId)
        {
            this.SuspendLayout(); // 폼 로드 중 레이아웃 업데이트 일시 중지
            CorpsMember member = GetMember(memberId);
            SelectedMember = member;
            ShowMemberImage(memberId);
            ShowMemberStatText(member);
            // 이미 선택된 병사 객체가 selectedCharacters에 있으면 결정 버튼 비활성화
            btnDecision.Enabled = !_selectedCharacters.Values.Any(m => m.Id == member.Id);
            btnDecision.Visible = btnDecision.Enabled; // 버튼 표시 여부 설정
            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
        }

        /*
         * ShowMemberStatText(CorpsMember member)
         * - member: 병사 객체
         * - 해당 병사의 정보를 레이블로 표시하는 메서드
         */
        private void ShowMemberStatText(CorpsMember member)
        {
            statList.Controls.Clear();
            var label = CreateStatLabel(BuildStatText(member));
            label.SetLineSpacing(1.4f);
            statList.Controls.Add(label);
            statList.Height = Math.Max(statList.Height, label.Height);
            ScrollBarManager.SetScrollBar(statContainer, statList, statScrollBar);
        }

        /*
         * BuildStatText(CorpsMember m)
         * - m: 병사 객체
         * - 해당 병사의 정보를 문자열로 반환하는 메서드
         */
        private string BuildStatText(CorpsMember member)
        {
            var sb = new StringBuilder();
            BuildBasicInfoText(member, sb); // 기본 정보 설정
            sb.AppendLine(" ");
            BuildDescriptionText(member, sb); // 설명 정보 설정
            sb.AppendLine(" ");
            BuildSkillText(member.Passives, member.Actives, sb, 0, "기본"); // 기본 스킬 정보 설정
            BuildSkillText(member.Passives, member.Actives, sb, 1, "1강"); // 1레벨 스킬 정보 설정
            BuildSkillText(member.Passives, member.Actives, sb, 2, "2강"); // 2레벨 스킬 정보 설정
            BuildSkillText(member.Passives, member.Actives, sb, 3, "궁극 강화"); // 3레벨 스킬 정보 설정                

            return sb.ToString();
        }

        /*
         * BuildSkillText(IEnumerable<Skill> passives, IEnumerable<Skill> actives, StringBuilder sb, ushort requiredLevel, string text)
         * 스킬 정보를 문자열로 설정하는 메서드
         * - passives: 패시브 스킬 목록
         * - actives: 액티브 스킬 목록
         * - sb: 문자열 빌더
         * - requiredLevel: 요구 레벨
         * - text: 스킬 레벨에 대한 설명 텍스트 (예: "기본", "1강", "2강", "궁극 강화" 등)
         */
        private void BuildSkillText(IEnumerable<Skill> passives, IEnumerable<Skill> actives, StringBuilder sb, ushort requiredLevel, string text)
        {
            var validSkills = passives.Concat(actives).Where(s => s.RequiredLevel == requiredLevel).ToList();
            if (validSkills.Count == 0) return; // 해당 레벨의 스킬이 없으면 반환

            sb.AppendLine(text + new string('⭐', requiredLevel));
            foreach (var skill in validSkills)
            {
                if (skill is ActiveSkill activeSkill)
                {
                    sb.AppendLine($"[액티브] {activeSkill.Name} (쿨타임: {activeSkill.Cooldown})");
                }
                else if (skill is PassiveSkill passiveSkill)
                {
                    sb.AppendLine($"[패시브] {passiveSkill.Name}");
                }
                foreach (var line in skill.Description)
                {
                    sb.AppendLine($"· {line}");
                }
            }
            foreach (var skill in actives.Where(s => s.RequiredLevel == 1))
            {
                sb.AppendLine($"[액티브] {skill.Name}");
                foreach (var line in skill.Description)
                {
                    sb.AppendLine($"· {line}");
                }
            }
            sb.AppendLine(" ");
        }

        /*
         * BuildSkillText(CorpsMember member, StringBuilder sb)
         * - member: 병사 객체
         * - sb: 문자열 빌더
         * 해당 병사의 배경 설명을 문자열로 설정하는 메서드
         */
        private void BuildDescriptionText(CorpsMember member, StringBuilder sb)
        {
            foreach (var line in member.Description)
                sb.AppendLine(line);
        }

        /*
         * BuildDescriptionText(CorpsMember member, StringBuilder sb)
         * - member: 병사 객체
         * - sb: 문자열 빌더
         * 이름, 체력, 이동 거리 등의 기본 정보를 문자열로 설정하는 메서드
         */
        private void BuildBasicInfoText(CorpsMember member, StringBuilder sb)
        {
            sb.AppendLine($"이름: {member.Name}")
                .AppendLine($"체력: {member.Stat.MaxHp}");

            var types = new[] { "melee", "ranged" };
            var ranges = new List<string>();
            var values = new List<string>();

            foreach (var type in types)
                if (member.Stat.CombatStats.TryGetValue(type, out var s))
                {
                    var label = type == "melee" ? "근거리" : "원거리";
                    ranges.Add($"{label} {s.Range}");
                    values.Add($"{label} {s.Value} {{{s.AttackCount}}}");
                }

            if (ranges.Count > 0) sb.AppendLine("사거리:\n· " + string.Join("\n· ", ranges));
            if (values.Count > 0) sb.AppendLine("공격력:\n· " + string.Join("\n· ", values));
            sb.AppendLine($"이동 거리: {member.Stat.Movement}");
            if (member.Stat.Wisdom != null) sb.AppendLine($"지혜: {member.Stat.Wisdom}");
            if (member.Stat.SpellPower != null) sb.AppendLine($"주문력: {member.Stat.SpellPower}");
        }

        /*
         * CreateStatLabel(string text)
         * - text: 레이블에 표시할 텍스트
         * - return: 반투명한 레이블 객체
         */
        private TransparentTextLabel CreateStatLabel(string text) => new()
        {
            Text = text,
            AutoSize = true,
            Font = new Font("나눔고딕코딩", 22),
            ForeColor = Color.FromArgb(255, 245, 245, 245),
        };


        /*
         * ShowMemberImage(string memberId)
         * - memberId: 병사 ID
         * - 병사의 이미지를 표시하는 메서드
         */
        private void ShowMemberImage(string memberId)
        {
            // 병사 이미지 표시
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "image", "character", $"{memberId}.png");
            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"병사 이미지가 없습니다: {imagePath}");
                return;
            }
            characterImage.Image = Image.FromFile(imagePath);
            characterImage.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        /*
         * GetMember(string memberId)
         * - memberId: 병사 ID
         * - 해당 병사의 객체를 반환하는 메서드
         */
        private CorpsMember GetMember(string memberId)
        {
            switch (memberId)
            {
                case CharacterIds.Ruda:
                    return new Ruda(memberId);
                case CharacterIds.SkyHaneulSoraTen:
                    return new SkyHaneulSoraTen(memberId);
                case CharacterIds.Rudeus:
                    return new Rudeus(memberId);
                case CharacterIds.Valerian:
                    return new Valerian(memberId);
                default:
                    throw new ArgumentException($"잘못된 데이터입니다. memberId: {memberId}");
            }
        }

        /*
         * SelectPlayerForm_KeyPress(object sender, KeyPressEventArgs e)
         * escape 키를 누르면 폼을 닫는 메서드
         */
        private void SelectPlayerForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                SelectedMember = null; // 선택된 병사 객체 초기화
                this.Close();
            }
        }

        /*
         * corpsList_MouseEnter(object sender, EventArgs e)
         * 마우스가 corpsList에 들어오면 포커스를 corpsList로 이동시키는 메서드
         * 스크롤링 우선권을 위함
         */
        private void corpsList_MouseEnter(object sender, EventArgs e)
        {
            corpsList.Focus();
        }

        /*
         * corpsList_MouseWheel(object sender, MouseEventArgs e)
         * 마우스 휠을 돌리면 스크롤바를 조정하는 메서드
         */
        private void corpsList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!corpsScrollBar.Enabled) return;

            int delta = -e.Delta / SystemInformation.MouseWheelScrollDelta * corpsScrollBar.SmallStep;
            int newScrollValue = corpsScrollBar.Value + delta;

            // 스크롤 범위 안에서만 동작하도록 조정
            newScrollValue = Math.Max(corpsScrollBar.Minimum, Math.Min(corpsScrollBar.Maximum, newScrollValue));

            corpsScrollBar.Value = newScrollValue;
            corpsList.Top = -newScrollValue;
        }

        /*
         * statList_MouseEnter(object sender, EventArgs e)
         * 마우스가 statList에 들어오면 포커스를 statList로 이동시키는 메서드
         * 스크롤링 우선권을 위함
         */
        private void statList_MouseEnter(object sender, EventArgs e)
        {
            statList.Focus();
        }

        private void statList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!statScrollBar.Enabled) return;

            int delta = -e.Delta / SystemInformation.MouseWheelScrollDelta * statScrollBar.SmallStep;
            int newScrollValue = statScrollBar.Value + delta;

            // 스크롤 범위 안에서만 동작하도록 조정

            newScrollValue = Math.Max(statScrollBar.Minimum, Math.Min(statScrollBar.Maximum, newScrollValue));
            statScrollBar.Value = newScrollValue;
            statList.Top = -newScrollValue;
        }

        private void btnDecision_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // 선택된 병사 객체가 있을 경우 OK 반환
            this.Close(); // 폼 닫기
        }

        private void membersList_MouseEnter(object sender, EventArgs e)
        {
            membersList.Focus();
        }

        private void memberList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!membersScrollBar.Enabled) return;

            int delta = -e.Delta / SystemInformation.MouseWheelScrollDelta * membersScrollBar.SmallStep;
            int newScrollValue = membersScrollBar.Value + delta;

            // 스크롤 범위 안에서만 동작하도록 조정

            newScrollValue = Math.Max(membersScrollBar.Minimum, Math.Min(membersScrollBar.Maximum, newScrollValue));
            membersScrollBar.Value = newScrollValue;
            membersList.Top = -newScrollValue;
        }
    }
}
