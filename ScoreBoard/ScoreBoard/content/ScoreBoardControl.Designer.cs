namespace ScoreBoard.content
{
    partial class ScoreBoardControl
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
            playerContainer = new ScoreBoard.controls.DoubleBufferedPanel();
            playerList = new ScoreBoard.controls.CustomFlowLayoutPanel();
            pn1P = new ScoreBoard.controls.DoubleBufferedPanel();
            lbl1P = new ScoreBoard.controls.GradientLabel();
            fpn1P = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn1PStatus = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn1PArtifact = new ScoreBoard.controls.CustomFlowLayoutPanel();
            pn1PInfo = new ScoreBoard.controls.DoubleBufferedPanel();
            hb1P = new ScoreBoard.controls.HealthBar();
            pb1PLv = new PictureBox();
            lbl1PName = new ScoreBoard.controls.GradientLabel();
            pn2P = new ScoreBoard.controls.DoubleBufferedPanel();
            lbl2P = new ScoreBoard.controls.GradientLabel();
            pn2PInfo = new ScoreBoard.controls.DoubleBufferedPanel();
            hb2P = new ScoreBoard.controls.HealthBar();
            fpn2P = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn2PStatus = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn2PArtifact = new ScoreBoard.controls.CustomFlowLayoutPanel();
            pb2PLv = new PictureBox();
            lbl2PName = new ScoreBoard.controls.GradientLabel();
            pn3P = new ScoreBoard.controls.DoubleBufferedPanel();
            lbl3P = new ScoreBoard.controls.GradientLabel();
            pn3PInfo = new ScoreBoard.controls.DoubleBufferedPanel();
            hb3P = new ScoreBoard.controls.HealthBar();
            fpn3P = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn3PStatus = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn3PArtifact = new ScoreBoard.controls.CustomFlowLayoutPanel();
            pb3PLv = new PictureBox();
            lbl3PName = new ScoreBoard.controls.GradientLabel();
            pn4P = new ScoreBoard.controls.DoubleBufferedPanel();
            lbl4P = new ScoreBoard.controls.GradientLabel();
            pn4PInfo = new ScoreBoard.controls.DoubleBufferedPanel();
            hb4P = new ScoreBoard.controls.HealthBar();
            fpn4P = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn4PStatus = new ScoreBoard.controls.CustomFlowLayoutPanel();
            fpn4PArtifact = new ScoreBoard.controls.CustomFlowLayoutPanel();
            pb4PLv = new PictureBox();
            lbl4PName = new ScoreBoard.controls.GradientLabel();
            playerScrollBar = new ReaLTaiizor.Controls.CyberScrollBar();
            doubleBufferedPanel1 = new ScoreBoard.controls.DoubleBufferedPanel();
            lblTurn = new ScoreBoard.controls.GradientLabel();
            playerContainer.SuspendLayout();
            playerList.SuspendLayout();
            pn1P.SuspendLayout();
            fpn1P.SuspendLayout();
            pn1PInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb1PLv).BeginInit();
            pn2P.SuspendLayout();
            pn2PInfo.SuspendLayout();
            fpn2P.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb2PLv).BeginInit();
            pn3P.SuspendLayout();
            pn3PInfo.SuspendLayout();
            fpn3P.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb3PLv).BeginInit();
            pn4P.SuspendLayout();
            pn4PInfo.SuspendLayout();
            fpn4P.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb4PLv).BeginInit();
            doubleBufferedPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // playerContainer
            // 
            playerContainer.BackColor = Color.Transparent;
            playerContainer.Controls.Add(playerList);
            playerContainer.Controls.Add(playerScrollBar);
            playerContainer.Location = new Point(0, 112);
            playerContainer.Margin = new Padding(0);
            playerContainer.Name = "playerContainer";
            playerContainer.Size = new Size(600, 968);
            playerContainer.TabIndex = 0;
            // 
            // playerList
            // 
            playerList.BorderColor = Color.Transparent;
            playerList.BorderThickness = 0;
            playerList.Controls.Add(pn1P);
            playerList.Controls.Add(pn2P);
            playerList.Controls.Add(pn3P);
            playerList.Controls.Add(pn4P);
            playerList.Location = new Point(0, 0);
            playerList.Name = "playerList";
            playerList.Padding = new Padding(15, 35, 15, 35);
            playerList.Size = new Size(600, 968);
            playerList.TabIndex = 1;
            // 
            // pn1P
            // 
            pn1P.Controls.Add(lbl1P);
            pn1P.Controls.Add(fpn1P);
            pn1P.Controls.Add(pn1PInfo);
            pn1P.Location = new Point(30, 60);
            pn1P.Margin = new Padding(15, 25, 15, 25);
            pn1P.Name = "pn1P";
            pn1P.Size = new Size(555, 183);
            pn1P.TabIndex = 0;
            pn1P.Tag = "1";
            // 
            // lbl1P
            // 
            lbl1P.Anchor = AnchorStyles.None;
            lbl1P.BackColor = Color.Transparent;
            lbl1P.Font = new Font("Danjo-bold", 33.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl1P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl1P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl1P.Location = new Point(1, 1);
            lbl1P.Margin = new Padding(0);
            lbl1P.Name = "lbl1P";
            lbl1P.Size = new Size(88, 181);
            lbl1P.TabIndex = 0;
            lbl1P.Text = "1P";
            lbl1P.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // fpn1P
            // 
            fpn1P.BorderColor = Color.Transparent;
            fpn1P.BorderThickness = 0;
            fpn1P.Controls.Add(fpn1PStatus);
            fpn1P.Controls.Add(fpn1PArtifact);
            fpn1P.Location = new Point(89, 68);
            fpn1P.Margin = new Padding(0, 15, 0, 0);
            fpn1P.Name = "fpn1P";
            fpn1P.Size = new Size(466, 52);
            fpn1P.TabIndex = 2;
            // 
            // fpn1PStatus
            // 
            fpn1PStatus.BorderColor = Color.Transparent;
            fpn1PStatus.BorderThickness = 0;
            fpn1PStatus.Location = new Point(0, 0);
            fpn1PStatus.Margin = new Padding(0, 0, 15, 0);
            fpn1PStatus.Name = "fpn1PStatus";
            fpn1PStatus.Padding = new Padding(0, 0, 5, 0);
            fpn1PStatus.Size = new Size(225, 52);
            fpn1PStatus.TabIndex = 0;
            // 
            // fpn1PArtifact
            // 
            fpn1PArtifact.BorderColor = Color.Transparent;
            fpn1PArtifact.BorderThickness = 0;
            fpn1PArtifact.Location = new Point(240, 0);
            fpn1PArtifact.Margin = new Padding(0);
            fpn1PArtifact.Name = "fpn1PArtifact";
            fpn1PArtifact.Padding = new Padding(0, 0, 5, 0);
            fpn1PArtifact.Size = new Size(225, 52);
            fpn1PArtifact.TabIndex = 1;
            // 
            // pn1PInfo
            // 
            pn1PInfo.Controls.Add(hb1P);
            pn1PInfo.Controls.Add(pb1PLv);
            pn1PInfo.Controls.Add(lbl1PName);
            pn1PInfo.Location = new Point(89, 0);
            pn1PInfo.Margin = new Padding(0);
            pn1PInfo.Name = "pn1PInfo";
            pn1PInfo.Size = new Size(466, 182);
            pn1PInfo.TabIndex = 1;
            // 
            // hb1P
            // 
            hb1P.BackColor = Color.Transparent;
            hb1P.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hb1P.BorderThickness = 3F;
            hb1P.CornerRadius = 15;
            hb1P.Font = new Font("Danjo-bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hb1P.Health = 70;
            hb1P.HealthColor = Color.FromArgb(119, 185, 69);
            hb1P.Location = new Point(1, 135);
            hb1P.Margin = new Padding(0, 15, 0, 15);
            hb1P.MaxValue = 100;
            hb1P.Name = "hb1P";
            hb1P.Shield = 30;
            hb1P.ShieldColor = Color.FromArgb(245, 245, 245);
            hb1P.Size = new Size(466, 32);
            hb1P.TabIndex = 4;
            hb1P.Text = "healthBar2";
            hb1P.TextColor = Color.FromArgb(245, 245, 245);
            hb1P.TextVisible = true;
            // 
            // pb1PLv
            // 
            pb1PLv.Location = new Point(412, 1);
            pb1PLv.Margin = new Padding(0);
            pb1PLv.Name = "pb1PLv";
            pb1PLv.Size = new Size(53, 53);
            pb1PLv.TabIndex = 1;
            pb1PLv.TabStop = false;
            // 
            // lbl1PName
            // 
            lbl1PName.BackColor = Color.Transparent;
            lbl1PName.Font = new Font("Danjo-bold", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl1PName.GradientEnd = Color.FromArgb(72, 144, 170);
            lbl1PName.GradientStart = Color.FromArgb(221, 237, 240);
            lbl1PName.Location = new Point(1, 1);
            lbl1PName.Margin = new Padding(0, 0, 15, 0);
            lbl1PName.Name = "lbl1PName";
            lbl1PName.RightToLeft = RightToLeft.No;
            lbl1PName.Size = new Size(396, 52);
            lbl1PName.TabIndex = 0;
            lbl1PName.Text = "예시샘플입니다.";
            lbl1PName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pn2P
            // 
            pn2P.Controls.Add(lbl2P);
            pn2P.Controls.Add(pn2PInfo);
            pn2P.Location = new Point(30, 293);
            pn2P.Margin = new Padding(15, 25, 15, 25);
            pn2P.Name = "pn2P";
            pn2P.Size = new Size(511, 146);
            pn2P.TabIndex = 1;
            pn2P.Tag = "2";
            // 
            // lbl2P
            // 
            lbl2P.BackColor = Color.Transparent;
            lbl2P.Font = new Font("Danjo-bold", 27.7499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl2P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl2P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl2P.Location = new Point(1, 1);
            lbl2P.Margin = new Padding(0);
            lbl2P.Name = "lbl2P";
            lbl2P.Size = new Size(88, 144);
            lbl2P.TabIndex = 0;
            lbl2P.Text = "2P";
            lbl2P.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn2PInfo
            // 
            pn2PInfo.Controls.Add(hb2P);
            pn2PInfo.Controls.Add(fpn2P);
            pn2PInfo.Controls.Add(pb2PLv);
            pn2PInfo.Controls.Add(lbl2PName);
            pn2PInfo.Location = new Point(92, 0);
            pn2PInfo.Margin = new Padding(0);
            pn2PInfo.Name = "pn2PInfo";
            pn2PInfo.Size = new Size(419, 146);
            pn2PInfo.TabIndex = 2;
            // 
            // hb2P
            // 
            hb2P.BackColor = Color.Transparent;
            hb2P.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hb2P.BorderThickness = 3F;
            hb2P.CornerRadius = 15;
            hb2P.Font = new Font("Danjo-bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hb2P.Health = 70;
            hb2P.HealthColor = Color.FromArgb(119, 185, 69);
            hb2P.Location = new Point(1, 111);
            hb2P.Margin = new Padding(0, 10, 0, 10);
            hb2P.MaxValue = 100;
            hb2P.Name = "hb2P";
            hb2P.Shield = 30;
            hb2P.ShieldColor = Color.FromArgb(245, 245, 245);
            hb2P.Size = new Size(413, 26);
            hb2P.TabIndex = 4;
            hb2P.Text = "healthBar1";
            hb2P.TextColor = Color.FromArgb(245, 245, 245);
            hb2P.TextVisible = true;
            // 
            // fpn2P
            // 
            fpn2P.BorderColor = Color.Transparent;
            fpn2P.BorderThickness = 0;
            fpn2P.Controls.Add(fpn2PStatus);
            fpn2P.Controls.Add(fpn2PArtifact);
            fpn2P.Location = new Point(1, 56);
            fpn2P.Margin = new Padding(0, 10, 0, 0);
            fpn2P.Name = "fpn2P";
            fpn2P.Size = new Size(418, 45);
            fpn2P.TabIndex = 2;
            // 
            // fpn2PStatus
            // 
            fpn2PStatus.BorderColor = Color.Transparent;
            fpn2PStatus.BorderThickness = 0;
            fpn2PStatus.Location = new Point(0, 0);
            fpn2PStatus.Margin = new Padding(0, 0, 10, 0);
            fpn2PStatus.Name = "fpn2PStatus";
            fpn2PStatus.Padding = new Padding(0, 0, 2, 0);
            fpn2PStatus.Size = new Size(217, 45);
            fpn2PStatus.TabIndex = 3;
            // 
            // fpn2PArtifact
            // 
            fpn2PArtifact.BorderColor = Color.Transparent;
            fpn2PArtifact.BorderThickness = 0;
            fpn2PArtifact.Location = new Point(227, 0);
            fpn2PArtifact.Margin = new Padding(0, 0, 5, 0);
            fpn2PArtifact.Name = "fpn2PArtifact";
            fpn2PArtifact.Padding = new Padding(0, 0, 2, 0);
            fpn2PArtifact.Size = new Size(186, 45);
            fpn2PArtifact.TabIndex = 2;
            // 
            // pb2PLv
            // 
            pb2PLv.Location = new Point(374, 1);
            pb2PLv.Margin = new Padding(0);
            pb2PLv.Name = "pb2PLv";
            pb2PLv.Size = new Size(45, 45);
            pb2PLv.TabIndex = 1;
            pb2PLv.TabStop = false;
            // 
            // lbl2PName
            // 
            lbl2PName.BackColor = Color.Transparent;
            lbl2PName.Font = new Font("Danjo-bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl2PName.GradientEnd = Color.WhiteSmoke;
            lbl2PName.GradientStart = Color.WhiteSmoke;
            lbl2PName.Location = new Point(1, 1);
            lbl2PName.Margin = new Padding(0, 0, 10, 0);
            lbl2PName.Name = "lbl2PName";
            lbl2PName.RightToLeft = RightToLeft.No;
            lbl2PName.Size = new Size(363, 45);
            lbl2PName.TabIndex = 0;
            lbl2PName.Text = "예시샘플입니다.";
            lbl2PName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pn3P
            // 
            pn3P.Controls.Add(lbl3P);
            pn3P.Controls.Add(pn3PInfo);
            pn3P.Location = new Point(30, 489);
            pn3P.Margin = new Padding(15, 25, 15, 25);
            pn3P.Name = "pn3P";
            pn3P.Size = new Size(511, 146);
            pn3P.TabIndex = 4;
            pn3P.Tag = "2";
            // 
            // lbl3P
            // 
            lbl3P.BackColor = Color.Transparent;
            lbl3P.Font = new Font("Danjo-bold", 27.7499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl3P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl3P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl3P.Location = new Point(1, 1);
            lbl3P.Margin = new Padding(0);
            lbl3P.Name = "lbl3P";
            lbl3P.Size = new Size(88, 144);
            lbl3P.TabIndex = 0;
            lbl3P.Text = "3P";
            lbl3P.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn3PInfo
            // 
            pn3PInfo.Controls.Add(hb3P);
            pn3PInfo.Controls.Add(fpn3P);
            pn3PInfo.Controls.Add(pb3PLv);
            pn3PInfo.Controls.Add(lbl3PName);
            pn3PInfo.Location = new Point(92, 0);
            pn3PInfo.Margin = new Padding(0);
            pn3PInfo.Name = "pn3PInfo";
            pn3PInfo.Size = new Size(419, 146);
            pn3PInfo.TabIndex = 2;
            // 
            // hb3P
            // 
            hb3P.BackColor = Color.Transparent;
            hb3P.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hb3P.BorderThickness = 3F;
            hb3P.CornerRadius = 15;
            hb3P.Font = new Font("Danjo-bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hb3P.Health = 70;
            hb3P.HealthColor = Color.FromArgb(119, 185, 69);
            hb3P.Location = new Point(1, 111);
            hb3P.Margin = new Padding(0, 10, 0, 10);
            hb3P.MaxValue = 100;
            hb3P.Name = "hb3P";
            hb3P.Shield = 30;
            hb3P.ShieldColor = Color.FromArgb(245, 245, 245);
            hb3P.Size = new Size(413, 26);
            hb3P.TabIndex = 4;
            hb3P.Text = "healthBar1";
            hb3P.TextColor = Color.FromArgb(245, 245, 245);
            hb3P.TextVisible = true;
            // 
            // fpn3P
            // 
            fpn3P.BorderColor = Color.Transparent;
            fpn3P.BorderThickness = 0;
            fpn3P.Controls.Add(fpn3PStatus);
            fpn3P.Controls.Add(fpn3PArtifact);
            fpn3P.Location = new Point(1, 56);
            fpn3P.Margin = new Padding(0, 10, 0, 0);
            fpn3P.Name = "fpn3P";
            fpn3P.Size = new Size(418, 45);
            fpn3P.TabIndex = 2;
            // 
            // fpn3PStatus
            // 
            fpn3PStatus.BorderColor = Color.Transparent;
            fpn3PStatus.BorderThickness = 0;
            fpn3PStatus.Location = new Point(0, 0);
            fpn3PStatus.Margin = new Padding(0, 0, 10, 0);
            fpn3PStatus.Name = "fpn3PStatus";
            fpn3PStatus.Padding = new Padding(0, 0, 2, 0);
            fpn3PStatus.Size = new Size(217, 45);
            fpn3PStatus.TabIndex = 3;
            // 
            // fpn3PArtifact
            // 
            fpn3PArtifact.BorderColor = Color.Transparent;
            fpn3PArtifact.BorderThickness = 0;
            fpn3PArtifact.Location = new Point(227, 0);
            fpn3PArtifact.Margin = new Padding(0, 0, 5, 0);
            fpn3PArtifact.Name = "fpn3PArtifact";
            fpn3PArtifact.Padding = new Padding(0, 0, 2, 0);
            fpn3PArtifact.Size = new Size(186, 45);
            fpn3PArtifact.TabIndex = 2;
            // 
            // pb3PLv
            // 
            pb3PLv.Location = new Point(374, 1);
            pb3PLv.Margin = new Padding(0);
            pb3PLv.Name = "pb3PLv";
            pb3PLv.Size = new Size(45, 45);
            pb3PLv.TabIndex = 1;
            pb3PLv.TabStop = false;
            // 
            // lbl3PName
            // 
            lbl3PName.BackColor = Color.Transparent;
            lbl3PName.Font = new Font("Danjo-bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl3PName.GradientEnd = Color.WhiteSmoke;
            lbl3PName.GradientStart = Color.WhiteSmoke;
            lbl3PName.Location = new Point(1, 1);
            lbl3PName.Margin = new Padding(0, 0, 10, 0);
            lbl3PName.Name = "lbl3PName";
            lbl3PName.RightToLeft = RightToLeft.No;
            lbl3PName.Size = new Size(363, 45);
            lbl3PName.TabIndex = 0;
            lbl3PName.Text = "예시샘플입니다.";
            lbl3PName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // pn4P
            // 
            pn4P.Controls.Add(lbl4P);
            pn4P.Controls.Add(pn4PInfo);
            pn4P.Location = new Point(30, 685);
            pn4P.Margin = new Padding(15, 25, 15, 25);
            pn4P.Name = "pn4P";
            pn4P.Size = new Size(511, 146);
            pn4P.TabIndex = 5;
            pn4P.Tag = "2";
            // 
            // lbl4P
            // 
            lbl4P.BackColor = Color.Transparent;
            lbl4P.Font = new Font("Danjo-bold", 27.7499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl4P.GradientEnd = Color.FromArgb(107, 89, 50);
            lbl4P.GradientStart = Color.FromArgb(209, 162, 97);
            lbl4P.Location = new Point(1, 1);
            lbl4P.Margin = new Padding(0);
            lbl4P.Name = "lbl4P";
            lbl4P.Size = new Size(88, 144);
            lbl4P.TabIndex = 0;
            lbl4P.Text = "4P";
            lbl4P.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pn4PInfo
            // 
            pn4PInfo.Controls.Add(hb4P);
            pn4PInfo.Controls.Add(fpn4P);
            pn4PInfo.Controls.Add(pb4PLv);
            pn4PInfo.Controls.Add(lbl4PName);
            pn4PInfo.Location = new Point(92, 0);
            pn4PInfo.Margin = new Padding(0);
            pn4PInfo.Name = "pn4PInfo";
            pn4PInfo.Size = new Size(419, 146);
            pn4PInfo.TabIndex = 2;
            // 
            // hb4P
            // 
            hb4P.BackColor = Color.Transparent;
            hb4P.BorderColor = Color.FromArgb(75, 245, 245, 245);
            hb4P.BorderThickness = 3F;
            hb4P.CornerRadius = 15;
            hb4P.Font = new Font("Danjo-bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            hb4P.Health = 70;
            hb4P.HealthColor = Color.FromArgb(119, 185, 69);
            hb4P.Location = new Point(1, 111);
            hb4P.Margin = new Padding(0, 10, 0, 10);
            hb4P.MaxValue = 100;
            hb4P.Name = "hb4P";
            hb4P.Shield = 30;
            hb4P.ShieldColor = Color.FromArgb(245, 245, 245);
            hb4P.Size = new Size(413, 26);
            hb4P.TabIndex = 4;
            hb4P.Text = "healthBar1";
            hb4P.TextColor = Color.FromArgb(245, 245, 245);
            hb4P.TextVisible = true;
            // 
            // fpn4P
            // 
            fpn4P.BorderColor = Color.Transparent;
            fpn4P.BorderThickness = 0;
            fpn4P.Controls.Add(fpn4PStatus);
            fpn4P.Controls.Add(fpn4PArtifact);
            fpn4P.Location = new Point(1, 56);
            fpn4P.Margin = new Padding(0, 10, 0, 0);
            fpn4P.Name = "fpn4P";
            fpn4P.Size = new Size(418, 45);
            fpn4P.TabIndex = 2;
            // 
            // fpn4PStatus
            // 
            fpn4PStatus.BorderColor = Color.Transparent;
            fpn4PStatus.BorderThickness = 0;
            fpn4PStatus.Location = new Point(0, 0);
            fpn4PStatus.Margin = new Padding(0, 0, 10, 0);
            fpn4PStatus.Name = "fpn4PStatus";
            fpn4PStatus.Padding = new Padding(0, 0, 2, 0);
            fpn4PStatus.Size = new Size(217, 45);
            fpn4PStatus.TabIndex = 3;
            // 
            // fpn4PArtifact
            // 
            fpn4PArtifact.BorderColor = Color.Transparent;
            fpn4PArtifact.BorderThickness = 0;
            fpn4PArtifact.Location = new Point(227, 0);
            fpn4PArtifact.Margin = new Padding(0, 0, 5, 0);
            fpn4PArtifact.Name = "fpn4PArtifact";
            fpn4PArtifact.Padding = new Padding(0, 0, 2, 0);
            fpn4PArtifact.Size = new Size(186, 45);
            fpn4PArtifact.TabIndex = 2;
            // 
            // pb4PLv
            // 
            pb4PLv.Location = new Point(374, 1);
            pb4PLv.Margin = new Padding(0);
            pb4PLv.Name = "pb4PLv";
            pb4PLv.Size = new Size(45, 45);
            pb4PLv.TabIndex = 1;
            pb4PLv.TabStop = false;
            // 
            // lbl4PName
            // 
            lbl4PName.BackColor = Color.Transparent;
            lbl4PName.Font = new Font("Danjo-bold", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl4PName.GradientEnd = Color.WhiteSmoke;
            lbl4PName.GradientStart = Color.WhiteSmoke;
            lbl4PName.Location = new Point(1, 1);
            lbl4PName.Margin = new Padding(0, 0, 10, 0);
            lbl4PName.Name = "lbl4PName";
            lbl4PName.RightToLeft = RightToLeft.No;
            lbl4PName.Size = new Size(363, 45);
            lbl4PName.TabIndex = 0;
            lbl4PName.Text = "예시샘플입니다.";
            lbl4PName.TextAlign = ContentAlignment.BottomLeft;
            // 
            // playerScrollBar
            // 
            playerScrollBar.Alpha = 50;
            playerScrollBar.BackColor = Color.Transparent;
            playerScrollBar.Background = true;
            playerScrollBar.Background_WidthPen = 3F;
            playerScrollBar.BackgroundPen = false;
            playerScrollBar.ColorBackground = Color.Transparent;
            playerScrollBar.ColorBackground_1 = Color.Transparent;
            playerScrollBar.ColorBackground_2 = Color.Transparent;
            playerScrollBar.ColorBackground_Pen = Color.Transparent;
            playerScrollBar.ColorBackground_Value_1 = Color.Transparent;
            playerScrollBar.ColorBackground_Value_2 = Color.Transparent;
            playerScrollBar.ColorLighting = Color.Transparent;
            playerScrollBar.ColorPen_1 = Color.Transparent;
            playerScrollBar.ColorPen_2 = Color.Transparent;
            playerScrollBar.ColorScrollBar = Color.Transparent;
            playerScrollBar.ColorScrollBar_Transparency = 255;
            playerScrollBar.CyberScrollBarStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            playerScrollBar.ForeColor = Color.FromArgb(245, 245, 245);
            playerScrollBar.Lighting = false;
            playerScrollBar.LinearGradient_Background = false;
            playerScrollBar.LinearGradient_Value = false;
            playerScrollBar.LinearGradientPen = false;
            playerScrollBar.Location = new Point(574, 0);
            playerScrollBar.Margin = new Padding(0);
            playerScrollBar.Maximum = 100;
            playerScrollBar.Minimum = 0;
            playerScrollBar.Name = "playerScrollBar";
            playerScrollBar.OrientationValue = Orientation.Vertical;
            playerScrollBar.PenWidth = 10;
            playerScrollBar.RGB = false;
            playerScrollBar.Rounding = true;
            playerScrollBar.RoundingInt = 7;
            playerScrollBar.Size = new Size(26, 968);
            playerScrollBar.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            playerScrollBar.TabIndex = 0;
            playerScrollBar.Tag = "Cyber";
            playerScrollBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            playerScrollBar.ThumbSize = 60;
            playerScrollBar.Timer_RGB = 300;
            playerScrollBar.Value = 0;
            // 
            // doubleBufferedPanel1
            // 
            doubleBufferedPanel1.BackColor = Color.Transparent;
            doubleBufferedPanel1.Controls.Add(lblTurn);
            doubleBufferedPanel1.Location = new Point(0, 0);
            doubleBufferedPanel1.Margin = new Padding(0);
            doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            doubleBufferedPanel1.Size = new Size(1920, 112);
            doubleBufferedPanel1.TabIndex = 1;
            // 
            // lblTurn
            // 
            lblTurn.BackColor = Color.Transparent;
            lblTurn.Font = new Font("Danjo-bold", 47.9999924F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTurn.GradientEnd = Color.FromArgb(107, 89, 50);
            lblTurn.GradientStart = Color.FromArgb(209, 162, 97);
            lblTurn.Location = new Point(850, 15);
            lblTurn.Margin = new Padding(0, 15, 10, 0);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(220, 94);
            lblTurn.TabIndex = 0;
            lblTurn.Text = "1턴";
            lblTurn.TextAlign = ContentAlignment.BottomCenter;
            // 
            // ScoreBoardControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.ImgInGameBackground;
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(doubleBufferedPanel1);
            Controls.Add(playerContainer);
            Name = "ScoreBoardControl";
            Size = new Size(1920, 1080);
            Load += ScoreBoardControl_Load;
            playerContainer.ResumeLayout(false);
            playerList.ResumeLayout(false);
            pn1P.ResumeLayout(false);
            fpn1P.ResumeLayout(false);
            pn1PInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb1PLv).EndInit();
            pn2P.ResumeLayout(false);
            pn2PInfo.ResumeLayout(false);
            fpn2P.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb2PLv).EndInit();
            pn3P.ResumeLayout(false);
            pn3PInfo.ResumeLayout(false);
            fpn3P.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb3PLv).EndInit();
            pn4P.ResumeLayout(false);
            pn4PInfo.ResumeLayout(false);
            fpn4P.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pb4PLv).EndInit();
            doubleBufferedPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private controls.DoubleBufferedPanel playerContainer;
        private ReaLTaiizor.Controls.CyberScrollBar playerScrollBar;
        private controls.CustomFlowLayoutPanel playerList;
        private controls.DoubleBufferedPanel pn1P;
        private controls.DoubleBufferedPanel pn2P;
        private controls.GradientLabel lbl2P;
        private controls.DoubleBufferedPanel pn1PInfo;
        private controls.GradientLabel lbl1PName;
        private PictureBox pb1PLv;
        private controls.CustomFlowLayoutPanel fpn1P;
        private controls.CustomFlowLayoutPanel fpn1PStatus;
        private controls.CustomFlowLayoutPanel fpn1PArtifact;
        private controls.HealthBar hb1P;
        private controls.DoubleBufferedPanel pn2PInfo;
        private controls.HealthBar hb2P;
        private controls.CustomFlowLayoutPanel fpn2P;
        private PictureBox pb2PLv;
        private controls.GradientLabel lbl2PName;
        private controls.CustomFlowLayoutPanel fpn2PArtifact;
        private controls.CustomFlowLayoutPanel fpn2PStatus;
        private controls.GradientLabel lbl1P;
        private controls.DoubleBufferedPanel pn3P;
        private controls.GradientLabel lbl3P;
        private controls.DoubleBufferedPanel pn3PInfo;
        private controls.HealthBar hb3P;
        private controls.CustomFlowLayoutPanel fpn3P;
        private controls.CustomFlowLayoutPanel fpn3PStatus;
        private controls.CustomFlowLayoutPanel fpn3PArtifact;
        private PictureBox pb3PLv;
        private controls.GradientLabel lbl3PName;
        private controls.DoubleBufferedPanel pn4P;
        private controls.GradientLabel lbl4P;
        private controls.DoubleBufferedPanel pn4PInfo;
        private controls.HealthBar hb4P;
        private controls.CustomFlowLayoutPanel fpn4P;
        private controls.CustomFlowLayoutPanel fpn4PStatus;
        private controls.CustomFlowLayoutPanel fpn4PArtifact;
        private PictureBox pb4PLv;
        private controls.GradientLabel lbl4PName;
        private controls.DoubleBufferedPanel doubleBufferedPanel1;
        private controls.GradientLabel lblTurn;
    }
}
