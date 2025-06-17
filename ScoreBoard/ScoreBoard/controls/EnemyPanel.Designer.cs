namespace ScoreBoard.controls
{
    partial class EnemyPanel
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
            pnEnemy = new DoubleBufferedPanel();
            hbEnemy = new HealthBar();
            fpnStatus = new CustomFlowLayoutPanel();
            lblName = new GradientLabel();
            pnEnemy.SuspendLayout();
            SuspendLayout();
            // 
            // pnEnemy
            // 
            pnEnemy.Controls.Add(fpnStatus);
            pnEnemy.Controls.Add(hbEnemy);
            pnEnemy.Controls.Add(lblName);
            pnEnemy.Location = new Point(0, 0);
            pnEnemy.Margin = new Padding(0);
            pnEnemy.Name = "pnEnemy";
            pnEnemy.Size = new Size(546, 172);
            pnEnemy.TabIndex = 4;
            // 
            // hbEnemy
            // 
            hbEnemy.BackColor = Color.Transparent;
            hbEnemy.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hbEnemy.BorderThickness = 3F;
            hbEnemy.CornerRadius = 15;
            hbEnemy.Font = new Font("Danjo-bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hbEnemy.Health = -1;
            hbEnemy.HealthColor = Color.FromArgb(145, 145, 145);
            hbEnemy.Location = new Point(0, 125);
            hbEnemy.Margin = new Padding(0, 15, 0, 15);
            hbEnemy.MaxValue = -1;
            hbEnemy.Name = "hbEnemy";
            hbEnemy.Shield = 0;
            hbEnemy.ShieldColor = Color.FromArgb(245, 245, 245);
            hbEnemy.Size = new Size(545, 34);
            hbEnemy.TabIndex = 4;
            hbEnemy.Text = "healthBar1";
            hbEnemy.TextColor = Color.FromArgb(245, 245, 245);
            hbEnemy.TextVisible = true;
            // 
            // fpnStatus
            // 
            fpnStatus.BorderColor = Color.Transparent;
            fpnStatus.BorderThickness = 0;
            fpnStatus.Location = new Point(1, 67);
            fpnStatus.Margin = new Padding(0, 15, 0, 0);
            fpnStatus.Name = "fpnStatus";
            fpnStatus.Size = new Size(545, 45);
            fpnStatus.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Danjo-bold", 23.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.GradientEnd = Color.WhiteSmoke;
            lblName.GradientStart = Color.WhiteSmoke;
            lblName.Location = new Point(0, 16);
            lblName.Margin = new Padding(0, 0, 15, 0);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.No;
            lblName.Size = new Size(544, 45);
            lblName.TabIndex = 0;
            lblName.Text = "미확인";
            lblName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // EnemyPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnEnemy);
            Name = "EnemyPanel";
            Size = new Size(546, 172);
            pnEnemy.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DoubleBufferedPanel pnEnemy;
        private HealthBar hbEnemy;
        private CustomFlowLayoutPanel fpnStatus;
        private GradientLabel lblName;
    }
}
