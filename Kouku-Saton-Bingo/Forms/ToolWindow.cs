using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kouku_Saton_Bingo
{
    public partial class ToolWindow : Form
    {
        private Parameter parameter;
        private Point _dragOffset;
        private bool _isDragging = false;
        private int Offset;
        private int FormSize;
        private bool isMouseOver = false;

        private Action UndoClickAction;
        private Action ResetClickAction;
        private Action CloseClickAction;

        public ToolWindow(ref Parameter parameter,string start_directory, int formsize , int opacity, int offset)
        {
            InitializeComponent();

            // set the window's double buffering property
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.Visible = false;

            this.parameter = parameter;

            this.Close.OpenSvgDoucument(start_directory);
            this.Undo.OpenSvgDoucument(start_directory);
            this.Move.OpenSvgDoucument(start_directory);
            this.Reset.OpenSvgDoucument(start_directory);

            this.Offset = offset;
            this.FormSize = formsize;
            this.Opacity = opacity / 100.0;

            this.ShowInTaskbar = false;

            // set the background color and transparency key color
            this.BackColor = Color.Gray;
            this.TransparencyKey = Color.Gray;

            // set borderless style
            this.FormBorderStyle = FormBorderStyle.None;

            this.Size = new Size(FormSize, 25);
            this.ClientSize = new Size(FormSize, 25);
            this.MinimumSize = new Size(FormSize, 25);
            this.MaximumSize = new Size(FormSize, 25);

            // add mouse event handlers
            this.Move.MouseDown += Dragging_MouseDown;
            this.Move.MouseMove += Dragging_MouseMove;
            this.Move.MouseUp += Dragging_MouseUp;
        }


        public void SetTitle(string title) 
        {
            iTalk_Label_Title.Text = title;
        }

        public void SetSize(int size)
        {
            this.Size = new Size(size, 25);
            this.ClientSize = new Size(size, 25);
            this.MaximumSize = new Size(size, 25);
            this.MinimumSize = new Size(size, 25);
        }

        public void SetOpacity(float value)
        {
            this.Opacity = value;
        }

        public void BindUndoAction(Action action) 
        {
            this.UndoClickAction = action;
            Undo.Click += (sender, e) =>
            {
                this.UndoClickAction?.Invoke();
            };
        }
        
        public void BindResetAction(Action action) 
        {
            this.ResetClickAction = action;
            Reset.Click += (sender, e) =>
            {
                this.ResetClickAction?.Invoke();
            };
        }

        public void BindClosetAction(Action action)
        {
            this.CloseClickAction = action;
            Close.Click += (sender, e) =>
            {
                this.CloseClickAction?.Invoke();
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.AutoScaleMode = AutoScaleMode.None;  // disable auto scaling
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isMouseOver)
            {
                // draw a semi-transparent rectangle the same size as the control's BBox
                using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.Gray)))
                {
                    e.Graphics.FillRectangle(brush, Undo.Bounds);
                }
            }
        }

        private void Dragging_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragOffset = e.Location;
            }
        }

        private void Dragging_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Point newLocation = new Point(currentScreenPos.X - _dragOffset.X, currentScreenPos.Y - _dragOffset.Y);

                this.Location = newLocation;

                // simultaneously update the position of the main window to keep it below the tool window, taking the offset into account.

                if (this.Owner != null)
                {
                    this.parameter.pPosition.Value = new Point(this.Left, this.Bottom + Offset);
                }
            }
            else
            {
                if (this.Undo.Bounds.Contains(e.Location))
                {
                    isMouseOver = true;
                    this.Invalidate();
                }
                else
                {
                    isMouseOver = false;
                    this.Invalidate();
                }
            }
            
        }

        private void Dragging_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }
    }
}
