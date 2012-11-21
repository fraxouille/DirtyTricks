using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Data;

namespace DirtyTricks
{
    static class Collision
    {
        public static Hitbox CollisionMapSprite(Texture2D hitboxMap)
        {
            Color[] t = new Color[hitboxMap.Width * hitboxMap.Height];
            hitboxMap.GetData<Color>(t);

            int[] tab = new int[2];

            int index = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == Color.Red)
                {
                    tab[index] = i;
                    index++;
                }
            }

            int x1 = tab[0] % hitboxMap.Width;
            int y1 = (int)(tab[0] / hitboxMap.Width);
            int x2 = tab[1] % hitboxMap.Width;
            int y2 = (int)(tab[1] / hitboxMap.Width);

            return new Hitbox(x1, y1, x2, y2);
        }

        public static Vector2 MoveSpriteOnMap(Axis axis, Vector2 previousPosition, Vector2 position, Point spriteSize, Hitbox spriteHitbox)
        {
            Vector2 point = new Vector2();
            float speed;
            float X = position.X;
            float Y = position.Y;
            int point1Index, point2Index;

            switch (axis)
            {
                case Axis.YAxis: //Up-Down
                    {
                        point.X = position.X;
                        speed = position.Y - previousPosition.Y;

                        if (previousPosition.Y > position.Y) //Up
                        {
                            if (speed >= 1 || speed <= 1)
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y1);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y1);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                {
                                    Y = previousPosition.Y;
                                    for (int i = 0; i < Math.Abs(speed); i++)
                                    {
                                        Y--;
                                        point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y1);
                                        point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y1);
                                        if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                        {
                                            Y++;
                                            break;
                                        }
                                    }
                                }
                                else
                                    Y = position.Y;
                                point.Y = Y;
                            }
                            else
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y1);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y1);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                    point.Y = previousPosition.Y;
                                else
                                    point.Y = Y;
                            }
                        }
                        else //Down
                        {
                            if (speed >= 1 || speed <= 1)
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y2);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y2);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                {
                                    Y = previousPosition.Y;
                                    for (int i = 0; i < Math.Abs(speed); i++)
                                    {
                                        Y++;
                                        point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y2);
                                        point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y2);
                                        if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                        {
                                            Y--;
                                            break;
                                        }
                                    }
                                }
                                else
                                    Y = position.Y;
                                point.Y = Y;
                            }
                            else
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y2);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y2);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                    point.Y = previousPosition.Y;
                                else
                                    point.Y = Y;
                            }
                        }
                        break;
                    }

                case Axis.XAxis: //Left-Right
                    {
                        point.Y = position.Y;
                        speed = position.X - previousPosition.X;

                        if (previousPosition.X > position.X) //Left
                        {
                            if (speed >= 1 || speed <= 1)
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y1);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y2);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                {
                                    X = previousPosition.X;
                                    for (int i = 0; i < Math.Abs(speed); i++)
                                    {
                                        X--;
                                        point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y1);
                                        point2Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y2);
                                        if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                        {
                                            X++;
                                            break;
                                        }
                                    }
                                }
                                else
                                    X = position.X;
                                point.X = X;
                            }
                            else
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y1);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x1, spriteHitbox.y2);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                    point.X = previousPosition.X;
                                else
                                    point.X = X;
                            }
                        }
                        else //Right
                        {
                            if (speed >= 1 || speed <= 1)
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y1);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y2);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                {
                                    X = previousPosition.X;
                                    for (int i = 0; i < Math.Abs(speed); i++)
                                    {
                                        X++;
                                        point1Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y1);
                                        point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y2);
                                        if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                        {
                                            X--;
                                            break;
                                        }
                                    }
                                }
                                else
                                    X = position.X;
                                point.X = X;
                            }
                            else
                            {
                                point1Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y1);
                                point2Index = Position2MapIndex(X, Y, spriteHitbox.x2, spriteHitbox.y2);
                                if (TestPoints(point1Index, point2Index) || TestScreen(spriteSize, X, Y))
                                    point.X = previousPosition.X;
                                else
                                    point.X = X;
                            }
                        }

                    }
                    break;
            }
            return point;
        }


        private static bool TestPoints(int point1Index, int point2Index)
        {
            if (point1Index >= Map.Current.size ||
                point2Index >= Map.Current.size ||
                point1Index < 0 ||
                point2Index < 0)
                return true;

            return (Map.Current.MapPathPixel[point1Index] == Color.Black ||
                    Map.Current.MapPathPixel[point2Index] == Color.Black);
        }

        private static bool TestScreen(Point spriteSize, float x, float y)
        {
            return ((int)x < 0 ||
                    (int)x > Settings.Current.ScreenWidth - spriteSize.X ||
                    (int)y < 0 ||
                    (int)y > Settings.Current.ScreenHeight - spriteSize.Y);
        }

        private static int Position2MapIndex(float x, float y, int hitboxX, int hitboxY)
        {
            return (int)x + hitboxX - (int)Map.Current.position.X +
                  ((int)y + hitboxY - (int)Map.Current.position.Y) * Map.Current.width;
        }
    }
}
