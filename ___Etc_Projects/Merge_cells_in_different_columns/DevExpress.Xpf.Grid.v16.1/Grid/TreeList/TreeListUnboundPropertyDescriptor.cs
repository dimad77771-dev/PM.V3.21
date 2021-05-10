// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListUnboundPropertyDescriptor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering.Helpers;
using System;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListUnboundPropertyDescriptor : PropertyDescriptor
  {
    private UnboundColumnInfo unboundInfo;
    private TreeListDataProvider provider;
    private ExpressionEvaluator evaluator;
    private Type dataType;
    private Exception evaluatorCreateException;

    public override bool IsBrowsable
    {
      get
      {
        return this.unboundInfo.Visible;
      }
    }

    protected ExpressionEvaluator Evaluator
    {
      get
      {
        if (this.evaluator == null)
          this.evaluator = this.CreateEvaluator();
        return this.evaluator;
      }
    }

    protected TreeListDataProvider DataProvider
    {
      get
      {
        return this.provider;
      }
    }

    public UnboundColumnInfo UnboundInfo
    {
      get
      {
        return this.unboundInfo;
      }
    }

    public override bool IsReadOnly
    {
      get
      {
        return this.UnboundInfo.ReadOnly;
      }
    }

    public override string Category
    {
      get
      {
        return string.Empty;
      }
    }

    public override Type PropertyType
    {
      get
      {
        return this.UnboundInfo.DataType;
      }
    }

    public override Type ComponentType
    {
      get
      {
        return typeof (IList);
      }
    }

    private bool RequireValueConversion
    {
      get
      {
        return this.UnboundInfo.RequireValueConversion;
      }
    }

    protected internal TreeListUnboundPropertyDescriptor(TreeListDataProvider provider, UnboundColumnInfo unboundInfo)
      : base(unboundInfo.Name, (Attribute[]) null)
    {
      this.evaluator = (ExpressionEvaluator) null;
      this.provider = provider;
      this.unboundInfo = unboundInfo;
      this.dataType = this.UnboundInfo.DataType;
    }

    protected virtual ExpressionEvaluator CreateEvaluator()
    {
      return this.DataProvider.CreateExpressionEvaluator(this.UnboundInfo.Expression, out this.evaluatorCreateException);
    }

    public override void ResetValue(object component)
    {
    }

    public override bool CanResetValue(object component)
    {
      return false;
    }

    public override bool ShouldSerializeValue(object component)
    {
      return false;
    }

    public override object GetValue(object component)
    {
      object obj = (object) null;
      if (this.Evaluator != null)
        obj = this.GetEvaluatorValue((TreeListNode) component);
      else if (this.evaluatorCreateException != null)
        obj = (object) UnboundErrorObject.Value;
      return this.DataProvider.GetUnboundData(component, this.Name, obj);
    }

    public override void SetValue(object component, object value)
    {
      this.DataProvider.SetUnboundData(component, this.Name, value);
      this.DataProvider.OnNodeCollectionChanged((TreeListNode) component, NodeChangeType.Content, true, this.Name);
    }

    protected virtual object GetEvaluatorValue(TreeListNode node)
    {
      try
      {
        return this.Convert(this.Evaluator.Evaluate((object) node));
      }
      catch
      {
        return (object) UnboundErrorObject.Value;
      }
    }

    protected object Convert(object value)
    {
      if (!this.RequireValueConversion || value == null)
        return value;
      if (TreeListUnboundPropertyDescriptor.IsErrorValue(value))
        return value;
      try
      {
        if (value.GetType().Equals(this.dataType))
          return value;
        return Convert.ChangeType(value, this.dataType, (IFormatProvider) Thread.CurrentThread.CurrentCulture);
      }
      catch
      {
      }
      return (object) null;
    }

    public static bool IsErrorValue(object value)
    {
      return object.ReferenceEquals(value, (object) UnboundErrorObject.Value);
    }
  }
}
