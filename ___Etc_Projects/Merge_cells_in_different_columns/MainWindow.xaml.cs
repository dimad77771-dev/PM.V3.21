// Developer Express Code Central Example:
// How to merge cells in the GridControl
// 
// This example demonstrates how to achieve the merged cells effect in the
// GridControl. The main idea consists of stretching the top merged cell and
// showing it over other merged cells. For this purpose, the Panel.ZIndex and
// Margin properties are used.
// Please take special note that this example doesn't
// support editing cells. This example is designed for the DeepBlue theme and may
// need to be modified if you wish to use it in other themes.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E4614

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;
using System.Globalization;
using DevExpress.Xpf.Core;

namespace CellMerging {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
        public MainWindow() {
            InitializeComponent();
            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            customers.Add(new Customer() { ID1 = 0, ID2 = 1, Name = "Name0" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 1, Name = "Name0" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 1, ID2 = 2, Name = "Name1" });
            customers.Add(new Customer() { ID1 = 2, ID2 = 2, Name = "Name1" });
            gridControl1.ItemsSource = customers;
            this.InvalidateArrange();
        }
    }

    public class MyTemplateSelector : DataTemplateSelector {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            CellEditor cellEditor = container as CellEditor;
            EditGridCellData cellData = item as EditGridCellData;
            if (cellData.Column.FieldName != "ID1") {
                cellEditor.Margin = new Thickness(0, 0, 0, 0);
                return base.SelectTemplate(item, container);
            }
            TableView view = cellData.View as TableView;

            TryMerge(cellEditor, cellData, view.Grid.GetRowVisibleIndexByHandle(cellData.RowData.RowHandle.Value));
            return base.SelectTemplate(item, container);
        }
        protected bool TryMerge(CellEditor container, EditGridCellData cellData, int curIndex) {
            int mergCount = 0;
            TableView view = (TableView)cellData.View;
            GridControl grid = view.Grid;

            //for (int i = curIndex + 1; i < grid.VisibleRowCount; i++)
            //{
            //    int nextRH = grid.GetRowHandleByVisibleIndex(i);
            //    if (object.Equals(grid.GetCellValue(nextRH, (GridColumn)cellData.Column), cellData.Value) && nextRH > 0)
            //        mergCount++;
            //    else
            //        break;
            //}

            double width = 0;
            for (int i = cellData.Column.VisibleIndex + 1; i < view.VisibleColumns.Count; i++) {
                var c = view.VisibleColumns[i];
                if (object.Equals(grid.GetCellValue(curIndex, c), cellData.Value))
                    width += c.ActualDataWidth;
                else
                    break;
            }
            container.Margin = new Thickness(0, 0, -width, 0);
            return true;
        }
    }
    public class NegativeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return -(int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
    public class MyStackVisibleIndexPanel : StackVisibleIndexPanel {
        protected override Size MeasureSortedChildrenOverride(Size availableSize, IList<UIElement> sortedChildren) {
            Size origAvalSize = availableSize;
            Size baseMeasureSize = base.MeasureSortedChildrenOverride(availableSize, sortedChildren);
            if (origAvalSize.Width < baseMeasureSize.Width)
                baseMeasureSize.Width = origAvalSize.Width;
            return baseMeasureSize;
        }

    }
    public class Customer {
        public int ID1 {
            get;
            set;
        }
        public int ID2 {
            get;
            set;
        }
        public string Name {
            get;
            set;
        }
    }
}