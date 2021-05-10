// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListCustomSummaryEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.CustomSummary" /> event.
  /// </para>
  ///             </summary>
  public class TreeListCustomSummaryEventArgs : EventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets the total summary value.
    /// </para>
    ///             </summary>
    /// <value>An object that specifies the total summary value.</value>
    public object TotalValue { get; set; }

    /// <summary>
    ///                 <para>Gets the processed field value.
    /// </para>
    ///             </summary>
    /// <value>An object that specifies the processed field value.</value>
    public object FieldValue { get; private set; }

    /// <summary>
    ///                 <para>Gets the processed node.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the processed node.
    /// </value>
    public TreeListNode Node { get; private set; }

    /// <summary>
    ///                 <para>Gets a summary item whose value is being calculated.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.SummaryItemBase" /> descendant that is the summary item whose values is being calculated.
    /// </value>
    public DevExpress.Xpf.Grid.SummaryItemBase SummaryItem { get; private set; }

    /// <summary>
    ///                 <para>Gets a value indicating the calculation stage.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Data.CustomSummaryProcess" /> enumeration value indicating the calculation stage.
    /// 
    /// </value>
    public CustomSummaryProcess SummaryProcess { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListCustomSummaryEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the processed node. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomSummaryEventArgs.Node" /> property.
    /// 
    ///           </param>
    /// <param name="summaryItem">
    /// A <see cref="T:DevExpress.Xpf.Grid.SummaryItemBase" /> descendant that is the summary item whose values is being calculated. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomSummaryEventArgs.SummaryItem" /> property.
    /// 
    ///           </param>
    /// <param name="summaryProcess">
    /// A <see cref="T:DevExpress.Data.CustomSummaryProcess" /> enumeration value indicating the calculation stage. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomSummaryEventArgs.SummaryProcess" /> property.
    /// 
    ///           </param>
    /// <param name="fieldValue">
    /// An object that specifies the processed field value. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListCustomSummaryEventArgs.FieldValue" /> property.
    /// 
    ///           </param>
    public TreeListCustomSummaryEventArgs(TreeListNode node, DevExpress.Xpf.Grid.SummaryItemBase summaryItem, CustomSummaryProcess summaryProcess, object fieldValue)
    {
      this.Node = node;
      this.SummaryItem = summaryItem;
      this.SummaryProcess = summaryProcess;
      this.FieldValue = fieldValue;
    }
  }
}
