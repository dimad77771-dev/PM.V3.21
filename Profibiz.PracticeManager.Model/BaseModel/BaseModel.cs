using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	public class BaseModel
	{

		public virtual T DeepCopy<T>()
		{
			T other = (T)this.MemberwiseClone();
			return other;
		}

		public void BaseRaisePropertyChanged(string col)
		{
			var tp = this.GetType();
			var prop = tp.GetMethod("OnPropertyChanged");
			prop.Invoke(this, new object[] { col });
		}
		

	}
}
