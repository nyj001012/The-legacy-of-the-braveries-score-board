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
            tbInput = new TextBox();
            SuspendLayout();
            // 
            // tbInput
            // 
            tbInput.BackColor = Color.FromArgb(229, 176, 0);
            tbInput.BorderStyle = BorderStyle.None;
            tbInput.Font = new Font("Danjo-bold", 23.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbInput.ForeColor = Color.WhiteSmoke;
            tbInput.Location = new Point(3, 3);
            tbInput.Margin = new Padding(0);
            tbInput.Name = "tbInput";
            tbInput.Size = new Size(214, 39);
            tbInput.TabIndex = 0;
            // 
            // DetailEditModal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(254, 235, 22);
            ClientSize = new Size(220, 45);
            Controls.Add(tbInput);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DetailEditModal";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "DetailEditModal";
            TopMost = true;
            KeyDown += DetailEditModal_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private TextBox tbInput;
    }
}