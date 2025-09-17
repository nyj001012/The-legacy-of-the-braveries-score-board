using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.minion;
using ScoreBoard.data.stat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ScoreBoard.controls
{
    public partial class CurrentPlayerPanel : BasePlayerPanel
    {
        private CorpsMember _player;
        private int _order;

        public CurrentPlayerPanel(CorpsMember player, int order)
        {
            InitializeComponent();

            // ---- AutoSize 체인 설정 (스크롤 없이 키우기) ----
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // 바인딩
            this.hbPlayer.Name = $"hb{player.Id}";
            base.LblName = this.lblName;
            base.LblOrder = this.lblOrder;
            base.PbLv = this.pbLv;
            base.FpnStatus = this.fpnStatus;
            base.FpnArtifact = this.fpnArtifact;
            base.HbPlayer = this.hbPlayer;
            base.PnInfo = this.pnInfo;
            base.PnPlayer = this.pnPlayer;

            _player = player;
            _order = order;

            this.Load += CurrentPlayerPanel_Load;
        }

        private void CurrentPlayerPanel_Load([NotNull] object? sender, EventArgs e)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender), "'sender' 매개 변수는 null일 수 없습니다.");
            }

            InitBase(_player, _order);
        }
    }
}
