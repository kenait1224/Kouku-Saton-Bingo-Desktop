using System;
using System.Collections.Generic;
using System.Linq;

namespace Kouku_Saton_Bingo
{
    public class BingoBoard
    {
        private bool[,] board;
        public int Size { get; } = 5;
        public event Action BoardChanged;

        private double[] scoreWeight = new double[]
        {
            1e10,                // 1e10 (10,000,000,000)                           
            2e9,                 // 2e9 (2,000,000,000)                             
            1e7,                 // 1e7 (10,000,000)                                
            5e4,                 // 5e4 (50,000)                                    
            5e4 * 10 * 2 + 1e4,  // 5e4 * 10 * 1 + 1e4 = 500,000 + 10,000 = 510,000 
            100,                 // 100                                                   
            1                    // 1
        };

    public BingoBoard()
        {
            // initialize a 5x5 bingo board, all values set to false
            board = new bool[Size, Size];
        }

        public bool this[int i, int j]
        {
            get => board[i, j];
            set
            {
                if (board[i, j] != value)
                {
                    board[i, j] = value;
                    BoardChanged?.Invoke(); // trigger the event indicating the tile has changed
                }
            }
        }

        public void Toggle(int x, int y)
        {
            if (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                board[x, y] = !board[x, y];
                BoardChanged?.Invoke();
            }
        }

        public void ResetBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = false;
                }
            }
            // trigger the change event after the tile reset
            BoardChanged?.Invoke(); 
        }

        public object Clone()
        {
            var clone = new BingoBoard();
            Array.Copy(board, clone.board, board.Length);
            return clone;
        }

        public void UpdateFrom(BingoBoard other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            Array.Copy(other.board, this.board, this.board.Length);
            BoardChanged?.Invoke();
        }

        public List<double> ScorePlace()
        {
            var res = new List<double> { 0, 0, 0, 0 };
            var bfsQueue = new Queue<(int, int)>();
            BingoResult bingo = BingoResult.CheckBingo(this);
            bfsQueue.Enqueue((1, 1)); // start the search from (1,1)

            // initialize the checklist, assuming only (1,1) has been checked
            bool[,] check = new bool[Size, Size];
            check[1, 1] = true;

            double[,] isc = {
            { 2, 2, 2, 2, 0.5 },
            { 2, 20, 10, 2, 0.5 },
            { 2, 10, 7, 2, 0.5 },
            { 2, 2, 2, 2, 0.5 },
            { 0.5, 0.5, 0.5, 0.5, 0.5 }
            };


            // Breadth-First Search (BFS)
            while (bfsQueue.Count > 0)
            {
                var (tx, ty) = bfsQueue.Dequeue();
                if (!IsSkull(tx,ty)) res[1] += isc[tx, ty];
                if (!bingo.IsRed(tx, ty))
                {
                    res[0]++;
                    foreach (var (dx, dy) in new[] { (0, 0), (0, 1), (0, -1), (1, 0), (-1, 0) })
                    {
                        int nx = tx + dx, ny = ty + dy;
                        if (nx >= 0 && nx < Size && ny >= 0 && ny < Size && !check[nx, ny] && !bingo.IsRed(nx, ny))
                        {
                            check[nx, ny] = true;
                            bfsQueue.Enqueue((nx, ny));
                        }
                    }
                }
            }

            // Holistic Evaluation Strategy (HES)
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!bingo.IsRed(i, j)) res[2]++;
                    if (!IsSkull(i,j)) res[3] += isc[i, j];
                }
            }

            return res;
        }

        public void PlaceBingo(List<(int, int)> coordinates)
        {
            if (coordinates == null)
                return;

            foreach (var (row, col) in coordinates)
            {
                BingoResult result = BingoResult.CheckBingo(this);

                var directions = new (int dx, int dy)[] { (0, 0), (0, 1), (0, -1), (1, 0), (-1, 0) };

                foreach (var (dx, dy) in directions)
                {
                    int nx = row + dx, ny = col + dy;
                    if (0 <= nx && nx < 5 && 0 <= ny && ny < 5 && !result.IsRed(nx, ny))
                    {
                        this.Toggle(nx, ny);
                    }
                }
            }
        }

        public bool IsSkull(int x, int y)
        {
            if (x < 0 || x >= Size || y < 0 || y >= Size)
                return true;  // treat as a skull if out of bounds
            return board[x, y]; 
        }

        public int IsIsolate(int x, int y)
        {
            int count = 0;

            // check the skull status in the eight surrounding directions
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx != 0 || dy != 0)  // exclude the center point itself
                        count += IsSkull(x + dx, y + dy) ? 1 : 0;
                }
            }

            return count;
        }

        public List<(int x, int y, double score)> ScoreThird(List<(int, int)> coordinates)
        {
            var res = new List<(int x, int y, double score)>();
            BingoBoard nowBoard = (BingoBoard)this.Clone();
            nowBoard.PlaceBingo(coordinates);
            BingoResult nowBingo = BingoResult.CheckBingo(nowBoard);  // This should return some status of the board

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    List<(int, int)> testCoordinates = new List<(int, int)> { (i, j) };
                    var score = new double[7];

                    BingoBoard testBoard = (BingoBoard)nowBoard.Clone();
                    testBoard.PlaceBingo(testCoordinates);
                    BingoResult testBingo = BingoResult.CheckBingo(testBoard);

                    if (true)  // isHellOver always returns false, so it's ignored
                    {
                        if (!nowBingo.Equals(testBingo)) score[0] = 1;  
                        if (!nowBingo.IsRed(i, j)) score[1] = 1;
                        if (!nowBoard.IsSkull(i, j)) score[4] = 1;
                        if (nowBoard.IsIsolate(i, j) == 8) score[4] = 0;

                        var sp = testBoard.ScorePlace();
                        score[2] = sp[0];
                        score[3] = sp[1];
                        score[5] = sp[2];
                        score[6] = sp[3];
                    }

                    double sc = 0;
                    for (int k = 0; k < score.Length; k++)
                        sc += score[k] * scoreWeight[k];

                    res.Add((i, j, sc));
                }
            }

            return res;
        }

        public List<(int x, int y, double score)> ScoreSecond(List<(int, int)> coordinates)
        {
            var res = new List<(int x, int y, double score)>();
            BingoBoard nowBoard = (BingoBoard)this.Clone();
            nowBoard.PlaceBingo(coordinates);
            BingoResult nowBingo = BingoResult.CheckBingo(nowBoard);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    List<(int, int)> testCoordinates = new List<(int, int)> { (i, j) };
                    var score = new double[7];
                    BingoBoard testBoard = (BingoBoard)nowBoard.Clone();
                    testBoard.PlaceBingo(testCoordinates);
                    if (true)
                    {
                        if (!nowBingo.IsRed(i, j)) score[0] = 1;
                        if (!nowBoard.IsSkull(i, j)) score[3] = 1;
                        if (nowBoard.IsIsolate(i, j) == 8) score[3] = 0;

                        var sp = testBoard.ScorePlace();
                        score[1] = sp[0];
                        score[2] = sp[1];
                        score[4] = sp[2];
                        score[5] = sp[3];
                    }

                    double sc = 0;
                    for (int k = 0; k < 6; k++) sc += score[k] * scoreWeight[k + 1];

                    List<(int, int)> mergeCoordinates = (coordinates != null)? coordinates.Concat(testCoordinates).ToList() : testCoordinates;
                    
                    double scThird = ScoreThird(mergeCoordinates).Max(x => x.score); 
                    res.Add((i, j, sc + scThird));
                }
            }

            return res;
        }

        public List<(int x, int y, double score)> ScoreFirst()
        {
            var results = new List<(int x, int y, double score)>();
            BingoResult nowBingo = BingoResult.CheckBingo(this);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    List<(int, int)> testCoordinates = new List<(int, int)> { (i, j) };
                    var score = new double[7];
                    BingoBoard testBoard = (BingoBoard)this.Clone();
                    testBoard.PlaceBingo(testCoordinates);

                    if (true)
                    {
                        if (!nowBingo.IsRed(i, j)) score[0] = 1;
                        if (!this.IsSkull(i, j)) score[3] = 1;
                        if (this.IsIsolate(i, j) == 8) score[3] = 0;

                        var sp = testBoard.ScorePlace();
                        score[1] = sp[0];
                        score[2] = sp[1];
                        score[4] = sp[2];
                        score[5] = sp[3];
                    }

                    double sc = 0;
                    for (int k = 0; k < 6; k++) sc += score[k] * scoreWeight[k + 1];
                    double scSecond = ScoreSecond(testCoordinates).Max(x => x.score); 
                    Console.WriteLine(sc.ToString());
                    Console.WriteLine(scSecond.ToString());
                    results.Add((i, j, sc + scSecond));
                }
            }
            return results;
        }
    }
}
