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
            pnPlayer = new DoubleBufferedPanel();
            lblOrder = new GradientLabel();
            pn1PInfo = new DoubleBufferedPanel();
            hbPlayer = new HealthBar();
            fpnPlayer = new CustomFlowLayoutPanel();
            fpnStatus = new CustomFlowLayoutPanel();
            fpnArtifact = new CustomFlowLayoutPanel();
            pbLv = new PictureBox();
            lblName = new GradientLabel();
            pnPlayer.SuspendLayout();
            pn1PInfo.SuspendLayout();
            fpnPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLv).BeginInit();
            SuspendLayout();
            // 
            // pnPlayer
            // 
            pnPlayer.Controls.Add(lblOrder);
            pnPlayer.Controls.Add(pn1PInfo);
            pnPlayer.Location = new Point(0, 0);
            pnPlayer.Margin = new Padding(15, 25, 15, 25);
            pnPlayer.Name = "pnPlayer";
            pnPlayer.Size = new Size(555, 183);
            pnPlayer.TabIndex = 1;
            pnPlayer.Tag = "1";
            // 
            // lblOrder
            // 
            lblOrder.Anchor = AnchorStyles.None;
            lblOrder.BackColor = Color.Transparent;
            lblOrder.Font = new Font("Danjo-bold", 33.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrder.GradientEnd = Color.FromArgb(107, 89, 50);
            lblOrder.GradientStart = Color.FromArgb(209, 162, 97);
            lblOrder.Location = new Point(0, 1);
            lblOrder.Margin = new Padding(0);
            lblOrder.Name = "lblOrder";
            lblOrder.Size = new Size(88, 181);
            lblOrder.TabIndex = 0;
            lblOrder.Text = "1P";
            lblOrder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn1PInfo
            // 
            pn1PInfo.Controls.Add(hbPlayer);
            pn1PInfo.Controls.Add(fpnPlayer);
            pn1PInfo.Controls.Add(pbLv);
            pn1PInfo.Controls.Add(lblName);
            pn1PInfo.Location = new Point(89, 0);
            pn1PInfo.Margin = new Padding(0);
            pn1PInfo.Name = "pn1PInfo";
            pn1PInfo.Size = new Size(466, 182);
            pn1PInfo.TabIndex = 1;
            // 
            // hbPlayer
            // 
            hbPlayer.BackColor = Color.Transparent;
            hbPlayer.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hbPlayer.BorderThickness = 3F;
            hbPlayer.CornerRadius = 15;
            hbPlayer.Font = new Font("Danjo-bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hbPlayer.Health = 70;
            hbPlayer.HealthColor = Color.FromArgb(119, 185, 69);
            hbPlayer.Location = new Point(1, 135);
            hbPlayer.Margin = new Padding(0, 15, 0, 15);
            hbPlayer.MaxValue = 100;
            hbPlayer.Name = "hbPlayer";
            hbPlayer.Shield = 30;
            hbPlayer.ShieldColor = Color.FromArgb(245, 245, 245);
            hbPlayer.Size = new Size(466, 32);
            hbPlayer.TabIndex = 4;
            hbPlayer.Text = "healthBar2";
            hbPlayer.TextColor = Color.FromArgb(245, 245, 245);
            hbPlayer.TextVisible = true;
            // 
            // fpnPlayer
            // 
            fpnPlayer.BorderColor = Color.Transparent;
            fpnPlayer.BorderThickness = 0;
            fpnPlayer.Controls.Add(fpnStatus);
            fpnPlayer.Controls.Add(fpnArtifact);
            fpnPlayer.Location = new Point(1, 68);
            fpnPlayer.Margin = new Padding(0, 15, 0, 0);
            fpnPlayer.Name = "fpnPlayer";
            fpnPlayer.Size = new Size(466, 52);
            fpnPlayer.TabIndex = 2;
            // 
            // fpnStatus
            // 
            fpnStatus.BorderColor = Color.Transparent;
            fpnStatus.BorderThickness = 0;
            fpnStatus.Location = new Point(0, 0);
            fpnStatus.Margin = new Padding(0, 0, 15, 0);
            fpnStatus.Name = "fpnStatus";
            fpnStatus.Padding = new Padding(0, 0, 5, 0);
            fpnStatus.Size = new Size(225, 52);
            fpnStatus.TabIndex = 0;
            // 
            // fpnArtifact
            // 
            fpnArtifact.BorderColor = Color.Transparent;
            fpnArtifact.BorderThickness = 0;
            fpnArtifact.Location = new Point(240, 0);
            fpnArtifact.Margin = new Padding(0);
            fpnArtifact.Name = "fpnArtifact";
            fpnArtifact.Padding = new Padding(0, 0, 5, 0);
            fpnArtifact.Size = new Size(225, 52);
            fpnArtifact.TabIndex = 1;
            // 
            // pbLv
            // 
            pbLv.BackgroundImageLayout = ImageLayout.Stretch;
            pbLv.Location = new Point(411, 12);
            pbLv.Margin = new Padding(0);
            pbLv.Name = "pbLv";
            pbLv.Size = new Size(53, 53);
            pbLv.TabIndex = 1;
            pbLv.TabStop = false;
            // 
            // lblName
            // 
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Danjo-bold", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.GradientEnd = Color.FromArgb(72, 144, 170);
            lblName.GradientStart = Color.FromArgb(221, 237, 240);
            lblName.Location = new Point(0, 13);
            lblName.Margin = new Padding(0, 0, 15, 0);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.No;
            lblName.Size = new Size(396, 52);
            lblName.TabIndex = 0;
            lblName.Text = "예시샘플입니다.";
            lblName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // CurrentPlayerPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnPlayer);
            Margin = new Padding(15, 25, 15, 25);
            Name = "CurrentPlayerPanel";
            Size = new Size(555, 183);
            pnPlayer.ResumeLayout(false);
            pn1PInfo.ResumeLayout(false);
            fpnPlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLv).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedPanel pnPlayer;
        private GradientLabel lblOrder;
        private CustomFlowLayoutPanel fpnPlayer;
        private CustomFlowLayoutPanel fpnStatus;
        private CustomFlowLayoutPanel fpnArtifact;
        private DoubleBufferedPanel pn1PInfo;
        private HealthBar hbPlayer;
        private PictureBox pbLv;
        private GradientLabel lblName;
    }
}
