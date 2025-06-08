namespace ScoreBoard.content
{
    partial class ScoreBoardControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            playerContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            playerList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            playerScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            playerContainer.SuspendLayout();
            SuspendLayout();
            // 
            // playerContainer
            // 
            playerContainer.BackColor = Color.Transparent;
            playerContainer.Controls.Add(playerList);
            playerContainer.Controls.Add(playerScrollBar);
            playerContainer.Location = new Point(0, 100);
            playerContainer.Name = "playerContainer";
            playerContainer.Size = new Size(600, 980);
            playerContainer.TabIndex = 0;
            // 
            // playerList
            // 
            playerList.BorderColor = Color.Transparent;
            playerList.BorderThickness = 0;
            playerList.Location = new Point(0, 0);
            playerList.Name = "playerList";
            playerList.Size = new Size(600, 980);
            playerList.TabIndex = 1;
            // 
            // playerScrollBar
            // 
            playerScrollBar.Alpha = 50;
            playerScrollBar.BackColor = Color.Transparent;
            playerScrollBar.Background = true;
            playerScrollBar.Background_WidthPen = 3F;
            playerScrollBar.BackgroundPen = false;
            playerScrollBar.ColorBackground = Color.Transparent;
            playerScrollBar.ColorBackground_1 = Color.Transparent;
            playerScrollBar.ColorBackground_2 = Color.Transparent;
            playerScrollBar.ColorBackground_Pen = Color.Transparent;
            playerScrollBar.ColorBackground_Value_1 = Color.Transparent;
            playerScrollBar.ColorBackground_Value_2 = Color.Transparent;
            playerScrollBar.ColorLighting = Color.Transparent;
            playerScrollBar.ColorPen_1 = Color.Transparent;
            playerScrollBar.ColorPen_2 = Color.Transparent;
            playerScrollBar.ColorScrollBar = Color.Transparent;
            playerScrollBar.ColorScrollBar_Transparency = 255;
            playerScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            playerScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            playerScrollBar.Lighting = false;
            playerScrollBar.LinearGradient_Background = false;
            playerScrollBar.LinearGradient_Value = false;
            playerScrollBar.LinearGradientPen = false;
            playerScrollBar.Location = new Point(574, 0);
            playerScrollBar.Maximum = 100;
            playerScrollBar.Minimum = 0;
            playerScrollBar.Name = "playerScrollBar";
            playerScrollBar.OrientationValue = Orientation.Vertical;
            playerScrollBar.PenWidth = 10;
            playerScrollBar.RGB = false;
            playerScrollBar.Rounding = true;
            playerScrollBar.RoundingInt = 7;
            playerScrollBar.Size = new Size(26, 980);
            playerScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            playerScrollBar.TabIndex = 0;
            playerScrollBar.Tag = "Cyber";
            playerScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            playerScrollBar.ThumbSize = 60;
            playerScrollBar.Timer_RGB = 300;
            playerScrollBar.Value = 0;
            // 
            // ScoreBoardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ImgInGameBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(playerContainer);
            Name = "ScoreBoardControl";
            Size = new Size(1920, 1080);
            playerContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private controls.DoubleBufferedPanel playerContainer;
        private ReaLTaiizor.Controls.CyberScrollBar playerScrollBar;
        private controls.CustomFlowLayoutPanel playerList;
    }
}
