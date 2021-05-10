// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ProgressControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid
{
  public class ProgressControl : UserControl, IComponentConnector
  {
    private DispatcherTimer stopTimer;
    private ManualResetEvent stopEvent;
    private bool shutdownDispatcher;
    private DXWindow progressWindow;
    internal Button btnCancel;
    private bool _contentLoaded;

    public DXWindow ProgressWindow
    {
      get
      {
        return this.progressWindow;
      }
    }

    public ProgressControl(DXWindow progressWindow, ManualResetEvent stopEvent, bool shutdownDispatcher, string cancelCaption)
    {
      this.InitializeComponent();
      this.progressWindow = progressWindow;
      this.progressWindow.Closed += (EventHandler) ((s, e) => this.stopTimer.Stop());
      this.stopEvent = stopEvent;
      this.shutdownDispatcher = shutdownDispatcher;
      this.btnCancel.Content = (object) cancelCaption;
      this.stopTimer = new DispatcherTimer(DispatcherPriority.Background);
      this.stopTimer.Interval = TimeSpan.FromMilliseconds(100.0);
      this.stopTimer.Tick += new EventHandler(this.stopTimer_Tick);
      this.stopTimer.Start();
    }

    private void Stop()
    {
      this.stopTimer.Stop();
      this.ProgressWindow.Close();
      if (!this.shutdownDispatcher)
        return;
      this.Dispatcher.InvokeShutdown();
    }

    private void stopTimer_Tick(object sender, EventArgs e)
    {
      if (!this.stopTimer.IsEnabled || this.stopEvent == null || !this.stopEvent.WaitOne(1))
        return;
      this.Stop();
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
      this.Stop();
      e.Handled = true;
    }

    public static DXWindow CreateProgressWindow(ManualResetEvent stopEvent, bool shutdownDispatcher, string title, string cancelCaption)
    {
      DXWindow progressWindow = new DXWindow();
      progressWindow.Content = (object) new ProgressControl(progressWindow, stopEvent, shutdownDispatcher, cancelCaption);
      progressWindow.Title = title;
      progressWindow.Width = 500.0;
      progressWindow.SizeToContent = SizeToContent.Height;
      progressWindow.ShowIcon = false;
      progressWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      progressWindow.WindowStyle = WindowStyle.ToolWindow;
      return progressWindow;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/DevExpress.Xpf.Grid.v16.1;component/grid/frameworkelements/progresscontrol.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      if (connectionId == 1)
      {
        this.btnCancel = (Button) target;
        this.btnCancel.Click += new RoutedEventHandler(this.btnCancel_Click);
      }
      else
        this._contentLoaded = true;
    }
  }
}
