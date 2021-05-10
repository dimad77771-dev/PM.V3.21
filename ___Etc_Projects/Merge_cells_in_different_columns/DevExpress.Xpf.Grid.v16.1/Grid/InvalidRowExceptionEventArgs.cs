// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridViewBase.InvalidRowException" /> event.
  /// </para>
  ///             </summary>
  public class InvalidRowExceptionEventArgs : RowEventArgs, IInvalidRowExceptionEventArgs
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
    ///                 <para>Gets the exception that raised the <see cref="E:DevExpress.Xpf.Grid.GridViewBase.InvalidRowException" /> event.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Exception" /> object representing the exception that raised the event.
    /// </value>
    public Exception Exception { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the InvalidRowExceptionEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewBase" /> descendant that represents the grid's view.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row that failed validation. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.RowEventArgs.RowHandle" /> property.
    /// 
    ///           </param>
    /// <param name="errorText">
    /// A <see cref="T:System.String" /> value that specifies the error description. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs.ErrorText" /> property.
    /// 
    ///           </param>
    /// <param name="windowCaption">
    /// A <see cref="T:System.String" /> value that specifies the caption of the error window. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs.WindowCaption" /> property.
    /// 
    ///           </param>
    /// <param name="exception">
    /// A <see cref="T:System.Exception" /> object representing the exception that raised the event. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs.Exception" /> property.
    /// 
    ///           </param>
    /// <param name="exceptionMode">
    /// An <see cref="T:DevExpress.Xpf.Grid.ExceptionMode" /> enumeration value that specifies how an exception should be handled. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs.ExceptionMode" /> property.
    /// 
    ///           </param>
    public InvalidRowExceptionEventArgs(GridViewBase view, int rowHandle, string errorText, string windowCaption, Exception exception, ExceptionMode exceptionMode)
      : base(GridViewBase.InvalidRowExceptionEvent, view, rowHandle)
    {
      this.ErrorText = errorText;
      this.WindowCaption = windowCaption;
      this.Exception = exception;
      this.ExceptionMode = exceptionMode;
    }
  }
}
