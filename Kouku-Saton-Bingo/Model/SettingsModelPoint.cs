using System;
using System.Drawing;

namespace Kouku_Saton_Bingo.Model
{
    public class SettingsModelPoint
    {
        private Point _value;

        // event to notify value changes
        public event Action<Point> ValueChanged;

        public SettingsModelPoint(string value)
        {
            string[] point = value.Split(',');
            if (point.Length > 1)
                Value = new Point(int.Parse(point[0]), int.Parse(point[1]));
            else
                Value = new Point(200,200);
        }

        public SettingsModelPoint(Point value)
        {
            Value = value;
        }

        // externally exposed Point property, triggers an event when the value changes
        public Point Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    // 觸發事件，通知外部訂閱者數值發生變化
                    if (ValueChanged != null)
                        ValueChanged?.Invoke(_value);
                }
            }
        }

        public override string ToString()
        {
            return _value.X.ToString() + "," + _value.Y.ToString();
        }
    }
}
