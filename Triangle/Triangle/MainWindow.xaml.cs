using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

namespace Triangle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double AX, AY, BX, BY, CX, CY;
        private double result;
        private double p;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            AX = Convert.ToDouble(AXTextBox.Text);
            AY = Convert.ToDouble(AYTextBox.Text);

            BX = Convert.ToDouble(BXTextBox.Text);
            BY = Convert.ToDouble(BYTextBox.Text);

            CX = Convert.ToDouble(CXTextBox.Text);
            CY = Convert.ToDouble(CYTextBox.Text);

            DrawTriangle();

            double AB = Math.Sqrt(Math.Pow(BX - AX, 2) + Math.Pow(BY - AY, 2));
            double BC = Math.Sqrt(Math.Pow(CX - BX, 2) + Math.Pow(CY - BY, 2));
            double CA = Math.Sqrt(Math.Pow(AX - CX, 2) + Math.Pow(AY - CY, 2));

            p = (AB + BC + CA) / 2;

            result = Math.Sqrt(p * (p - AB) * (p - BC) * (p - CA));

            ResultLabel.Visibility = Visibility.Visible;
            ResultTextBlock.Text = Convert.ToString(Math.Round(result, 4));
        }

        private void DrawTriangle()
        {
            int[] xPoints = new int[] { Convert.ToInt32(AX), Convert.ToInt32(BX), Convert.ToInt32(CX) };      
            int[] yPoints = new int[] { Convert.ToInt32(AY), Convert.ToInt32(BY), Convert.ToInt32(CY) };

            int xMin = 0;
            int yMin = 0;
            if(xPoints.Min() < 0)
            {
                xMin = xPoints.Min();
            }
            if(yPoints.Min() < 0)
            {
                yMin = yPoints.Min();
            }

            int height = (yPoints.Max() + (-yMin)) * 100;
            int width = (xPoints.Max() + (-xMin)) * 100;

            Image myImage = new Image();

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawLine(new Pen(Brushes.Green, 1), new Point(CX, CY  ), new Point(BX  , BY  ));
            drawingContext.DrawLine(new Pen(Brushes.Black, 1), new Point(CX, CY  ), new Point(AX  , AY  ));
            drawingContext.DrawLine(new Pen(Brushes.Red, 1), new Point(AX * 300, AY * 300), new Point(BX * 300, BY * 300));


            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 100, 100, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            myImage.Source = bmp;

            MainCanvas.Children.Add(myImage);

            using (FileStream stream = new FileStream("ColorSamples.png", FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(stream);
            }

            //if (MainCanvas.Children.Count >= 1)
            //{

            //        MainCanvas.Children.Clear();

            //}
            //StackPanel stackPanel = new StackPanel();
            //double multipler = 30;/* * MainCanvas.ActualWidth * 10 / 100;*/
            //Line ABline = new Line();

            //ABline.Stroke = Brushes.Red;

            //ABline.X1 = AX * multipler;
            //ABline.X2 = BX * multipler;
            //ABline.Y1 = AY * multipler;
            //ABline.Y2 = BY * multipler;
            //ABline.StrokeThickness = 1;

            //ABline.HorizontalAlignment = HorizontalAlignment.Right;
            //ABline.VerticalAlignment = VerticalAlignment.Bottom;

            //MainCanvas.Children.Add(ABline);

            //Line BCline = new Line();

            //BCline.Stroke = Brushes.Black;

            //BCline.X1 = BX * multipler;
            //BCline.X2 = CX * multipler;
            //BCline.Y1 = BY * multipler;
            //BCline.Y2 = CY * multipler;
            //BCline.StrokeThickness = 1;

            //BCline.HorizontalAlignment = HorizontalAlignment.Right;
            //BCline.VerticalAlignment = VerticalAlignment.Bottom;

            //MainCanvas.Children.Add(BCline);

            //Line CAline = new Line();


            //CAline.Stroke = Brushes.Green;

            //CAline.X1 = CX * multipler;
            //CAline.X2 = AX * multipler;
            //CAline.Y1 = CY * multipler;
            //CAline.Y2 = AY * multipler;
            //CAline.StrokeThickness = 1;


            //CAline.HorizontalAlignment = HorizontalAlignment.Right;
            //CAline.VerticalAlignment = VerticalAlignment.Bottom;

            //MainCanvas.Children.
            //    Add(CAline);



        }
    }
}
