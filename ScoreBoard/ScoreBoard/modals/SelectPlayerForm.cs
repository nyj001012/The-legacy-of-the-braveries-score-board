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

        private void LoadLegionsFromJson()
        {

        }

        public int SelectedPlayerId { get; private set; }

        public SelectPlayerForm()
        {
            InitializeComponent();
            unitsJsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "meta_data", "units.json");
            unitsMap = JsonReader.ReadJsonStringValue(unitsJsonPath);
        }

        private void SelectPlayerForm_Load(object sender, EventArgs e)
        {

        }

        private void SelectPlayerForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void SelectPlayerForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
