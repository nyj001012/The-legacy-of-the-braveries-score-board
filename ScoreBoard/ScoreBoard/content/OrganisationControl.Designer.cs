namespace ScoreBoard.content
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
            btnSelect1P = new PictureBox();
            btnSelect2P = new PictureBox();
            btnSelect3P = new PictureBox();
            gradientLabel7 = new ScoreBoard.controls.GradientLabel();
            gradientLabel5 = new ScoreBoard.controls.GradientLabel();
            gradientLabel2 = new ScoreBoard.controls.GradientLabel();
            gradientLabel1 = new ScoreBoard.controls.GradientLabel();
            gradientLabel3 = new ScoreBoard.controls.GradientLabel();
            btnSelect4P = new PictureBox();
            btnJoin = new PictureBox();
            lbl1P = new ScoreBoard.controls.GradientLabel();
            lbl2P = new ScoreBoard.controls.GradientLabel();
            lbl3P = new ScoreBoard.controls.GradientLabel();
            lbl4P = new ScoreBoard.controls.GradientLabel();
            ((System.ComponentModel.ISupportInitialize)btnSelect1P).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect2P).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect3P).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect4P).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnJoin).BeginInit();
            SuspendLayout();
            // 
            // btnSelect1P
            // 
            btnSelect1P.BackColor = Color.Transparent;
            btnSelect1P.BackgroundImage = Properties.Resources.BtnSelectCharacter;
            btnSelect1P.BackgroundImageLayout = ImageLayout.Stretch;
            btnSelect1P.Cursor = Cursors.Hand;
            btnSelect1P.Location = new Point(108, 310);
            btnSelect1P.Margin = new Padding(3, 4, 3, 4);
            btnSelect1P.Name = "btnSelect1P";
            btnSelect1P.Size = new Size(345, 535);
            btnSelect1P.TabIndex = 1;
            btnSelect1P.TabStop = false;
            btnSelect1P.Tag = "1";
            btnSelect1P.Click += BtnSelectPlayer_Click;
            // 
            // btnSelect2P
            // 
            btnSelect2P.BackColor = Color.Transparent;
            btnSelect2P.BackgroundImage = Properties.Resources.BtnSelectCharacter;
            btnSelect2P.BackgroundImageLayout = ImageLayout.Stretch;
            btnSelect2P.Cursor = Cursors.Hand;
            btnSelect2P.Location = new Point(561, 310);
            btnSelect2P.Margin = new Padding(3, 4, 3, 4);
            btnSelect2P.Name = "btnSelect2P";
            btnSelect2P.Size = new Size(345, 535);
            btnSelect2P.TabIndex = 4;
            btnSelect2P.TabStop = false;
            btnSelect2P.Tag = "2";
            btnSelect2P.Click += BtnSelectPlayer_Click;
            // 
            // btnSelect3P
            // 
            btnSelect3P.BackColor = Color.Transparent;
            btnSelect3P.BackgroundImage = Properties.Resources.BtnSelectCharacter;
            btnSelect3P.BackgroundImageLayout = ImageLayout.Stretch;
            btnSelect3P.Cursor = Cursors.Hand;
            btnSelect3P.Location = new Point(1014, 310);
            btnSelect3P.Margin = new Padding(3, 4, 3, 4);
            btnSelect3P.Name = "btnSelect3P";
            btnSelect3P.Size = new Size(345, 535);
            btnSelect3P.TabIndex = 7;
            btnSelect3P.TabStop = false;
            btnSelect3P.Tag = "3";
            btnSelect3P.Click += BtnSelectPlayer_Click;
            // 
            // gradientLabel7
            // 
            gradientLabel7.BackColor = Color.Transparent;
            gradientLabel7.Font = new Font("Danjo-bold", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradientLabel7.GradientEnd = Color.FromArgb(107, 89, 50);
            gradientLabel7.GradientStart = Color.FromArgb(209, 162, 97);
            gradientLabel7.Location = new Point(1014, 186);
            gradientLabel7.Margin = new Padding(3, 4, 3, 4);
            gradientLabel7.Name = "gradientLabel7";
            gradientLabel7.Size = new Size(345, 112);
            gradientLabel7.TabIndex = 8;
            gradientLabel7.Text = "3P";
            // 
            // gradientLabel5
            // 
            gradientLabel5.BackColor = Color.Transparent;
            gradientLabel5.Font = new Font("Danjo-bold", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradientLabel5.GradientEnd = Color.FromArgb(107, 89, 50);
            gradientLabel5.GradientStart = Color.FromArgb(209, 162, 97);
            gradientLabel5.Location = new Point(561, 186);
            gradientLabel5.Margin = new Padding(3, 4, 3, 4);
            gradientLabel5.Name = "gradientLabel5";
            gradientLabel5.Size = new Size(345, 112);
            gradientLabel5.TabIndex = 5;
            gradientLabel5.Text = "2P";
            // 
            // gradientLabel2
            // 
            gradientLabel2.BackColor = Color.Transparent;
            gradientLabel2.Font = new Font("Danjo-bold", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradientLabel2.GradientEnd = Color.FromArgb(107, 89, 50);
            gradientLabel2.GradientStart = Color.FromArgb(209, 162, 97);
            gradientLabel2.Location = new Point(108, 186);
            gradientLabel2.Margin = new Padding(3, 4, 3, 4);
            gradientLabel2.Name = "gradientLabel2";
            gradientLabel2.Size = new Size(345, 112);
            gradientLabel2.TabIndex = 2;
            gradientLabel2.Text = "1P";
            // 
            // gradientLabel1
            // 
            gradientLabel1.BackColor = Color.Transparent;
            gradientLabel1.Dock = DockStyle.Top;
            gradientLabel1.Font = new Font("Danjo-bold", 38.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradientLabel1.GradientEnd = Color.FromArgb(107, 89, 50);
            gradientLabel1.GradientStart = Color.FromArgb(209, 162, 97);
            gradientLabel1.Location = new Point(0, 0);
            gradientLabel1.Margin = new Padding(0);
            gradientLabel1.Name = "gradientLabel1";
            gradientLabel1.Size = new Size(1920, 170);
            gradientLabel1.TabIndex = 0;
            gradientLabel1.Text = "어떤 형제와 전장에 나서겠는가?";
            // 
            // gradientLabel3
            // 
            gradientLabel3.BackColor = Color.Transparent;
            gradientLabel3.Font = new Font("Danjo-bold", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gradientLabel3.GradientEnd = Color.FromArgb(107, 89, 50);
            gradientLabel3.GradientStart = Color.FromArgb(209, 162, 97);
            gradientLabel3.Location = new Point(1467, 186);
            gradientLabel3.Margin = new Padding(3, 4, 3, 4);
            gradientLabel3.Name = "gradientLabel3";
            gradientLabel3.Size = new Size(345, 112);
            gradientLabel3.TabIndex = 10;
            gradientLabel3.Text = "4P";
            // 
            // btnSelect4P
            // 
            btnSelect4P.BackColor = Color.Transparent;
            btnSelect4P.BackgroundImage = Properties.Resources.BtnSelectCharacter;
            btnSelect4P.BackgroundImageLayout = ImageLayout.Stretch;
            btnSelect4P.Cursor = Cursors.Hand;
            btnSelect4P.Location = new Point(1467, 310);
            btnSelect4P.Margin = new Padding(3, 4, 3, 4);
            btnSelect4P.Name = "btnSelect4P";
            btnSelect4P.Size = new Size(345, 535);
            btnSelect4P.TabIndex = 9;
            btnSelect4P.TabStop = false;
            btnSelect4P.Tag = "4";
            btnSelect4P.Click += BtnSelectPlayer_Click;
            // 
            // btnJoin
            // 
            btnJoin.BackColor = Color.Transparent;
            btnJoin.BackgroundImage = Properties.Resources.BtnJoin;
            btnJoin.Cursor = Cursors.Hand;
            btnJoin.Enabled = false;
            btnJoin.Location = new Point(842, 913);
            btnJoin.Name = "btnJoin";
            btnJoin.Size = new Size(235, 104);
            btnJoin.TabIndex = 11;
            btnJoin.TabStop = false;
            btnJoin.Visible = false;
            btnJoin.Click += btnJoin_Click;
            // 
            // lbl1P
            // 
            lbl1P.BackColor = Color.Transparent;
            lbl1P.Font = new Font("Danjo-bold", 26.25F);
            lbl1P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl1P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl1P.Location = new Point(108, 852);
            lbl1P.Name = "lbl1P";
            lbl1P.Size = new Size(345, 50);
            lbl1P.TabIndex = 12;
            // 
            // lbl2P
            // 
            lbl2P.BackColor = Color.Transparent;
            lbl2P.Font = new Font("Danjo-bold", 26.25F);
            lbl2P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl2P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl2P.Location = new Point(561, 852);
            lbl2P.Name = "lbl2P";
            lbl2P.Size = new Size(345, 50);
            lbl2P.TabIndex = 13;
            // 
            // lbl3P
            // 
            lbl3P.BackColor = Color.Transparent;
            lbl3P.Font = new Font("Danjo-bold", 26.25F);
            lbl3P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl3P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl3P.Location = new Point(1014, 852);
            lbl3P.Name = "lbl3P";
            lbl3P.Size = new Size(345, 50);
            lbl3P.TabIndex = 14;
            // 
            // lbl4P
            // 
            lbl4P.BackColor = Color.Transparent;
            lbl4P.Font = new Font("Danjo-bold", 26.25F);
            lbl4P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl4P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl4P.Location = new Point(1467, 852);
            lbl4P.Name = "lbl4P";
            lbl4P.Size = new Size(345, 50);
            lbl4P.TabIndex = 15;
            // 
            // OrganisationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ImgInGameBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(lbl4P);
            Controls.Add(lbl3P);
            Controls.Add(lbl2P);
            Controls.Add(lbl1P);
            Controls.Add(btnJoin);
            Controls.Add(gradientLabel3);
            Controls.Add(btnSelect4P);
            Controls.Add(gradientLabel7);
            Controls.Add(btnSelect3P);
            Controls.Add(gradientLabel5);
            Controls.Add(btnSelect2P);
            Controls.Add(gradientLabel2);
            Controls.Add(btnSelect1P);
            Controls.Add(gradientLabel1);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "OrganisationControl";
            Size = new Size(1920, 1080);
            ((System.ComponentModel.ISupportInitialize)btnSelect1P).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect2P).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect3P).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect4P).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnJoin).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private controls.GradientLabel gradientLabel1;
        private System.Windows.Forms.PictureBox btnSelect1P;
        private controls.GradientLabel gradientLabel2;
        private controls.GradientLabel gradientLabel5;
        private System.Windows.Forms.PictureBox btnSelect2P;
        private controls.GradientLabel gradientLabel7;
        private System.Windows.Forms.PictureBox btnSelect3P;
        private controls.GradientLabel gradientLabel3;
        private System.Windows.Forms.PictureBox btnSelect4P;
        private PictureBox btnJoin;
        private controls.GradientLabel lbl1P;
        private controls.GradientLabel lbl2P;
        private controls.GradientLabel lbl3P;
        private controls.GradientLabel lbl4P;
    }
}
