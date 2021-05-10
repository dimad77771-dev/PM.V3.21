﻿using DevExpress.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class InsuranceProvider
    {
        public Guid RowId { get; set; }
        public string Code { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Fax { get; set; }
		public string WebSite { get; set; }
		public string Notes { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";
		public string DragDropRowText => CompanyName;
		public string FullName => CompanyName;
		public string Name => CompanyName;

		public Boolean IsSelected { get; set; }
		public DelegateCommand InsuranceSelectUnselectCommand => new DelegateCommand(() =>
		{
			IsSelected = !IsSelected;
		});

	}
}
