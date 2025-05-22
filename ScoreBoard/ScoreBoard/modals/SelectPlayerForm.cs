using ScoreBoard.controls;
using ScoreBoard.utils;
using System.Diagnostics;

namespace ScoreBoard.modals
{
    public partial class SelectPlayerForm : Form
    {
        private string corpsJsonPath;
        private Dictionary<string, string> corpsMap;
        private int labelHeight = 0;
        private int verticalSpace = 20;
        public int SelectedPlayerId { get; private set; }

        public SelectPlayerForm()
        {
            InitializeComponent();

            // Key Preview
            this.KeyPreview = true;

            // 군단 JSON 파일 읽고 리스트에 표시
            corpsJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "corps.json");
            corpsMap = JsonReader.ReadJsonStringValue(corpsJsonPath);
            ShowCorps();
            ScrollBarManager.SetScrollBar(corpsListContainer, corpsList, corpsScrollBar);
        }

        private void ShowCorps()
        {
            foreach (var unit in corpsMap)
            {
                var label = new TransparentTextLabel
                {
                    Text = unit.Value,
                    Tag = unit.Key,
                    AutoSize = true,
                    Cursor = Cursors.Hand,
                    Font = new Font("나눔고딕코딩", 25, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 245, 245, 245),
                    Margin = new Padding(0, verticalSpace, 0, verticalSpace),
                };

                label.MouseEnter += (s, e) =>
                {
                    label.ForeColor = Color.FromArgb(255, 245, 245, 245);
                    string corpsId = (string)label.Tag;
                    // TODO => ShowCorpsMember(corpsId);
                };
                label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
                corpsList.Controls.Add(label);
                labelHeight += label.Height + verticalSpace * 2; // 레이블 높이 + 여백
            }
            corpsList.Height = labelHeight;
            Debug.WriteLine($"corpsList Height: {corpsList.Height}");
        }

        private void SelectPlayerForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void corpsList_MouseEnter(object sender, EventArgs e)
        {
            corpsList.Focus();
        }

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
