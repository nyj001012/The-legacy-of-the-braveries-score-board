using ScoreBoard.content;
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
        public string InputText => tbInput.Text; // 입력된 텍스트를 반환하는 프로퍼티

        public DetailEditModal(string initialText)
        {
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 이벤트를 받을 수 있도록 설정
            tbInput.Text = initialText; // 초기 텍스트 설정
        }

        private void DetailEditModal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                // TODO => SaveDetails(); 구현. 호출한 부모 폼에 값을 전달
                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }
    }
}
