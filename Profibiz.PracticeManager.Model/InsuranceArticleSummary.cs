using DevExpress.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class InsuranceArticleSummary
	{
		public InsuranceArticleSummary()
		{
		}
		public InsuranceArticleV[] Rows { get; set; }
	}
}
