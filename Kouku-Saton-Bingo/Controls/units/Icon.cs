using Svg;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Kouku_Saton_Bingo.Controls
{
    public abstract partial class Icon : Control
    {
        protected SvgDocument svgDocument;
        protected Size originalSvgSize;

        public Icon()
        {
            InitializeComponent();

            // enable double buffering to reduce flickering
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // set the background to transparent
            this.BackColor = Color.Transparent; 
        }

        public abstract void OpenSvgDoucument(string path);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (svgDocument != null)
            {
                // calculate the scaling ratio between the control size and the original SVG size
                float scaleX = (float)this.ClientSize.Width / originalSvgSize.Width;
                float scaleY = (float)this.ClientSize.Height / originalSvgSize.Height;

                // maintain aspect ratio
                float scale = Math.Min(scaleX, scaleY);

                // adjust the scaling ratio to ensure the bottom is not cropped
                float adjustmentFactor = 0.95f;  
                scale *= adjustmentFactor;

                // calculate the offset for centered drawing, leaving extra space at the bottom
                float offsetX = (this.ClientSize.Width - (originalSvgSize.Width * scale)) / 2;
                float offsetY = (this.ClientSize.Height - (originalSvgSize.Height * scale)) / 2;

                // to prevent bottom clipping, slightly increase the padding on the Y-axis
                float bottomPadding = 1.1f;

                // set the graphics drawing mode
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // apply scaling and translation transforms, with special consideration for bottom clipping
                e.Graphics.TranslateTransform(offsetX, offsetY - bottomPadding); 
                e.Graphics.ScaleTransform(scale, scale);

                // draw the SVG image
                svgDocument.Draw(e.Graphics);
            }
        }
    }
}
