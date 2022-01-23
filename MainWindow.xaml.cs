using System.Windows.Controls;

namespace RGDTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UserControl
    {
        public MainWindow()
        {
            InitializeComponent();
            ParkViewModel vm = DataContext as ParkViewModel;
            if (vm.WayPath != null)
            {
                drawArea.Children.Add(vm.WayPath);
                drawArea.Children.Add(vm.FillPath);
            }
        }

        private void variantComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParkViewModel vm = DataContext as ParkViewModel;
            var rect = vm.FillGeometry.Bounds;
            if (!rect.IsEmpty)
            {
                var startPoint = vm.FillGeometry.Figures[0].StartPoint;

                Canvas.SetLeft(variantText, startPoint.X + ((rect.Right - startPoint.X) / 2) - variantText.Width / 2);
                Canvas.SetTop(variantText, startPoint.Y + ((rect.Bottom - startPoint.Y) / 2) - variantText.Height / 2);
            }
        }
    }
}