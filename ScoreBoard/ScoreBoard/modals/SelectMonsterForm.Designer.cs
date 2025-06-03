namespace ScoreBoard.modals
{
    partial class SelectMonsterForm
    {
        private void InitializeComponent()
        {
            corpsList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            corpsScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            corpsListContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            MembersListContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            membersScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            membersList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            transparentTextLabel1 = new ScoreBoard.controls.TransparentTextLabel();
            statContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            statList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            statScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            panel1 = new ScoreBoard.controls.DoubleBufferedPanel();
            btnDecision = new PictureBox();
            corpsListContainer.SuspendLayout();
            MembersListContainer.SuspendLayout();
            statContainer.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnDecision).BeginInit();
            SuspendLayout();
            // 
            // corpsList
            // 
            corpsList.FlowDirection = FlowDirection.TopDown;
            corpsList.Location = new Point(0, 0);
            corpsList.Name = "corpsList";
            corpsList.Size = new Size(194, 714);
            corpsList.TabIndex = 0;
            corpsList.MouseEnter += corpsList_MouseEnter;
            corpsList.MouseWheel += corpsList_MouseWheel;
            // 
            // corpsScrollBar
            // 
            corpsScrollBar.Alpha = 50;
            corpsScrollBar.BackColor = Color.Transparent;
            corpsScrollBar.Background = true;
            corpsScrollBar.Background_WidthPen = 0F;
            corpsScrollBar.BackgroundPen = false;
            corpsScrollBar.ColorBackground = Color.Transparent;
            corpsScrollBar.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            corpsScrollBar.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            corpsScrollBar.ColorBackground_Pen = Color.Transparent;
            corpsScrollBar.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            corpsScrollBar.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            corpsScrollBar.ColorLighting = Color.FromArgb(29, 200, 238);
            corpsScrollBar.ColorPen_1 = Color.FromArgb(37, 52, 68);
            corpsScrollBar.ColorPen_2 = Color.FromArgb(41, 63, 86);
            corpsScrollBar.ColorScrollBar = Color.FromArgb(29, 200, 238);
            corpsScrollBar.ColorScrollBar_Transparency = 255;
            corpsScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            corpsScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            corpsScrollBar.Lighting = false;
            corpsScrollBar.LinearGradient_Background = false;
            corpsScrollBar.LinearGradient_Value = false;
            corpsScrollBar.LinearGradientPen = false;
            corpsScrollBar.Location = new Point(193, 3);
            corpsScrollBar.Maximum = 100;
            corpsScrollBar.Minimum = 0;
            corpsScrollBar.Name = "corpsScrollBar";
            corpsScrollBar.OrientationValue = Orientation.Vertical;
            corpsScrollBar.PenWidth = 10;
            corpsScrollBar.RGB = false;
            corpsScrollBar.Rounding = false;
            corpsScrollBar.RoundingInt = 7;
            corpsScrollBar.Size = new Size(10, 720);
            corpsScrollBar.SmallStep = 10;
            corpsScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            corpsScrollBar.TabIndex = 5;
            corpsScrollBar.Tag = "Cyber";
            corpsScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            corpsScrollBar.ThumbSize = 150;
            corpsScrollBar.Timer_RGB = 300;
            corpsScrollBar.Value = 0;
            corpsScrollBar.Visible = false;
            // 
            // corpsListContainer
            // 
            corpsListContainer.BackColor = Color.Transparent;
            corpsListContainer.BackgroundImage = Properties.Resources.ImgMajorListBackground;
            corpsListContainer.BackgroundImageLayout = ImageLayout.Stretch;
            corpsListContainer.Controls.Add(corpsList);
            corpsListContainer.Controls.Add(corpsScrollBar);
            corpsListContainer.Location = new Point(92, 68);
            corpsListContainer.Name = "corpsListContainer";
            corpsListContainer.Size = new Size(194, 720);
            corpsListContainer.TabIndex = 5;
            // 
            // MembersListContainer
            // 
            MembersListContainer.BackColor = Color.Transparent;
            MembersListContainer.BackgroundImageLayout = ImageLayout.Stretch;
            MembersListContainer.Controls.Add(membersScrollBar);
            MembersListContainer.Location = new Point(301, 68);
            MembersListContainer.Name = "MembersListContainer";
            MembersListContainer.Size = new Size(506, 720);
            MembersListContainer.TabIndex = 6;
            // 
            // membersScrollBar
            // 
            membersScrollBar.Alpha = 50;
            membersScrollBar.BackColor = Color.Transparent;
            membersScrollBar.Background = true;
            membersScrollBar.Background_WidthPen = 0F;
            membersScrollBar.BackgroundPen = false;
            membersScrollBar.ColorBackground = Color.Transparent;
            membersScrollBar.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            membersScrollBar.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            membersScrollBar.ColorBackground_Pen = Color.Transparent;
            membersScrollBar.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            membersScrollBar.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            membersScrollBar.ColorLighting = Color.FromArgb(29, 200, 238);
            membersScrollBar.ColorPen_1 = Color.FromArgb(37, 52, 68);
            membersScrollBar.ColorPen_2 = Color.FromArgb(41, 63, 86);
            membersScrollBar.ColorScrollBar = Color.FromArgb(29, 200, 238);
            membersScrollBar.ColorScrollBar_Transparency = 255;
            membersScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            membersScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            membersScrollBar.Lighting = false;
            membersScrollBar.LinearGradient_Background = false;
            membersScrollBar.LinearGradient_Value = false;
            membersScrollBar.LinearGradientPen = false;
            membersScrollBar.Location = new Point(193, 3);
            membersScrollBar.Maximum = 100;
            membersScrollBar.Minimum = 0;
            membersScrollBar.Name = "membersScrollBar";
            membersScrollBar.OrientationValue = Orientation.Vertical;
            membersScrollBar.PenWidth = 10;
            membersScrollBar.RGB = false;
            membersScrollBar.Rounding = false;
            membersScrollBar.RoundingInt = 7;
            membersScrollBar.Size = new Size(10, 720);
            membersScrollBar.SmallStep = 10;
            membersScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            membersScrollBar.TabIndex = 5;
            membersScrollBar.Tag = "Cyber";
            membersScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            membersScrollBar.ThumbSize = 150;
            membersScrollBar.Timer_RGB = 300;
            membersScrollBar.Value = 0;
            membersScrollBar.Visible = false;
            // 
            // membersList
            // 
            membersList.BackColor = Color.Transparent;
            membersList.FlowDirection = FlowDirection.TopDown;
            membersList.Location = new Point(301, 68);
            membersList.Name = "membersList";
            membersList.Size = new Size(506, 714);
            membersList.TabIndex = 0;
            // 
            // transparentTextLabel1
            // 
            transparentTextLabel1.AutoSize = true;
            transparentTextLabel1.BackColor = Color.Transparent;
            transparentTextLabel1.Font = new Font("나눔고딕코딩", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            transparentTextLabel1.ForeColor = Color.WhiteSmoke;
            transparentTextLabel1.Location = new Point(1015, 88);
            transparentTextLabel1.Name = "transparentTextLabel1";
            transparentTextLabel1.Size = new Size(177, 35);
            transparentTextLabel1.TabIndex = 0;
            transparentTextLabel1.Text = "보고된 적";
            // 
            // statContainer
            // 
            statContainer.BackColor = Color.Transparent;
            statContainer.Controls.Add(statList);
            statContainer.Controls.Add(statScrollBar);
            statContainer.Location = new Point(0, 3);
            statContainer.Name = "statContainer";
            statContainer.Size = new Size(544, 482);
            statContainer.TabIndex = 11;
            // 
            // statList
            // 
            statList.FlowDirection = FlowDirection.TopDown;
            statList.Location = new Point(3, 3);
            statList.Name = "statList";
            statList.Size = new Size(541, 476);
            statList.TabIndex = 0;
            // 
            // statScrollBar
            // 
            statScrollBar.Alpha = 50;
            statScrollBar.BackColor = Color.Transparent;
            statScrollBar.Background = true;
            statScrollBar.Background_WidthPen = 3F;
            statScrollBar.BackgroundPen = true;
            statScrollBar.ColorBackground = Color.Transparent;
            statScrollBar.ColorBackground_1 = Color.Transparent;
            statScrollBar.ColorBackground_2 = Color.Transparent;
            statScrollBar.ColorBackground_Pen = Color.Transparent;
            statScrollBar.ColorBackground_Value_1 = Color.Transparent;
            statScrollBar.ColorBackground_Value_2 = Color.Transparent;
            statScrollBar.ColorLighting = Color.Transparent;
            statScrollBar.ColorPen_1 = Color.Transparent;
            statScrollBar.ColorPen_2 = Color.Transparent;
            statScrollBar.ColorScrollBar = Color.Transparent;
            statScrollBar.ColorScrollBar_Transparency = 255;
            statScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            statScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            statScrollBar.Lighting = false;
            statScrollBar.LinearGradient_Background = false;
            statScrollBar.LinearGradient_Value = false;
            statScrollBar.LinearGradientPen = false;
            statScrollBar.Location = new Point(382, 3);
            statScrollBar.Maximum = 100;
            statScrollBar.Minimum = 0;
            statScrollBar.Name = "statScrollBar";
            statScrollBar.OrientationValue = Orientation.Vertical;
            statScrollBar.PenWidth = 10;
            statScrollBar.RGB = false;
            statScrollBar.Rounding = true;
            statScrollBar.RoundingInt = 7;
            statScrollBar.Size = new Size(26, 476);
            statScrollBar.SmallStep = 10;
            statScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            statScrollBar.TabIndex = 1;
            statScrollBar.Tag = "Cyber";
            statScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            statScrollBar.ThumbSize = 60;
            statScrollBar.Timer_RGB = 300;
            statScrollBar.Value = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(statContainer);
            panel1.Controls.Add(btnDecision);
            panel1.Location = new Point(828, 144);
            panel1.Name = "panel1";
            panel1.Size = new Size(544, 646);
            panel1.TabIndex = 12;
            // 
            // btnDecision
            // 
            btnDecision.BackColor = Color.Transparent;
            btnDecision.BackgroundImage = Properties.Resources.BtnDecisionInMonitor;
            btnDecision.BackgroundImageLayout = ImageLayout.Stretch;
            btnDecision.Cursor = Cursors.Hand;
            btnDecision.Location = new Point(187, 562);
            btnDecision.Name = "btnDecision";
            btnDecision.Size = new Size(177, 60);
            btnDecision.TabIndex = 9;
            btnDecision.TabStop = false;
            btnDecision.Visible = false;
            // 
            // SelectMonsterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImage = Properties.Resources.ImgModalBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1452, 860);
            Controls.Add(panel1);
            Controls.Add(transparentTextLabel1);
            Controls.Add(membersList);
            Controls.Add(MembersListContainer);
            Controls.Add(corpsListContainer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectMonsterForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "SelectMonster";
            TopMost = true;
            TransparencyKey = Color.FromArgb(64, 64, 64);
            Load += SelectPlayerForm_Load;
            KeyPress += SelectPlayerForm_KeyPress;
            corpsListContainer.ResumeLayout(false);
            MembersListContainer.ResumeLayout(false);
            statContainer.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnDecision).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private ReaLTaiizor.Controls.CyberScrollBar corpsScrollBar;
        private controls.CustomFlowLayoutPanel corpsList;
        private ScoreBoard.controls.DoubleBufferedPanel corpsListContainer;
        private ScoreBoard.controls.DoubleBufferedPanel MembersListContainer;
        private controls.CustomFlowLayoutPanel membersList;
        private ReaLTaiizor.Controls.CyberScrollBar membersScrollBar;
        private controls.TransparentTextLabel transparentTextLabel1;
        private controls.DoubleBufferedPanel statContainer;
        private controls.CustomFlowLayoutPanel statList;
        private ReaLTaiizor.Controls.CyberScrollBar statScrollBar;
        private controls.DoubleBufferedPanel panel1;
        private PictureBox btnDecision;
    }
}