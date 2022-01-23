using System.Windows.Shapes;

namespace RGDTest
{
    public class WayModel
    {
        /// <summary>
        /// Линия
        /// </summary>
        public Line line { get; set; }

        /// <summary>
        /// В какой вариант входит линия
        /// </summary>
        public VariantType? variant { get; set; }
    }
}
