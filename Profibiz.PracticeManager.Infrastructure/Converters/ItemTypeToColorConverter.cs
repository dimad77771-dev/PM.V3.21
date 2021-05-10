using System;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Infrastructure
{
    public class ItemTypeToColorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //return value is string && (string)value == "+" ? Brushes.White : Brushes.Gray;
            if ((string)value == "Service")
            {
                return Brushes.LightGray;
            }
            else
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
