namespace ScoreBoard.forms
{
    partial class MainForm : System.Windows.Forms.Form // Ensure MainForm inherits from Form
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) // Correctly overrides Dispose
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
            pbGoBack = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbGoBack).BeginInit();
            SuspendLayout();
            // 
            // pbGoBack
            // 
            pbGoBack.BackColor = Color.Transparent;
            pbGoBack.BackgroundImage = Properties.Resources.btnGoBack;
            pbGoBack.BackgroundImageLayout = ImageLayout.Stretch;
            pbGoBack.Cursor = Cursors.Hand;
            pbGoBack.Location = new Point(50, 25);
            pbGoBack.Margin = new Padding(0);
            pbGoBack.Name = "pbGoBack";
            pbGoBack.Size = new Size(70, 70);
            pbGoBack.TabIndex = 0;
            pbGoBack.TabStop = false;
            pbGoBack.Visible = false;
            pbGoBack.Click += pbGoBack_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ImgInGameBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1904, 1041);
            Controls.Add(pbGoBack);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1920, 1080);
            Name = "MainForm";
            Text = "용사 이야기 점수판";
            Shown += MainForm_Shown;
            ((System.ComponentModel.ISupportInitialize)pbGoBack).EndInit();
            ResumeLayout(false);

        }
        #endregion

        private PictureBox pbGoBack;
    }
}

