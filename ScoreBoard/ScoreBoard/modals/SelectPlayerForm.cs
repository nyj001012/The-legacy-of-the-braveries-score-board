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

namespace ScoreBoard.modals
{
    public partial class SelectPlayerForm : Form
    {
        private string unitsJsonPath;
        private Dictionary<string, string> unitsMap;

        public int SelectedPlayerId { get; private set; }

        public SelectPlayerForm()
        {
            InitializeComponent();
            unitsJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "units.json");
            unitsMap = JsonReader.ReadJsonStringValue(unitsJsonPath);
            ShowUnits();
        }

        private void ShowUnits()
        {
            foreach (var unit in unitsMap)
            {
                var label = new Label
                {
                    Text = unit.Value,
                    Tag = unit.Key,
                    AutoSize = true,
                    Location = new Point(10, 10 + 30 * unitsMap.Keys.ToList().IndexOf(unit.Key)),
                    Cursor = Cursors.Hand,
                    Font = new Font("나눔고딕코딩", 12)
                };
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
