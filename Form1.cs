namespace CatchButton
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "잡아봐!";
            UpdateScore();
        }

        private void UpdateScore()
        {
            this.Text = $"점수: {score}점";
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            int maxX = Math.Max(0, this.ClientSize.Width - button1.Width);
            int maxY = Math.Max(0, this.ClientSize.Height - button1.Height);

            int x = random.Next(0, maxX + 1);
            int y = random.Next(0, maxY + 1);

            button1.Location = new Point(x, y);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            score -= 10;
            UpdateScore();

            int maxX = Math.Max(0, this.ClientSize.Width - button1.Width);
            int maxY = Math.Max(0, this.ClientSize.Height - button1.Height);

            int x = random.Next(0, maxX + 1);
            int y = random.Next(0, maxY + 1);

            button1.Location = new Point(x, y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            score += 100;
            UpdateScore();

            // 버튼 크기를 10% 줄임 (90%로 유지)
            button1.Width = (int)(button1.Width * 0.9);
            button1.Height = (int)(button1.Height * 0.9);

            MessageBox.Show($"잡았다!\n현재 점수: {score}점");
        }

    }
}
