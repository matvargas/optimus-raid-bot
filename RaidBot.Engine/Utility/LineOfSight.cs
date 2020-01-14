using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Utility
{
    public class LineOfSight
    {
        public static Point[] GetLine(double x, double y, double targetX, double targetY)
        {
            List<Point> line = new List<Point>();
            x += 0.5f;
            y += 0.5f;
            targetX += 0.5f;
            targetY += 0.5f;

            double padX = 0;
            double padY = 0;
            double steps = 0;
            double cas = 0;

            if (Math.Abs(x - targetX) == Math.Abs(y - targetY))
            {
                // Diagonale parfaite
                steps = Math.Abs(x - targetX);
                padX = (targetX > x) ? 1 : -1;
                padY = (targetY > y) ? 1 : -1;
                cas = 1;
            }
            else if (Math.Abs(x - targetX) > Math.Abs(y - targetY))
            {
                // On se base sur l'axe X, qui a plus de divisions que l'autre
                steps = Math.Abs(x - targetX);
                padX = (targetX > x) ? 1 : -1;
                padY = (targetY - y) / steps;
                padY = padY * 100;
                padY = Math.Ceiling(padY) / 100;
                cas = 2;
            }
            else
            {
                // On se base sur l'axe Y, qui a plus de divisions que l'autre
                steps = Math.Abs(y - targetY);
                padX = (targetX - x) / steps;
                padX = padX * 100;
                padX = Math.Ceiling(padX) / 100;
                padY = (targetY > y) ? 1 : -1;
                cas = 3;
            }

            int errorSup = (int)Math.Floor(3 + (steps / 2));
            int errorInf = (int)Math.Floor(97 - (steps / 2));

            for (int i = 0; i < (int)steps; i++)
            {
                double cellX, cellY;
                double xPadX = x + padX;
                double yPadY = y + padY;

                if (cas == 2)
                {
                    double beforeY = Math.Ceiling(y * 100 + padY * 50) / 100;
                    double afterY = Math.Floor(y * 100 + padY * 150) / 100;
                    double diffBeforeCenterY = Math.Floor(Math.Abs((double)Math.Floor((double)beforeY) * 100 - beforeY * 100)) / 100;
                    double diffCenterAfterY = Math.Ceiling(Math.Abs((double)Math.Ceiling((double)afterY) * 100 - afterY * 100)) / 100;
                    cellX = Math.Floor(xPadX);

                    if (Math.Floor((double)beforeY) == Math.Floor((double)afterY))
                    {
                        cellY = Math.Floor(yPadY);
                        if ((beforeY == cellY && afterY < cellY) || (afterY == cellY && beforeY < cellY))
                        {
                            cellY = (int)Math.Ceiling(yPadY);
                        }
                        line.Add(new Point((int)cellX, (int)cellY));
                    }
                    else if (Math.Ceiling(beforeY) == Math.Ceiling(afterY))
                    {
                        cellY = Math.Ceiling(yPadY);
                        if ((beforeY == cellY && afterY < cellY) || (afterY == cellY && beforeY < cellY))
                        {
                            cellY = Math.Floor(yPadY);
                        }
                        line.Add(new Point((int)cellX, (int)cellY));
                    }
                    else if (Math.Floor(diffBeforeCenterY * 100) <= errorSup)
                    {
                        //attention aux arrondis selon la distance du pt de départ
                        line.Add(new Point((int)cellX, (int)Math.Floor(afterY)));

                    }
                    else if (Math.Floor(diffCenterAfterY * 100) >= errorInf)
                    {
                        //attention aux arrondis selon la distance du pt de départ
                        line.Add(new Point((int)cellX, (int)Math.Floor(beforeY)));
                    }
                    else
                    {
                        line.Add(new Point((int)cellX, (int)Math.Floor(beforeY)));
                        line.Add(new Point((int)cellX, (int)Math.Floor(beforeY)));
                    }
                }
                else if (cas == 3)
                {
                    double beforeX = Math.Ceiling(x * 100 + padX * 50) / 100;
                    double afterX = Math.Floor(x * 100 + padX * 150) / 100;
                    double diffBeforeCenterX = Math.Floor(Math.Abs(Math.Floor(beforeX) * 100 - beforeX * 100)) / 100;
                    double diffCenterAfterX = Math.Ceiling(Math.Abs(Math.Ceiling(afterX) * 100 - afterX * 100)) / 100;
                    cellY = Math.Floor(yPadY);
                    if (Math.Floor(beforeX) == Math.Floor(afterX))
                    {
                        cellX = Math.Floor(xPadX);
                        if ((beforeX == cellX && afterX < cellX) || (afterX == cellX && beforeX < cellX))
                        {
                            cellX = Math.Ceiling(xPadX);
                        }
                        line.Add(new Point((int)cellX, (int)cellY));
                    }
                    else if (Math.Ceiling(beforeX) == Math.Ceiling(afterX))
                    {
                        cellX = Math.Ceiling(xPadX);
                        if ((beforeX == cellX && afterX < cellX) || (afterX == cellX && beforeX < cellX))
                        {
                            cellX = Math.Floor(xPadX);
                        }
                        line.Add(new Point((int)cellX, (int)cellY));

                    }
                    else if (Math.Floor(diffBeforeCenterX * 100) <= errorSup)
                    {
                        line.Add(new Point((int)Math.Floor(afterX), (int)cellY));

                    }
                    else if (Math.Floor(diffCenterAfterX * 100) >= errorInf)
                    {
                        //attention aux arrondis selon la distance du pt de départ
                        line.Add(new Point((int)Math.Floor(beforeX), (int)cellY));
                    }
                    else
                    {
                        line.Add(new Point((int)Math.Floor(afterX), (int)cellY));
                        line.Add(new Point((int)Math.Floor(beforeX), (int)cellY));
                    }
                }
                else
                {
                    line.Add(new Point((int)Math.Floor(xPadX), (int)Math.Floor(yPadY)));
                }
                x = (x * 100 + padX * 100) / 100;
                y = (y * 100 + padY * 100) / 100;
            }
            return line.ToArray();
        }
    }
}