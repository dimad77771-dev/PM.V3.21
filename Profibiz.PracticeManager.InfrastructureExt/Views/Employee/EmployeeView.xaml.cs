using System;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.UI.Interactivity;

namespace DevExpress.DevAV.Views
{
    public partial class EmployeeView : UserControl
	{
        public EmployeeView()
		{
            InitializeComponent();
			//aaaaa.OverridesDefaultStyle
			//aaaaa

			//<из wpf_4_podrobnoe_rukovodstvo.pdf>
			// Получить ключ стиля по умолчанию
			object defaultStyleKey = aaaaa.GetValue(FrameworkElement.DefaultStyleKeyProperty);
			// Извлечь ресурс с этим ключом
			Style style = (Style)Application.Current.FindResource(defaultStyleKey);
			// Сериализовать его XAML-представлениев виде строки 
			string xaml = System.Windows.Markup.XamlWriter.Save(style);
			//Для других типов стилей можно вызвать метод FindResource с соответствующим


			//var meth = typeof(FrameworkElement).GetField("DefaultStyleKeyProperty", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
			//var dp = meth.GetValue(null);
			//var vl = aaaaa.GetValue((DependencyProperty)dp);

		}
	}
}
