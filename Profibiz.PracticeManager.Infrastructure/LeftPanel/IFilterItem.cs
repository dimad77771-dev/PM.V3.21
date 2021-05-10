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
	public interface IFilterItem
	{
		string Name { get; set; }
		string DisplayText { get; }
	}

	public interface ILeftPanelViewModel
	{
		void Init();
	}

	
}

