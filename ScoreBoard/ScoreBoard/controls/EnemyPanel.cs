using ScoreBoard.data.monster;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ScoreBoard.controls
{
    public partial class EnemyPanel : UserControl
    {
        private readonly Timer longPressTimer; // 타이머 변수
        private bool isLongPressing = false; // 롱 프레스 상태 변수
        private const int LongPressThreshold = 1000; // 롱 프레스 시간 임계값 (ms)
        private bool isExposed = false; // 상태가 노출되었는지 여부

        private readonly Monster _monster;

        public EnemyPanel(Monster monster, ushort count)
        {
            // 생성자에서 id, name, count 초기화
            _monster = monster ?? throw new ArgumentNullException(nameof(monster), "몬스터 정보가 null입니다.");

            // 컨트롤 초기화
            InitializeComponent();
            lblName.Text = $"{_monster.Name} ({count})";

            // EnemyPanel 컨트롤의 마우스 이벤트 핸들러 등록
            RegisterMouseEvents(this);

            // 타이머 초기화
            longPressTimer = new Timer
            {
                Interval = 1000 // 1초
            };
            longPressTimer.Tick += LongPressTimer_Tick;
        }

        /*
         * RegisterMouseEvents(Control control)
         * - 적 패널의 마우스 이벤트를 등록하는 메서드
         * - control: 이벤트를 등록할 컨트롤
         */
        private void RegisterMouseEvents(Control control)
        {
            // 이미 연결되어 있을 수도 있으니 중복 등록 방지
            control.MouseDown -= EnemyPanel_MouseDown;
            control.MouseDown += EnemyPanel_MouseDown;
            control.MouseUp -= EnemyPanel_MouseUp;
            control.MouseUp += EnemyPanel_MouseUp;
            control.MouseLeave -= EnemyPanel_MouseLeave;
            control.MouseLeave += EnemyPanel_MouseLeave;
            control.Click -= EnemyPanel_Click;
            control.Click += EnemyPanel_Click;

            foreach (Control child in control.Controls)
            {
                RegisterMouseEvents(child); // 자식도 재귀적으로 등록
            }
        }

        /*
         * ShowEnemyStatus()
         * - 적의 상태를 표시하는 메서드
         */
        private void ShowEnemyStatus()
        {
            // Monster의 Stat을 바탕으로 체력바 세팅
            hbEnemy.SetValues(_monster.Stat.Hp, 0, _monster.Stat.MaxHp);
            hbEnemy.HealthColor = Color.FromArgb(119, 185, 69);
            isExposed = true; // 상태가 노출되었음을 표시
        }

        private void LongPressTimer_Tick(object? sender, EventArgs e)
        {
            if (isLongPressing)
            {
                // 롱 프레스 상태가 유지되고 있다면 타이머를 중지하고 롱 프레스 이벤트를 발생시킴
                longPressTimer.Stop();
                ShowEnemyStatus();
                isLongPressing = false;
            }
        }

        private void EnemyPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            isLongPressing = true; // 롱 프레스 상태 시작
            longPressTimer.Start(); // 타이머 시작
        }

        private void ResetLongPress()
        {
            longPressTimer.Stop(); // 타이머 중지
            isLongPressing = false; // 롱 프레스 상태 종료
        }

        private void EnemyPanel_MouseLeave(object? sender, EventArgs e)
        {
            ResetLongPress(); // 마우스가 컨트롤을 떠날 때 롱 프레스 상태를 초기화
        }

        private void EnemyPanel_MouseUp(object? sender, MouseEventArgs e)
        {
            ResetLongPress(); // 마우스 버튼을 놓을 때 롱 프레스 상태를 초기화
        }

        private void EnemyPanel_Click(object? sender, EventArgs e)
        {
            // 상태가 노출되어야 상세 정보 노출
            if (isExposed)
            {

            }
            return;
        }
    }
}
