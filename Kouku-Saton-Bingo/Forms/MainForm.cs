using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kouku_Saton_Bingo
{
    public partial class MainForm : Form
    {
        private int cellSize;
        private int Offset = 20;
        private int Round = -1;

        private readonly object lockObject = new object();

        private string StartDirectory;
        private ToolWindow toolWindow;
        private RecommendsForm recommendsForm;
        private BingoBoard bingo = new BingoBoard();
        private List<(int x,int y)> recommends = new List<(int x, int y)>();
        private List<BingoBoard> history = new List<BingoBoard>();
        private Point highlightedCell = new Point(-1, -1);

        private Image Red_Skull;
        private Image Black_Skull;
        private Image Red_Bingo;
        private Image Black_Bingo;
        private Image Background;

        private Image Recommend_1;
        private Image Recommend_2;
        private Image Recommend_3;

        private const int TYPE_BOARD_TITLE = -1;
        private const int TYPE_MOUSE_HOVER = 0;
        private const int TYPE_SKULL = 1;
        private const int TYPE_BINGO = 2;
        private const int TYPE_RECOMMEDN_1ST = 3;
        private const int TYPE_RECOMMEDN_2ND = 4;
        private const int TYPE_RECOMMEDN_3RD = 5;

        Parameter parameter;
        SettingForm settingForm;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                string tmp = System.AppDomain.CurrentDomain.BaseDirectory;

                if (tmp.Contains("Release") || tmp.Contains("Debug"))
                {
                    StartDirectory = System.Environment.CurrentDirectory;
                }
                else
                {
                    StartDirectory = Application.StartupPath;
                }

                // system initialize
                InitializeParameter();
                InitializeNotifyIcon();
                InitializeResources();
                InitializeForm();
                InitializeUserInterface();
                InitializeEvent();

                // update the position of the tool window
                UpdateToolWindowPosition();
                UpdateRecommendsFormPosition();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Initialize Error");
                this.Close();
            }
        }

        #region System Initialization

        private void InitializeParameter()
        {
            parameter = new Parameter(StartDirectory);

            this.cellSize = (int)(parameter.iFormSize.SizeValue / 5.0 / Math.Sqrt(2));

            // bind value change event
            parameter.iFormSize.SizeValueChanged += OnSizeValueChanged;
            parameter.iOpacity.SizeValueChanged += OnOpacityValueChanged;
            parameter.bRecommendedIsShow.ValueChanged += OnRecommendIsShowValueChanged;
            parameter.pPosition.ValueChanged += OnLocationChangeValueChanged;
        }

        private void InitializeForm() 
        {
            // initialize the tool window
            toolWindow = new ToolWindow(ref parameter, StartDirectory, parameter.iFormSize.SizeValue, parameter.iOpacity.SizeValue, Offset);

            // initialize the recommendation window
            recommendsForm = new RecommendsForm(ref parameter, StartDirectory, parameter.iFormSize.SizeValue, parameter.iOpacity.SizeValue);

            // set the window owner
            recommendsForm.Owner = this;
            toolWindow.Owner = this;

            // display the window
            toolWindow.Show();
            recommendsForm.Show();

            // bind button event
            toolWindow.BindUndoAction(Undo);
            toolWindow.BindResetAction(Reset);
            toolWindow.BindClosetAction(Exit);
        }

        private void InitializeNotifyIcon() 
        {
            Icon icon = new Icon(StartDirectory + @"\resources\Bingo.ico");

            this.Icon = icon;

            notifyIcon_main.Icon = icon;
            notifyIcon_main.Text = "Kouku-Saton-Bingo Toolkit (Official)";

            iTalk_ContextMenuStrip_main.Items.Add("Reset", null, (sender, e) => Reset());
            iTalk_ContextMenuStrip_main.Items.Add("Undo", null, (sender, e) => Undo());
            iTalk_ContextMenuStrip_main.Items.Add("Setting",null,(sender, e) => OpneSettingForm());
            iTalk_ContextMenuStrip_main.Items.Add(new ToolStripSeparator());
            iTalk_ContextMenuStrip_main.Items.Add("Exit",null, (sender, e) => Exit());

            notifyIcon_main.ContextMenuStrip = iTalk_ContextMenuStrip_main;

        }

        private void InitializeResources()
        {
            this.Red_Skull = Image.FromFile( StartDirectory + @"\resources\skull_red.png");
            this.Black_Skull = Image.FromFile(StartDirectory + @"\resources\skull_black.png");
            this.Red_Bingo = Image.FromFile(StartDirectory + @"\resources\bingo_black.png");
            this.Black_Bingo = Image.FromFile(StartDirectory + @"\resources\bingo_red.png");
            this.Background = Image.FromFile(StartDirectory + @"\resources\background.png");
            this.Recommend_1 = Image.FromFile(StartDirectory + @"\resources\#1.png");
            this.Recommend_2 = Image.FromFile(StartDirectory + @"\resources\#2.png");
            this.Recommend_3 = Image.FromFile(StartDirectory + @"\resources\#3.png");
        }

        private void InitializeUserInterface()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.ShowInTaskbar = false;

            // set the double buffering property of the window
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();


            // set the background color and transparency key color
            this.BackColor = Color.DarkGray;
            this.TransparencyKey = Color.DarkGray;

            // set the background image
            this.BackgroundImage = this.Background;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // set the form size
            this.OnSizeValueChanged(parameter.iFormSize.SizeValue);
            this.OnOpacityValueChanged(parameter.iOpacity.SizeValue);
            this.OnRecommendIsShowValueChanged(parameter.bRecommendedIsShow.Value);
            this.OnLocationChangeValueChanged(parameter.pPosition.Value);
            
        }

        private void InitializeEvent()
        {
            this.MouseMove += new MouseEventHandler(OnMouseMove);
            this.MouseLeave += new EventHandler(OnMouseLeave);
            this.bingo.BoardChanged += OnBoardChanged;
            this.LocationChanged += MainForm_LocationChanged;
        }

        #endregion

        #region ValueChanged Events

        private void OnSizeValueChanged(int newSize)
        {
            this.Size = new Size(newSize, newSize);
            this.ClientSize = new Size(newSize, newSize);
            this.MaximumSize = new Size(newSize, newSize);
            this.MinimumSize = new Size(newSize, newSize);

            this.cellSize = (int)(parameter.iFormSize.SizeValue / 5.0 / Math.Sqrt(2));

            if (toolWindow != null)
                this.toolWindow.SetSize(newSize);

            if(recommendsForm != null)
                this.recommendsForm.SetSize(newSize);

            UpdateToolWindowPosition();
            UpdateRecommendsFormPosition();
        }

        private void OnOpacityValueChanged(int value)
        {
            float opacity = (float)(value / 100.0);
            this.Opacity = opacity;

            if (toolWindow != null)
                this.toolWindow.SetOpacity(opacity);
            if (recommendsForm != null)
                this.recommendsForm.SetOpacity(opacity);
        }

        private void OnRecommendIsShowValueChanged(bool value)
        {
            if (recommendsForm != null)
                this.recommendsForm.SetRecommendsVisible(value);
        }

        private void OnLocationChangeValueChanged(Point value)
        {
            this.Location = value;
        }

        #endregion

        #region MainForm Events

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // disable auto scaling
            this.AutoScaleMode = AutoScaleMode.None; 
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // ensure the tool window closes together with the main form when the main form is closed
            parameter.SaveConfig();
            toolWindow.Close();
            recommendsForm.Close();
            base.OnFormClosing(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // generic drawing parameters
            double sideLength = parameter.iFormSize.SizeValue / Math.Sqrt(2);
            int gapSize = 2;  // gap between each cell
            int RealcellSize =  cellSize - gapSize;  // actual size of the drawn squares
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            int offset = 1;

            // save the current graphical state
            e.Graphics.Save();

            // move the drawing origin to the center and rotate 45 degrees
            e.Graphics.TranslateTransform(centerX, centerY);
            e.Graphics.RotateTransform(45);

            BingoResult result = BingoResult.CheckBingo(bingo);

            // draw chessboard numbering
            DrawCellAtPosition(e.Graphics, -1, -1, cellSize, RealcellSize, centerX, centerY, (int)sideLength, gapSize, offset, TYPE_BOARD_TITLE, 1f);

            // draw the selected chessboard cells
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (bingo[row, col])
                    {
                        float image_opacity = 1f;
                        int type = result.IsRed(row, col)? 2 : 1;

                        if (recommends.Contains((row, col))) 
                           image_opacity = 0.5f;

                        DrawCellAtPosition(e.Graphics, row, col, cellSize, RealcellSize, centerX, centerY, (int)sideLength, gapSize, offset, type, image_opacity);
                    }
                }
            }

            // draw the recommended chessboard cells
            for (int index = 0; index < recommends.Count; index++)
            {
                DrawCellAtPosition(e.Graphics, recommends[index].x, recommends[index].y, cellSize, RealcellSize, centerX, centerY, (int)sideLength, gapSize, offset, index + 3 , 1f);
            }

            //draw the chessboard cell at the mouse hover position
            if (highlightedCell != new Point(-1, -1))
            {
                int[,] directions = new int[,]
                {
                    { 0, 0 },   // center
                    { -1, 0 },  // top
                    { 1, 0 },   // bottom
                    { 0, -1 },  // left
                    { 0, 1 }    // right
                };

                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    int offsetY = highlightedCell.Y + directions[i, 0];
                    int offsetX = highlightedCell.X + directions[i, 1];
                    DrawCellAtPosition(e.Graphics, offsetY, offsetX, cellSize, RealcellSize, centerX, centerY, (int)sideLength, gapSize, offset, TYPE_MOUSE_HOVER, 1f);
                }
            }

        }

        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            UpdateToolWindowPosition();
            UpdateRecommendsFormPosition();
        }

        #endregion

        #region Mouse Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            lock (lockObject)
            {

                // generic chessboard parameters
                double sideLength = parameter.iFormSize.SizeValue / Math.Sqrt(2);
                int centerX = this.ClientSize.Width / 2;
                int centerY = this.ClientSize.Height / 2;

                // convert the mouse click position to the chessboard coordinate system
                int relativeX = e.X - centerX;
                int relativeY = e.Y - centerY;

                // rotate the click position by -45 degrees to align with the original unrotated coordinate system
                double angle = -Math.PI / 4; 
                int transformedX = (int)(relativeX * Math.Cos(angle) - relativeY * Math.Sin(angle));
                int transformedY = (int)(relativeX * Math.Sin(angle) + relativeY * Math.Cos(angle));

                // convert coordinates to chessboard grid indices
                int col = (transformedX + (int)(sideLength / 2)) / cellSize;
                int row = (transformedY + (int)(sideLength / 2)) / cellSize;


                // check if the click is within the valid range of the chessboard
                if (row >= 0 && row < 5 && col >= 0 && col < 5)
                {
                    // if fewer than two rounds have passed, only change the state of the clicked target
                    if (Round < 1)
                    {
                        bingo[row, col] = true;
                        history.Add((BingoBoard)bingo.Clone());
                        Round += 1;

                        if (Round == 1) 
                        {
                            toolWindow.SetTitle("Round 1");
                            RecommendsUpdate(bingo, Round);
                        }                      
                    }
                    else
                    {
                        // clear the current recommended position
                        recommends.Clear();

                        // update the current chessboard state
                        bingo.PlaceBingo(new List<(int, int)> { (row, col)});
                        history.Add((BingoBoard)bingo.Clone());
                        Round += 1;
                        string title = "Round " + Round.ToString() + ((Round % 3 == 0) ? " ( Bingo )" : "");
                        toolWindow.SetTitle(title);

                        // avoid blocking due to excessively long calculation times for recommended positions
                        Task.Run(() =>
                        {
                            RecommendsUpdate(bingo, Round);
                        });
                    }

                    // force update the UI interface
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            // calculate the side length of the chessboard
            double sideLength = parameter.iFormSize.SizeValue / Math.Sqrt(2);

            // get the center point of the form
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            // convert the mouse position to a coordinate system centered on the chessboard
            int relativeX = e.X - centerX;
            int relativeY = e.Y - centerY;

            // handle coordinate transformation after rotating 45 degrees 
            // coordinates obtained after rotation projection
            int transformedX = (int)((relativeX + relativeY) / Math.Sqrt(2));
            int transformedY = (int)((relativeY - relativeX) / Math.Sqrt(2));

            // convert the coordinates back to the standard coordinate system, used to determine the chessboard cells
            int col = (transformedX + (int)(sideLength / 2)) / cellSize;
            int row = (transformedY + (int)(sideLength / 2)) / cellSize;
            Point newHighlightedCell = new Point(col, row);

            // if the mouse moves to a new cell, update and redraw
            if (newHighlightedCell != highlightedCell)
            {
                highlightedCell = newHighlightedCell;
                this.Invalidate();
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            // reset highlightedCell and trigger a redraw
            highlightedCell = new Point(-1, -1);
            this.Invalidate();
        }

        #endregion

        #region Main Functions

        private void UpdateToolWindowPosition()
        {
            // set the position of the tool window to be right above the main form and offset it upwards by a specified distance
            if (toolWindow != null)
                toolWindow.Location = new Point(this.Left, this.Top - toolWindow.Height - Offset);
        }

        private void UpdateRecommendsFormPosition()
        {
            // set the position of the recommends window to be right above the main form and offset it upwards by a specified distance
            if (recommendsForm != null)
                recommendsForm.Location = new Point(this.Left, this.Bottom + Offset);
        }

        public void PlaceBingo(BingoResult result,int row,int col)
        {
            // change the state of the chessboard in the up, down, left, and right directions centered on the target position
            var directions = new (int dx, int dy)[] { (0, 0), (0, 1), (0, -1), (1, 0), (-1, 0) };

            foreach (var (dx, dy) in directions)
            {
                int nx = row + dx, ny = col + dy;
                if (0 <= nx && nx < 5 && 0 <= ny && ny < 5 && !result.IsRed(nx, ny))
                {
                    bingo.Toggle(nx, ny);
                }
            }
        }

        public void Undo() 
        {
            lock (lockObject)
            {

                // Rounds start at -1 and increment by one each round; if fewer than two rounds have passed, it will reset directly
                if (Round > 0)
                {
                    //return to the previous round
                    Round -= 1;
                    history.RemoveAt(history.Count - 1);
                    bingo.UpdateFrom(history[history.Count - 1]);
                    RecommendsUpdate(bingo,Round);

                    //redisplay round display title
                    if (Round <= 0)
                        toolWindow.SetTitle("Ready Start");
                    else
                    {
                        string title = "Round " + Round.ToString() + ((Round % 3 == 0) ? " ( Bingo )" : "");
                        toolWindow.SetTitle(title);
                    }
                }
                else
                {
                    //reset the round
                    Reset();
                }
            }
        }

        public void Reset() 
        {
            toolWindow.SetTitle("Ready Start");
            Round = -1;
            history.Clear();
            recommends.Clear();
            bingo.ResetBoard();
        }

        public void Exit() 
        {
            this.Close();
        }

        public void OpneSettingForm()
        {
            if (settingForm == null || !settingForm.Visible)
            {
                // if the settings window does not exist or is not displayed, create and display it
                settingForm = new SettingForm(ref this.parameter);
                settingForm.Show();
            }
            else
            {
                // if already open, bring the window to the foreground
                settingForm.BringToFront();
            }
        }

        private void OnBoardChanged()
        {
            this.Invalidate();
        }   

        private void RecommendsUpdate(BingoBoard board , int round) 
        {
            //used to update the current recommended position.
            BingoGame game = new BingoGame(board, round);
            recommends = game.CandidateList();
            recommendsForm.SetReommends(recommends);
            this.Invalidate();
        }

        private void DrawCellAtPosition(Graphics g, int row, int col, int totalCellSize, int cellSize, int centerX, int centerY, int sideLength, int gapSize, int offset , int type , float opacity)
        {
            if ((row < 0 || row >= 5 || col < 0 || col >= 5) && type != TYPE_BOARD_TITLE)
                return;

            if (type == TYPE_BOARD_TITLE)
            {
                string[] EnglishText = new string[] { "A", "B", "C", "D", "E" };
                string[] NumberText = new string[] { "1", "2", "3", "4", "5" };


                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        string textToDraw = (i == 0) ? EnglishText[j] : NumberText[j];

                        Font font = new Font("Segoe UI", 11, FontStyle.Regular);
                        SizeF textSize = g.MeasureString(textToDraw, font);

                        // only draw the header numbering for the first row and first column
                        col = (i == 0) ? j : 0;
                        row = (i == 0) ? 0 : j;

                        float x = col * totalCellSize - (int)(sideLength / 2) + gapSize / 2 + ((i == 0) ? (totalCellSize / 2 - textSize.Width / 2) : -textSize.Width * 1.5f);
                        float y = row * totalCellSize - (int)(sideLength / 2) + gapSize / 2 + ((i == 0) ? -textSize.Height : (totalCellSize / 2 - textSize.Height / 2));

                        // adjust the offset (move offset pixel to the top right)
                        x += offset;
                        y += offset;


                        Brush brush = Brushes.LightGray;
                        if ((recommends.Any(item => item.y == col) && i == 0) || recommends.Any(item => item.x == row) && i != 0)
                            brush = Brushes.Lime;

                        g.DrawString(textToDraw, font, brush, new PointF(x, y));
                    }
                }
            }
            else if (type == TYPE_MOUSE_HOVER)
            {
                // calculate the top-left coordinates of each cell
                int x = col * totalCellSize - (int)(sideLength / 2) + gapSize / 2;
                int y = row * totalCellSize - (int)(sideLength / 2) + gapSize / 2;

                // adjust the offset (move offset pixel to the top right)
                x += offset;
                y += offset;

                // draw the highlighted cell
                Color highlightColor = Color.FromArgb(128, Color.Gray); 
                using (SolidBrush brush = new SolidBrush(highlightColor))
                {
                    g.FillRectangle(brush, x, y, cellSize - offset, cellSize - offset);
                }
            }
            else
            {
                // calculate the top-left coordinates of each cell
                int x = col * totalCellSize - (int)(sideLength / 2) + totalCellSize / 2;
                int y = row * totalCellSize - (int)(sideLength / 2) + totalCellSize / 2;

                // adjust the offset (move offset pixel to the top right)
                x += offset;
                y += offset;

                // set the opacity
                ColorMatrix colorMatrix = new ColorMatrix
                {
                    Matrix33 = opacity 
                };

                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                int index = row * 5 + col;

                Image imageToDraw;

                // place images based on the type
                switch (type)
                {
                    case TYPE_SKULL:
                        imageToDraw = (index % 2 == 0) ? Red_Skull : Black_Skull;
                        break;
                    case TYPE_BINGO:
                        imageToDraw = (index % 2 == 0) ? Red_Bingo : Black_Bingo;
                        break;
                    case TYPE_RECOMMEDN_1ST:
                        imageToDraw = Recommend_1;
                        break;
                    case TYPE_RECOMMEDN_2ND:
                        imageToDraw = Recommend_2;
                        break;
                    case TYPE_RECOMMEDN_3RD:
                        imageToDraw = Recommend_3;
                        break;
                    default:
                        return;
                }

                // save the current graphical state
                GraphicsState state = g.Save();

                // move the drawing origin to the center 
                g.TranslateTransform(x, y);

                // draw the image, scaled to the size of the cell
                g.DrawImage(imageToDraw,
                            new Rectangle(-cellSize / 2, -cellSize / 2, cellSize, cellSize),
                            0, 0, imageToDraw.Width, imageToDraw.Height,
                            GraphicsUnit.Pixel,
                            imageAttributes);

                g.Restore(state);
            }
        }
        #endregion
    }
}
