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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Rectangle background;
        public Label lblMouseTest;
        public Button tempTwrBtn;
        public Rectangle tempRect;
        public bool mouseTest;
        public Button[] towerIcons = new Button[4];
        Button btnStart = new Button();
        public ImageBrush[] towerFill = new ImageBrush[4];
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        public enum GameState {play, store, test};
        public GameState gameState;
        public Polygon trackHit = new Polygon();
        StreamWriter sw;
        public MainWindow()
        {
            InitializeComponent();
            btnStart.Height = 20;
            btnStart.Width = 70;
            btnStart.Content = "start";
            btnStart.Click += BtnStart_Click;
            cBackground.Children.Add(btnStart);
            try
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
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            /*
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
             */
            if (gameState == GameState.store)
            {
               // sw.Close();
                Canvas.SetTop(tempRect, Mouse.GetPosition(cBackground).Y - tempRect.Height / 2);
                Canvas.SetLeft(tempRect, Mouse.GetPosition(cBackground).X - tempRect.Width / 2);
                bool valid = true;
                if (cObstacles.InputHitTest(Mouse.GetPosition(cBackground)) == null)
                {
                    valid = true;
                    tempRect.Stroke = Brushes.Transparent;
                }
                else
                {
                    valid = false;
                    tempRect.Stroke = Brushes.Red;
                    tempRect.StrokeThickness = 5;
                }
                MouseButtonState pmbs = MouseButtonState.Released;
                if (Mouse.LeftButton == MouseButtonState.Pressed && valid)
                {
                    Point temp = Mouse.GetPosition(cBackground);
                    Rectangle tempRect2 = new Rectangle();
                    tempRect2.Fill = tempRect.Fill;
                    tempRect2.Width = tempRect.Width;
                    tempRect2.Height = tempRect.Height;
                    Canvas.SetTop(tempRect2, Canvas.GetTop(tempRect));
                    Canvas.SetLeft(tempRect2, Canvas.GetLeft(tempRect));
                    cBackground.Children.Remove(tempRect);
                    cBackground.Children.Add(tempRect2);
                    cObstacles.Children.Add(tempRect);
                    gameState = GameState.play;

                }
                else
                {
                    pmbs = Mouse.LeftButton;
                }
            }
            else if (gameState == GameState.test)
            {
                MouseButtonState pmbs = MouseButtonState.Released;
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Point temp = Mouse.GetPosition(cBackground);
                    try
                    {
                        sw.WriteLine(temp.ToString());
                    }
                    catch (FileNotFoundException) //exception for if the file does not exist
                    {
                        sw.WriteLine(temp.ToString());
                    }
                }
                else
                {
                    pmbs = Mouse.LeftButton;
                }
            }
            else if (gameState == GameState.play)
            {
                //sw.Close();
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            gameTimer.Start();

            cBackground.Children.Remove(btnStart);
            background = new Rectangle();
            background.Height = 650;
            background.Width = 1125;
            BitmapImage bi = new BitmapImage(new Uri("track.png", UriKind.Relative));
            ImageBrush img = new ImageBrush(bi);
            background.Fill = img;

            lblMouseTest = new Label();
            lblMouseTest.Height = 50;
            lblMouseTest.Width = 70;

            tempTwrBtn = new Button();
            tempTwrBtn.Height = 20;
            tempTwrBtn.Width = 40;
            tempTwrBtn.Content = "test";

            tempTwrBtn.Click += TempTwrBtn_Click;

            cBackground.Children.Add(background);
            Canvas.SetTop(tempTwrBtn, 17);
            Canvas.SetLeft(tempTwrBtn, 857);
            cBackground.Children.Add(tempTwrBtn);
            bi = new BitmapImage(new Uri("normal.png", UriKind.Relative));
            towerFill[0] = new ImageBrush(bi);
            bi = new BitmapImage(new Uri("police.png", UriKind.Relative));
            towerFill[1] = new ImageBrush(bi);
            bi = new BitmapImage(new Uri("family.png", UriKind.Relative));
            towerFill[2] = new ImageBrush(bi);
            bi = new BitmapImage(new Uri("tank.png", UriKind.Relative));
            towerFill[3] = new ImageBrush(bi);
            for (int i = 0; i < towerIcons.Length; i++)
            {
                Rectangle backDrop = new Rectangle();
                backDrop.Height = 100;
                backDrop.Width = 100;
                backDrop.Fill = Brushes.Red;
                Canvas.SetTop(backDrop, i * 150 + 50);
                Canvas.SetLeft(backDrop, 900);
                cBackground.Children.Add(backDrop);
                towerIcons[i] = new Button();
                towerIcons[i].Background = towerFill[i];
                towerIcons[i].Height = 80;
                towerIcons[i].Width = 80;
                towerIcons[i].Click += iconsClick;
                towerIcons[i].BorderBrush = Brushes.Transparent;
                Canvas.SetTop(towerIcons[i], i * 150 + 60);
                Canvas.SetLeft(towerIcons[i], 910);
                cBackground.Children.Add(towerIcons[i]);
            }

        }

        private void TempTwrBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gameState = GameState.test;
                /*sw = new StreamWriter("tankBox.txt");
                sw.Close();*/
                StreamReader sr = new StreamReader("tankBox.txt");
                List<Point> points = new List<Point>();
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine();
                    double xPosition, yPosition;
                    double.TryParse(currentLine.Split(',')[0], out xPosition);
                    double.TryParse(currentLine.Split(',')[1], out yPosition);
                    Point point = new Point(xPosition, yPosition);
                    points.Add(point);
                }
                sr.Close();
                PointCollection myPointCollection = new PointCollection();
                for (int i = 0; i < points.Count; i++)
                {
                    myPointCollection.Add(points[i]);
                }
                trackHit.Points = myPointCollection;
                trackHit.Stroke = Brushes.Red;
                trackHit.Fill = Brushes.Blue;
                cObstacles.Children.Add(trackHit);
                sr = new StreamReader("line1.txt");
                myPointCollection = new PointCollection();
                while (!sr.EndOfStream)
                {
                    string currentLine = sr.ReadLine();
                    double xPosition, yPosition;
                    double.TryParse(currentLine.Split(',')[0], out xPosition);
                    double.TryParse(currentLine.Split(',')[1], out yPosition);
                    Point point = new Point(xPosition, yPosition);
                    myPointCollection.Add(point);
                }
                sr.Close();
                Polyline test = new Polyline();
                test.Points = myPointCollection;
                test.Stroke = Brushes.Red;
                test.StrokeThickness = 3;
                //test.Fill = Brushes.Blue;
                cBackground.Children.Add(test);
                sw = new StreamWriter("tankBox.txt");
            }
            catch
            {
                sw.Close();
            }
        }
        private void iconsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("true");
            gameState = GameState.store;
            Button button = sender as Button;
            int towerType = -1;
            for (int i = 0; i < towerIcons.Length; i++)
            {
                if (towerIcons[i] == button)
                {
                    towerType = i;
                }
            }
            tempRect = new Rectangle();
            tempRect.Fill = towerFill[towerType];
            if (towerType < 2)
            {
                tempRect.Height = 35;
                tempRect.Width = 35;
            }
            else if (towerType == 2)
            {
                tempRect.Height = 45;
                tempRect.Width = 70;
            }
            else
            {
                tempRect.Height = 70;
                tempRect.Width = 70;
            }
            cBackground.Children.Add(tempRect);
        }

    }
}