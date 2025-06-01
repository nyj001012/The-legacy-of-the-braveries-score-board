using ScoreBoard.controls;
using ScoreBoard.data;
using ScoreBoard.data.character;
using ScoreBoard.Properties;
using ScoreBoard.utils;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace ScoreBoard.modals
{
    public partial class SelectPlayerForm : Form
    {
        private int labelHeight = 0;
        private int verticalSpace = 20;
        public string? SelectedPlayerId { get; private set; }

        public SelectPlayerForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 입력을 우선하여 받을 수 있도록 설정
            ShowCorps(); // 군단 리스트 표시
        }

        /*
         * ShowCorps()
         * 군단 JSON 파일을 읽어와서 레이블로 표시하는 메서드
         */
        private void ShowCorps()
        {
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
        }

        /*
         * ShowMemberStat(string memberId)
         * - memberId: 병사 ID
         * - 해당 병사의 정보를 표시하는 메서드
         */
        private void ShowMemberStat(string memberId)
        {
            SelectedPlayerId = memberId;
            CorpsMember member = GetMember(memberId);
            ShowMemberImage(memberId);
            ShowMemberStatText(member);
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
            var sb = new StringBuilder()
                .AppendLine($"이름: {member.Name}")
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
            sb.AppendLine(" ");
            foreach (var line in member.Description)
                sb.AppendLine(line);
            return sb.ToString();
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
    }
}
