using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using ScoreBoard.utils;
using ScoreBoard.controls;

namespace ScoreBoard.modals
{
    public partial class SelectPlayerForm : Form
    {
        private string corpsJsonPath;
        private Dictionary<string, string> corpsMap;

        public int SelectedPlayerId { get; private set; }

        public SelectPlayerForm()
        {
            InitializeComponent();
            corpsJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "corps.json");
            corpsMap = JsonReader.ReadJsonStringValue(corpsJsonPath);
            ShowUnits();
        }

        private void ShowUnits()
        {
            foreach (var unit in corpsMap)
            {
                var label = new TransparentTextLabel
                {
                    Text = unit.Value,
                    Tag = unit.Key,
                    AutoSize = true,
                    Cursor = Cursors.Hand,
                    Font = new Font("나눔고딕코딩", 25, FontStyle.Bold),
                    BackColor = Color.Transparent,
                    ForeColor = Color.FromArgb(100, 245, 245, 245),
                    Margin = new Padding(0, 20, 0, 20),
                };
                label.MouseEnter += (s, e) =>
                {
                    label.ForeColor = Color.FromArgb(255, 245, 245, 245);
                    string corpsId = (string)label.Tag;
                    // TODO => ShowCorpsMember(corpsId);
                };
                label.MouseLeave += (s, e) => label.ForeColor = Color.FromArgb(100, 245, 245, 245);
                unitList.Controls.Add(label);
            }
        }

        private void SelectPlayerForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
