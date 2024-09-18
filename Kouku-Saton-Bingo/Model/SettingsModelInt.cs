using System;

namespace Kouku_Saton_Bingo.Model
{
    public class SettingsModelInt
    {
        private int _sizeValue;

        // event to notify value changes
        public event Action<int> SizeValueChanged;

        public SettingsModelInt(string value)
        {
            SizeValue = int.Parse(value); ;
        }

        public SettingsModelInt(int value)
        {
            SizeValue = value;
        }

        // externally exposed Size property, triggers an event when the value changes
        public int SizeValue
        {
            get => _sizeValue;
            set
            {
                if (_sizeValue != value)
                {
                    _sizeValue = value;
                    if (SizeValueChanged != null)
                        SizeValueChanged?.Invoke(_sizeValue);
                }
            }
        }

        public override string ToString()
        {
            return _sizeValue.ToString();
        }
    }
}
