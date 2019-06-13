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
    class Tower
    {
        Point Location;
        int towerType;
        Rectangle towerRect;
        Canvas cBackground;
        Canvas cObstacles;
        public Tower(Point l, int tT, Canvas cBack, Canvas cObs)
        {
            Location = l;
            towerType = tT;
            towerRect = new Rectangle();
            cBackground = cBack;
            cObstacles = cObs;

            if (towerType == 0)//norm
            {
                towerRect.Height = 35;
                towerRect.Width = 35;
                BitmapImage bi = new BitmapImage(new Uri("normal.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y + towerRect.Height / 2);
                Canvas.SetTop(towerRect, Location.X - towerRect.Width / 2);
                cBackground.Children.Add(towerRect);
                Rectangle tempTower = new Rectangle();
                cObstacles.Children.Add(tempTower);
            }
            else if (towerType == 1)//popo
            {
                towerRect.Height = 35;
                towerRect.Width = 35;
                BitmapImage bi = new BitmapImage(new Uri("police.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y - towerRect.Height / 2);
                Canvas.SetTop(towerRect, Location.X = towerRect.Width / 2);
                towerRect.Visibility = Visibility.Hidden;
                cBackground.Children.Add(towerRect);
                Rectangle tempTower = new Rectangle();
                cObstacles.Children.Add(tempTower);
            }
            else if (towerType == 2)//fam
            {
                towerRect.Height = 45;
                towerRect.Width = 70;
                BitmapImage bi = new BitmapImage(new Uri("family.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y - towerRect.Height / 2);
                Canvas.SetTop(towerRect, Location.X = towerRect.Width / 2);
                cBackground.Children.Add(towerRect);
                Rectangle tempTower = new Rectangle();
                cObstacles.Children.Add(tempTower);
            }
            else//thicc
            {
                towerRect.Height = 70;
                towerRect.Width = 70;
                BitmapImage bi = new BitmapImage(new Uri("tank.png", UriKind.Relative));
                towerRect.Fill = new ImageBrush(bi);
                Canvas.SetTop(towerRect, Location.Y - towerRect.Height / 2);
                Canvas.SetTop(towerRect, Location.X = towerRect.Width / 2);
                cObstacles.Children.Add(towerRect);
                Rectangle tempTower = new Rectangle();
                tempTower.Visibility = Visibility.Hidden;
                cBackground.Children.Add(tempTower);
            }

        }
    }
}
