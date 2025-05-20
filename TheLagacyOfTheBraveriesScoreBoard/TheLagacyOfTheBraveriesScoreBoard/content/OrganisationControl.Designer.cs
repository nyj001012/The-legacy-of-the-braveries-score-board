namespace TheLagacyOfTheBraveriesScoreBoard.content
{
    partial class OrganisationControl
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
            this.gradientLabel1 = new TheLagacyOfTheBraveriesScoreBoard.controls.GradientLabel();
            this.SuspendLayout();
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.Color.Transparent;
            this.gradientLabel1.Font = new System.Drawing.Font("Danjo-bold", 50.24999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientLabel1.GradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(89)))), ((int)(((byte)(50)))));
            this.gradientLabel1.GradientStart = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(162)))), ((int)(((byte)(97)))));
            this.gradientLabel1.Location = new System.Drawing.Point(0, 0);
            this.gradientLabel1.Margin = new System.Windows.Forms.Padding(0);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(1920, 170);
            this.gradientLabel1.TabIndex = 0;
            this.gradientLabel1.Text = "어떤 형제와 전장에 나서겠는가?";
            // 
            // OrganisationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TheLagacyOfTheBraveriesScoreBoard.Properties.Resources.ImgBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.gradientLabel1);
            this.Name = "OrganisationControl";
            this.Size = new System.Drawing.Size(1920, 1080);
            this.Load += new System.EventHandler(this.OrganisationControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private controls.GradientLabel gradientLabel1;
    }
}
