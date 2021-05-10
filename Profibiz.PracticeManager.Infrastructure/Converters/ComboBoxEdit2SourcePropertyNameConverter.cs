using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using System.Windows.Media;


namespace Profibiz.PracticeManager.Infrastructure
{
	public class ComboBoxEdit2SourcePropertyNameConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var comboBoxEdit = (ComboBoxEdit)value;
			var bindingExpression = comboBoxEdit.GetBindingExpression(BaseEdit.EditValueProperty);
			return bindingExpression.ResolvedSourcePropertyName;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
