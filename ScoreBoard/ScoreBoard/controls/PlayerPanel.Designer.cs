namespace ScoreBoard.controls
{
    partial class PlayerPanel
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
            pn2P = new DoubleBufferedPanel();
            lblOrder = new GradientLabel();
            pn2PInfo = new DoubleBufferedPanel();
            hbPlayer = new HealthBar();
            fpnPlayer = new CustomFlowLayoutPanel();
            fpnStatus = new CustomFlowLayoutPanel();
            fpnArtifact = new CustomFlowLayoutPanel();
            pbLv = new PictureBox();
            lblName = new GradientLabel();
            customFlowLayoutPanel1 = new CustomFlowLayoutPanel();
            pn2P.SuspendLayout();
            pn2PInfo.SuspendLayout();
            fpnPlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLv).BeginInit();
            customFlowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pn2P
            // 
            pn2P.Controls.Add(lblOrder);
            pn2P.Controls.Add(pn2PInfo);
            pn2P.Location = new Point(0, 0);
            pn2P.Margin = new Padding(0);
            pn2P.Name = "pn2P";
            pn2P.Size = new Size(511, 146);
            pn2P.TabIndex = 2;
            pn2P.Tag = "2";
            // 
            // lblOrder
            // 
            lblOrder.BackColor = Color.Transparent;
            lblOrder.Font = new Font("Danjo-bold", 27.7499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblOrder.GradientEnd = Color.FromArgb(107, 89, 50);
            lblOrder.GradientStart = Color.FromArgb(209, 162, 97);
            lblOrder.Location = new Point(1, 1);
            lblOrder.Margin = new Padding(0);
            lblOrder.Name = "lblOrder";
            lblOrder.Size = new Size(88, 144);
            lblOrder.TabIndex = 0;
            lblOrder.Text = "2P";
            lblOrder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn2PInfo
            // 
            pn2PInfo.Controls.Add(customFlowLayoutPanel1);
            pn2PInfo.Controls.Add(hbPlayer);
            pn2PInfo.Controls.Add(fpnPlayer);
            pn2PInfo.Location = new Point(92, 0);
            pn2PInfo.Margin = new Padding(0);
            pn2PInfo.Name = "pn2PInfo";
            pn2PInfo.Size = new Size(419, 146);
            pn2PInfo.TabIndex = 2;
            // 
            // hbPlayer
            // 
            hbPlayer.BackColor = Color.Transparent;
            hbPlayer.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hbPlayer.BorderThickness = 3F;
            hbPlayer.CornerRadius = 15;
            hbPlayer.Font = new Font("Danjo-bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hbPlayer.Health = 70;
            hbPlayer.HealthColor = Color.FromArgb(119, 185, 69);
            hbPlayer.Location = new Point(1, 110);
            hbPlayer.Margin = new Padding(0, 10, 0, 10);
            hbPlayer.MaxValue = 100;
            hbPlayer.Name = "hbPlayer";
            hbPlayer.Shield = 30;
            hbPlayer.ShieldColor = Color.FromArgb(225, 225, 225);
            hbPlayer.Size = new Size(413, 26);
            hbPlayer.TabIndex = 4;
            hbPlayer.Text = "healthBar1";
            hbPlayer.TextColor = Color.FromArgb(245, 245, 245);
            hbPlayer.TextVisible = true;
            // 
            // fpnPlayer
            // 
            fpnPlayer.BorderColor = Color.Transparent;
            fpnPlayer.BorderThickness = 0;
            fpnPlayer.Controls.Add(fpnStatus);
            fpnPlayer.Controls.Add(fpnArtifact);
            fpnPlayer.Location = new Point(1, 56);
            fpnPlayer.Margin = new Padding(0, 10, 0, 0);
            fpnPlayer.Name = "fpnPlayer";
            fpnPlayer.Size = new Size(418, 45);
            fpnPlayer.TabIndex = 2;
            // 
            // fpnStatus
            // 
            fpnStatus.BorderColor = Color.Transparent;
            fpnStatus.BorderThickness = 0;
            fpnStatus.Location = new Point(0, 0);
            fpnStatus.Margin = new Padding(0, 0, 10, 0);
            fpnStatus.Name = "fpnStatus";
            fpnStatus.Padding = new Padding(0, 0, 2, 0);
            fpnStatus.Size = new Size(217, 45);
            fpnStatus.TabIndex = 3;
            // 
            // fpnArtifact
            // 
            fpnArtifact.BorderColor = Color.Transparent;
            fpnArtifact.BorderThickness = 0;
            fpnArtifact.Location = new Point(227, 0);
            fpnArtifact.Margin = new Padding(0, 0, 5, 0);
            fpnArtifact.Name = "fpnArtifact";
            fpnArtifact.Padding = new Padding(0, 0, 2, 0);
            fpnArtifact.Size = new Size(186, 45);
            fpnArtifact.TabIndex = 2;
            // 
            // pbLv
            // 
            pbLv.BackgroundImageLayout = ImageLayout.Stretch;
            pbLv.Location = new Point(11, 5);
            pbLv.Margin = new Padding(0);
            pbLv.Name = "pbLv";
            pbLv.Size = new Size(45, 45);
            pbLv.TabIndex = 1;
            pbLv.TabStop = false;
            // 
            // lblName
            // 
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Danjo-bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.GradientEnd = Color.WhiteSmoke;
            lblName.GradientStart = Color.WhiteSmoke;
            lblName.Location = new Point(0, 5);
            lblName.Margin = new Padding(0, 0, 10, 0);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.No;
            lblName.Size = new Size(1, 45);
            lblName.TabIndex = 0;
            lblName.Text = "예시샘플입니다.";
            lblName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // customFlowLayoutPanel1
            // 
            customFlowLayoutPanel1.BorderColor = Color.Transparent;
            customFlowLayoutPanel1.BorderThickness = 0;
            customFlowLayoutPanel1.Controls.Add(lblName);
            customFlowLayoutPanel1.Controls.Add(pbLv);
            customFlowLayoutPanel1.Location = new Point(0, 0);
            customFlowLayoutPanel1.Margin = new Padding(0);
            customFlowLayoutPanel1.Name = "customFlowLayoutPanel1";
            customFlowLayoutPanel1.Padding = new Padding(0, 5, 0, 0);
            customFlowLayoutPanel1.Size = new Size(419, 56);
            customFlowLayoutPanel1.TabIndex = 3;
            // 
            // PlayerPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pn2P);
            Margin = new Padding(15, 25, 15, 25);
            Name = "PlayerPanel";
            Size = new Size(511, 146);
            pn2P.ResumeLayout(false);
            pn2PInfo.ResumeLayout(false);
            fpnPlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLv).EndInit();
            customFlowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedPanel pn2P;
        private GradientLabel lblOrder;
        private DoubleBufferedPanel pn2PInfo;
        private HealthBar hbPlayer;
        private CustomFlowLayoutPanel fpnPlayer;
        private CustomFlowLayoutPanel fpnStatus;
        private CustomFlowLayoutPanel fpnArtifact;
        private PictureBox pbLv;
        private GradientLabel lblName;
        private CustomFlowLayoutPanel customFlowLayoutPanel1;
    }
}
