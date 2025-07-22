using ScoreBoard.content;
using ScoreBoard.data.statusEffect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreBoard.modals
{
    public partial class StatusEffectEditModal : Form
    {
        public List<StatusEffect> newStatusEffects = [];

        public StatusEffectEditModal()
        {
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 이벤트를 받을 수 있도록 설정
        }

        private void StatusEffectEditModal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
