using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird_Windows_Form
{

        public class PipeTopControl : Control
        {
            public PipeTopControl()
            {
                // Set default size for the custom control
                this.Size = new Size(100, 230);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                DrawPipeTop(e.Graphics);
            }

            private void DrawPipeTop(Graphics g)
            {
                // Define the points for the polygon (pipeTop)
                Point[] pipeTopPoints = {
                new Point(0, 0),
                new Point(this.Width, 0),
                new Point(this.Width, 40),
                new Point(this.Width - 10, 40),
                new Point(this.Width - 10, this.Height),
                new Point(10, this.Height),
                new Point(10, 40),
                new Point(0, 40)
            };

                // Draw the polygon using a solid brush
                g.FillPolygon(Brushes.Green, pipeTopPoints);

                // Optionally, draw the border of the polygon
                g.DrawPolygon(Pens.Black, pipeTopPoints);
            }
        }
    }

