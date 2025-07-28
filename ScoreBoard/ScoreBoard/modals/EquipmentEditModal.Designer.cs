namespace ScoreBoard.modals
{
    partial class EquipmentEditModal
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
            equipContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            fpnDetails = new ScoreBoard.controls.CustomFlowLayoutPanel();
            equipList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            equipScrollbar = new ReaLTaiizor.Controls.CyberScrollBar();
            lblName = new ScoreBoard.controls.TransparentTextLabel();
            fpnDescription = new ScoreBoard.controls.CustomFlowLayoutPanel();
            equipContainer.SuspendLayout();
            fpnDetails.SuspendLayout();
            SuspendLayout();
            // 
            // equipContainer
            // 
            equipContainer.BackColor = Color.Gold;
            equipContainer.Controls.Add(equipList);
            equipContainer.Controls.Add(equipScrollbar);
            equipContainer.Location = new Point(3, 3);
            equipContainer.Margin = new Padding(0);
            equipContainer.Name = "equipContainer";
            equipContainer.Size = new Size(327, 244);
            equipContainer.TabIndex = 0;
            // 
            // fpnDetails
            // 
            fpnDetails.BackColor = Color.Gold;
            fpnDetails.BorderColor = Color.Transparent;
            fpnDetails.BorderThickness = 0;
            fpnDetails.Controls.Add(lblName);
            fpnDetails.Controls.Add(fpnDescription);
            fpnDetails.FlowDirection = FlowDirection.TopDown;
            fpnDetails.ImeMode = ImeMode.On;
            fpnDetails.Location = new Point(330, 3);
            fpnDetails.Margin = new Padding(0);
            fpnDetails.Name = "fpnDetails";
            fpnDetails.Size = new Size(267, 244);
            fpnDetails.TabIndex = 0;
            fpnDetails.Paint += customFlowLayoutPanel1_Paint;
            // 
            // equipList
            // 
            equipList.BackColor = Color.Gold;
            equipList.BorderColor = Color.Transparent;
            equipList.BorderThickness = 0;
            equipList.ImeMode = ImeMode.On;
            equipList.Location = new Point(0, 0);
            equipList.Margin = new Padding(0);
            equipList.Name = "equipList";
            equipList.Size = new Size(327, 244);
            equipList.TabIndex = 1;
            // 
            // equipScrollbar
            // 
            equipScrollbar.Alpha = 50;
            equipScrollbar.BackColor = Color.Transparent;
            equipScrollbar.Background = true;
            equipScrollbar.Background_WidthPen = 3F;
            equipScrollbar.BackgroundPen = true;
            equipScrollbar.ColorBackground = Color.FromArgb(37, 52, 68);
            equipScrollbar.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            equipScrollbar.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            equipScrollbar.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            equipScrollbar.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            equipScrollbar.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            equipScrollbar.ColorLighting = Color.FromArgb(29, 200, 238);
            equipScrollbar.ColorPen_1 = Color.FromArgb(37, 52, 68);
            equipScrollbar.ColorPen_2 = Color.FromArgb(41, 63, 86);
            equipScrollbar.ColorScrollBar = Color.FromArgb(29, 200, 238);
            equipScrollbar.ColorScrollBar_Transparency = 255;
            equipScrollbar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            equipScrollbar.ForeColor = Color.FromArgb(245, 245, 245);
            equipScrollbar.Lighting = false;
            equipScrollbar.LinearGradient_Background = false;
            equipScrollbar.LinearGradient_Value = false;
            equipScrollbar.LinearGradientPen = false;
            equipScrollbar.Location = new Point(298, 0);
            equipScrollbar.Margin = new Padding(0);
            equipScrollbar.Maximum = 100;
            equipScrollbar.Minimum = 0;
            equipScrollbar.Name = "equipScrollbar";
            equipScrollbar.OrientationValue = Orientation.Vertical;
            equipScrollbar.PenWidth = 10;
            equipScrollbar.RGB = false;
            equipScrollbar.Rounding = true;
            equipScrollbar.RoundingInt = 7;
            equipScrollbar.Size = new Size(26, 244);
            equipScrollbar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            equipScrollbar.TabIndex = 2;
            equipScrollbar.Tag = "Cyber";
            equipScrollbar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            equipScrollbar.ThumbSize = 60;
            equipScrollbar.Timer_RGB = 300;
            equipScrollbar.Value = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Danjo-bold", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.WhiteSmoke;
            lblName.Location = new Point(10, 20);
            lblName.Margin = new Padding(10, 20, 10, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(154, 42);
            lblName.TabIndex = 0;
            lblName.Text = "으하하하";
            // 
            // fpnDescription
            // 
            fpnDescription.AutoSize = true;
            fpnDescription.BorderColor = Color.Transparent;
            fpnDescription.BorderThickness = 0;
            fpnDescription.FlowDirection = FlowDirection.TopDown;
            fpnDescription.Location = new Point(10, 82);
            fpnDescription.Margin = new Padding(10, 20, 10, 0);
            fpnDescription.Name = "fpnDescription";
            fpnDescription.Size = new Size(0, 0);
            fpnDescription.TabIndex = 1;
            // 
            // EquipmentEditModal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Goldenrod;
            ClientSize = new Size(600, 250);
            ControlBox = false;
            Controls.Add(fpnDetails);
            Controls.Add(equipContainer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "EquipmentEditModal";
            Opacity = 0.7D;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "EquipmentEditModal";
            equipContainer.ResumeLayout(false);
            fpnDetails.ResumeLayout(false);
            fpnDetails.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private controls.DoubleBufferedPanel equipContainer;
        private controls.CustomFlowLayoutPanel fpnDetails;
        private controls.CustomFlowLayoutPanel equipList;
        private ReaLTaiizor.Controls.CyberScrollBar equipScrollbar;
        private controls.TransparentTextLabel lblName;
        private controls.CustomFlowLayoutPanel fpnDescription;
    }
}