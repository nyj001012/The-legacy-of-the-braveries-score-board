using ScoreBoard.data.artifact;
using ScoreBoard.data.character;
using ScoreBoard.data.stat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.controls
{
    public partial class PlayerPanel : BasePlayerPanel
    {
        private CorpsMember _player;
        private int _order;

        public PlayerPanel(CorpsMember player, int order)
        {
            InitializeComponent();

            base.LblName = this.lblName;
            base.LblOrder = this.lblOrder;
            base.PbLv = this.pbLv;
            base.FpnStatus = this.fpnStatus;
            base.FpnArtifact = this.fpnArtifact;
            base.HbPlayer = this.hbPlayer;

            _player = player;
            _order = order;
            
            this.Load += PlayerPanel_Load;
        }

        private void PlayerPanel_Load([NotNull] object? sender, EventArgs e)
        {
            InitBase(_player, _order);
        }
    }
}
