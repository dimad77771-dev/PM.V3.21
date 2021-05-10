using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using Profibiz.PracticeManager.Infrastructure;

namespace Profibiz.PracticeManager.Patients.Views
{
	public class OrderItemsInfoUserControl : ContentControl
	{
		public static readonly DependencyProperty InfoProperty =
			DependencyProperty.Register(nameof(Info), typeof(string), typeof(OrderItemsInfoUserControl), new PropertyMetadata(OnInfoChange));
		public string Info
		{
			get { return (string)GetValue(InfoProperty); }
			set { SetValue(InfoProperty, value); }
		}

		public static void OnInfoChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((OrderItemsInfoUserControl)d).RedrawGrid();
		}



		public OrderItemsInfoUserControl()
		{

		}

		private Grid inlineGrid = null;
		public void RedrawGrid()
		{
			var grid = new Grid();
			//grid.Background = new SolidColorBrush(Colors.Yellow);


			var xdoc = XDocument.Parse("<root>" + Info + "</root>");

			var items = xdoc.Elements().Single().Elements().Select(q =>
			new
			{
				MedicalServiceName = (string)q.Element("MedicalServiceName"),
				Qty = (decimal?)q.Element("Qty"),
				Price = (decimal?)q.Element("Price"),
				Tax = (decimal?)q.Element("Tax"),
				Description = (string)q.Element("Description"),
			}).OrderBy(q => q.MedicalServiceName).ToArray();


			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80, GridUnitType.Pixel) });

			for (int r = 0; r < items.Length; r++)
			{
				grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

				var margin = new Thickness(4, 0, 4, 0);

				var price = ((items[r].Price ?? 0) + (items[r].Tax ?? 0));
				var qty = items[r].Qty ?? 0;
				var total = price * qty;
				var text = "" + (r + 1) + ") " + items[r].MedicalServiceName + " * " + items[r].Qty.Format("#,##0.####") + " * " + price.FormatMoney();
				var lab = new TextBlock { Text = text, Margin = margin, TextWrapping = TextWrapping.Wrap };
				grid.Children.Add(lab);
				Grid.SetColumn(lab, 0);
				Grid.SetRow(lab, r);

				
				//var total = 9999.99M;
				lab = new TextBlock { Text = total.FormatMoney(), Margin = margin };
				//lab.Background = new SolidColorBrush(Colors.Yellow);
				lab.HorizontalAlignment = HorizontalAlignment.Right;
				grid.Children.Add(lab);
				Grid.SetColumn(lab, 1);
				Grid.SetRow(lab, r);
			}

			this.Content = grid;
			inlineGrid = grid;
		}
	}
}
