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

        internal SelectMonsterForm()
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
                gradeList.Controls.Add(label);
                labelHeight += label.Height + verticalSpace * 2; // 레이블 높이 + 여백
            }
            ScrollBarManager.SetScrollBar(gradeListContainer, gradeList, gradeScrollBar); // 스크롤바 설정
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
            monsterList.Controls.Clear(); // 이전 병사 리스트 초기화
            foreach (var member in membersMap)
            {
                var label = CreateListItem(member.Value);

                label.MouseEnter += (s, e) => label.ForeColor = Color.FromArgb(255, 245, 245, 245);
                label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
                label.Click += (s, e) => AddToReported(member.Key);
                monsterList.Controls.Add(label);
                labelHeight += label.Height + verticalSpace * 2; // 레이블 높이 + 여백
            }
            ScrollBarManager.SetScrollBar(monsterListContainer, monsterList, monsterScrollBar); // 병사 리스트 스크롤바 설정
            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
        }

        /*
         * AddToReported(string monsterId)
         * - monsterId: 몬스터 ID
         * - 해당 몬스터를 reportedList에 추가
         */
        private void AddToReported(string monsterId)
        {
            this.SuspendLayout(); // 폼 로드 중 레이아웃 업데이트 일시 중지
            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
        }

        /*
         * SelectPlayerForm_KeyPress(object sender, KeyPressEventArgs e)
         * escape 키를 누르면 폼을 닫는 메서드
         */
        private void SelectMonsterForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        /*
         * gradeList_MouseEnter(object sender, EventArgs e)
         * 마우스가 gradeList에 들어오면 포커스를 gradeList로 이동시키는 메서드
         * 스크롤링 우선권을 위함
         */
        private void GradeList_MouseEnter(object sender, EventArgs e)
        {
            gradeList.Focus();
        }

        /*
         * gradeList_MouseWheel(object sender, MouseEventArgs e)
         * 마우스 휠을 돌리면 스크롤바를 조정하는 메서드
         */
        private void GradeList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!gradeScrollBar.Enabled) return;

            int delta = -e.Delta / SystemInformation.MouseWheelScrollDelta * gradeScrollBar.SmallStep;
            int newScrollValue = gradeScrollBar.Value + delta;

            // 스크롤 범위 안에서만 동작하도록 조정
            newScrollValue = Math.Max(gradeScrollBar.Minimum, Math.Min(gradeScrollBar.Maximum, newScrollValue));

            gradeScrollBar.Value = newScrollValue;
            gradeList.Top = -newScrollValue;
        }

        /*
         * monsterList_MouseEnter(object sender, EventArgs e)
         * 마우스가 monsterList에 들어오면 포커스를 monsterList로 이동시키는 메서드
         * 스크롤링 우선권을 위함
         */
        private void monsterList_MouseEnter(object sender, EventArgs e)
        {
            monsterList.Focus();
        }

        private void monsterList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!monsterScrollBar.Enabled) return;

            int delta = -e.Delta / SystemInformation.MouseWheelScrollDelta * monsterScrollBar.SmallStep;
            int newScrollValue = monsterScrollBar.Value + delta;

            // 스크롤 범위 안에서만 동작하도록 조정

            newScrollValue = Math.Max(monsterScrollBar.Minimum, Math.Min(monsterScrollBar.Maximum, newScrollValue));
            monsterScrollBar.Value = newScrollValue;
            monsterList.Top = -newScrollValue;
        }

        private void reportedList_MouseEnter(object sender, EventArgs e)
        {
            reportedList.Focus();
        }

        private void reportedList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!reportedScrollBar.Enabled) return;

            int delta = -e.Delta / SystemInformation.MouseWheelScrollDelta * reportedScrollBar.SmallStep;
            int newScrollValue = reportedScrollBar.Value + delta;

            // 스크롤 범위 안에서만 동작하도록 조정

            newScrollValue = Math.Max(reportedScrollBar.Minimum, Math.Min(reportedScrollBar.Maximum, newScrollValue));
            reportedScrollBar.Value = newScrollValue;
            reportedList.Top = -newScrollValue;
        }
        private void btnDecision_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // 선택된 병사 객체가 있을 경우 OK 반환
            this.Close(); // 폼 닫기
        }
    }
}
