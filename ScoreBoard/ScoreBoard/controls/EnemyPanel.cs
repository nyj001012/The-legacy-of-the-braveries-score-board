using ScoreBoard.content;
using ScoreBoard.data.monster;
using ScoreBoard.data.statusEffect;
using ScoreBoard.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
        public event EventHandler<(bool, Monster)>? DetailRequested; // 상세 정보 요청 이벤트

        private readonly Monster _monster;

        public EnemyPanel(Monster monster)
        {
            // 생성자에서 id, name, count 초기화
            _monster = monster ?? throw new ArgumentNullException(nameof(monster), "몬스터 정보가 null입니다.");

            // 컨트롤 초기화
            InitializeComponent();
            lblName.Text = $"{_monster.Name} ({_monster.Count})";
            hbEnemy.Name = $"hb{_monster.Id}";
            if (monster.IsReported)
                ShowEnemyStatus();

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
            ShowEnemyHealth();
            ShowEnemyStatusEffect();
        }

        /*
         * ShowEnemyHealth()
         * - 몬스터의 체력을 보여주는 메서드
         */
        private void ShowEnemyHealth()
        {
            // Monster의 Stat을 바탕으로 체력바 세팅
            hbEnemy.SetValues(_monster.Stat.Hp, 0, _monster.Stat.MaxHp);
            hbEnemy.HealthColor = Color.FromArgb(119, 185, 69);
            _monster.IsReported = true; // 상태가 보고되었음을 표시
        }

        /*
         * ShowEnemyStatusEffect()
         * - 몬스터의 상태이상을 보여주는 메서드
         */
        private void ShowEnemyStatusEffect()
        {
            fpnStatus.Controls.Clear(); // 기존 아이콘 제거

            int iconSize = fpnStatus.Height;
            int margin = 3; // 기본 Margin값
            int count = 0;

            foreach (var effect in _monster.Stat.StatusEffects)
            {
                // 다음 아이콘까지 포함했을 때 공간이 부족하면 +N 표시
                if (count == 3)
                {
                    int remaining = _monster.Stat.StatusEffects.Count - count;
                    if (remaining > 0)
                    {
                        TransparentTextLabel label = new()
                        {
                            Text = $"+{remaining}",
                            ForeColor = Color.WhiteSmoke,
                            Font = new Font("Danjo-bold", iconSize / 2),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Size = new Size(iconSize, iconSize),
                            Margin = new Padding(0, margin, 0, 0)
                        };
                        fpnStatus.Controls.Add(label);
                    }
                    break;
                }

                CreateStatusEffectIcon(effect, iconSize, margin);
                count++;
            }
        }

        /*
         * CreateStatusEffectIcon(StatusEffect effect, int size)
         * - 상태이상 아이콘을 생성하고 패널에 추가하는 메서드
         */
        private void CreateStatusEffectIcon(StatusEffect effect, int size, int margin)
        {
            PictureBox pb = new()
            {
                Name = $"pbStatus{effect.Type}",
                Size = new Size(size, size),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = DataReader.GetStatusEffectImage(effect.Type), // 상태이상 아이콘을 설정
                Margin = new Padding(0, 0, margin, 0), // 오른쪽에만 마진을 줌
                Tag = effect.Type // 상태이상 타입을 태그로 설정
            };

            // 툴팁 설정: 상태이상 이름과 설명을 툴팁으로 표시
            ToolTip toolTip = new()
            {
                AutomaticDelay = 0, // 툴팁 표시 지연 시간 (ms)
                AutoPopDelay = 0, // 툴팁 자동 사라지는 시간 (ms)
                InitialDelay = 0, // 툴팁 초기 지연 시간 (ms)
                ReshowDelay = 0, // 툴팁 다시 표시 지연 시간 (ms)
            };

            string caption = $"{EnumHelper.GetEnumName(effect.Type)}:" +
                             $"{(effect.IsInfinite ? "∞" : $"{effect.Duration}턴")}";
            toolTip.SetToolTip(pb, caption);
            fpnStatus.Controls.Add(pb);
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
            DetailRequested?.Invoke(this, (_monster.IsReported, _monster)); // 상세 정보 요청 이벤트 발생
        }
    }
}
