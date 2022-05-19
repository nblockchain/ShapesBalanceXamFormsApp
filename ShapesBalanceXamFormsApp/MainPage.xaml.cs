using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace ShapesBalanceXamFormsApp
{
    public partial class MainPage : ContentPage
    {



        public class Wallet
        {
            public Wallet(double value, Brush color)
            {
                CryptoValue = value;
                Stroke = color;
            }

            public double CryptoValue { get; private set; }
            public Brush Stroke { get; private set; }
        }

        public IEnumerable<Wallet> GetAmount()
        {


            yield return new Wallet(400, Brush.Green);
            yield return new Wallet(40, Brush.Blue);
            yield return new Wallet(20, Brush.Gold);
            yield return new Wallet(20, Brush.Olive);
            yield return new Wallet(20, Brush.Black);

            //yield return new Wallet(50, Brush.Orange);
            //yield return new Wallet(5, Brush.Yellow);
            //yield return new Wallet(5, Brush.Blue);
        }

        public MainPage()
        {
            InitializeComponent();


            IEnumerable<Wallet> amounts = GetAmount();
            double amount = 0;

            foreach (Wallet bal in amounts)
            {
                amount += bal.CryptoValue;
            }

            Grid grid = new Grid
            {
                RowDefinitions = { new RowDefinition() },
                ColumnDefinitions = { new ColumnDefinition() },

            };

            Label currency = new Label
            {
                Text = "€",
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center,
                TranslationY = 160,
                TranslationX = -30
            };

            grid.Children.Add(currency);

            Label balance = new Label
            {
                Text = amount.ToString(),
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center,
                TranslationY = 160
            };

            grid.Children.Add(balance);

            Label total_tag = new Label
            {
                Text = "Amount Balance",
                FontAttributes = FontAttributes.Bold,
                FontSize = 15,
                HorizontalOptions = LayoutOptions.Center,
                TranslationY = 200
            };

            grid.Children.Add(total_tag);



            makePies(grid, amounts);
            Content = grid;



        }



        public void makePies(Grid grid, IEnumerable<Wallet> amounts)
        {


            int n = amounts.Count();
            double change, hold;
            double arcAngle = 0;
         

            //manipulate the gap between the arcs
            double gap = 5;

            List<double> Ranks = new List<double>();

            normalize(Ranks, amounts);



            for (int i = 0; i < n; i++)
            {

                hold = Ranks[i];
                change = (hold / n) * 360;


                Path path = new Path { Stroke = amounts.ElementAt(i).Stroke };
                PathGeometry geometry = new PathGeometry();
                PathFigureCollection pathFigures = new PathFigureCollection();
                PathFigure pathFigure = new PathFigure();
                PathSegmentCollection pathSegmentCollection = new PathSegmentCollection();
                ArcSegment arcSegment = new ArcSegment();

                

                if (change > gap)
                {
                    arcSegment = renderArc(path, pathFigure, arcSegment, arcAngle + gap, change - gap * 2);
                    arcAngle = arcAngle + change;

                }
                else
                {
                    arcSegment = renderArc(path, pathFigure, arcSegment, arcAngle - gap/2, change + gap);
                    arcAngle = arcAngle + change;

                }


                path.Data = geometry;
                geometry.Figures = pathFigures;
                pathFigures.Add(pathFigure);
                pathFigure.Segments.Add(arcSegment);

                grid.Children.Add(path);

            }




        }

        public void normalize(List<double> Ranks, IEnumerable<Wallet> amounts)
        {

            double n = amounts.Count();
            double rank = n;
            double total = 0;

            foreach (Wallet bal in amounts)
            {
                total += bal.CryptoValue;
            }

            for (int i = 0; i < n; i++)
            {

                rank = n*(amounts.ElementAt(i).CryptoValue/total);
                Ranks.Add(rank);
            }

        }

        private ArcSegment renderArc(Path pathRoot, PathFigure pathFigure, ArcSegment ARC, double startAngle, double endAngle)
        {
            double Radius = 150;
            double angle = 0;
            bool largeArc = false;

            if (endAngle > 180)
                largeArc = true;

            pathRoot.StrokeLineCap = PenLineCap.Round;
            pathRoot.StrokeThickness = 12;
            ARC.SweepDirection = SweepDirection.Clockwise;
            ARC.Size = new Size(Radius, Radius);
            ARC.RotationAngle = angle;
            ARC.IsLargeArc = largeArc;
            //////////////////////////
            pathFigure.StartPoint = ComputeCartesianCoordinate(startAngle, Radius);
            ARC.Point = ComputeCartesianCoordinate(startAngle + endAngle, Radius);

            return ARC;

        }

        private Point ComputeCartesianCoordinate(double angle, double radius)
        {
            //center (50,50)
            double cX = 50;
            double cY = 50;
            // convert to radians
            double angleRad = (Math.PI / 180.0) * (angle - 90);

            double x = radius * Math.Cos(angleRad);
            double y = radius * Math.Sin(angleRad);

            x += radius + cX;
            y += radius + cY;

            return new Point(x, y);

        }

    }
}




