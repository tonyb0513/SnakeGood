using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake__good_
{
    public partial class Form1 : Form
    {
        private List<Segment> Snake = new List<Segment>();  //Creates a list (array) for the player
        private List<Segment> Snake2 = new List<Segment>();
        private Segment fruit = new Segment();
        private Difficulty diff = new Difficulty();
        private bool dead1 = false;
        private bool dead2 = false;
        private bool multiplayer = true;
        public Form1()
        {
            
            InitializeComponent();

                //gameTimer.Interval = 50;
                //gameTimer.Tick += updateScreen;
                //gameTimer.Start();
                startGame();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SolidBrush wallBlack = new SolidBrush(Color.Black);
            SolidBrush wallWhite = new SolidBrush(Color.Empty);
            SolidBrush background = new SolidBrush(Color.Gray);
            SolidBrush snake = new SolidBrush(Color.Black);
            SolidBrush snake2 = new SolidBrush(Color.White);
            SolidBrush fruitPrint = new SolidBrush(Color.Red);
            Graphics graphics;
            graphics = this.CreateGraphics();
            graphics.FillRectangle(background, new Rectangle(0, 0, 410, 410));
            for (int i = 0; i < 41; i++)
            {
                graphics.FillRectangle(wallBlack, new Rectangle(0, i * 10, 10, 10));
                graphics.FillRectangle(wallBlack, new Rectangle(i * 10, 0, 10, 10));
                graphics.FillRectangle(wallBlack, new Rectangle(400, i * 10, 10, 10));
                graphics.FillRectangle(wallBlack, new Rectangle(i * 10, 400, 10, 10));
            }
            if (dead1 == true && dead2 == true)
                GameState.GameOver = true;

            if (Snake != null)
            {
                    for (int i = 0; i < Snake.Count; i++)
                    {
                        graphics.FillRectangle(snake, new Rectangle(Snake[i].X, Snake[i].Y, 10, 10));
                    }
            }
            if (multiplayer == true)
            {
                if (Snake2 != null)
                {
                    for (int i = 0; i < Snake2.Count; i++)
                    {
                        graphics.FillRectangle(snake2, new Rectangle(Snake2[i].X, Snake2[i].Y, 10, 10));
                    }
                }
            }
            graphics.FillRectangle(fruitPrint, new Rectangle(fruit.X, fruit.Y, 10, 10));
            graphics.Dispose();
            wallBlack.Dispose();
            wallWhite.Dispose();
            snake.Dispose();
            snake2.Dispose();
        }

        private void updateScreen(object sender, EventArgs e)
        {
            if (GameState.GameOver == true)
            {
                if (multiplayer == true)
                    label3.Text = "Game over! \nPress space to play again.";
                else
                    label3.Text = "Game over! \n Score: " + GameState.Score + "\n Press space to play again.";
                label3.Visible = true;
                if (input.KeyPress(Keys.Space))
                    startGame();
            }
            else 
            {
                if (input.KeyPress(Keys.Right) && GameState.direction != Directions.Left)
                    GameState.direction = Directions.Right;
                if (input.KeyPress(Keys.Left) && GameState.direction != Directions.Right)
                    GameState.direction = Directions.Left;
                if (input.KeyPress(Keys.Up) && GameState.direction != Directions.Down)
                    GameState.direction = Directions.Up;
                if (input.KeyPress(Keys.Down) && GameState.direction != Directions.Up)
                    GameState.direction = Directions.Down;
                if (multiplayer == true)
                {
                    if (input.KeyPress(Keys.A) && GameState2.direction2 != Directions2.Left2)
                        GameState2.direction2 = Directions2.Right2;
                    if (input.KeyPress(Keys.D) && GameState2.direction2 != Directions2.Right2)
                        GameState2.direction2 = Directions2.Left2;
                    if (input.KeyPress(Keys.W) && GameState2.direction2 != Directions2.Down2)
                        GameState2.direction2 = Directions2.Up2;
                    if (input.KeyPress(Keys.S) && GameState2.direction2 != Directions2.Up2)
                        GameState2.direction2 = Directions2.Down2;
                }
                movePlayer();
            }
            this.Invalidate();
        }


        private void updateGraphics(object sender, PaintEventArgs e)
        {
            SolidBrush snakeColor = new SolidBrush(Color.Black);
            Graphics graphics = this.CreateGraphics();
            if (GameState.GameOver == false)
            {

                for (int i = 0; i < Snake.Count; i++)
                {
                    graphics.FillRectangle(snakeColor, new Rectangle(Snake[i].X, Snake[i].Y, 10, 10));
                    graphics.FillRectangle(Brushes.Red, new Rectangle(fruit.X, fruit.Y, 10, 10));
                }
            }
        }

        private void startGame()
        {
            GameState.GameOver = false;
            label3.Visible = false;
            new GameState();
            new GameState2();
            Snake.Clear();
            Snake2.Clear();
            dead1 = false;
            if (multiplayer == true)
                dead2 = false;
            if (multiplayer == false)
            {
                Segment head = new Segment { X = 200, Y = 200 };
                Snake.Add(head);
            }
            if (multiplayer == true)
            {
                Segment head = new Segment { X = 100, Y = 100 };
                Snake.Add(head);
                Segment head2 = new Segment { X = 300, Y = 300 };
                Snake2.Add(head2);
            }
            label2.Text = GameState.Score.ToString();

            generateFruit();
        }

        private void movePlayer()
        {
            if (dead1 == false)
            {
                for (int i = Snake.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        switch (GameState.direction)
                        {
                            case Directions.Right:
                                Snake[i].X += 10;
                                break;
                            case Directions.Left:
                                Snake[i].X -= 10;
                                break;
                            case Directions.Up:
                                Snake[i].Y -= 10;
                                break;
                            case Directions.Down:
                                Snake[i].Y += 10;
                                break;
                        }
                        if (diff.easy == true)
                        {
                            if (Snake[0].X <= 00 && GameState.direction == Directions.Left)
                                Snake[0].X = 390;
                            else if (Snake[0].Y <= 0 && GameState.direction == Directions.Up)
                                Snake[0].Y = 390;
                            else if (Snake[0].X >= 400 && GameState.direction == Directions.Right)
                                Snake[0].X = 10;
                            else if (Snake[0].Y >= 400 && GameState.direction == Directions.Down)
                                Snake[0].Y = 10;
                        }
                        if (diff.easy == false)
                        {
                            if (Snake[0].X <= 10 && GameState.direction == Directions.Left)
                                dead1 = true;
                            else if (Snake[0].Y <= 10 && GameState.direction == Directions.Up)
                                dead1 = true;
                            else if (Snake[0].X >= 390 && GameState.direction == Directions.Right)
                                dead1 = true;
                            else if (Snake[0].Y >= 390 && GameState.direction == Directions.Down)
                                dead1 = true;
                        }
                        for (int k = 1; k < Snake.Count; k++)
                        {
                            if ((Snake[i].X == Snake[k].X) && (Snake[i].Y == Snake[k].Y))
                                dead1 = true;
                        }
                        if (multiplayer == true)
                        {
                            for (int k = 1; k < Snake2.Count; k++)
                            {
                                if ((Snake[0].X == Snake2[k].X) && (Snake[0].Y == Snake2[k].Y))
                                {
                                    dead1 = true;
                                }
                            }
                        }
                        if (Snake[0].X == fruit.X && Snake[0].Y == fruit.Y)
                            eatFruit();
                        if (multiplayer == true)
                        {
                            if (Snake[0].X == Snake2[0].X && Snake[0].Y == Snake2[0].Y)
                            {
                                dead1 = true;
                                dead2 = true;
                            }
                        }
                    }
                    else
                    {
                        Snake[i].X = Snake[i - 1].X;
                        Snake[i].Y = Snake[i - 1].Y;
                    }
                }
            }
            if (multiplayer == true)
            {
                if (dead2 == false)
                {
                    for (int i = Snake2.Count - 1; i >= 0; i--)
                    {
                        if (i == 0)
                        {
                            switch (GameState2.direction2)
                            {
                                case Directions2.Right2:
                                    Snake2[i].X -= 10;
                                    break;
                                case Directions2.Left2:
                                    Snake2[i].X += 10;
                                    break;
                                case Directions2.Up2:
                                    Snake2[i].Y -= 10;
                                    break;
                                case Directions2.Down2:
                                    Snake2[i].Y += 10;
                                    break;
                            }
                            if (diff.easy == true)
                            {
                                if (Snake2[0].X <= 00 && GameState2.direction2 == Directions2.Right2)
                                    Snake2[0].X = 390;
                                else if (Snake2[0].Y <= 0 && GameState2.direction2 == Directions2.Up2)
                                    Snake2[0].Y = 390;
                                else if (Snake2[0].X >= 400 && GameState2.direction2 == Directions2.Left2)
                                    Snake2[0].X = 10;
                                else if (Snake2[0].Y >= 400 && GameState2.direction2 == Directions2.Down2)
                                    Snake2[0].Y = 10;
                            }
                            if (diff.easy == false)
                            {
                                if (Snake2[0].X <= 10 && GameState2.direction2 == Directions2.Right2)
                                    dead2 = true;
                                else if (Snake2[0].Y <= 10 && GameState2.direction2 == Directions2.Up2)
                                    dead2 = true;
                                else if (Snake2[0].X >= 390 && GameState2.direction2 == Directions2.Left2)
                                    dead2 = true;
                                else if (Snake2[0].Y >= 390 && GameState2.direction2 == Directions2.Down2)
                                    dead2 = true;
                            }
                            for (int k = 1; k < Snake2.Count; k++)
                            {
                                if ((Snake2[i].X == Snake2[k].X) && (Snake2[i].Y == Snake2[k].Y))
                                    dead2 = true;
                            }
                            for (int k = 0; k < Snake.Count; k++)
                            {
                                if ((Snake2[0].X == Snake[k].X) && (Snake2[0].Y == Snake[k].Y))
                                    dead2 = true;
                            }
                            if (Snake[0].X == Snake2[0].X && Snake[0].Y == Snake2[0].Y)
                            {
                                dead1 = true;
                                dead2 = true;
                            }
                            if (Snake2[0].X == fruit.X && Snake2[0].Y == fruit.Y)
                                eatFruit2();
                        }
                        else
                        {
                            Snake2[i].X = Snake2[i - 1].X;
                            Snake2[i].Y = Snake2[i - 1].Y;
                        }
                    }
                }
            }
        }

        private void generateFruit()
        {
            Random rand = new Random();
            fruit = new Segment { X = rand.Next(1, 40) * 10, Y = rand.Next(1,40) * 10 };
            for(int i = 0; i < Snake.Count; i++)
            {
                if (fruit.X == Snake[i].X && fruit.Y == Snake[i].Y)
                    
                    generateFruit();
            }
            if(multiplayer == true)
            {
                for (int i = 0; i < Snake2.Count; i++)
                {
                    if (fruit.X == Snake2[i].X && fruit.Y == Snake2[i].Y)
                        generateFruit();
                }
            }

        }

        private void eatFruit()
        {
            Segment body = new Segment { X = Snake[Snake.Count - 1].X, Y = Snake[Snake.Count - 1].Y };
            Snake.Add(body);
            GameState.Score += 10;
            label2.Text = GameState.Score.ToString();
            generateFruit();
        }
        private void eatFruit2()
        {
            Segment body = new Segment { X = Snake2[Snake2.Count - 1].X, Y = Snake2[Snake2.Count - 1].Y };
            Snake2.Add(body);
            GameState2.Score += 10;
            label2.Text = GameState2.Score.ToString();
            generateFruit();
        }

        private void die()
        {
            GameState.GameOver = true;
        }
        private void keyisdown(object sender, KeyEventArgs e)
        {
            input.changeState(e.KeyCode, true);                
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            input.changeState(e.KeyCode, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameTimer.Interval = 50;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();
            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            diff.easy = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameTimer.Interval = 30;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();
            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            diff.easy = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button3.Enabled = false;
            button1.Visible = true;
            button1.Enabled = true;
            button4.Visible = false;
            button4.Enabled = false;
            button2.Visible = true;
            button2.Enabled = true;
            dead2 = true;
            multiplayer = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            startGame();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button3.Enabled = false;
            button1.Visible = true;
            button1.Enabled = true;
            button4.Visible = false;
            button4.Enabled = false;
            button2.Visible = true;
            button2.Enabled = true;
            multiplayer = true;
            label5.Visible = true;
            label4.Visible = true;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
        }
    }
}
