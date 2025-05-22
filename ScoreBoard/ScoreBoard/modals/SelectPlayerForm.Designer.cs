namespace ScoreBoard.modals
{
    partial class SelectPlayerForm
    {
        private void InitializeComponent()
        {
            unitList = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // unitList
            // 
            unitList.BackColor = Color.Transparent;
            unitList.BackgroundImage = Properties.Resources.ImgMajorListBackground;
            unitList.BackgroundImageLayout = ImageLayout.Stretch;
            unitList.FlowDirection = FlowDirection.TopDown;
            unitList.Location = new Point(65, 70);
            unitList.Name = "unitList";
            unitList.Padding = new Padding(5, 10, 5, 10);
            unitList.Size = new Size(194, 720);
            unitList.TabIndex = 2;
            // 
            // SelectPlayerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImage = Properties.Resources.ImgModalBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1452, 860);
            Controls.Add(unitList);
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
        private FlowLayoutPanel unitList;
    }
}