using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ClientWPF.Converter
{
    class ConverterBoolToString: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "Non";
            if (value.GetType() != typeof(bool)) return "Non";
            if (!(bool)value) return "Non";
            return "Oui";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(string)) return false;
            switch ((string)value)
            {
                case "Non":
                    return false;
                case "Oui":
                    return true;
            }
            return false;
        }
    }
}
