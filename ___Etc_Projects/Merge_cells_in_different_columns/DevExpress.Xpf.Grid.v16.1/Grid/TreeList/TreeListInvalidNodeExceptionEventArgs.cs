// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListInvalidNodeExceptionEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.InvalidNodeException" /> event.
  /// </para>
  ///             </summary>
  public class TreeListInvalidNodeExceptionEventArgs : TreeListNodeEventArgs, IInvalidRowExceptionEventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets the error description.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the error description.
    /// </value>
    public string ErrorText { get; set; }

    /// <summary>
    ///                 <para>Gets or sets the error window's caption.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the caption of the error window.
    /// </value>
    public string WindowCaption { get; set; }

    /// <summary>
    ///                 <para>Gets or sets a value that specifies how an exception should be handled.
    /// </para>
    ///             </summary>
    /// <value>An <see cref="T:DevExpress.Xpf.Grid.ExceptionMode" /> enumeration value that specifies how an exception should be handled.
    /// </value>
    public ExceptionMode ExceptionMode { get; set; }

    /// <summary>
    ///                 <para>Gets the exception that raised the <see cref="E:DevExpress.Xpf.Grid.TreeListView.InvalidNodeException" /> event.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Exception" /> object that is the exception that raised the event.
    /// </value>
    public Exception Exception { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListInvalidNodeExceptionEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node which failed validation. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListNodeEventArgs.Node" /> property.
    /// 
    ///           </param>
    /// <param name="errorText">
    /// A <see cref="T:System.String" /> value that specifies the error description. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListInvalidNodeExceptionEventArgs.ErrorText" /> property.
    /// 
    ///           </param>
    /// <param name="windowCaption">
    /// A <see cref="T:System.String" /> value that specifies the caption of the error window. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListInvalidNodeExceptionEventArgs.WindowCaption" /> property.
    /// 
    ///           </param>
    /// <param name="exception">
    /// A <see cref="T:System.Exception" /> object that is the exception that raised the event. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListInvalidNodeExceptionEventArgs.Exception" /> property.
    /// 
    ///           </param>
    /// <param name="exceptionMode">
    /// An <see cref="T:DevExpress.Xpf.Grid.ExceptionMode" /> enumeration value that specifies how an exception should be handled. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListInvalidNodeExceptionEventArgs.ExceptionMode" /> property.
    /// 
    ///           </param>
    public TreeListInvalidNodeExceptionEventArgs(TreeListNode node, string errorText, string windowCaption, Exception exception, ExceptionMode exceptionMode)
      : base(node)
    {
      this.ErrorText = errorText;
      this.WindowCaption = windowCaption;
      this.Exception = exception;
      this.ExceptionMode = exceptionMode;
    }
  }
}
