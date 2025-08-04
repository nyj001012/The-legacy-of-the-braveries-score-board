using ScoreBoard.content;
using ScoreBoard.data.weather;
using ScoreBoard.utils;
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
    public partial class WeatherEditModal : Form
    {
        private Weather OldWeather { get; set; }
        private WeatherType _currentType;
        public Weather NewWeather;

        public WeatherEditModal(Weather currentWeather)
        {
            InitializeComponent();
            this.KeyPreview = true; // 폼에서 키 이벤트를 받을 수 있도록 설정
            OldWeather = currentWeather;
            NewWeather = currentWeather;
            lblWeatherName.Text = EnumHelper.GetEnumName(currentWeather.Type);
            lblWeatherDescription.Text = EnumHelper.GetEnumDescription(currentWeather.Type);
            tbDuration.Text = currentWeather.Duration.ToString();
        }

        private void WeatherEditModal_KeyDown(object sender, KeyEventArgs e)
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

        private void WeatherEditModal_Load(object sender, EventArgs e)
        {
            ShowWeatherIcons();
            weatherList.MouseWheel += WeatherList_MouseWheel;
        }

        /*
         * ShowWeatherIcons()
         * - 날씨 아이콘을 불러와서 WeatherList에 추가하는 메소드
         */
        private void ShowWeatherIcons()
        {
            // 날씨 아이콘을 불러와서 WeatherList에 추가하는 로직 구현
            foreach (WeatherType type in Enum.GetValues(typeof(WeatherType)))
            {
                Image? icon = DataReader.GetWeatherImage(type);
                if (icon == null)
                {
                    MessageBox.Show($"날씨 아이콘을 불러오는 중 오류 발생: {type} 아이콘이 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    weatherList.Controls.Clear();
                    break;
                }
                PictureBox pb = new()
                {
                    Image = icon,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 64,
                    Height = 64,
                    Tag = type, // 날씨 타입을 태그에 저장
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand // 마우스 커서를 손 모양으로 변경
                };
                pb.Click += (s, e) => ShowWeatherDetails(type); // 클릭 시 날씨 세부 정보 표시
                weatherList.Controls.Add(pb);
            }
            weatherList.Height = weatherList.Controls.Cast<Control>().Where(c => c.Visible).Max(c => c.Bottom); ;
            ScrollBarManager.SetScrollBar(pnWeatherContainer, weatherList, sbWeather); // 스크롤바 설정
        }

        private void WeatherList_MouseEnter(object sender, EventArgs e)
        {
            weatherList.Focus(); // 마우스가 WeatherList에 들어오면 포커스를 줌
        }

        private void WeatherList_MouseWheel(object? sender, MouseEventArgs e)
        {
            // 마우스 휠 이벤트를 처리하여 스크롤바를 조정
            if (e.Delta > 0) // 위로 스크롤
            {
                sbWeather.Value = Math.Max(sbWeather.Minimum, sbWeather.Value - sbWeather.SmallStep);
            }
            else if (e.Delta < 0) // 아래로 스크롤
            {
                sbWeather.Value = Math.Min(sbWeather.Maximum, sbWeather.Value + sbWeather.SmallStep);
            }
        }

        /*
         * ShowWeatherDetails(WeatherType type)
         * - 선택한 날씨의 이름, 설명, 지속시간을 표시하는 메소드
         */
        private void ShowWeatherDetails(WeatherType type)
        {
            _currentType = type;
            lblWeatherName.Visible = lblWeatherDescription.Visible = tbDuration.Visible = true; // 날씨 정보 표시
            tbDuration.Focus(); // 지속시간 입력 필드에 포커스 설정
            lblWeatherName.Text = EnumHelper.GetEnumName(type);
            lblWeatherDescription.Text = EnumHelper.GetEnumDescription(type);
            if (type == OldWeather.Type)
            {
                tbDuration.Text = OldWeather.IsInfinite ? "-1" : OldWeather.Duration.ToString();
            }
            else
            {
                tbDuration.Text = "0"; // 새로 추가하는 날씨는 기본적으로 0으로 설정
            }
        }

        private void tbDuration_Leave(object sender, EventArgs e)
        {
            UpdateWeather(); // 지속시간 입력 필드에서 포커스를 잃을 때 날씨 업데이트
        }

        private void tbDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter 키 입력을 무시
                UpdateWeather(); // Enter 키를 누르면 날씨 업데이트
                this.DialogResult = DialogResult.OK; // 대화상자 닫기
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true; // Escape 키 입력을 무시
                this.Close(); // 대화상자 닫기
            }
        }

        /*
         * UpateWeather()
         * - 날씨의 지속시간을 업데이트하는 메소드
         */
        private void UpdateWeather()
        {
            tbDuration.Text = tbDuration.Text.Trim(); // 입력값의 앞뒤 공백 제거
            if (int.TryParse(tbDuration.Text, out int duration))
            {
                if (duration > 99)
                {
                    MessageBox.Show("지속시간은 99 이하의 숫자만 입력할 수 있습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 날씨를 새로 생성하거나 업데이트
                NewWeather = new(_currentType, duration);
            }
            else
            {
                MessageBox.Show("지속시간으로 유효한 숫자를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
