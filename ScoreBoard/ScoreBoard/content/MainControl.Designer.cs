namespace ScoreBoard
{
    partial class MainControl
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
            pictureBox1 = new PictureBox();
            btnOrganise = new PictureBox();
            btnDataArchive = new PictureBox();
            btnGuide = new PictureBox();
            btnSecretCode = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnOrganise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnDataArchive).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnGuide).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSecretCode).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.ImgBanner;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(665, 25);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(590, 592);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnOrganise
            // 
            btnOrganise.BackColor = Color.Transparent;
            btnOrganise.BackgroundImage = Properties.Resources.BtnOrganise;
            btnOrganise.BackgroundImageLayout = ImageLayout.Stretch;
            btnOrganise.Cursor = Cursors.Hand;
            btnOrganise.Location = new Point(540, 712);
            btnOrganise.Margin = new Padding(3, 4, 3, 4);
            btnOrganise.Name = "btnOrganise";
            btnOrganise.Size = new Size(392, 104);
            btnOrganise.TabIndex = 3;
            btnOrganise.TabStop = false;
            btnOrganise.Click += btnOrganise_Click;
            // 
            // btnDataArchive
            // 
            btnDataArchive.BackColor = Color.Transparent;
            btnDataArchive.BackgroundImageLayout = ImageLayout.Stretch;
            btnDataArchive.Cursor = Cursors.Hand;
            btnDataArchive.Image = Properties.Resources.BtnDataArchive;
            btnDataArchive.Location = new Point(976, 712);
            btnDataArchive.Margin = new Padding(3, 4, 3, 4);
            btnDataArchive.Name = "btnDataArchive";
            btnDataArchive.Size = new Size(392, 104);
            btnDataArchive.TabIndex = 4;
            btnDataArchive.TabStop = false;
            btnDataArchive.Click += btnDataArchive_Click;
            // 
            // btnGuide
            // 
            btnGuide.BackColor = Color.Transparent;
            btnGuide.BackgroundImageLayout = ImageLayout.Stretch;
            btnGuide.Cursor = Cursors.Hand;
            btnGuide.Image = Properties.Resources.BtnGuide;
            btnGuide.Location = new Point(540, 868);
            btnGuide.Margin = new Padding(3, 4, 3, 4);
            btnGuide.Name = "btnGuide";
            btnGuide.Size = new Size(392, 104);
            btnGuide.TabIndex = 5;
            btnGuide.TabStop = false;
            btnGuide.Click += btnGuide_Click;
            // 
            // btnSecretCode
            // 
            btnSecretCode.BackColor = Color.Transparent;
            btnSecretCode.BackgroundImageLayout = ImageLayout.Stretch;
            btnSecretCode.Cursor = Cursors.Hand;
            btnSecretCode.Image = Properties.Resources.BtnSecretCode;
            btnSecretCode.Location = new Point(976, 868);
            btnSecretCode.Margin = new Padding(3, 4, 3, 4);
            btnSecretCode.Name = "btnSecretCode";
            btnSecretCode.Size = new Size(392, 104);
            btnSecretCode.TabIndex = 6;
            btnSecretCode.TabStop = false;
            btnSecretCode.Click += btnSecretCode_Click;
            // 
            // MainControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ImgBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(btnSecretCode);
            Controls.Add(btnGuide);
            Controls.Add(btnDataArchive);
            Controls.Add(btnOrganise);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1920, 1080);
            Name = "MainControl";
            Size = new Size(1920, 1080);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnOrganise).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnDataArchive).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnGuide).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSecretCode).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox btnOrganise;
        private System.Windows.Forms.PictureBox btnDataArchive;
        private System.Windows.Forms.PictureBox btnGuide;
        private System.Windows.Forms.PictureBox btnSecretCode;
    }
}

