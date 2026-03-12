namespace CatchButton
{
    public partial class Form1 : Form
    {
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "잡아봐!";
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            int maxX = Math.Max(0, this.ClientSize.Width - button1.Width);
            int maxY = Math.Max(0, this.ClientSize.Height - button1.Height);

            int x = random.Next(0, maxX + 1);
            int y = random.Next(0, maxY + 1);

            button1.Location = new Point(x, y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("잡았다!");
        }

    }
}
