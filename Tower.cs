/*
 * Name: Riley, Peter and Quinn
 * Date: June 18th, 2019
 * Description: A tower defense game where you try to eat angry food to protect a sacred fridge
 */﻿
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
    public class Tower
    {
        public Point Location;
        int towerType;
        Rectangle towerRect;
        Canvas cBackground;
        Canvas cObstacles;
        int[] positions;
        Point[] track;
        List<int> targets = new List<int>();
        int range;
        int cost;
        public Tower(int tT, Canvas cBack, Canvas cObs, int[] p, Point[] t)
        {
            towerType = tT;
            towerRect = new Rectangle();
            cBackground = cBack;
            cObstacles = cObs;
            positions = p;
            track = t;
        }

        public void DrawTower(Point l)
        {
            Location = l;
            if (towerType == 0)//norm
            {
                range = 150;

                BitmapImage bi = new BitmapImage(new Uri("normal.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(towerRect, Location.X - towerRect.Width / 2);
                cBackground.Children.Add(towerRect);

                Rectangle tempTower = new Rectangle();
                tempTower.Height = towerRect.Height;
                tempTower.Width = towerRect.Width;
                tempTower.Fill = Brushes.Transparent;
                Canvas.SetTop(tempTower, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(tempTower, Location.X - towerRect.Width / 2);
                cObstacles.Children.Add(tempTower);
                //MessageBox.Show(Canvas.GetTop(towerRect).ToString());
            }
            else if (towerType == 1)//popo
            {
                range = 300;

                BitmapImage bi = new BitmapImage(new Uri("police.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(towerRect, Location.X - towerRect.Width / 2);
                cBackground.Children.Add(towerRect);

                Rectangle tempTower = new Rectangle();
                tempTower.Height = towerRect.Height;
                tempTower.Width = towerRect.Width;
                tempTower.Fill = Brushes.Transparent;
                Canvas.SetTop(tempTower, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(tempTower, Location.X - towerRect.Width / 2);
                cObstacles.Children.Add(tempTower);
            }
            else if (towerType == 2)//fam
            {
                range = 50;

                BitmapImage bi = new BitmapImage(new Uri("family.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(towerRect, Location.X - towerRect.Width / 2);
                cBackground.Children.Add(towerRect);

                Rectangle tempTower = new Rectangle();
                tempTower.Height = towerRect.Height;
                tempTower.Width = towerRect.Width;
                tempTower.Fill = Brushes.Transparent;
                Canvas.SetTop(tempTower, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(tempTower, Location.X - towerRect.Width / 2);
                cObstacles.Children.Add(tempTower);
            }
            else//thicc
            {
                range = 50;

                BitmapImage bi = new BitmapImage(new Uri("tank.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(towerRect, Location.X - towerRect.Width / 2);
                cBackground.Children.Add(towerRect);

                Rectangle tempTower = new Rectangle();
                tempTower.Height = towerRect.Height;
                tempTower.Width = towerRect.Width;
                tempTower.Fill = Brushes.Transparent;
                Canvas.SetTop(tempTower, Location.Y - towerRect.Height / 2);
                Canvas.SetLeft(tempTower, Location.X - towerRect.Width / 2);
                cObstacles.Children.Add(tempTower);
            }

            for (int i = 0; i > positions.Length; i++)
            {
                double xDistance = 0;
                double yDistance = 0;

                xDistance = track[i].X - Location.X;
                yDistance = Location.Y - track[i].Y;

                double TotalDistance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

                if (TotalDistance > range)
                {
                    targets.Add(i);
                }
            }
        }

        public bool CheckTower()
        {
            Canvas.SetTop(towerRect, Mouse.GetPosition(cBackground).Y - towerRect.Height / 2);
            Canvas.SetLeft(towerRect, Mouse.GetPosition(cBackground).X - towerRect.Width / 2);
            if (towerType < 2)
            {
                towerRect.Height = 35;
                towerRect.Width = 35;
            }
            else if (towerType == 2)
            {
                towerRect.Height = 45;
                towerRect.Width = 70;
            }
            else
            {
                towerRect.Height = 70;
                towerRect.Width = 70;
            }
            bool valid = true;
            double x = Mouse.GetPosition(cBackground).X;
            double y = Mouse.GetPosition(cBackground).Y;
            bool check1 = cObstacles.InputHitTest(new Point(x + towerRect.Width / 2 - 5, y + towerRect.Height / 2 - 5)) == null;
            bool check2 = cObstacles.InputHitTest(new Point(x - towerRect.Width / 2 + 5, y + towerRect.Height / 2 - 5)) == null;
            bool check3 = cObstacles.InputHitTest(new Point(x + towerRect.Width / 2 - 5, y - towerRect.Height / 2 + 5)) == null;
            bool check4 = cObstacles.InputHitTest(new Point(x - towerRect.Width / 2 + 5, y - towerRect.Height / 2 + 5)) == null;
            bool check5 = cObstacles.InputHitTest(new Point(x, y)) == null;
            if (check1 && check2 && check3 && check4 && check5)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            return valid;
        }

        public void Shoot()
        {
            Point currentTarget;
            for (int i = 0; i < targets.Count; i++)
            {
                if (positions[targets[i]] != -1)
                {
                    currentTarget = track[targets[i]];
                }
            }
            Bullet shot = new Bullet(10, 1, Location, Location, cBackground);
            shot.DrawBullet();
        }
    }
}
