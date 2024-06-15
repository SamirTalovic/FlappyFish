using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Flappy_Bird_Windows_Form
{
    public partial class Form1 : Form
    {
        int pipeSpeed = 8;
        int gravity = 15;
        int score = 0;

        int birdX = 69;
        int birdY = 228;
        int birdWidth = 40;
        int birdHeight = 30;

        int pipeWidth = 60;
        int pipeGap = 150;
        int pipeTopHeight = 200;
        int pipeBottomHeight = 200;
        int pipeX = 489;

        int groundHeight = 126;

        GraphicsPath birdPath;
        GraphicsPath pipeTopPath;
        GraphicsPath pipeBottomPath;
        Rectangle groundRect;
        SolidBrush birdBrush = new SolidBrush(Color.Yellow); // Default color is yellow
        SolidBrush pipeBrush = new SolidBrush(Color.Green); // Default color is yellow
        SolidBrush groundBrush = new SolidBrush(Color.Brown); // Default color is yellow
        SolidBrush scoreBrush = new SolidBrush(Color.Black); // Default color is yellow

        public Form1()
        {
            InitializeComponent();
            InitializeGameObjects();
        }

        private void InitializeGameObjects()
        {
            birdPath = new GraphicsPath();

            // Square body
            birdPath.AddRectangle(new RectangleF(birdX, birdY, birdWidth, birdHeight));
            

            // Left leg (triangle)
            birdPath.AddPolygon(new PointF[]
            {
                new PointF(birdX + birdWidth / 4, birdY + birdHeight),
                new PointF(birdX + birdWidth / 2, birdY + birdHeight + birdHeight / 4),
                new PointF(birdX + birdWidth / 4 * 3, birdY + birdHeight),
            });

            // Right leg (triangle)
            birdPath.AddPolygon(new PointF[]
            {
                new PointF(birdX + birdWidth / 4 * 3, birdY + birdHeight),
                new PointF(birdX + birdWidth / 2, birdY + birdHeight + birdHeight / 4),
                new PointF(birdX + birdWidth / 4 * 5, birdY + birdHeight),
            });

            // Eye (circle)
            birdPath.AddEllipse(birdX + birdWidth / 2 + 6, birdY + birdHeight / 4, 4, 4);

            // Tail (triangle)
            birdPath.AddPolygon(new PointF[]
            {
                new PointF(birdX, birdY + birdHeight / 2),
                new PointF(birdX - 10, birdY + birdHeight / 2 - 5),
                new PointF(birdX - 10, birdY + birdHeight / 2 + 5),
            });


            pipeTopPath = new GraphicsPath();
            pipeTopPath.AddPolygon(new PointF[]
            {
                new PointF(pipeX, 0),
                new PointF(pipeX + pipeWidth, 0),
                new PointF(pipeX + pipeWidth, pipeTopHeight - 5),
                new PointF(pipeX + pipeWidth + 5, pipeTopHeight - 5),
                new PointF(pipeX + pipeWidth + 5, pipeTopHeight),
                new PointF(pipeX - 5, pipeTopHeight),
                new PointF(pipeX - 5, pipeTopHeight - 5),
                new PointF(pipeX, pipeTopHeight - 5),
            });

            pipeBottomPath = new GraphicsPath();
            pipeBottomPath.AddPolygon(new PointF[]
            {
                new PointF(pipeX, this.ClientSize.Height),
                new PointF(pipeX + pipeWidth, this.ClientSize.Height),
                new PointF(pipeX + pipeWidth, this.ClientSize.Height - pipeBottomHeight + 5),
                new PointF(pipeX + pipeWidth + 5, this.ClientSize.Height - pipeBottomHeight + 5),
                new PointF(pipeX + pipeWidth + 5, this.ClientSize.Height - pipeBottomHeight),
                new PointF(pipeX - 5, this.ClientSize.Height - pipeBottomHeight),
                new PointF(pipeX - 5, this.ClientSize.Height - pipeBottomHeight + 5),
                new PointF(pipeX, this.ClientSize.Height - pipeBottomHeight + 5)
            });

            groundRect = new Rectangle(0, this.ClientSize.Height - groundHeight, this.ClientSize.Width, groundHeight);
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -15;
            }
            else if (e.KeyCode == Keys.F)
            {
                // Change bird color to a random color
                Random random = new Random();
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                birdBrush = new SolidBrush(randomColor);
                Invalidate();
            }
            else if (e.KeyCode == Keys.P)
            {
                // Change bird color to a random color
                Random random = new Random();
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                pipeBrush = new SolidBrush(randomColor);
                Invalidate();
            }
            else if (e.KeyCode == Keys.G)
            {
                // Change bird color to a random color
                Random random = new Random();
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                groundBrush = new SolidBrush(randomColor);
                Invalidate();
            }
            else if (e.KeyCode == Keys.S)
            {
                // Change bird color to a random color
                Random random = new Random();
                Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                scoreBrush = new SolidBrush(randomColor);
                Invalidate();
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 15;
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            MessageBox.Show($"Game over!!! Your score is {score}");
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            birdY += gravity;

            Matrix translateMatrix = new Matrix();
            translateMatrix.Translate(-pipeSpeed, 0);

            pipeTopPath.Transform(translateMatrix);
            pipeBottomPath.Transform(translateMatrix);

            if (pipeBottomPath.GetBounds().Left < -pipeWidth)
            {
                ResetPipePaths();
                score++;
            }
            birdPath.Reset();
            birdPath.AddRectangle(new RectangleF(birdX, birdY, birdWidth, birdHeight)); // Square body

            birdPath.AddEllipse(birdX + birdWidth / 2 + 6, birdY + birdHeight / 4, 4, 4); // Eye (circle)

            birdPath.AddPolygon(new PointF[] // Tail (triangle)
            {
                new PointF(birdX, birdY + birdHeight / 2),
                new PointF(birdX - 10, birdY + birdHeight / 2 - 7),
                new PointF(birdX - 10, birdY + birdHeight / 2 + 7),
            });

            if (birdPath.GetBounds().IntersectsWith(pipeBottomPath.GetBounds()) ||
                birdPath.GetBounds().IntersectsWith(pipeTopPath.GetBounds()) ||
                birdPath.GetBounds().IntersectsWith(groundRect) ||
                birdY < -25)
            {
                endGame();
            }

            if (score > 5)
            {
                pipeSpeed = 15;
            }

            Invalidate();
        }

        private void ResetPipePaths()
        {
            Random rand = new Random();
            int gapY = rand.Next(50, this.ClientSize.Height - pipeGap - groundHeight - 50);

            pipeTopPath.Reset();
            pipeTopPath.AddPolygon(new PointF[]
            {
                new PointF(this.ClientSize.Width, 0),
                new PointF(this.ClientSize.Width + pipeWidth, 0),
                new PointF(this.ClientSize.Width + pipeWidth, gapY - 5),
                new PointF(this.ClientSize.Width + 5 + pipeWidth, gapY - 5),
                new PointF(this.ClientSize.Width + 5 + pipeWidth, gapY),
                new PointF(this.ClientSize.Width - 5, gapY),
                new PointF(this.ClientSize.Width - 5, gapY - 5),


                  new PointF(this.ClientSize.Width, gapY - 5),
            });

            pipeBottomPath.Reset();
            pipeBottomPath.AddPolygon(new PointF[]
            {
                new PointF(this.ClientSize.Width, this.ClientSize.Height),
                new PointF(this.ClientSize.Width + pipeWidth, this.ClientSize.Height),
                new PointF(this.ClientSize.Width + pipeWidth, gapY + pipeBottomHeight + 5),
                new PointF(this.ClientSize.Width + pipeWidth + 5, gapY + pipeBottomHeight + 5),
                new PointF(this.ClientSize.Width + pipeWidth + 5, gapY + pipeBottomHeight),
                new PointF(this.ClientSize.Width - 5, gapY + pipeBottomHeight),
                new PointF(this.ClientSize.Width - 5, gapY + pipeBottomHeight + 5),
                new PointF(this.ClientSize.Width, gapY + pipeBottomHeight + 5)
            });
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw bird
            g.RotateTransform(-10);

            g.FillPath(birdBrush, birdPath);
            g.RotateTransform(10);

            // Draw pipes
            g.FillPath(pipeBrush, pipeTopPath);
            g.FillPath(pipeBrush, pipeBottomPath);

            // Draw ground
            g.FillRectangle(groundBrush, groundRect);

            // Draw score
            using (Font font = new Font("Arial Narrow", 24F, FontStyle.Bold))
            {
                g.DrawString($"Score: {score}", font, scoreBrush , 181, this.ClientSize.Height - groundHeight + 20);
            }
        }
    }
}