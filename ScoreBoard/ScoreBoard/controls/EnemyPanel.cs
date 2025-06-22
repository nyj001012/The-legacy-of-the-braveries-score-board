using ScoreBoard.data.monster;
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
using Timer = System.Windows.Forms.Timer;

namespace ScoreBoard.controls
{
    public partial class EnemyPanel : UserControl
    {
        private Timer longPressTimer; // 타이머 변수
        private bool isLongPressing = false; // 롱 프레스 상태 변수
        private const int LongPressThreshold = 1000; // 롱 프레스 시간 임계값 (ms)

        public EnemyPanel(string id, string name, ushort count)
        {
            InitializeComponent();
            lblName.Text = $"{name} ({count})";

            // 타이머 초기화
            longPressTimer = new Timer
            {
                Interval = 1000 // 1초
            };
            longPressTimer.Tick += LongPressTimer_Tick;
        }

        /*
         * ShowEnemyStatus()
         * - 적의 상태를 표시하는 메서드
         */
        private void ShowEnemyStatus()
        {
            // 적의 id를 바탕으로 Monster 데이터 불러오기
            Monster monster = DataReader.ReadMonsterData("monsterId")
                              ?? throw new ArgumentException("몬스터 데이터를 불러올 수 없습니다.");
            // Monster의 Stat을 바탕으로 체력바 세팅
            hbEnemy.SetValues(monster.Stat.Hp, 0, monster.Stat.MaxHp);
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

        private void EnemyPanel_StartLongPress(object sender, MouseEventArgs e)
        {
            isLongPressing = true; // 롱 프레스 상태 시작
            longPressTimer.Start(); // 타이머 시작
        }

        private void ResetLongPress()
        {
            longPressTimer.Stop(); // 타이머 중지
            isLongPressing = false; // 롱 프레스 상태 종료
        }

        private void EnemyPanel_MouseLeave(object sender, EventArgs e)
        {
            ResetLongPress(); // 마우스가 컨트롤을 떠날 때 롱 프레스 상태를 초기화
        }

        private void EnemyPanel_MouseUp(object sender, MouseEventArgs e)
        {
            ResetLongPress(); // 마우스 버튼을 놓을 때 롱 프레스 상태를 초기화
        }
    }
}
