/*
 * Name: Riley, Peter and Quinn
 * Date: June 18th, 2019
 * Description: A tower defense game where you try to eat angry food to protect a sacred fridge
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace hungaryTDv1
{
    public class Enemy
    {
        public ImageBrush enemyFill = new ImageBrush();
        public BitmapImage bi;
        public Type type;
        public enum Type {apple, pizza, donut, hamburger, fries}
        public Canvas cEnemies = new Canvas();
        public Canvas cBackground = new Canvas();
        public Rectangle sprite = new Rectangle();
        public int speed;
        public int health;
        public int damage;
        public Point[] track;
        public int[] positions;
        int position = 0;
        public Enemy(int ty, Canvas e, Canvas b, Point[] tr, int[] p)
        {
            type = (Type)ty;
            cEnemies = e;
            cBackground = b;
            track = tr;
            positions = p;
            if (type == Type.apple)
            {
                bi = new BitmapImage(new Uri("apple.png", UriKind.Relative));
                speed = 3;
                health = 3;
            }
            else if (type == Type.pizza)
            {
                bi = new BitmapImage(new Uri("pizza.png", UriKind.Relative));
                speed = 5;
                health = 5;
            }
            else if (type == Type.donut)
            {
                bi = new BitmapImage(new Uri("donut.png", UriKind.Relative));
                speed = 3;
                health = 3;
            }
            else if (type == Type.hamburger)
            {
                bi = new BitmapImage(new Uri("hamburger.png", UriKind.Relative));
                speed = 2;
                health = 50;
            }
            else if (type == Type.fries)
            {
                bi = new BitmapImage(new Uri("fries.png", UriKind.Relative));
                speed = 10;
                health = 5;
            }
            enemyFill = new ImageBrush(bi);
            sprite.Fill = enemyFill;
            sprite.Height = 50;
            sprite.Width = 50;
            Canvas.SetLeft(sprite, track[position].X - 25);
            Canvas.SetTop(sprite, track[position].Y - 25);
            cEnemies.Children.Add(sprite);
            cBackground.Children.Remove(cEnemies);
            cBackground.Children.Add(cEnemies);

        }
        public int update(int index)
        {
            positions[position] = -1;
            for (int i = 1; i < 10; i++)
            {
                if (position + i < positions.Length)
                {
                    positions[position + i] = -1;
                }
                if (position - i > -1)
                {
                    positions[position - i] = -1;
                }
            }
            if (position < 1450 - speed - 9)
            {
                for (int i = 0; i < speed + 1; i++)
                {
                    if (positions[position + i + 9] != -1)
                    {
                        position = position + i - 1;
                        positions[position] = index;
                        for (int x = 1; x < 10; x++)
                        {
                            if (position + x < positions.Length)
                            {
                                positions[position + x] = index;
                            }
                            if (position - x > -1)
                            {
                                positions[position - x] = index;
                            }
                        }
                        break;
                    }
                    else if (i ==  speed && positions[position + i] == -1)
                    {
                        position = position + i - 1;
                        positions[position] = index;
                        for (int x = 1; x < 10; x++)
                        {
                            if (position + x < positions.Length)
                            {
                                positions[position + x] = index;
                            }
                            if (position - x > -1)
                            {
                                positions[position - x] = index;
                            }
                        }

                        break;
                    }
                }
                Canvas.SetLeft(sprite, track[position].X - 25);
                Canvas.SetTop(sprite, track[position].Y - 25);
                return 0;
            }
            else
            {
                cEnemies.Children.Remove(sprite);
                return damage;
            }
        }
    }
}
/*                        try
            {
                sw = new StreamWriter("line1.txt");
            }
            catch (FileNotFoundException)
            {
                sw = new StreamWriter("line1.txt");
            }
            for (int i = 0; i < 175; i+=1)
            {
                sw.WriteLine(i + ",275");
            }
            for (int i = 275; i > 205; i-=1)
            {
                sw.WriteLine("175," + i);
            }
            for (int i = 175; i < 315; i += 1)
            {
                sw.WriteLine(i + ",205");
            }
            for (int i = 205; i < 490; i += 1)
            {
                sw.WriteLine("315," + i);
            }
            for (int i = 315; i < 525; i += 1)
            {
                sw.WriteLine(i + ",490");
            }
            for (int i = 490; i > 275; i-=1)
            {
                sw.WriteLine("525," + i);
            }
            for (int i = 525; i < 675; i += 1)
            {
                sw.WriteLine(i + ",275");
            }
            for (int i = 275; i < 345; i++)
            {
                sw.WriteLine("675," + i);
            }
            for (int i = 675; i < 810; i += 1)
            {
                sw.WriteLine(i + ",345");
            }
            sw.Close();
*/




/*            /*
             * track:
0,240
140,240
140,170
350,170
350,455
490,455
490,240
700,240
700,310
750,310
750,210
845,210
845,380
820,410
730,380
630,380
630,310
560,310
560,525
280,525
280,240
210,240
210,310
0,310
           * menu:
0,120
845,120
845,650
1125,650
1125,0
0,0
0,120
            * polyline
0,275
175,275
175,205
315,205
315,490
525,490
525,275
675,275
675,345
810,345
        * fork
2.666666666667,19.333333333333
6.666666666667,16.666666666667
6.666666666667,6
10,3.333333333333
10,-0.6666666666666
0.666666666667,0
-0.6666666666666,4.666666666667
2.666666666667,6.666666666667
             */
