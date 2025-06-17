namespace ScoreBoard.controls
{
    partial class CurrentPlayerPanel
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
            pn1P = new DoubleBufferedPanel();
            lbl1P = new GradientLabel();
            fpn1P = new CustomFlowLayoutPanel();
            fpn1PStatus = new CustomFlowLayoutPanel();
            fpn1PArtifact = new CustomFlowLayoutPanel();
            pn1PInfo = new DoubleBufferedPanel();
            hb1P = new HealthBar();
            pb1PLv = new PictureBox();
            lbl1PName = new GradientLabel();
            pn1P.SuspendLayout();
            fpn1P.SuspendLayout();
            pn1PInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb1PLv).BeginInit();
            SuspendLayout();
            // 
            // pn1P
            // 
            pn1P.Controls.Add(lbl1P);
            pn1P.Controls.Add(pn1PInfo);
            pn1P.Location = new Point(0, 0);
            pn1P.Margin = new Padding(15, 25, 15, 25);
            pn1P.Name = "pn1P";
            pn1P.Size = new Size(555, 183);
            pn1P.TabIndex = 1;
            pn1P.Tag = "1";
            // 
            // lbl1P
            // 
            lbl1P.Anchor = AnchorStyles.None;
            lbl1P.BackColor = Color.Transparent;
            lbl1P.Font = new Font("Danjo-bold", 33.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl1P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl1P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl1P.Location = new Point(0, 1);
            lbl1P.Margin = new Padding(0);
            lbl1P.Name = "lbl1P";
            lbl1P.Size = new Size(88, 181);
            lbl1P.TabIndex = 0;
            lbl1P.Text = "1P";
            lbl1P.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fpn1P
            // 
            fpn1P.BorderColor = Color.Transparent;
            fpn1P.BorderThickness = 0;
            fpn1P.Controls.Add(fpn1PStatus);
            fpn1P.Controls.Add(fpn1PArtifact);
            fpn1P.Location = new Point(1, 68);
            fpn1P.Margin = new Padding(0, 15, 0, 0);
            fpn1P.Name = "fpn1P";
            fpn1P.Size = new Size(466, 52);
            fpn1P.TabIndex = 2;
            // 
            // fpn1PStatus
            // 
            fpn1PStatus.BorderColor = Color.Transparent;
            fpn1PStatus.BorderThickness = 0;
            fpn1PStatus.Location = new Point(0, 0);
            fpn1PStatus.Margin = new Padding(0, 0, 15, 0);
            fpn1PStatus.Name = "fpn1PStatus";
            fpn1PStatus.Padding = new Padding(0, 0, 5, 0);
            fpn1PStatus.Size = new Size(225, 52);
            fpn1PStatus.TabIndex = 0;
            // 
            // fpn1PArtifact
            // 
            fpn1PArtifact.BorderColor = Color.Transparent;
            fpn1PArtifact.BorderThickness = 0;
            fpn1PArtifact.Location = new Point(240, 0);
            fpn1PArtifact.Margin = new Padding(0);
            fpn1PArtifact.Name = "fpn1PArtifact";
            fpn1PArtifact.Padding = new Padding(0, 0, 5, 0);
            fpn1PArtifact.Size = new Size(225, 52);
            fpn1PArtifact.TabIndex = 1;
            // 
            // pn1PInfo
            // 
            pn1PInfo.Controls.Add(hb1P);
            pn1PInfo.Controls.Add(fpn1P);
            pn1PInfo.Controls.Add(pb1PLv);
            pn1PInfo.Controls.Add(lbl1PName);
            pn1PInfo.Location = new Point(89, 0);
            pn1PInfo.Margin = new Padding(0);
            pn1PInfo.Name = "pn1PInfo";
            pn1PInfo.Size = new Size(466, 182);
            pn1PInfo.TabIndex = 1;
            // 
            // hb1P
            // 
            hb1P.BackColor = Color.Transparent;
            hb1P.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hb1P.BorderThickness = 3F;
            hb1P.CornerRadius = 15;
            hb1P.Font = new Font("Danjo-bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hb1P.Health = 70;
            hb1P.HealthColor = Color.FromArgb(119, 185, 69);
            hb1P.Location = new Point(1, 135);
            hb1P.Margin = new Padding(0, 15, 0, 15);
            hb1P.MaxValue = 100;
            hb1P.Name = "hb1P";
            hb1P.Shield = 30;
            hb1P.ShieldColor = Color.FromArgb(245, 245, 245);
            hb1P.Size = new Size(466, 32);
            hb1P.TabIndex = 4;
            hb1P.Text = "healthBar2";
            hb1P.TextColor = Color.FromArgb(245, 245, 245);
            hb1P.TextVisible = true;
            // 
            // pb1PLv
            // 
            pb1PLv.BackgroundImageLayout = ImageLayout.Stretch;
            pb1PLv.Location = new Point(411, 12);
            pb1PLv.Margin = new Padding(0);
            pb1PLv.Name = "pb1PLv";
            pb1PLv.Size = new Size(53, 53);
            pb1PLv.TabIndex = 1;
            pb1PLv.TabStop = false;
            // 
            // lbl1PName
            // 
            lbl1PName.BackColor = Color.Transparent;
            lbl1PName.Font = new Font("Danjo-bold", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl1PName.GradientEnd = Color.FromArgb(72, 144, 170);
            lbl1PName.GradientStart = Color.FromArgb(221, 237, 240);
            lbl1PName.Location = new Point(0, 13);
            lbl1PName.Margin = new Padding(0, 0, 15, 0);
            lbl1PName.Name = "lbl1PName";
            lbl1PName.RightToLeft = RightToLeft.No;
            lbl1PName.Size = new Size(396, 52);
            lbl1PName.TabIndex = 0;
            lbl1PName.Text = "예시샘플입니다.";
            lbl1PName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // CurrentPlayerPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pn1P);
            Margin = new Padding(15, 25, 15, 25);
            Name = "CurrentPlayerPanel";
            Size = new Size(555, 183);
            pn1P.ResumeLayout(false);
            fpn1P.ResumeLayout(false);
            pn1PInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb1PLv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedPanel pn1P;
        private GradientLabel lbl1P;
        private CustomFlowLayoutPanel fpn1P;
        private CustomFlowLayoutPanel fpn1PStatus;
        private CustomFlowLayoutPanel fpn1PArtifact;
        private DoubleBufferedPanel pn1PInfo;
        private HealthBar hb1P;
        private PictureBox pb1PLv;
        private GradientLabel lbl1PName;
    }
}
