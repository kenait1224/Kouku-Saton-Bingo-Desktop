using System.Collections.Generic;
using System.Linq;

namespace Kouku_Saton_Bingo
{
    public class BingoGame
    {
        private BingoBoard board;
        private int round;
        private bool isInanna = false;

        public BingoGame(BingoBoard board,int round)
        {
            this.board = board;
            this.round = round;
        }

        public List<(int x, int y)> CandidateList()
        {
            var res = new List<(int x, int y)>();
            List<(int x, int y, double score)> score;

            if (round % 3 == 1)
                score = board.ScoreFirst();
            else if (round % 3 == 2)
                score = board.ScoreSecond(null);
            else
                score = board.ScoreThird(null);

            if (!isInanna)
            {
                score = score.Where(x => x.score >= 1e10).ToList();
            }

            // sorting logic: determine which sorting method to use based on isInanna
            if (isInanna)
            {
                // use modulus sorting
                score.Sort((a, b) => (b.score % 1e10).CompareTo(a.score % 1e10));
            }
            else
            {
                // use full score sorting
                score.Sort((a, b) => b.score.CompareTo(a.score));
            }

            if (score.Count > 0)
                res.Add((score[0].x, score[0].y));
            if (score.Count > 1)
                res.Add((score[1].x, score[1].y));

            if (score.Count > 2)
            {
                if (board.IsSkull(score[0].x, score[0].y) && board.IsSkull(score[1].x, score[1].y))
                {
                    var empt = score.Where(x => !board.IsSkull(x.x, x.y)).ToList();
                    if (empt.Count > 0)
                        res.Add((empt[0].x, empt[0].y));
                    else
                        res.Add((score[2].x, score[2].y));
                }
                else
                {
                    res.Add((score[2].x, score[2].y));
                }
            }
            return res;
        }
    }
}
