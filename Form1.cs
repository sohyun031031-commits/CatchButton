namespace CatchButton
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private int score = 0;
        private int missCount = 0; // 놓친 횟수
        private const int MAX_MISSES = 20; // 최대 놓친 횟수
        private DateTime mouseEnterTime = DateTime.MinValue;
        private bool isMouseInButton = false;
        private const int EscapeDelayMilliseconds = 300; // 0.3초
        private System.Windows.Forms.Timer checkTimer;
        private bool isGameOver = false; // 게임 종료 여부

        public Form1()
        {
            InitializeComponent();

            // 타이머 초기화
            checkTimer = new System.Windows.Forms.Timer();
            checkTimer.Interval = 50; // 50ms마다 체크
            checkTimer.Tick += CheckTimer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "잡아봐!";
            UpdateScore();
            checkTimer.Start(); // 타이머 시작
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return; // 게임 종료 상태면 체크 안 함

            // 마우스 위치 가져오기
            Point mousePos = Control.MousePosition;
            Point formPos = this.PointToClient(mousePos);

            // 버튼 중심 계산
            int buttonCenterX = button1.Location.X + button1.Width / 2;
            int buttonCenterY = button1.Location.Y + button1.Height / 2;

            // 커서 감지: 버튼 100% 영역
            int detectionRadiusX = button1.Width / 2;
            int detectionRadiusY = button1.Height / 2;

            // 마우스와 버튼 중심 사이의 거리
            int distanceX = Math.Abs(formPos.X - buttonCenterX);
            int distanceY = Math.Abs(formPos.Y - buttonCenterY);

            // 버튼 전체 영역(100%)에 마우스가 있는지 확인
            bool isInButtonArea = distanceX <= detectionRadiusX && distanceY <= detectionRadiusY;

            if (isInButtonArea)
            {
                // 마우스가 버튼 영역에 처음 들어온 경우
                if (!isMouseInButton)
                {
                    isMouseInButton = true;
                    mouseEnterTime = DateTime.Now;
                }
                // 마우스가 버튼 영역에 있는 상태에서 0.3초 경과하면 도망가기
                else if ((DateTime.Now - mouseEnterTime).TotalMilliseconds >= EscapeDelayMilliseconds)
                {
                    score -= 10;
                    missCount++; // 놓친 횟수 증가
                    UpdateScore();
                    isMouseInButton = false;

                    // 20번 놓치면 게임 종료
                    if (missCount >= MAX_MISSES)
                    {
                        EndGame();
                        return;
                    }

                    int maxX = Math.Max(0, this.ClientSize.Width - button1.Width);
                    int maxY = Math.Max(0, this.ClientSize.Height - button1.Height);

                    int x = random.Next(0, maxX + 1);
                    int y = random.Next(0, maxY + 1);

                    button1.Location = new Point(x, y);
                    mouseEnterTime = DateTime.Now; // 새 위치에서 다시 타이머 시작
                }
            }
            else
            {
                // 마우스가 버튼 영역을 벗어남
                isMouseInButton = false;
            }
        }

        private void EndGame()
        {
            isGameOver = true;
            checkTimer.Stop();
            checkTimer.Dispose(); // 타이머 완전 해제
            button1.Enabled = false;

            MessageBox.Show($"게임 종료!\n최종 점수: {score}점\n놓친 횟수: {missCount}/{MAX_MISSES}", "게임 오버");

            ResetGame();
        }

        private void ResetGame()
        {
            score = 0;
            missCount = 0;
            isGameOver = false;
            button1.Size = new Size(89, 23); // 원래 크기로 복원
            button1.Enabled = true;
            isMouseInButton = false;
            mouseEnterTime = DateTime.MinValue;

            // 버튼 위치 초기화
            button1.Location = new Point(332, 69);

            UpdateScore();
            
            checkTimer = new System.Windows.Forms.Timer(); // 새 타이머 생성
            checkTimer.Interval = 50;
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();
        }

        private void UpdateScore()
        {
            this.Text = $"점수: {score}점 | 놓친 횟수: {missCount}/{MAX_MISSES}";
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // 이제 CheckTimer_Tick이 모든 처리를 담당합니다
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 클릭 성공 범위 확인: 버튼 중심부 70% 영역에서만 클릭 성공
            Point mousePos = Control.MousePosition;
            Point formPos = this.PointToClient(mousePos);

            int buttonCenterX = button1.Location.X + button1.Width / 2;
            int buttonCenterY = button1.Location.Y + button1.Height / 2;

            // 클릭 성공 범위: 버튼 70% 영역
            int clickRadiusX = (int)(button1.Width * 0.35);
            int clickRadiusY = (int)(button1.Height * 0.35);

            int distanceX = Math.Abs(formPos.X - buttonCenterX);
            int distanceY = Math.Abs(formPos.Y - buttonCenterY);

            // 50% 영역 내에서만 클릭 성공
            if (distanceX <= clickRadiusX && distanceY <= clickRadiusY)
            {
                // 클릭 성공 - 타이머 리셋
                isMouseInButton = false;
                mouseEnterTime = DateTime.MinValue;

                score += 100;
                UpdateScore();
                MessageBox.Show($"잡았다!\n현재 점수: {score}점");

                // 버튼 크기 10% 감소
                int newWidth = (int)(button1.Width * 0.9);
                int newHeight = (int)(button1.Height * 0.9);
                button1.Size = new Size(newWidth, newHeight);
            }
        }

    }
}
