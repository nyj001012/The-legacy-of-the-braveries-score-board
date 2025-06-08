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
            pn1P = new ScoreBoard.controls.DoubleBufferedPanel();
            pn1PInform = new ScoreBoard.controls.DoubleBufferedPanel();
            healthBar2 = new ScoreBoard.controls.HealthBar();
            fpn1P = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn1PStatus = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn1PArtifact = new ScoreBoard.controls.CustomFlowLayoutPanel();
            pb1PLv = new PictureBox();
            lbl1PName = new ScoreBoard.controls.GradientLabel();
            doubleBufferedPanel1 = new ScoreBoard.controls.DoubleBufferedPanel();
            lbl1P = new ScoreBoard.controls.GradientLabel();
            pn2P = new ScoreBoard.controls.DoubleBufferedPanel();
            doubleBufferedPanel2 = new ScoreBoard.controls.DoubleBufferedPanel();
            lbl2P = new ScoreBoard.controls.GradientLabel();
            pn3P = new ScoreBoard.controls.DoubleBufferedPanel();
            doubleBufferedPanel3 = new ScoreBoard.controls.DoubleBufferedPanel();
            gradientLabel2 = new ScoreBoard.controls.GradientLabel();
            pn4P = new ScoreBoard.controls.DoubleBufferedPanel();
            doubleBufferedPanel4 = new ScoreBoard.controls.DoubleBufferedPanel();
            gradientLabel3 = new ScoreBoard.controls.GradientLabel();
            playerScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            playerContainer.SuspendLayout();
            playerList.SuspendLayout();
            pn1P.SuspendLayout();
            pn1PInform.SuspendLayout();
            fpn1P.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb1PLv).BeginInit();
            doubleBufferedPanel1.SuspendLayout();
            pn2P.SuspendLayout();
            doubleBufferedPanel2.SuspendLayout();
            pn3P.SuspendLayout();
            doubleBufferedPanel3.SuspendLayout();
            pn4P.SuspendLayout();
            doubleBufferedPanel4.SuspendLayout();
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
            playerList.Controls.Add(pn1P);
            playerList.Controls.Add(pn2P);
            playerList.Controls.Add(pn3P);
            playerList.Controls.Add(pn4P);
            playerList.Location = new Point(0, 0);
            playerList.Name = "playerList";
            playerList.Padding = new Padding(15);
            playerList.Size = new Size(600, 980);
            playerList.TabIndex = 1;
            // 
            // pn1P
            // 
            pn1P.Controls.Add(pn1PInform);
            pn1P.Controls.Add(doubleBufferedPanel1);
            pn1P.Location = new Point(30, 30);
            pn1P.Margin = new Padding(15, 15, 0, 15);
            pn1P.Name = "pn1P";
            pn1P.Size = new Size(538, 245);
            pn1P.TabIndex = 0;
            // 
            // pn1PInform
            // 
            pn1PInform.Controls.Add(healthBar2);
            pn1PInform.Controls.Add(fpn1P);
            pn1PInform.Controls.Add(pb1PLv);
            pn1PInform.Controls.Add(lbl1PName);
            pn1PInform.Dock = DockStyle.Right;
            pn1PInform.Location = new Point(89, 0);
            pn1PInform.Name = "pn1PInform";
            pn1PInform.Size = new Size(449, 245);
            pn1PInform.TabIndex = 1;
            // 
            // healthBar2
            // 
            healthBar2.BackColor = Color.Transparent;
            healthBar2.BorderColor = Color.FromArgb(125, 245, 245, 245);
            healthBar2.BorderThickness = 3F;
            healthBar2.CornerRadius = 15;
            healthBar2.Font = new Font("Danjo-bold", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            healthBar2.Health = 70;
            healthBar2.HealthColor = Color.FromArgb(119, 185, 69);
            healthBar2.Location = new Point(3, 132);
            healthBar2.MaxValue = 100;
            healthBar2.Name = "healthBar2";
            healthBar2.Shield = 30;
            healthBar2.ShieldColor = Color.FromArgb(245, 245, 245);
            healthBar2.Size = new Size(440, 29);
            healthBar2.TabIndex = 4;
            healthBar2.Text = "healthBar2";
            healthBar2.TextColor = Color.FromArgb(245, 245, 245);
            healthBar2.TextVisible = true;
            // 
            // fpn1P
            // 
            fpn1P.BorderColor = Color.Transparent;
            fpn1P.BorderThickness = 0;
            fpn1P.Controls.Add(fpn1PStatus);
            fpn1P.Controls.Add(fpn1PArtifact);
            fpn1P.Location = new Point(0, 61);
            fpn1P.Margin = new Padding(0);
            fpn1P.Name = "fpn1P";
            fpn1P.Size = new Size(449, 52);
            fpn1P.TabIndex = 2;
            // 
            // fpn1PStatus
            // 
            fpn1PStatus.BorderColor = Color.Transparent;
            fpn1PStatus.BorderThickness = 0;
            fpn1PStatus.Location = new Point(0, 0);
            fpn1PStatus.Margin = new Padding(0, 0, 5, 0);
            fpn1PStatus.Name = "fpn1PStatus";
            fpn1PStatus.Padding = new Padding(0, 0, 5, 0);
            fpn1PStatus.Size = new Size(221, 52);
            fpn1PStatus.TabIndex = 0;
            // 
            // fpn1PArtifact
            // 
            fpn1PArtifact.BorderColor = Color.Transparent;
            fpn1PArtifact.BorderThickness = 0;
            fpn1PArtifact.Location = new Point(226, 0);
            fpn1PArtifact.Margin = new Padding(0);
            fpn1PArtifact.Name = "fpn1PArtifact";
            fpn1PArtifact.Padding = new Padding(0, 0, 5, 0);
            fpn1PArtifact.Size = new Size(156, 52);
            fpn1PArtifact.TabIndex = 1;
            // 
            // pb1PLv
            // 
            pb1PLv.Location = new Point(391, 3);
            pb1PLv.Name = "pb1PLv";
            pb1PLv.Size = new Size(52, 52);
            pb1PLv.TabIndex = 1;
            pb1PLv.TabStop = false;
            // 
            // lbl1PName
            // 
            lbl1PName.BackColor = Color.Transparent;
            lbl1PName.Font = new Font("Danjo-bold", 23.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl1PName.GradientEnd = Color.FromArgb(72, 144, 170);
            lbl1PName.GradientStart = Color.FromArgb(221, 237, 240);
            lbl1PName.Location = new Point(3, 3);
            lbl1PName.Name = "lbl1PName";
            lbl1PName.RightToLeft = RightToLeft.No;
            lbl1PName.Size = new Size(382, 52);
            lbl1PName.TabIndex = 0;
            lbl1PName.Text = "예시샘플입니다.";
            lbl1PName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // doubleBufferedPanel1
            // 
            doubleBufferedPanel1.Controls.Add(lbl1P);
            doubleBufferedPanel1.Location = new Point(0, 0);
            doubleBufferedPanel1.Margin = new Padding(0);
            doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            doubleBufferedPanel1.Size = new Size(80, 245);
            doubleBufferedPanel1.TabIndex = 0;
            // 
            // lbl1P
            // 
            lbl1P.BackColor = Color.Transparent;
            lbl1P.Dock = DockStyle.Fill;
            lbl1P.Font = new Font("Danjo-bold", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl1P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl1P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl1P.Location = new Point(0, 0);
            lbl1P.Name = "lbl1P";
            lbl1P.Size = new Size(80, 245);
            lbl1P.TabIndex = 0;
            lbl1P.Text = "1P";
            lbl1P.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn2P
            // 
            pn2P.Controls.Add(doubleBufferedPanel2);
            pn2P.Location = new Point(30, 315);
            pn2P.Margin = new Padding(15, 25, 3, 3);
            pn2P.Name = "pn2P";
            pn2P.Size = new Size(505, 200);
            pn2P.TabIndex = 1;
            // 
            // doubleBufferedPanel2
            // 
            doubleBufferedPanel2.Controls.Add(lbl2P);
            doubleBufferedPanel2.Location = new Point(0, 0);
            doubleBufferedPanel2.Margin = new Padding(0);
            doubleBufferedPanel2.Name = "doubleBufferedPanel2";
            doubleBufferedPanel2.Size = new Size(80, 200);
            doubleBufferedPanel2.TabIndex = 1;
            // 
            // lbl2P
            // 
            lbl2P.BackColor = Color.Transparent;
            lbl2P.Dock = DockStyle.Fill;
            lbl2P.Font = new Font("Danjo-bold", 27.7499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl2P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl2P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl2P.Location = new Point(0, 0);
            lbl2P.Name = "lbl2P";
            lbl2P.Size = new Size(80, 200);
            lbl2P.TabIndex = 0;
            lbl2P.Text = "2P";
            lbl2P.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn3P
            // 
            pn3P.Controls.Add(doubleBufferedPanel3);
            pn3P.Location = new Point(30, 543);
            pn3P.Margin = new Padding(15, 25, 3, 3);
            pn3P.Name = "pn3P";
            pn3P.Size = new Size(505, 200);
            pn3P.TabIndex = 2;
            // 
            // doubleBufferedPanel3
            // 
            doubleBufferedPanel3.Controls.Add(gradientLabel2);
            doubleBufferedPanel3.Location = new Point(0, 0);
            doubleBufferedPanel3.Margin = new Padding(0);
            doubleBufferedPanel3.Name = "doubleBufferedPanel3";
            doubleBufferedPanel3.Size = new Size(80, 200);
            doubleBufferedPanel3.TabIndex = 2;
            // 
            // gradientLabel2
            // 
            gradientLabel2.BackColor = Color.Transparent;
            gradientLabel2.Dock = DockStyle.Fill;
            gradientLabel2.Font = new Font("Danjo-bold", 27.7499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradientLabel2.GradientEnd = Color.FromArgb(107, 89, 50);
            gradientLabel2.GradientStart = Color.FromArgb(209, 162, 97);
            gradientLabel2.Location = new Point(0, 0);
            gradientLabel2.Name = "gradientLabel2";
            gradientLabel2.Size = new Size(80, 200);
            gradientLabel2.TabIndex = 0;
            gradientLabel2.Text = "3P";
            gradientLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn4P
            // 
            pn4P.Controls.Add(doubleBufferedPanel4);
            pn4P.Location = new Point(30, 771);
            pn4P.Margin = new Padding(15, 25, 3, 3);
            pn4P.Name = "pn4P";
            pn4P.Size = new Size(505, 200);
            pn4P.TabIndex = 3;
            // 
            // doubleBufferedPanel4
            // 
            doubleBufferedPanel4.Controls.Add(gradientLabel3);
            doubleBufferedPanel4.Location = new Point(0, 0);
            doubleBufferedPanel4.Margin = new Padding(0);
            doubleBufferedPanel4.Name = "doubleBufferedPanel4";
            doubleBufferedPanel4.Size = new Size(80, 200);
            doubleBufferedPanel4.TabIndex = 2;
            // 
            // gradientLabel3
            // 
            gradientLabel3.BackColor = Color.Transparent;
            gradientLabel3.Dock = DockStyle.Fill;
            gradientLabel3.Font = new Font("Danjo-bold", 27.7499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradientLabel3.GradientEnd = Color.FromArgb(107, 89, 50);
            gradientLabel3.GradientStart = Color.FromArgb(209, 162, 97);
            gradientLabel3.Location = new Point(0, 0);
            gradientLabel3.Name = "gradientLabel3";
            gradientLabel3.Size = new Size(80, 200);
            gradientLabel3.TabIndex = 0;
            gradientLabel3.Text = "4P";
            gradientLabel3.TextAlign = ContentAlignment.MiddleCenter;
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
            playerList.ResumeLayout(false);
            pn1P.ResumeLayout(false);
            pn1PInform.ResumeLayout(false);
            fpn1P.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb1PLv).EndInit();
            doubleBufferedPanel1.ResumeLayout(false);
            pn2P.ResumeLayout(false);
            doubleBufferedPanel2.ResumeLayout(false);
            pn3P.ResumeLayout(false);
            doubleBufferedPanel3.ResumeLayout(false);
            pn4P.ResumeLayout(false);
            doubleBufferedPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private controls.DoubleBufferedPanel playerContainer;
        private ReaLTaiizor.Controls.CyberScrollBar playerScrollBar;
        private controls.CustomFlowLayoutPanel playerList;
        private controls.DoubleBufferedPanel pn1P;
        private controls.DoubleBufferedPanel pn2P;
        private controls.DoubleBufferedPanel pn3P;
        private controls.DoubleBufferedPanel pn4P;
        private controls.DoubleBufferedPanel doubleBufferedPanel1;
        private controls.GradientLabel lbl1P;
        private controls.DoubleBufferedPanel doubleBufferedPanel2;
        private controls.GradientLabel lbl2P;
        private controls.DoubleBufferedPanel doubleBufferedPanel3;
        private controls.GradientLabel gradientLabel2;
        private controls.DoubleBufferedPanel doubleBufferedPanel4;
        private controls.GradientLabel gradientLabel3;
        private controls.DoubleBufferedPanel pn1PInform;
        private controls.GradientLabel lbl1PName;
        private PictureBox pb1PLv;
        private controls.CustomFlowLayoutPanel fpn1P;
        private controls.CustomFlowLayoutPanel fpn1PStatus;
        private controls.CustomFlowLayoutPanel fpn1PArtifact;
        private controls.HealthBar healthBar2;
    }
}
