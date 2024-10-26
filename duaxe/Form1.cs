using System;
using System.Windows.Forms;

namespace duaxe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            over.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        int carspeed = 3;
        int score;
        int collectedcoin = 0;
        Random pos = new Random();

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && mycar.Left > 0) mycar.Left -= 3;
            if (e.KeyCode == Keys.Right && mycar.Left < 400) mycar.Left += 3;
            if (e.KeyCode == Keys.Up && carspeed < 30) carspeed++;
            if (e.KeyCode == Keys.Down && carspeed > 3) carspeed--;
        }

        void totalscore()
        {
            textBox1.Text = score.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            linemove(carspeed);
            gameover();
            totalscore();
            Coinscollection();
        }

        void linemove(int speed)
        {
            MoveLine(pictureBox1, speed);
            MoveLine(pictureBox2, speed);
            MoveLine(pictureBox3, speed);
            MoveEnemy(enemy1, speed);
            MoveEnemy(enemy2, speed);
            MoveEnemy(enemy3, speed);
            MoveEnemy(enemy4, speed);
            MoveCoin(v1, speed);
            MoveCoin(v2, speed);
            MoveCoin(v4, speed);
            MoveCoin(v5, speed);
        }

        void MoveLine(PictureBox line, int speed)
        {
            if (line.Top > 500) line.Top = 0;
            else line.Top += speed;
        }

        void MoveEnemy(PictureBox enemy, int speed)
        {
            if (enemy.Top > 500)
            {
                score++;
                enemy.Left = pos.Next(0, 400);
                enemy.Top = 0;
            }
            else enemy.Top += speed;
        }

        void MoveCoin(PictureBox coin, int speed)
        {
            if (coin.Top > 500)
            {
                coin.Left = pos.Next(0, 400);
                coin.Top = 0;
            }
            else coin.Top += speed;
        }

        void Coinscollection()
        {
            CheckCoinCollection(v1);
            CheckCoinCollection(v2);
            CheckCoinCollection(v4);
            CheckCoinCollection(v5);
        }

        void CheckCoinCollection(PictureBox coin)
        {
            if (mycar.Bounds.IntersectsWith(coin.Bounds))
            {
                collectedcoin++;
                coins.Text = "V =" + collectedcoin.ToString();
                coin.Left = pos.Next(0, 400);
                coin.Top = pos.Next(0, 40);

                // Tăng tốc độ sau mỗi đồng xu
                if (collectedcoin % 3 == 0)
                {
                    carspeed++;
                }
            }
            
        }

        void gameover()
        {
            if (mycar.Bounds.IntersectsWith(enemy1.Bounds) ||
                mycar.Bounds.IntersectsWith(enemy2.Bounds) ||
                mycar.Bounds.IntersectsWith(enemy3.Bounds) ||
                mycar.Bounds.IntersectsWith(enemy4.Bounds))
            {
                timer1.Enabled = false;
                DialogResult go = MessageBox.Show("Do you want to play again?", "GAME OVER", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (go == DialogResult.Yes)
                {
                    ResetGame();
                }
                else
                {
                    this.Close();
                }
                over.Visible = true;
            }
        }

        void ResetGame()
        {
            timer1.Enabled = true;
            score = 0;
            collectedcoin = 0; 
            coins.Text = "V = 0"; 
            carspeed = 3; 
            ResetPositions();
        }

        void ResetPositions()
        {
            foreach (var enemy in new[] { enemy1, enemy2, enemy3, enemy4 })
            {
                enemy.Left = pos.Next(0, 400);
                enemy.Top = 0;
            }

            foreach (var coin in new[] { v1, v2, v4, v5 })
            {
                coin.Left = pos.Next(0, 400);
                coin.Top = pos.Next(0, 40);
            }

        }
    }
}
