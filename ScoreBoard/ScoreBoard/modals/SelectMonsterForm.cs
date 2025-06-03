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
    public partial class SelectMonsterForm : Form
    {
        private int labelHeight = 0; // 레이블의 총 높이. 동적 레이블 높이를 계산하기 위해 사용
        private readonly int verticalSpace = 20; // 레이블 간의 수직 여백. 동적으로 레이블을 생성할 때 사용

        internal SelectMonsterForm(Dictionary<string, CorpsMember> selectedCharacters)
        {
            this.DoubleBuffered = true; // 폼의 더블 버퍼링 활성화
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 입력을 우선하여 받을 수 있도록 설정
        }

        private void SelectMonsterForm_Load(object sender, EventArgs e)
        {
            ShowMonsterGrade(); // 몬스터 종류 표시
        }

        /*
         * ShowMonsterType()
         * 몬스터 종류 JSON 파일을 읽어와서 레이블로 표시하는 메서드
         */
        private void ShowMonsterGrade()
        {
            this.SuspendLayout(); // 폼 로드 중 레이아웃 업데이트 일시 중지
            // 군단 JSON 파일 읽고 리스트에 표시
            Dictionary<string, MonsterGrade>? MonsterGradeDict = DataReader.ReadMonsterGrade();

            if (MonsterGradeDict == null)
            {
                MessageBox.Show("몬스터 정보가 없습니다.");
                return;
            }
            foreach (var grade in MonsterGradeDict)
            {
                var label = CreateListItem(grade.Key);

                label.MouseEnter += (s, e) => label.ForeColor = Color.FromArgb(255, 245, 245, 245);
                label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
                label.Click += (s, e) => ShowMonsters(grade.Value.Id);
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
        private void ShowMonsters(ushort gradeId)
        {
            this.SuspendLayout(); // 폼 로드 중 레이아웃 업데이트 일시 중지
            Dictionary<string, string> membersMap = DataReader.ReadMonsterDataByGradeId(gradeId);
            if (membersMap.Count == 0)
            {
                MessageBox.Show("해당 등급의 몬스터 정보가 없습니다.");
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
            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
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

        private void btnDecision_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // 선택된 병사 객체가 있을 경우 OK 반환
            this.Close(); // 폼 닫기
        }
    }
}
