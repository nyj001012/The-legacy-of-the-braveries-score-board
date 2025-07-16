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
    public partial class DetailEditModal : Form
    {
        public DetailEditModal()
        {
            InitializeComponent();
        }

        private void DetailEditModal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                // TODO => SaveDetails(); 구현. 호출한 부모 폼에 값을 전달
                this.Close();
            }
        }
    }
}
