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
            tbInput = new ReaLTaiizor.Controls.DungeonTextBox();
            SuspendLayout();
            // 
            // tbInput
            // 
            tbInput.BackColor = Color.Transparent;
            tbInput.BorderColor = Color.Goldenrod;
            tbInput.EdgeColor = Color.Gold;
            tbInput.Font = new Font("Danjo-bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbInput.ForeColor = Color.WhiteSmoke;
            tbInput.Location = new Point(0, 0);
            tbInput.Margin = new Padding(0);
            tbInput.MaxLength = 32767;
            tbInput.Multiline = false;
            tbInput.Name = "tbInput";
            tbInput.ReadOnly = false;
            tbInput.Size = new Size(220, 45);
            tbInput.TabIndex = 0;
            tbInput.Text = "123456(+234)";
            tbInput.TextAlignment = HorizontalAlignment.Left;
            tbInput.UseSystemPasswordChar = false;
            // 
            // DetailEditModal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Red;
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
            TransparencyKey = Color.Red;
            KeyPress += DetailEditModal_KeyPress;
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.DungeonTextBox tbInput;
    }
}