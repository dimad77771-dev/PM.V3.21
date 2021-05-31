using PropertyChanged;
using System;
using System.Collections.Generic;


namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class FormItem
    {
        public FormItem()
        {
        }
    
        public Guid RowId { get; set; }
        public Guid? FormRowId { get; set; }
        public string Code { get; set; }
        public int? SectionOrder { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public int? OrderInSection { get; set; }
        public bool? IsDetailsRequired { get; set; }
        public string DetailsLabel { get; set; }
        public bool? IsBoolean { get; set; }
        public bool? IsDateTime { get; set; }
        public bool? IsText { get; set; }
        public bool? IsNumeric { get; set; }

		public string ValueText { get; set; }
		public DateTime? ValueDateTime { get; set; }
		public bool? ValueBoolean { get; set; }
		public decimal? ValueNumeric { get; set; }

		public int RowNum { get; set; }
		public int ColNum { get; set; }

		public bool IsChanged { get; set; }
	}
}
