// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.SelectionModeHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  internal class SelectionModeHelper
  {
    public static MultiSelectMode ConvertToMultiSelectMode(TableViewSelectMode selectMode)
    {
      switch (selectMode)
      {
        case TableViewSelectMode.Row:
          return MultiSelectMode.Row;
        case TableViewSelectMode.Cell:
          return MultiSelectMode.Cell;
        default:
          return MultiSelectMode.None;
      }
    }

    public static MultiSelectMode ConvertToMultiSelectMode(CardViewSelectMode selectMode)
    {
      return selectMode == CardViewSelectMode.Row ? MultiSelectMode.Row : MultiSelectMode.None;
    }
  }
}
