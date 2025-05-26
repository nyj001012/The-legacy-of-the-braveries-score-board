namespace ScoreBoard.modals
{
    partial class SelectPlayerForm
    {
        private void InitializeComponent()
        {
            corpsList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            corpsScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            corpsListContainer = new Panel();
            MembersListContainer = new Panel();
            membersList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            membersScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            corpsListContainer.SuspendLayout();
            MembersListContainer.SuspendLayout();
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
            MembersListContainer.Size = new Size(194, 720);
            MembersListContainer.TabIndex = 6;
            // 
            // membersList
            // 
            membersList.BackColor = Color.Transparent;
            membersList.FlowDirection = FlowDirection.TopDown;
            membersList.Location = new Point(301, 68);
            membersList.Name = "membersList";
            membersList.Size = new Size(194, 714);
            membersList.TabIndex = 0;
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
            // SelectPlayerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImage = Properties.Resources.ImgModalBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1452, 860);
            Controls.Add(membersList);
            Controls.Add(MembersListContainer);
            Controls.Add(corpsListContainer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectPlayerForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "SelectPlayer";
            TopMost = true;
            TransparencyKey = Color.FromArgb(64, 64, 64);
            KeyPress += SelectPlayerForm_KeyPress;
            corpsListContainer.ResumeLayout(false);
            MembersListContainer.ResumeLayout(false);
            ResumeLayout(false);
        }
        private ReaLTaiizor.Controls.CyberScrollBar corpsScrollBar;
        private controls.CustomFlowLayoutPanel corpsList;
        private Panel corpsListContainer;
        private Panel MembersListContainer;
        private controls.CustomFlowLayoutPanel membersList;
        private ReaLTaiizor.Controls.CyberScrollBar membersScrollBar;
    }
}