using DevExpress.Mvvm;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using System.Linq;
using System.Windows;

namespace RGDTest
{
    public class ParkViewModel : ViewModelBase
    {
        public List<WayModel> WaysSource
        {
            get => GetProperty(() => WaysSource);
            set => SetProperty(() => WaysSource, value);
        }

        public VariantType SelectedVariant
        {
            get => selectedVariant;
            set
            {
                if (selectedVariant != value)
                {
                    selectedVariant = value;
                    FillVariant();
                }
            }
        }
        private VariantType selectedVariant;

        public System.Reflection.PropertyInfo[] ColorSource
        {
            get => GetProperty(() => ColorSource);
            set => SetProperty(() => ColorSource, value);
        }

        public Brush SelectedBrush
        {
            get => selectedBrush;
            set
            {
                if (selectedBrush != value)
                {
                    selectedBrush = value;
                    FillPath.Fill = SelectedBrush;
                }
            }
        }
        private Brush selectedBrush;

        public System.Windows.Shapes.Path WayPath
        {
            get => GetProperty(() => WayPath);
            set => SetProperty(() => WayPath, value);
        }

        public PathGeometry WayGeometry
        {
            get => GetProperty(() => WayGeometry);
            set => SetProperty(() => WayGeometry, value);
        }

        public System.Windows.Shapes.Path FillPath
        {
            get => GetProperty(() => FillPath);
            set => SetProperty(() => FillPath, value);
        }

        public PathGeometry FillGeometry
        {
            get => GetProperty(() => FillGeometry);
            set => SetProperty(() => FillGeometry, value);
        }

        public ParkViewModel()
        {
            ColorSource = typeof(Colors).GetProperties();
            WaysSource = new List<WayModel>();
            WaysSource = ReadFile();
            if (WaysSource.Any())
            {
                WayGeometry = new PathGeometry();
                WayPath = new System.Windows.Shapes.Path();

                foreach (var way in WaysSource)
                {
                    PathFigure pathFigure = new PathFigure();
                    LineSegment lineSegment = new LineSegment();
                    pathFigure.StartPoint = new Point(way.line.X1, way.line.Y1);
                    lineSegment.Point = new Point(way.line.X2, way.line.Y2);
                    pathFigure.Segments.Add(lineSegment);
                    WayGeometry.Figures.Add(pathFigure);
                }

                WayPath.Data = WayGeometry;
                WayPath.Stroke = Brushes.Black;
                //  FillGeometry = new PathGeometry();
                FillPath = new System.Windows.Shapes.Path();
                //  FillPath.Data = FillGeometry;
            }
        }

        private List<WayModel> ReadFile()
        {
            List<WayModel> stationWays = new List<WayModel>();
            string readedLine;
            try
            {
                StreamReader sr = new StreamReader("graphData.txt");
                readedLine = sr.ReadLine();
                while (readedLine != null)
                {
                    string[] stringPoints = readedLine.Split(',');
                    Line line = new Line();
                    line.X1 = int.Parse(stringPoints[0]);
                    line.Y1 = int.Parse(stringPoints[1]);
                    line.X2 = int.Parse(stringPoints[2]);
                    line.Y2 = int.Parse(stringPoints[3]);
                    WayModel stationWay = new WayModel();
                    stationWay.line = line;
                    stationWay.variant = (VariantType)(int.Parse(stringPoints[4]));
                    stationWays.Add(stationWay);

                    readedLine = sr.ReadLine();
                }
                sr.Close();
                return stationWays;
            }
            catch
            {
                return null;
            }
        }

        private void FillVariant()
        {
            FillGeometry = new PathGeometry();

            var needFill = WaysSource.Where(x => x.variant == SelectedVariant);
            if (needFill.Any())
            {
                PathFigure pathFigure = new PathFigure();
                pathFigure.StartPoint = new Point(needFill.First().line.X1, needFill.First().line.Y1);
                foreach (var line in needFill)
                {
                    LineSegment startSegment = new LineSegment();
                    startSegment.Point = new Point(line.line.X1, line.line.Y1);
                    pathFigure.Segments.Add(startSegment);

                    LineSegment endSegment = new LineSegment();
                    endSegment.Point = new Point(line.line.X2, line.line.Y2);
                    pathFigure.Segments.Add(endSegment);
                }

                FillGeometry.Figures.Add(pathFigure);
                FillPath.Data = FillGeometry;
                FillPath.Fill = SelectedBrush;
                FillPath.Opacity = 0.1f;
            }
        }
    }
}
