using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class LogicalExceptionValue<T>
	{
		public LogicalExceptionValue()
		{
			throw new LogicalException();
		}

		public LogicalExceptionValue(string arg)
		{
			throw new LogicalException(arg);
		}
	}
}

