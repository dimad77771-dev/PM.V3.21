using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;


namespace Profibiz.PracticeManager.Infrastructure
{
	public class MouseWaitCursor : IDisposable
	{
		private Cursor _previousCursor;

		public MouseWaitCursor()
		{
			_previousCursor = Mouse.OverrideCursor;

			Mouse.OverrideCursor = Cursors.Wait;
		}

		#region IDisposable Members

		public void Dispose()
		{
			Mouse.OverrideCursor = _previousCursor;
		}

		#endregion
	}
}
