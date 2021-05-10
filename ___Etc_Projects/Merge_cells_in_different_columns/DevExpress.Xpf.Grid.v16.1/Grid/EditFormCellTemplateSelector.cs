// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.EditFormCellTemplateSelector
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.EditForm;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class EditFormCellTemplateSelector : DataTemplateSelector
  {
    public DataTemplate CaptionTemplate { get; set; }

    public DataTemplate EditorTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      IEditFormLayoutItem editFormLayoutItem = item as IEditFormLayoutItem;
      if (editFormLayoutItem == null)
        return (DataTemplate) null;
      int num = (int) editFormLayoutItem.ItemType;
      if (editFormLayoutItem.ItemType == EditFormLayoutItemType.Caption)
        return this.CaptionTemplate;
      if (editFormLayoutItem.ItemType == EditFormLayoutItemType.Editor)
        return this.EditorTemplate;
      return (DataTemplate) null;
    }
  }
}
