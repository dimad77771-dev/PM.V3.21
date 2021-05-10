// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.HitTest.CardViewHitTestAcceptorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid.HitTest
{
  public abstract class CardViewHitTestAcceptorBase : GridViewHitTestAcceptorBase
  {
    public override sealed void Accept(FrameworkElement element, GridViewHitTestVisitorBase visitor)
    {
      this.AcceptCardVisitor(element, (CardViewHitTestVisitorBase) visitor);
    }

    public abstract void AcceptCardVisitor(FrameworkElement element, CardViewHitTestVisitorBase visitor);
  }
}
