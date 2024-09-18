using System;

namespace Kouku_Saton_Bingo.Model
{
    public class SettingsModelBoolean
    {
        private bool _value;

        // event to notify value changes
        public event Action<bool> ValueChanged;

        public SettingsModelBoolean(string value)
        {
            Value = bool.Parse(value); ;
        }

        public SettingsModelBoolean(bool value)
        {
            Value = value;
        }
        // externally exposed Boolean property, triggers an event when the value changes
        public bool Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (ValueChanged != null)
                        ValueChanged?.Invoke(_value);
                }
            }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
