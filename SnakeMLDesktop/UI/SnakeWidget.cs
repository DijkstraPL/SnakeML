using SnakeMLDesktop.NeuralNet.GameEngine;
using System;
using System.Windows.Controls;

namespace SnakeMLDesktop.UI
{
    //public class SnakeWidget : UserControl
    //{
    //    private Snake _snake;
    //    private int[,] _board;
    //    private bool _drawVision = true;

    //    public SnakeWidget(Control parent, Tuple<int, int> boardSize, Snake snake = null)
    //    {
    //        Parent = parent;
    //        _board = new int[boardSize.Item1, boardSize.Item2];
    //        _snake = snake ?? new Snake(boardSize);
    //        Focus();
    //        Show();
    //    }

    //    public void NewGame()
    //    {
    //        _snake = new Snake(_board.GetLength(0), _board.GetLength(1));
    //        _drawVision = true;
    //    }

    //    public void Update()
    //    {
    //        if (_snake.IsAlive)
    //        {
    //            _snake.Update();
    //            Invalidate();
    //        }
    //        else
    //        {
    //            // Dead
    //        }
    //    }

    //    private void DrawBorder(Graphics g)
    //    {
    //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    //        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

    //        var width = Width;
    //        var height = Height;
    //        var pen = new Pen(Color.Black);

    //        g.DrawLine(pen, 0, 0, width, 0);
    //        g.DrawLine(pen, width, 0, width, height);
    //        g.DrawLine(pen, 0, height, width, height);
    //        g.DrawLine(pen, 0, 0, 0, height);
    //    }

    //    private void DrawSnake(Graphics g)
    //    {
    //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
    //        var pen = new Pen(Color.Black);
    //        var brush = new SolidBrush(Color.FromArgb(198, 5, 20));

    //        foreach (var point in _snake.SnakeArray)
    //        {
    //            g.FillRectangle(brush, point.X * SQUARE_SIZE[0], point.Y * SQUARE_SIZE[1], SQUARE_SIZE[0], SQUARE_SIZE[1]);
    //        }

    //        if (_drawVision)
    //        {
    //            var start = _snake.SnakeArray[0];

    //            if (_snake.DrawableVision[0])
    //            {
    //                foreach (var drawableVision in _snake.DrawableVision)
    //                {
    //                    var startX = start.X * SQUARE_SIZE[0] + SQUARE_SIZE[0] / 2;
    //                    var startY = start.Y * SQUARE_SIZE[1] + SQUARE_SIZE[1] / 2;

    //                    if (drawableVision.AppleLocation != null && drawableVision.SelfLocation != null)
    //                    {
    //                        var appleDist = CalcDistance(start.X, drawableVision.AppleLocation.X, start.Y, drawableVision.AppleLocation.Y);
    //                        var selfDist = CalcDistance(start.X, drawableVision.SelfLocation.X, start.Y, drawableVision.SelfLocation.Y);

    //                        if (appleDist <= selfDist)
    //                        {
    //                            DrawLineToApple(g, startX, startY, drawableVision);
    //                            DrawLineToSelf(g, startX, startY, drawableVision);
    //                        }
    //                        else
    //                        {
    //                            DrawLineToSelf(g, startX, startY, drawableVision);
    //                            DrawLineToApple(g, startX, startY, drawableVision);
    //                        }
    //                    }
    //                    else if (drawableVision.AppleLocation != null)
    //                    {
    //                        DrawLineToApple(g, startX, startY, drawableVision);
    //                    }
    //                    else if (drawableVision.SelfLocation != null)
    //                    {
    //                        DrawLineToSelf(g, startX, startY, drawableVision);
    //                    }

    //                    if (drawableVision.WallLocation != null)
    //                    {
    //                        g.DrawLine(pen, startX, startY, drawableVision.WallLocation.X * SQUARE_SIZE[0] + SQUARE_SIZE[0] / 2, drawableVision.WallLocation.Y * SQUARE_SIZE[1] + SQUARE_SIZE[1] / 2);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    private void DrawLineToApple(Graphics g, int startX, int startY, DrawableVision drawableVision)
    //    {
    //        g.DrawLine(new Pen(Color.Green), startX, startY, drawableVision.AppleLocation.X * SQUARE_SIZE[0] + SQUARE_SIZE[0] / 2, drawableVision.AppleLocation.Y * SQUARE_SIZE[1] + SQUARE_SIZE[1] / 2);
    //    }

    //    private void DrawLineToSelf(Graphics g, int startX, int startY, DrawableVision drawableVision)
    //    {
    //        g.DrawLine(new Pen(Color.Red), startX, startY, drawableVision.SelfLocation.X * SQUARE_SIZE[0] + SQUARE_SIZE[0] / 2, drawableVision.SelfLocation.Y * SQUARE_SIZE[1] + SQUARE_SIZE[1] / 2);
    //    }

    //    private void DrawApple(Graphics g)
    //    {
    //        var appleLocation = _snake.AppleLocation;

    //        if (appleLocation != null)
    //        {
    //            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
    //            g.FillRectangle(Brushes.Green, appleLocation.X * SQUARE_SIZE[0], appleLocation.Y * SQUARE_SIZE[1], SQUARE_SIZE[0], SQUARE_SIZE[1]);
    //        }
    //    }

    //    protected override void OnPaint(PaintEventArgs e)
    //    {
    //        base.OnPaint(e);
    //        var g = e.Graphics;

    //        DrawBorder(g);
    //        DrawApple(g);
    //        DrawSnake(g);
    //    }

    //    protected override void OnKeyPress(KeyPressEventArgs e)
    //    {
    //        base.OnKeyPress(e);

    //        var key = e.KeyChar;
    //        // Handle key presses
    //    }

    //    private float CalcDistance(int x1, int x2, int y1, int y2)
    //    {
    //        var diffX = Math.Abs(x2 - x1);
    //        var diffY = Math.Abs(y2 - y1);
    //        return (float)Math.Sqrt(diffX * diffX + diffY * diffY);
    //    }
    //}
}
