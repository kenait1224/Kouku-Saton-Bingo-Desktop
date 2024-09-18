using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kouku_Saton_Bingo
{
    public partial class RecommendsForm : Form
    {
        private int FormSize;
        private Parameter parameter;
        private int FormHeight = 209;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // enable WS_EX_COMPOSITED
                return cp;
            }
        }

        public RecommendsForm(ref Parameter parameter, string start_directory, int formsize, int opacity)
        {
            InitializeComponent();

            // set the window's double buffering property
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.recommends.OpenSvgDoucument(start_directory);
            this.setting.OpenSvgDoucument(start_directory);

            this.parameter = parameter;
            this.FormSize = formsize;
            this.Opacity = opacity / 100.0;

            this.ShowInTaskbar = false;

            // set the background color and transparency key color
            this.BackColor = Color.Gray;
            this.TransparencyKey = Color.Gray;

            this.Size = new Size(FormSize, FormHeight);
            this.ClientSize = new Size(FormSize, FormHeight);
            this.MinimumSize = new Size(FormSize, FormHeight);
            this.MaximumSize = new Size(FormSize, FormHeight);
        }

        public void SetSize(int size)
        {
            this.Size = new Size(size, FormHeight);
            this.ClientSize = new Size(size, FormHeight);
            this.MaximumSize = new Size(size, FormHeight);
            this.MinimumSize = new Size(size, FormHeight);
        }

        public void SetOpacity(float value)
        {
            this.Opacity = value;
        }

        public void SetReommends(List<(int x, int y)> recommends)
        {
            string[] EnglishText = new string[] { "A", "B", "C", "D", "E" };

            this.BeginInvoke(new MethodInvoker(() => {
                HashTag1.Text = "-";
                HashTag2.Text = "-";
                HashTag3.Text = "-";

                for (int i = 0; i < recommends.Count; i++)
                {
                    string text = EnglishText[recommends[i].y] + " - " + (recommends[i].x + 1).ToString();

                    switch (i)
                    {
                        case 0:
                            HashTag1.Text = text;
                            break;
                        case 1:
                            HashTag2.Text = text;
                            break;
                        case 2:
                            HashTag3.Text = text;
                            break;
                    }
                }
            }));
        }

        public void SetRecommendsVisible(bool value) 
        {
            tableLayoutPanel_Recommends.Visible = value;
        }

        private void Recommends_Click(object sender, System.EventArgs e)
        {
            tableLayoutPanel_Recommends.Visible = !tableLayoutPanel_Recommends.Visible;
            parameter.bRecommendedIsShow.Value = tableLayoutPanel_Recommends.Visible;
        }

        private void Setting_Click(object sender, System.EventArgs e)
        {
            if (this.Owner != null)
                ((MainForm)this.Owner).OpneSettingForm();
        }
    }
}
