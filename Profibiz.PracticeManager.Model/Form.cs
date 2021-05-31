using PropertyChanged;
using System;
using System.Collections.Generic;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class Form
    {
        public Form()
        {
        }
    
        public Guid RowId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
