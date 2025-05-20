namespace TheLagacyOfTheBraveriesScoreBoard
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOrganise = new System.Windows.Forms.PictureBox();
            this.btnDataArchive = new System.Windows.Forms.PictureBox();
            this.btnGuide = new System.Windows.Forms.PictureBox();
            this.btnSecretCode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrganise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDataArchive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGuide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSecretCode)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::TheLagacyOfTheBraveriesScoreBoard.Properties.Resources.ImgBanner;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(665, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(590, 590);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnOrganise
            // 
            this.btnOrganise.BackColor = System.Drawing.Color.Transparent;
            this.btnOrganise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOrganise.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOrganise.Image = global::TheLagacyOfTheBraveriesScoreBoard.Properties.Resources.BtnOrganise;
            this.btnOrganise.Location = new System.Drawing.Point(540, 645);
            this.btnOrganise.Name = "btnOrganise";
            this.btnOrganise.Size = new System.Drawing.Size(392, 104);
            this.btnOrganise.TabIndex = 3;
            this.btnOrganise.TabStop = false;
            this.btnOrganise.Click += new System.EventHandler(this.btnOrganise_Click);
            // 
            // btnDataArchive
            // 
            this.btnDataArchive.BackColor = System.Drawing.Color.Transparent;
            this.btnDataArchive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDataArchive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDataArchive.Image = global::TheLagacyOfTheBraveriesScoreBoard.Properties.Resources.BtnDataArchive;
            this.btnDataArchive.Location = new System.Drawing.Point(987, 645);
            this.btnDataArchive.Name = "btnDataArchive";
            this.btnDataArchive.Size = new System.Drawing.Size(392, 104);
            this.btnDataArchive.TabIndex = 4;
            this.btnDataArchive.TabStop = false;
            this.btnDataArchive.Click += new System.EventHandler(this.btnDataArchive_Click);
            // 
            // btnGuide
            // 
            this.btnGuide.BackColor = System.Drawing.Color.Transparent;
            this.btnGuide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuide.Image = global::TheLagacyOfTheBraveriesScoreBoard.Properties.Resources.BtnGuide;
            this.btnGuide.Location = new System.Drawing.Point(540, 792);
            this.btnGuide.Name = "btnGuide";
            this.btnGuide.Size = new System.Drawing.Size(392, 104);
            this.btnGuide.TabIndex = 5;
            this.btnGuide.TabStop = false;
            this.btnGuide.Click += new System.EventHandler(this.btnGuide_Click);
            // 
            // btnSecretCode
            // 
            this.btnSecretCode.BackColor = System.Drawing.Color.Transparent;
            this.btnSecretCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSecretCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSecretCode.Image = global::TheLagacyOfTheBraveriesScoreBoard.Properties.Resources.BtnSecretCode;
            this.btnSecretCode.Location = new System.Drawing.Point(987, 792);
            this.btnSecretCode.Name = "btnSecretCode";
            this.btnSecretCode.Size = new System.Drawing.Size(392, 104);
            this.btnSecretCode.TabIndex = 6;
            this.btnSecretCode.TabStop = false;
            this.btnSecretCode.Click += new System.EventHandler(this.btnSecretCode_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TheLagacyOfTheBraveriesScoreBoard.Properties.Resources.ImgBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.btnSecretCode);
            this.Controls.Add(this.btnGuide);
            this.Controls.Add(this.btnDataArchive);
            this.Controls.Add(this.btnOrganise);
            this.Controls.Add(this.pictureBox1);
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.Name = "MainForm";
            this.Text = "용사 이야기 점수판";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOrganise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDataArchive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGuide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSecretCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox btnOrganise;
        private System.Windows.Forms.PictureBox btnDataArchive;
        private System.Windows.Forms.PictureBox btnGuide;
        private System.Windows.Forms.PictureBox btnSecretCode;
    }
}

