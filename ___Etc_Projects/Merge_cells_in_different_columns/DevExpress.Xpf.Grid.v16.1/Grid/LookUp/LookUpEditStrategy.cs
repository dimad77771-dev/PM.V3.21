// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.LookUpEditStrategy
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.EditStrategy;
using DevExpress.Xpf.Editors.Internal;
using DevExpress.Xpf.Grid.LookUp.Native;
using System;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.LookUp
{
  public class LookUpEditStrategy : LookUpEditStrategyBase, ISelectorEditStrategy, IEditStrategy
  {
    private readonly Locker PopupSizeChangeLocker = new Locker();

    private LookUpEdit Editor
    {
      get
      {
        return base.Editor as LookUpEdit;
      }
    }

    internal GridControlVisualClientOwner VisualClient
    {
      get
      {
        return base.VisualClient as GridControlVisualClientOwner;
      }
    }

    protected override bool IsLockedByValueChanging
    {
      get
      {
        if (!base.IsLockedByValueChanging)
          return this.PopupSizeChangeLocker.IsLocked;
        return true;
      }
    }

    public LookUpEditStrategy(LookUpEdit editor)
      : base((LookUpEditBase) editor)
    {
    }

    public virtual bool AllowPopupProcessGestures(Key key, ModifierKeys modifiers)
    {
      if (!this.VisualClient.IsSearchControlFocused)
        return false;
      if (key == Key.Escape)
        return !this.VisualClient.IsSearchTextEmpty;
      return true;
    }

    public virtual void SetInitialPopupSize()
    {
      this.PopupSizeChangeLocker.DoLockedAction((Action) (() => this.Editor.SetInitialPopupSizeInternal()));
    }
  }
}
