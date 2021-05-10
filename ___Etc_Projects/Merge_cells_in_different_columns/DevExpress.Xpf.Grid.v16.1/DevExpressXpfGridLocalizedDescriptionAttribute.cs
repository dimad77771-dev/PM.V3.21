// Decompiled with JetBrains decompiler
// Type: DevExpressXpfGridLocalizedDescriptionAttribute
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.ComponentModel;
using System.Resources;

[AttributeUsage(AttributeTargets.All)]
internal class DevExpressXpfGridLocalizedDescriptionAttribute : DescriptionAttribute
{
  private static ResourceManager rm;
  private bool loaded;

  public override string Description
  {
    get
    {
      if (!this.loaded)
      {
        this.loaded = true;
        if (DevExpressXpfGridLocalizedDescriptionAttribute.rm == null)
        {
          lock (typeof (DevExpressXpfGridLocalizedDescriptionAttribute))
          {
            if (DevExpressXpfGridLocalizedDescriptionAttribute.rm == null)
              DevExpressXpfGridLocalizedDescriptionAttribute.rm = new ResourceManager("DevExpress.Xpf.Grid.Descriptions", typeof (DevExpressXpfGridLocalizedDescriptionAttribute).Assembly);
          }
        }
        this.DescriptionValue = DevExpressXpfGridLocalizedDescriptionAttribute.rm.GetString(base.Description);
      }
      return base.Description;
    }
  }

  public DevExpressXpfGridLocalizedDescriptionAttribute(string name)
    : base(name)
  {
  }
}
