using Svg;
using System.Drawing;

namespace Kouku_Saton_Bingo.Controls.Icons
{
    public partial class Setting_Icon : Icon
    {
        public override void OpenSvgDoucument(string path)
        {
            // load the SVG file
            svgDocument = SvgDocument.Open(path + @"/resources/Setting.svg");

            // get the original size of the SVG
            originalSvgSize = new Size((int)svgDocument.Bounds.Width, (int)svgDocument.Bounds.Height);

            this.Invalidate();
        }
    }
}
