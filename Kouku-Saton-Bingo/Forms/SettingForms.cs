using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kouku_Saton_Bingo
{
    public partial class SettingForm : Form
    {
        Parameter parameter;

        public SettingForm(ref Parameter parameter)
        {
            InitializeComponent();

            // set the window's double buffering property
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.parameter = parameter;

            iTalk_TrackBar_FormSize.ValueChanged += () =>
            {
                this.parameter.iFormSize.SizeValue = 300 + iTalk_TrackBar_FormSize.Value * 50;
            };

            iTalk_TrackBar_Opacity.ValueChanged += () =>
            {
                this.parameter.iOpacity.SizeValue = iTalk_TrackBar_Opacity.Value;
            };

            iTalk_TrackBar_FormSize.Value = (this.parameter.iFormSize.SizeValue - 300) / 50;
            iTalk_TrackBar_Opacity.Value = this.parameter.iOpacity.SizeValue;
        }
    }
}
