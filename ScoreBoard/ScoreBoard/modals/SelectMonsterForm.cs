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
        internal List<(string id, string name, ushort count)> selectedMonsters = []; // 선택된 몬스터들을 저장하는 리스트 (monsterId, count)
        private Point dragStartPoint; // 드래그 시작 지점
        private TransparentTextLabel? draggedLabel = null; // 드래그된 레이블 저장

        internal SelectMonsterForm()
        {
            this.DoubleBuffered = true; // 폼의 더블 버퍼링 활성화
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 입력을 우선하여 받을 수 있도록 설정
            this.reportedList.BorderThickness = 3;
            this.reportedList.BorderColor = Color.FromArgb(100, 245, 245, 245);
            this.gradeScrollBar.Enabled = false;
            this.monsterScrollBar.Enabled = false;
            this.reportedScrollBar.Enabled = false;
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
                var label = CreateListItem(null, grade.Key);
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
        private TransparentTextLabel CreateListItem(string? name, string text)
        {
            var label = new TransparentTextLabel()
            {
                Text = text,
                AutoSize = true,
                Cursor = Cursors.Hand,
                Font = new Font("나눔고딕코딩", 25, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 245, 245, 245),
                Margin = new Padding(0, verticalSpace, 0, verticalSpace)
            };
            label.MouseEnter += (s, e) => label.ForeColor = Color.FromArgb(255, 245, 245, 245);
            label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
            if (name != null)
                label.Name = name;
            return label;
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
                var label = CreateListItem(member.Key, member.Value);
                label.Click += (s, e) => AddToReported(member.Key, member.Value);
                label.DoubleClick += (s, e) => AddToReported(member.Key, member.Value);
                monsterList.Controls.Add(label);
                labelHeight += label.Height + verticalSpace * 2; // 레이블 높이 + 여백
            }
            ScrollBarManager.SetScrollBar(monsterListContainer, monsterList, monsterScrollBar); // 병사 리스트 스크롤바 설정
            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
        }

        /*
         * AddToReported(string monsterId)
         * - monsterName: 몬스터 이름
         * - 해당 몬스터를 reportedList에 추가
         * - 이미 있는 몬스터라면 [monsterName](2) 형식으로 표현
         */
        private void AddToReported(string id, string monsterName)
        {
            this.SuspendLayout(); // 폼 로드 중 레이아웃 업데이트 일시 중지
            // id를 이용하여 이미 추가된 항목인지 확인
            int index = selectedMonsters.FindIndex(monster => monster.id == id);
            if (index == -1)
            {
                // 새로 추가한 몬스터라면 reportedList 및 List에 추가
                (string id, string name, ushort count) newMonsterTuple = (id, monsterName, 1);
                selectedMonsters.Add(newMonsterTuple);
                var newLabel = CreateListItem(id, monsterName);
                newLabel.Click += (s, e) => removeFromReported(newLabel, newMonsterTuple);
                newLabel.DoubleClick += (s, e) => removeFromReported(newLabel, newMonsterTuple);
                MakeDraggable(newLabel);
                reportedList.Controls.Add(newLabel);
                ScrollBarManager.SetScrollBar(reportedContainer, reportedList, reportedScrollBar);
            }
            else
            {
                // 이미 추가되어 있다면 개수 +1
                selectedMonsters[index] = (id, monsterName, (ushort)(selectedMonsters[index].count + 1));
                // 레이블 텍스트 업데이트
                if (reportedList.Controls.Find(id, true)[0] is TransparentTextLabel label)
                {
                    label.Text = $"{monsterName} ({selectedMonsters[index].count})";
                }
            }

            this.ResumeLayout(); // 폼 로드 후 레이아웃 업데이트 재개
        }

        /*
         * MakeDraggable(TransparentTextLabel label)
         * - 레이블을 드래그 가능하게 하는 메서드
         */
        private void MakeDraggable(TransparentTextLabel label)
        {
            label.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    draggedLabel = label;
                    dragStartPoint = e.Location;
                }
            };

            label.MouseMove += (s, e) =>
            {
                if (draggedLabel != null && e.Button == MouseButtons.Left)
                {
                    if (Math.Abs(e.X - dragStartPoint.X) > 4 || Math.Abs(e.Y - dragStartPoint.Y) > 4)
                    {
                        draggedLabel.DoDragDrop(draggedLabel, DragDropEffects.Move);
                        draggedLabel = null;
                    }
                }
            };

            label.MouseUp += (s, e) => draggedLabel = null;
        }


        /*
         * removeFromReported(TransparentTextLabel label, (string id, string name, ushort count) monster)
         * - label: 텍스트를 갱신할 레이블
         * - monster: count를 갱신하거나 삭제할 튜플
         * 해당 monster 튜플을 selectedMonsters 리스트에서 count를 갱신하거나 삭제하고, label의 텍스트를 갱신
         */
        private void removeFromReported(TransparentTextLabel label, (string id, string name, ushort count) monster)
        {
            this.SuspendLayout();
            int index = selectedMonsters.FindIndex(monster => monster.id == label.Name);
            ushort newCount = (ushort)(selectedMonsters[index].count - 1);
            if (newCount == 0)
            {
                selectedMonsters.RemoveAt(index);
                reportedList.Controls.RemoveByKey(monster.id);
                reportedList.Invalidate();
            }
            else
            {
                selectedMonsters[index] = (monster.id, monster.name, newCount);
                label.Text = $"{monster.name} ({newCount})";
            }
            this.ResumeLayout();
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

        private void reportedList_DragLeave(object sender, EventArgs e)
        {
            if (draggedLabel != null)
            {
                // 드래그한 상태에서 reportedList 바깥으로 나감
                var labelToRemove = draggedLabel;
                draggedLabel = null;

                Task.Delay(100).ContinueWith(_ =>
                {
                    if (!IsHandleCreated || IsDisposed) return;
                    Invoke(() =>
                    {
                        Point pt = reportedList.PointToClient(Cursor.Position);
                        if (!reportedList.ClientRectangle.Contains(pt))
                        {
                            int index = selectedMonsters.FindIndex(m => m.id == labelToRemove.Name);
                            if (index != -1)
                            {
                                selectedMonsters.RemoveAt(index);
                                reportedList.Controls.Remove(labelToRemove);
                                reportedList.Invalidate();
                            }
                        }
                    });
                });
            }
        }

        private void reportedList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(typeof(TransparentTextLabel)) == true)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void reportedList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data?.GetData(typeof(TransparentTextLabel)) is not TransparentTextLabel dragged)
                return;

            Point pt = reportedList.PointToClient(Cursor.Position);

            // 바깥으로 드롭한 경우: 제거
            if (!reportedList.ClientRectangle.Contains(pt))
            {
                int index = selectedMonsters.FindIndex(m => m.id == dragged.Name);
                if (index != -1)
                {
                    selectedMonsters.RemoveAt(index);
                    reportedList.Controls.Remove(dragged);
                    reportedList.Invalidate();
                }
                return;
            }

            Control? target = reportedList.GetChildAtPoint(pt);
            if (target is not TransparentTextLabel targetLabel || targetLabel == dragged)
                return;

            int oldIndex = reportedList.Controls.GetChildIndex(dragged);
            int newIndex = reportedList.Controls.GetChildIndex(targetLabel);

            reportedList.Controls.SetChildIndex(dragged, newIndex);
            reportedList.Invalidate();

            // selectedMonsters 재정렬
            string draggedId = dragged.Name;
            var item = selectedMonsters.FirstOrDefault(m => m.id == draggedId);
            selectedMonsters.Remove(item);
            selectedMonsters.Insert(newIndex, item);
        }
    }
}
