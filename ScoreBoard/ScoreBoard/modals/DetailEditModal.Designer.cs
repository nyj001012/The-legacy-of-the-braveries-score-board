namespace ScoreBoard.modals
{
    partial class DetailEditModal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(229, 176, 0);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Danjo-bold", 23.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.ForeColor = Color.WhiteSmoke;
            textBox1.Location = new Point(3, 3);
            textBox1.Margin = new Padding(0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(214, 39);
            textBox1.TabIndex = 0;
            textBox1.Text = "asdf";
            // 
            // DetailEditModal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(254, 235, 22);
            ClientSize = new Size(220, 45);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DetailEditModal";
            Opacity = 0.8D;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "DetailEditModal";
            TopMost = true;
            KeyDown += DetailEditModal_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TextBox textBox1;
    }
}