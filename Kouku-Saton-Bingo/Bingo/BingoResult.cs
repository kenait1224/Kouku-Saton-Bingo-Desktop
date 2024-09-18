using System;
using System.Collections.Generic;
using System.Linq;

namespace Kouku_Saton_Bingo
{
    public class BingoResult
    {
        public bool[] Row { get; set; }
        public bool[] Col { get; set; }
        public bool DiS { get; set; }
        public bool DiD { get; set; }

        public static BingoResult CheckBingo(BingoBoard a)
        {
            int size = 5;
            var result = new BingoResult
            {
                Row = new bool[size],
                Col = new bool[size],
                DiS = true,
                DiD = true
            };

            for (int i = 0; i < size; i++)
            {
                result.Row[i] = true; // Assume true unless proven otherwise
                result.Col[i] = true; // Assume true unless proven otherwise

                for (int j = 0; j < size; j++)
                {
                    result.Row[i] &= a[i, j];
                    result.Col[i] &= a[j, i];
                }

                result.DiS &= a[i, i];
                result.DiD &= a[i, size - 1 - i];
            }

            return result;
        }

        public bool IsRed(int x, int y)
        {
            if (this.Row[x] || this.Col[y])
                return true;
            else if (x == y && this.DiS)
                return true;
            else if (x + y == 4 && this.DiD)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is BingoResult other)
            {
                return this.Row.SequenceEqual(other.Row) &&
                       this.Col.SequenceEqual(other.Col) &&
                       this.DiS == other.DiS &&
                       this.DiD == other.DiD;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -737785183;
            hashCode = hashCode * -1521134295 + EqualityComparer<bool[]>.Default.GetHashCode(Row);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool[]>.Default.GetHashCode(Col);
            hashCode = hashCode * -1521134295 + DiS.GetHashCode();
            hashCode = hashCode * -1521134295 + DiD.GetHashCode();
            return hashCode;
        }
    }
}
