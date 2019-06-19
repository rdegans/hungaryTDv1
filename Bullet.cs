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

namespace hungaryTDv1
{
    class Bullet
    {
        //Tower parentTower;
        int speed;
        int power;
        Point origin;
        Point enemy;
        Canvas canvas;
        double xDistance;
        double yDistance;
        double xMove;
        double yMove;
        Point CurrentLocation;
        Rectangle bullet;
        public double NumbOfTranforms = 0;
        bool bulletDrawn;
        int counter = 1;

        public Bullet(int s, int p, Point o, Point e, Canvas c)
        {
            speed = s;
            power = p;
            origin = o;
            enemy = e;
            canvas = c;
        }

        public Point DrawBullet()
        {
            if (bulletDrawn == false)
            {
                xDistance = 0;
                yDistance = 0;


                xDistance = enemy.X - origin.X;
                yDistance = origin.Y - enemy.Y;

                double TotalDistance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));
                NumbOfTranforms = Math.Ceiling(TotalDistance / speed);
                xMove = xDistance / NumbOfTranforms;
                yMove = yDistance / NumbOfTranforms;

                double temp = Math.Atan(xDistance / yDistance);
                double angle = temp * 180 / Math.PI;

                bullet = new Rectangle();
                bullet.Height = 10;
                bullet.Width = 5;
                BitmapImage bi = new BitmapImage(new Uri("fork.png", UriKind.Relative));
                bullet.Fill = new ImageBrush(bi);
                canvas.Children.Add(bullet);

                if (enemy.Y > origin.Y)
                {
                    angle += 180;
                    RotateTransform rotate = new RotateTransform(angle);
                    bullet.RenderTransformOrigin = new Point(0.5, 0.5);
                    bullet.RenderTransform = rotate;
                }
                else
                {
                    RotateTransform rotate = new RotateTransform(angle);
                    bullet.RenderTransformOrigin = new Point(0.5, 0.5);
                    bullet.RenderTransform = rotate;
                }
                bulletDrawn = true;
                CurrentLocation = origin;
                return CurrentLocation;
            }
            else
            {
                CurrentLocation.X = origin.X + (xMove * counter);
                CurrentLocation.Y = origin.Y - (yMove * counter);
                Canvas.SetLeft(bullet, CurrentLocation.X);
                Canvas.SetTop(bullet, CurrentLocation.Y);
                counter++;
                if (CurrentLocation == enemy)
                {
                    canvas.Children.Remove(bullet);
                    bulletDrawn = false;
                    counter = 1;
                }
                return CurrentLocation;
            }
        }
    }
}
