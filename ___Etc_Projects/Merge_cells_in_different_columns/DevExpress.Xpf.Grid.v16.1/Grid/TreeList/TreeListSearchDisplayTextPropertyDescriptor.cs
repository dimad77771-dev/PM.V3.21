// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSearchDisplayTextPropertyDescriptor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListSearchDisplayTextPropertyDescriptor : TreeListDisplayTextPropertyDescriptor
  {
    private string originalName;

    public TreeListSearchDisplayTextPropertyDescriptor(TreeListDataProvider provider, string name)
      : base(provider, TreeListSearchDisplayTextPropertyDescriptor.AddPrefix(name))
    {
      this.originalName = name;
    }

    protected override object GetValueCore(TreeListNode node)
    {
      return (object) this.DataProvider.View.GetNodeDisplayText(node, this.originalName, this.DataProvider.View.GetNodeValue(node, this.originalName));
    }

    private static string AddPrefix(string name)
    {
      return "DxFts_" + name;
    }
  }
}
