namespace ScoreBoard.modals
{
    partial class SelectPlayerForm
    {
        private void InitializeComponent()
        {
            corpsList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            SuspendLayout();
            // 
            // corpsList
            // 
            corpsList.AutoScroll = true;
            corpsList.BackColor = Color.Transparent;
            corpsList.BackgroundImage = Properties.Resources.ImgMajorListBackground;
            corpsList.BackgroundImageLayout = ImageLayout.Stretch;
            corpsList.Location = new Point(92, 68);
            corpsList.Name = "corpsList";
            corpsList.Size = new Size(194, 720);
            corpsList.TabIndex = 3;
            corpsList.Scroll += corpsList_Scroll;
            corpsList.Paint += corpsList_Paint;
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
            Controls.Add(corpsList);
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
            ResumeLayout(false);
        }
        private controls.CustomFlowLayoutPanel corpsList;
    }
}