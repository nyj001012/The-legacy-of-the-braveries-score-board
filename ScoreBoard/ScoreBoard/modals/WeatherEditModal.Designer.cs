namespace ScoreBoard.modals
{
    partial class WeatherEditModal
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
            pnWeatherContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            weatherList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            sbWeather = new ReaLTaiizor.Controls.CyberScrollBar();
            customFlowLayoutPanel2 = new ScoreBoard.controls.CustomFlowLayoutPanel();
            lblWeatherName = new ScoreBoard.controls.TransparentTextLabel();
            lblWeatherDescription = new ScoreBoard.controls.TransparentTextLabel();
            customFlowLayoutPanel1 = new ScoreBoard.controls.CustomFlowLayoutPanel();
            transparentTextLabel1 = new ScoreBoard.controls.TransparentTextLabel();
            tbDuration = new TextBox();
            pnWeatherContainer.SuspendLayout();
            customFlowLayoutPanel2.SuspendLayout();
            customFlowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnWeatherContainer
            // 
            pnWeatherContainer.BackColor = Color.Transparent;
            pnWeatherContainer.Controls.Add(weatherList);
            pnWeatherContainer.Controls.Add(sbWeather);
            pnWeatherContainer.Location = new Point(3, 3);
            pnWeatherContainer.Margin = new Padding(0);
            pnWeatherContainer.Name = "pnWeatherContainer";
            pnWeatherContainer.Size = new Size(150, 144);
            pnWeatherContainer.TabIndex = 1;
            // 
            // weatherList
            // 
            weatherList.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            weatherList.BackColor = Color.Transparent;
            weatherList.BorderColor = Color.Transparent;
            weatherList.BorderThickness = 0;
            weatherList.Location = new Point(0, 0);
            weatherList.Margin = new Padding(0);
            weatherList.Name = "weatherList";
            weatherList.Size = new Size(150, 144);
            weatherList.TabIndex = 1;
            weatherList.MouseEnter += WeatherList_MouseEnter;
            // 
            // sbWeather
            // 
            sbWeather.Alpha = 50;
            sbWeather.BackColor = Color.Transparent;
            sbWeather.Background = true;
            sbWeather.Background_WidthPen = 3F;
            sbWeather.BackgroundPen = true;
            sbWeather.ColorBackground = Color.FromArgb(37, 52, 68);
            sbWeather.ColorBackground_1 = Color.FromArgb(37, 52, 68);
            sbWeather.ColorBackground_2 = Color.FromArgb(41, 63, 86);
            sbWeather.ColorBackground_Pen = Color.FromArgb(29, 200, 238);
            sbWeather.ColorBackground_Value_1 = Color.FromArgb(28, 200, 238);
            sbWeather.ColorBackground_Value_2 = Color.FromArgb(100, 208, 232);
            sbWeather.ColorLighting = Color.FromArgb(29, 200, 238);
            sbWeather.ColorPen_1 = Color.FromArgb(37, 52, 68);
            sbWeather.ColorPen_2 = Color.FromArgb(41, 63, 86);
            sbWeather.ColorScrollBar = Color.FromArgb(29, 200, 238);
            sbWeather.ColorScrollBar_Transparency = 255;
            sbWeather.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            sbWeather.ForeColor = Color.FromArgb(245, 245, 245);
            sbWeather.Lighting = false;
            sbWeather.LinearGradient_Background = false;
            sbWeather.LinearGradient_Value = false;
            sbWeather.LinearGradientPen = false;
            sbWeather.Location = new Point(135, 0);
            sbWeather.Margin = new Padding(0);
            sbWeather.Maximum = 100;
            sbWeather.Minimum = 0;
            sbWeather.Name = "sbWeather";
            sbWeather.OrientationValue = Orientation.Vertical;
            sbWeather.PenWidth = 10;
            sbWeather.RGB = false;
            sbWeather.Rounding = true;
            sbWeather.RoundingInt = 7;
            sbWeather.Size = new Size(16, 144);
            sbWeather.SmallStep = 10;
            sbWeather.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            sbWeather.TabIndex = 0;
            sbWeather.Tag = "Cyber";
            sbWeather.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            sbWeather.ThumbSize = 60;
            sbWeather.Timer_RGB = 300;
            sbWeather.Value = 0;
            // 
            // customFlowLayoutPanel2
            // 
            customFlowLayoutPanel2.BackColor = Color.Transparent;
            customFlowLayoutPanel2.BorderColor = Color.Transparent;
            customFlowLayoutPanel2.BorderThickness = 0;
            customFlowLayoutPanel2.Controls.Add(lblWeatherName);
            customFlowLayoutPanel2.Controls.Add(lblWeatherDescription);
            customFlowLayoutPanel2.Controls.Add(customFlowLayoutPanel1);
            customFlowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            customFlowLayoutPanel2.Location = new Point(154, 3);
            customFlowLayoutPanel2.Margin = new Padding(0);
            customFlowLayoutPanel2.Name = "customFlowLayoutPanel2";
            customFlowLayoutPanel2.Size = new Size(240, 144);
            customFlowLayoutPanel2.TabIndex = 2;
            // 
            // lblWeatherName
            // 
            lblWeatherName.AutoSize = true;
            lblWeatherName.BackColor = Color.Transparent;
            lblWeatherName.Font = new Font("Danjo-bold", 20.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWeatherName.ForeColor = Color.WhiteSmoke;
            lblWeatherName.Location = new Point(3, 13);
            lblWeatherName.Margin = new Padding(3, 13, 3, 3);
            lblWeatherName.Name = "lblWeatherName";
            lblWeatherName.Size = new Size(123, 33);
            lblWeatherName.TabIndex = 3;
            lblWeatherName.Text = "효과 이름";
            // 
            // lblWeatherDescription
            // 
            lblWeatherDescription.AutoSize = true;
            lblWeatherDescription.BackColor = Color.Transparent;
            lblWeatherDescription.Font = new Font("Danjo-bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWeatherDescription.ForeColor = Color.WhiteSmoke;
            lblWeatherDescription.Location = new Point(3, 63);
            lblWeatherDescription.Margin = new Padding(3, 15, 3, 3);
            lblWeatherDescription.Name = "lblWeatherDescription";
            lblWeatherDescription.Size = new Size(110, 29);
            lblWeatherDescription.TabIndex = 4;
            lblWeatherDescription.Text = "효과 설명";
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
            tbDuration.Font = new Font("Danjo-bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbDuration.ForeColor = Color.WhiteSmoke;
            tbDuration.Location = new Point(125, 3);
            tbDuration.Margin = new Padding(0, 3, 0, 0);
            tbDuration.Name = "tbDuration";
            tbDuration.Size = new Size(110, 35);
            tbDuration.TabIndex = 0;
            tbDuration.Text = "0";
            tbDuration.KeyDown += tbDuration_KeyDown;
            tbDuration.Leave += tbDuration_Leave;
            // 
            // WeatherEditModal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImage = Properties.Resources.ImgWeatherEditModal;
            ClientSize = new Size(400, 150);
            Controls.Add(customFlowLayoutPanel2);
            Controls.Add(pnWeatherContainer);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WeatherEditModal";
            Opacity = 0.95D;
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "DetailEditModal";
            TopMost = true;
            TransparencyKey = Color.Black;
            Load += WeatherEditModal_Load;
            KeyDown += WeatherEditModal_KeyDown;
            pnWeatherContainer.ResumeLayout(false);
            customFlowLayoutPanel2.ResumeLayout(false);
            customFlowLayoutPanel2.PerformLayout();
            customFlowLayoutPanel1.ResumeLayout(false);
            customFlowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private controls.DoubleBufferedPanel pnWeatherContainer;
        private ReaLTaiizor.Controls.CyberScrollBar sbWeather;
        private controls.CustomFlowLayoutPanel customFlowLayoutPanel2;
        private controls.TransparentTextLabel lblWeatherName;
        private controls.TransparentTextLabel lblWeatherDescription;
        private controls.CustomFlowLayoutPanel customFlowLayoutPanel1;
        private controls.TransparentTextLabel transparentTextLabel1;
        private controls.CustomFlowLayoutPanel weatherList;
        private TextBox tbDuration;
    }
}