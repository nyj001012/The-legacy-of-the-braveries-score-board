namespace ScoreBoard.modals
{
    partial class SelectMonsterForm
    {
        private void InitializeComponent()
        {
            gradeList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            gradeScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            gradeListContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            monsterListContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            monsterScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            monsterList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            transparentTextLabel1 = new ScoreBoard.controls.TransparentTextLabel();
            reportedContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            reportedList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            reportedScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            panel1 = new ScoreBoard.controls.DoubleBufferedPanel();
            btnDecision = new PictureBox();
            gradeListContainer.SuspendLayout();
            monsterListContainer.SuspendLayout();
            reportedContainer.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnDecision).BeginInit();
            SuspendLayout();
            // 
            // gradeList
            // 
            gradeList.BorderColor = Color.Transparent;
            gradeList.BorderThickness = 0;
            gradeList.FlowDirection = FlowDirection.TopDown;
            gradeList.Location = new Point(0, 0);
            gradeList.Name = "gradeList";
            gradeList.Size = new Size(194, 714);
            gradeList.TabIndex = 0;
            gradeList.MouseEnter += GradeList_MouseEnter;
            gradeList.MouseWheel += GradeList_MouseWheel;
            // 
            // gradeScrollBar
            // 
            gradeScrollBar.Alpha = 50;
            gradeScrollBar.BackColor = Color.Transparent;
            gradeScrollBar.Background = true;
            gradeScrollBar.Background_WidthPen = 0F;
            gradeScrollBar.BackgroundPen = false;
            gradeScrollBar.ColorBackground = Color.Transparent;
            gradeScrollBar.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            gradeScrollBar.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            gradeScrollBar.ColorBackground_Pen = Color.Transparent;
            gradeScrollBar.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            gradeScrollBar.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            gradeScrollBar.ColorLighting = Color.FromArgb(29, 200, 238);
            gradeScrollBar.ColorPen_1 = Color.FromArgb(37, 52, 68);
            gradeScrollBar.ColorPen_2 = Color.FromArgb(41, 63, 86);
            gradeScrollBar.ColorScrollBar = Color.FromArgb(29, 200, 238);
            gradeScrollBar.ColorScrollBar_Transparency = 255;
            gradeScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            gradeScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            gradeScrollBar.Lighting = false;
            gradeScrollBar.LinearGradient_Background = false;
            gradeScrollBar.LinearGradient_Value = false;
            gradeScrollBar.LinearGradientPen = false;
            gradeScrollBar.Location = new Point(193, 3);
            gradeScrollBar.Maximum = 100;
            gradeScrollBar.Minimum = 0;
            gradeScrollBar.Name = "gradeScrollBar";
            gradeScrollBar.OrientationValue = Orientation.Vertical;
            gradeScrollBar.PenWidth = 10;
            gradeScrollBar.RGB = false;
            gradeScrollBar.Rounding = false;
            gradeScrollBar.RoundingInt = 7;
            gradeScrollBar.Size = new Size(10, 720);
            gradeScrollBar.SmallStep = 10;
            gradeScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gradeScrollBar.TabIndex = 5;
            gradeScrollBar.Tag = "Cyber";
            gradeScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            gradeScrollBar.ThumbSize = 150;
            gradeScrollBar.Timer_RGB = 300;
            gradeScrollBar.Value = 0;
            gradeScrollBar.Visible = false;
            // 
            // gradeListContainer
            // 
            gradeListContainer.BackColor = Color.Transparent;
            gradeListContainer.BackgroundImage = Properties.Resources.ImgMajorListBackground;
            gradeListContainer.BackgroundImageLayout = ImageLayout.Stretch;
            gradeListContainer.Controls.Add(gradeList);
            gradeListContainer.Controls.Add(gradeScrollBar);
            gradeListContainer.Location = new Point(92, 68);
            gradeListContainer.Name = "gradeListContainer";
            gradeListContainer.Size = new Size(194, 720);
            gradeListContainer.TabIndex = 5;
            // 
            // monsterListContainer
            // 
            monsterListContainer.BackColor = Color.Transparent;
            monsterListContainer.BackgroundImageLayout = ImageLayout.Stretch;
            monsterListContainer.Controls.Add(monsterScrollBar);
            monsterListContainer.Location = new Point(301, 68);
            monsterListContainer.Name = "monsterListContainer";
            monsterListContainer.Size = new Size(506, 720);
            monsterListContainer.TabIndex = 6;
            // 
            // monsterScrollBar
            // 
            monsterScrollBar.Alpha = 50;
            monsterScrollBar.BackColor = Color.Transparent;
            monsterScrollBar.Background = true;
            monsterScrollBar.Background_WidthPen = 0F;
            monsterScrollBar.BackgroundPen = false;
            monsterScrollBar.ColorBackground = Color.Transparent;
            monsterScrollBar.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            monsterScrollBar.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            monsterScrollBar.ColorBackground_Pen = Color.Transparent;
            monsterScrollBar.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            monsterScrollBar.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            monsterScrollBar.ColorLighting = Color.FromArgb(29, 200, 238);
            monsterScrollBar.ColorPen_1 = Color.FromArgb(37, 52, 68);
            monsterScrollBar.ColorPen_2 = Color.FromArgb(41, 63, 86);
            monsterScrollBar.ColorScrollBar = Color.FromArgb(29, 200, 238);
            monsterScrollBar.ColorScrollBar_Transparency = 255;
            monsterScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            monsterScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            monsterScrollBar.Lighting = false;
            monsterScrollBar.LinearGradient_Background = false;
            monsterScrollBar.LinearGradient_Value = false;
            monsterScrollBar.LinearGradientPen = false;
            monsterScrollBar.Location = new Point(193, 3);
            monsterScrollBar.Maximum = 100;
            monsterScrollBar.Minimum = 0;
            monsterScrollBar.Name = "monsterScrollBar";
            monsterScrollBar.OrientationValue = Orientation.Vertical;
            monsterScrollBar.PenWidth = 10;
            monsterScrollBar.RGB = false;
            monsterScrollBar.Rounding = false;
            monsterScrollBar.RoundingInt = 7;
            monsterScrollBar.Size = new Size(10, 720);
            monsterScrollBar.SmallStep = 10;
            monsterScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            monsterScrollBar.TabIndex = 5;
            monsterScrollBar.Tag = "Cyber";
            monsterScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            monsterScrollBar.ThumbSize = 150;
            monsterScrollBar.Timer_RGB = 300;
            monsterScrollBar.Value = 0;
            monsterScrollBar.Visible = false;
            // 
            // monsterList
            // 
            monsterList.BackColor = Color.Transparent;
            monsterList.BorderColor = Color.Transparent;
            monsterList.BorderThickness = 0;
            monsterList.FlowDirection = FlowDirection.TopDown;
            monsterList.Location = new Point(301, 68);
            monsterList.Name = "monsterList";
            monsterList.Size = new Size(506, 714);
            monsterList.TabIndex = 0;
            // 
            // transparentTextLabel1
            // 
            transparentTextLabel1.BackColor = Color.Transparent;
            transparentTextLabel1.Font = new Font("나눔고딕코딩", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            transparentTextLabel1.ForeColor = Color.WhiteSmoke;
            transparentTextLabel1.Location = new Point(1034, 94);
            transparentTextLabel1.Name = "transparentTextLabel1";
            transparentTextLabel1.Size = new Size(158, 35);
            transparentTextLabel1.TabIndex = 0;
            transparentTextLabel1.Text = "보고된 적";
            // 
            // reportedContainer
            // 
            reportedContainer.BackColor = Color.Transparent;
            reportedContainer.Controls.Add(reportedList);
            reportedContainer.Controls.Add(reportedScrollBar);
            reportedContainer.Location = new Point(0, 3);
            reportedContainer.Name = "reportedContainer";
            reportedContainer.Size = new Size(544, 517);
            reportedContainer.TabIndex = 11;
            // 
            // reportedList
            // 
            reportedList.AllowDrop = true;
            reportedList.BorderColor = Color.Transparent;
            reportedList.BorderThickness = 0;
            reportedList.FlowDirection = FlowDirection.TopDown;
            reportedList.Location = new Point(3, 3);
            reportedList.Name = "reportedList";
            reportedList.Padding = new Padding(20, 0, 20, 0);
            reportedList.Size = new Size(541, 511);
            reportedList.TabIndex = 0;
            reportedList.ControlAdded += reportedList_UpdateBtnDecisionVisibility;
            reportedList.ControlRemoved += reportedList_UpdateBtnDecisionVisibility;
            reportedList.DragDrop += reportedList_DragDrop;
            reportedList.DragEnter += reportedList_DragEnter;
            reportedList.DragLeave += reportedList_DragLeave;
            reportedList.MouseEnter += reportedList_MouseEnter;
            reportedList.MouseWheel += reportedList_MouseWheel;
            // 
            // reportedScrollBar
            // 
            reportedScrollBar.Alpha = 50;
            reportedScrollBar.BackColor = Color.Transparent;
            reportedScrollBar.Background = true;
            reportedScrollBar.Background_WidthPen = 3F;
            reportedScrollBar.BackgroundPen = true;
            reportedScrollBar.ColorBackground = Color.Transparent;
            reportedScrollBar.ColorBackground_1 = Color.Transparent;
            reportedScrollBar.ColorBackground_2 = Color.Transparent;
            reportedScrollBar.ColorBackground_Pen = Color.Transparent;
            reportedScrollBar.ColorBackground_Value_1 = Color.Transparent;
            reportedScrollBar.ColorBackground_Value_2 = Color.Transparent;
            reportedScrollBar.ColorLighting = Color.Transparent;
            reportedScrollBar.ColorPen_1 = Color.Transparent;
            reportedScrollBar.ColorPen_2 = Color.Transparent;
            reportedScrollBar.ColorScrollBar = Color.Transparent;
            reportedScrollBar.ColorScrollBar_Transparency = 255;
            reportedScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            reportedScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            reportedScrollBar.Lighting = false;
            reportedScrollBar.LinearGradient_Background = false;
            reportedScrollBar.LinearGradient_Value = false;
            reportedScrollBar.LinearGradientPen = false;
            reportedScrollBar.Location = new Point(382, 3);
            reportedScrollBar.Maximum = 100;
            reportedScrollBar.Minimum = 0;
            reportedScrollBar.Name = "reportedScrollBar";
            reportedScrollBar.OrientationValue = Orientation.Vertical;
            reportedScrollBar.PenWidth = 10;
            reportedScrollBar.RGB = false;
            reportedScrollBar.Rounding = true;
            reportedScrollBar.RoundingInt = 7;
            reportedScrollBar.Size = new Size(26, 511);
            reportedScrollBar.SmallStep = 10;
            reportedScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            reportedScrollBar.TabIndex = 1;
            reportedScrollBar.Tag = "Cyber";
            reportedScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            reportedScrollBar.ThumbSize = 60;
            reportedScrollBar.Timer_RGB = 300;
            reportedScrollBar.Value = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(reportedContainer);
            panel1.Controls.Add(btnDecision);
            panel1.Location = new Point(828, 144);
            panel1.Name = "panel1";
            panel1.Size = new Size(544, 638);
            panel1.TabIndex = 12;
            // 
            // btnDecision
            // 
            btnDecision.BackColor = Color.Transparent;
            btnDecision.BackgroundImage = Properties.Resources.BtnDecisionInMonitor;
            btnDecision.BackgroundImageLayout = ImageLayout.Stretch;
            btnDecision.Cursor = Cursors.Hand;
            btnDecision.Location = new Point(187, 550);
            btnDecision.Name = "btnDecision";
            btnDecision.Size = new Size(177, 60);
            btnDecision.TabIndex = 9;
            btnDecision.TabStop = false;
            btnDecision.Visible = false;
            btnDecision.Click += btnDecision_Click;
            // 
            // SelectMonsterForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImage = Properties.Resources.ImgModalBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1452, 860);
            Controls.Add(panel1);
            Controls.Add(transparentTextLabel1);
            Controls.Add(monsterList);
            Controls.Add(monsterListContainer);
            Controls.Add(gradeListContainer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectMonsterForm";
            Padding = new Padding(20, 0, 20, 0);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "SelectMonster";
            TopMost = true;
            TransparencyKey = Color.FromArgb(64, 64, 64);
            Load += SelectMonsterForm_Load;
            KeyPress += SelectMonsterForm_KeyPress;
            gradeListContainer.ResumeLayout(false);
            monsterListContainer.ResumeLayout(false);
            reportedContainer.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnDecision).EndInit();
            ResumeLayout(false);
        }
        private ReaLTaiizor.Controls.CyberScrollBar gradeScrollBar;
        private controls.CustomFlowLayoutPanel gradeList;
        private ScoreBoard.controls.DoubleBufferedPanel gradeListContainer;
        private ScoreBoard.controls.DoubleBufferedPanel monsterListContainer;
        private controls.CustomFlowLayoutPanel monsterList;
        private ReaLTaiizor.Controls.CyberScrollBar monsterScrollBar;
        private controls.TransparentTextLabel transparentTextLabel1;
        private controls.DoubleBufferedPanel reportedContainer;
        private controls.CustomFlowLayoutPanel reportedList;
        private ReaLTaiizor.Controls.CyberScrollBar reportedScrollBar;
        private controls.DoubleBufferedPanel panel1;
        private PictureBox btnDecision;
    }
}