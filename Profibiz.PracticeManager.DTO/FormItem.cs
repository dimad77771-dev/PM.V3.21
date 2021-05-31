using System;
using System.Collections.Generic;


namespace Profibiz.PracticeManager.DTO
{
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
    }
}
