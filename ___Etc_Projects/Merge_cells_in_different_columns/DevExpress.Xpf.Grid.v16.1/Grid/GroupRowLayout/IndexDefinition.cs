// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowLayout.IndexDefinition
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.GroupRowLayout
{
  public struct IndexDefinition
  {
    private readonly int group;
    private readonly int layer;
    private readonly int column;

    public int Group
    {
      get
      {
        return this.group;
      }
    }

    public int Layer
    {
      get
      {
        return this.layer;
      }
    }

    public int Column
    {
      get
      {
        return this.column;
      }
    }

    public IndexDefinition(int group, int layer, int column)
    {
      this.group = group;
      this.layer = layer;
      this.column = column;
    }
  }
}
