using ScoreBoard.controls;
using ScoreBoard.utils;
using System.Data;
using System.Diagnostics;

namespace ScoreBoard.modals
{
    public partial class SelectPlayerForm : Form
    {
        private int labelHeight = 0;
        private int verticalSpace = 20;
        public int SelectedPlayerId { get; private set; }

        public SelectPlayerForm()
        {
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 입력을 우선하여 받을 수 있도록 설정
            ShowCorps(); // 군단 리스트 표시
            ScrollBarManager.SetScrollBar(corpsListContainer, corpsList, corpsScrollBar); // 스크롤바 설정
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

            foreach (var unit in corpsMap)
            {
                var label = CreateListItem(unit.Value);

                label.MouseEnter += (s, e) => label.ForeColor = Color.FromArgb(255, 245, 245, 245);
                label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
                label.Click += (s, e) => ShowCorpsMembers(unit.Key);
                corpsList.Controls.Add(label);
                labelHeight += label.Height + verticalSpace * 2; // 레이블 높이 + 여백
            }
            corpsList.Height = labelHeight;
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
            // TODO => 선택된 병사 Id 저장 및 병사 정보 표시
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
    }
}
