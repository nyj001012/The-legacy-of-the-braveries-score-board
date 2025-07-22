namespace ScoreBoard.modals
{
    partial class StatusEffectEditModal
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
            pnEffectContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            effectList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            sbEffect = new ReaLTaiizor.Controls.CyberScrollBar();
            customFlowLayoutPanel2 = new ScoreBoard.controls.CustomFlowLayoutPanel();
            lblEffectName = new ScoreBoard.controls.TransparentTextLabel();
            lblEffectDescription = new ScoreBoard.controls.TransparentTextLabel();
            customFlowLayoutPanel1 = new ScoreBoard.controls.CustomFlowLayoutPanel();
            transparentTextLabel1 = new ScoreBoard.controls.TransparentTextLabel();
            tbDuration = new TextBox();
            pnEffectContainer.SuspendLayout();
            customFlowLayoutPanel2.SuspendLayout();
            customFlowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnEffectContainer
            // 
            pnEffectContainer.BackColor = Color.Transparent;
            pnEffectContainer.Controls.Add(effectList);
            pnEffectContainer.Controls.Add(sbEffect);
            pnEffectContainer.Location = new Point(3, 3);
            pnEffectContainer.Margin = new Padding(0);
            pnEffectContainer.Name = "pnEffectContainer";
            pnEffectContainer.Size = new Size(250, 144);
            pnEffectContainer.TabIndex = 1;
            // 
            // effectList
            // 
            effectList.BackColor = Color.Transparent;
            effectList.BorderColor = Color.Transparent;
            effectList.BorderThickness = 0;
            effectList.Location = new Point(0, 0);
            effectList.Margin = new Padding(0);
            effectList.Name = "effectList";
            effectList.Size = new Size(250, 144);
            effectList.TabIndex = 1;
            // 
            // sbEffect
            // 
            sbEffect.Alpha = 50;
            sbEffect.BackColor = Color.Transparent;
            sbEffect.Background = true;
            sbEffect.Background_WidthPen = 3F;
            sbEffect.BackgroundPen = true;
            sbEffect.ColorBackground = Color.FromArgb(37, 52, 68);
            sbEffect.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            sbEffect.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            sbEffect.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            sbEffect.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            sbEffect.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            sbEffect.ColorLighting = Color.FromArgb(29, 200, 238);
            sbEffect.ColorPen_1 = Color.FromArgb(37, 52, 68);
            sbEffect.ColorPen_2 = Color.FromArgb(41, 63, 86);
            sbEffect.ColorScrollBar = Color.FromArgb(29, 200, 238);
            sbEffect.ColorScrollBar_Transparency = 255;
            sbEffect.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            sbEffect.ForeColor = Color.FromArgb(245, 245, 245);
            sbEffect.Lighting = false;
            sbEffect.LinearGradient_Background = false;
            sbEffect.LinearGradient_Value = false;
            sbEffect.LinearGradientPen = false;
            sbEffect.Location = new Point(221, 0);
            sbEffect.Margin = new Padding(0);
            sbEffect.Maximum = 100;
            sbEffect.Minimum = 0;
            sbEffect.Name = "sbEffect";
            sbEffect.OrientationValue = Orientation.Vertical;
            sbEffect.PenWidth = 10;
            sbEffect.RGB = false;
            sbEffect.Rounding = true;
            sbEffect.RoundingInt = 7;
            sbEffect.Size = new Size(16, 144);
            sbEffect.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            sbEffect.TabIndex = 0;
            sbEffect.Tag = "Cyber";
            sbEffect.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            sbEffect.ThumbSize = 60;
            sbEffect.Timer_RGB = 300;
            sbEffect.Value = 0;
            // 
            // customFlowLayoutPanel2
            // 
            customFlowLayoutPanel2.BackColor = Color.Transparent;
            customFlowLayoutPanel2.BorderColor = Color.Transparent;
            customFlowLayoutPanel2.BorderThickness = 0;
            customFlowLayoutPanel2.Controls.Add(lblEffectName);
            customFlowLayoutPanel2.Controls.Add(lblEffectDescription);
            customFlowLayoutPanel2.Controls.Add(customFlowLayoutPanel1);
            customFlowLayoutPanel2.Location = new Point(256, 3);
            customFlowLayoutPanel2.Margin = new Padding(0);
            customFlowLayoutPanel2.Name = "customFlowLayoutPanel2";
            customFlowLayoutPanel2.Size = new Size(240, 144);
            customFlowLayoutPanel2.TabIndex = 2;
            // 
            // lblEffectName
            // 
            lblEffectName.AutoSize = true;
            lblEffectName.BackColor = Color.Transparent;
            lblEffectName.Font = new Font("Danjo-bold", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEffectName.ForeColor = Color.WhiteSmoke;
            lblEffectName.Location = new Point(3, 13);
            lblEffectName.Margin = new Padding(3, 13, 3, 3);
            lblEffectName.Name = "lblEffectName";
            lblEffectName.Size = new Size(123, 33);
            lblEffectName.TabIndex = 3;
            lblEffectName.Text = "효과 이름";
            // 
            // lblEffectDescription
            // 
            lblEffectDescription.AutoSize = true;
            lblEffectDescription.BackColor = Color.Transparent;
            lblEffectDescription.Font = new Font("Danjo-bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEffectDescription.ForeColor = Color.WhiteSmoke;
            lblEffectDescription.Location = new Point(3, 63);
            lblEffectDescription.Margin = new Padding(3, 15, 3, 3);
            lblEffectDescription.Name = "lblEffectDescription";
            lblEffectDescription.Size = new Size(110, 29);
            lblEffectDescription.TabIndex = 4;
            lblEffectDescription.Text = "효과 설명";
            // 
            // customFlowLayoutPanel1
            // 
            customFlowLayoutPanel1.BorderColor = Color.Transparent;
            customFlowLayoutPanel1.BorderThickness = 0;
            customFlowLayoutPanel1.Controls.Add(transparentTextLabel1);
            customFlowLayoutPanel1.Controls.Add(tbDuration);
            customFlowLayoutPanel1.Location = new Point(0, 95);
            customFlowLayoutPanel1.Margin = new Padding(0);
            customFlowLayoutPanel1.Name = "customFlowLayoutPanel1";
            customFlowLayoutPanel1.Size = new Size(240, 45);
            customFlowLayoutPanel1.TabIndex = 6;
            // 
            // transparentTextLabel1
            // 
            transparentTextLabel1.AutoSize = true;
            transparentTextLabel1.BackColor = Color.Transparent;
            transparentTextLabel1.Font = new Font("Danjo-bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            transparentTextLabel1.ForeColor = Color.WhiteSmoke;
            transparentTextLabel1.Location = new Point(3, 10);
            transparentTextLabel1.Margin = new Padding(3, 10, 3, 3);
            transparentTextLabel1.Name = "transparentTextLabel1";
            transparentTextLabel1.Size = new Size(119, 29);
            transparentTextLabel1.TabIndex = 5;
            transparentTextLabel1.Text = "지속 시간:";
            // 
            // tbDuration
            // 
            tbDuration.BackColor = Color.FromArgb(180, 138, 0);
            tbDuration.BorderStyle = BorderStyle.None;
            tbDuration.Font = new Font("Danjo-bold", 23.9999962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbDuration.ForeColor = Color.WhiteSmoke;
            tbDuration.Location = new Point(125, 0);
            tbDuration.Margin = new Padding(0);
            tbDuration.Name = "tbDuration";
            tbDuration.Size = new Size(110, 39);
            tbDuration.TabIndex = 0;
            tbDuration.Text = "0";
            // 
            // StatusEffectEditModal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.ImgStatusEffectEditModal;
            ClientSize = new Size(500, 150);
            Controls.Add(customFlowLayoutPanel2);
            Controls.Add(pnEffectContainer);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StatusEffectEditModal";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "DetailEditModal";
            TopMost = true;
            TransparencyKey = Color.Black;
            KeyDown += StatusEffectEditModal_KeyDown;
            pnEffectContainer.ResumeLayout(false);
            customFlowLayoutPanel2.ResumeLayout(false);
            customFlowLayoutPanel2.PerformLayout();
            customFlowLayoutPanel1.ResumeLayout(false);
            customFlowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private controls.DoubleBufferedPanel pnEffectContainer;
        private ReaLTaiizor.Controls.CyberScrollBar sbEffect;
        private controls.CustomFlowLayoutPanel customFlowLayoutPanel2;
        private controls.TransparentTextLabel lblEffectName;
        private controls.TransparentTextLabel lblEffectDescription;
        private controls.CustomFlowLayoutPanel customFlowLayoutPanel1;
        private controls.TransparentTextLabel transparentTextLabel1;
        private controls.CustomFlowLayoutPanel effectList;
        private TextBox tbDuration;
    }
}