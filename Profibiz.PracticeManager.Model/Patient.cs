using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class Patient : BaseModel, IDataErrorInfo
	{
		public Guid RowId { get; set; }
		public string Title { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string PreferredName { get; set; }
		public Guid FamilyHeadRowId { get; set; }
		public string FamilyMemberType { get; set; }
		public string RelationToFamilyHead { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Sex { get; set; }
		public string CardNo { get; set; }
		public DateTime? FirstSeen { get; set; }
		public Guid? SendInvoicesToFamilyMember { get; set; }
		public string Address1 { get; set; }
		public string Province1 { get; set; }
		public string City1 { get; set; }
		public string Postcode1 { get; set; }
		public string Address2 { get; set; }
		public string Province2 { get; set; }
		public string City2 { get; set; }
		public string Postcode2 { get; set; }
		public string AddressToUse { get; set; }
		public string HomePhoneNumber { get; set; }
		public string MobileNumber { get; set; }
		public string Occupation { get; set; }
		public string EmployerName { get; set; }
		public string WorkPhone { get; set; }
		public string Fax { get; set; }
		public string EmailAddress { get; set; }
		public string FamilyDoctor { get; set; }
		public string FamilyDoctorAddress { get; set; }
		public string FamilyDoctorPhoneNumber { get; set; }
		public byte[] Photo { get; set; }
		public bool HasHighBloodPressure { get; set; }
		public bool HasPacemaker { get; set; }
		public bool HasDiabetes { get; set; }
		public bool HasHepatitis { get; set; }
		public bool HasHeadaches { get; set; }
		public bool HasSurgeries { get; set; }
		public bool HasMetalImplants { get; set; }
		public bool HasFractures { get; set; }
		public bool HasNeckPain { get; set; }
		public bool HasBackPain { get; set; }
		public bool HasShoulderElbowHandPain { get; set; }
		public bool HasHipKneeFootPain { get; set; }
		public bool HasShoulderPain { get; set; }
		public bool HasElbowPain { get; set; }
		public bool HasHandPain { get; set; }
		public bool HasHipPain { get; set; }
		public bool HasKneePain { get; set; }
		public bool HasFootPain { get; set; }
		public string OtherMedicalConditions { get; set; }
		public string HealthHistoryNotes { get; set; }
		public Guid? ReferrerRowId { get; set; }
		public bool UseHeadAddress { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
		public string ReferrerName { get; set; }
		public string PrimaryPolicies { get; set; }
		public string SecondaryPolicies { get; set; }
		public decimal? InvoiceFamilyBalance { get; set; }
		public decimal Rate { get; set; }
		public bool HasNoCoverage { get; set; }
		public bool IsNotRegistered { get; set; }
		public byte[] Signature { get; set; }

		public virtual ObservableCollection<Patient> FamilyMembers { get; set; }
		public virtual Patient FamilyHead { get; set; }
		public virtual ObservableCollection<PatientCoverage> PatientCoverage { get; set; } = new ObservableCollection<Model.PatientCoverage>();
		public virtual ObservableCollection<InsuranceCoverage> InsuranceCoverages { get; set; } = new ObservableCollection<InsuranceCoverage>();
		public virtual ObservableCollection<PatientNote> PatientNotes { get; set; } = new ObservableCollection<PatientNote>();
		public virtual ObservableCollection<PatientDocument> PatientDocuments { get; set; } = new ObservableCollection<PatientDocument>();
		public virtual ObservableCollection<Appointment> AppointmentWithClinicalNotes { get; set; } = new ObservableCollection<Appointment>();
		public virtual ObservableCollection<Appointment> AppointmentWithTreatmentNotes { get; set; } = new ObservableCollection<Appointment>();
		public virtual Appointment PatientFormDocuments { get; set; } = new Appointment();


		public ChangeFamilyMemberInfo ChangeFamilyMember { get; set; }
		public class ChangeFamilyMemberInfo
		{
			public ActionEnum Action { get; set; }
			public Guid NewFamilyHeadRowId { get; set; }
			public enum ActionEnum { RemoveFromFamily, MoveMemberToHeader, MoveToAnotherFamily }
		}

		public bool ChangeFamilyMembersAddress { get; set; }
		public bool IgnoreDuplicateLastFirstNameFlag { get; set; }

		public Patient() : base()
		{
			SubsribeAddressChange();
		}


		public bool IsFamilyHead
		{
			get
			{
				return (FamilyMemberType == TypeHelper.FamilyMemberType.Head);
			}
		}

		public string Rowtype9 => FamilyMemberType;
		public string Rowtype9b => (IsCheckBoxVisibility ? "Single" : FamilyMemberType);



		public string FullName
		{
			get
			{
				var ret = (LastName + (string.IsNullOrEmpty(FirstName) ? "" : ", " + FirstName))?.Trim();
				if (string.IsNullOrEmpty(ret))
				{
					ret = "<Empty>";
				}
				return ret;
			}
		}

		public string FullNameForAppointment
		{
			get
			{
				if (IsNotRegistered)
				{
					var name = string.Join(" / ", new[] { FirstName, EmailAddress, MobileNumber }.Where(q => !string.IsNullOrEmpty(q)));
					return name;
				}
				else
				{
					return FullName;
				}
			}
		}

		public string ContactPhone
		{
			get
			{
				if (!string.IsNullOrEmpty(MobileNumber)) return MobileNumber;
				if (!string.IsNullOrEmpty(MobileNumber)) return MobileNumber;
				if (!string.IsNullOrEmpty(WorkPhone)) return WorkPhone;
				return "";
			}
		}

		public int? GetAge()
		{
			if (BirthDate == null) return null;

			var today = DateTime.Today;
			var birthdate = BirthDate.Value;

			var age = today.Year - birthdate.Year;
			if (birthdate > today.AddYears(-age)) age--;
			return age;
		}

		public string GetAddress()
		{
			var adr = AddressToUse == TypeHelper.AddressToUse.SecondAddress ?
				Address2 + " " + City2 + " " + Province2 + " " + Postcode2 :
				Address1 + " " + City1 + " " + Province1 + " " + Postcode1;
			adr = (adr ?? "").Trim();
			return adr;
		}
		public string GetAddressLine()
		{
			var adr = AddressToUse == TypeHelper.AddressToUse.SecondAddress ? Address2 : Address1;
			adr = (adr ?? "").Trim();
			return adr;
		}
		public string GetAddressCity()
		{
			var adr = AddressToUse == TypeHelper.AddressToUse.SecondAddress ? City2 : City1;
			adr = (adr ?? "").Trim();
			return adr;
		}
		public string GetAddressProvince()
		{
			var adr = AddressToUse == TypeHelper.AddressToUse.SecondAddress ? Province2 : Province1;
			adr = (adr ?? "").Trim();
			return adr;
		}
		public string GetAddressPostcode()
		{
			var adr = AddressToUse == TypeHelper.AddressToUse.SecondAddress ? Postcode2 : Postcode1;
			adr = (adr ?? "").Trim();
			return adr;
		}


		public bool IsReadOnlyAddress => UseHeadAddress;
		public bool IsVisibleUseHeadAddress => !IsFamilyHead;

		public bool ReadOnlyFamilyMemberType { get; set; }
		public bool IsChanged { get; set; }

		public bool IsExpanded { get; set; }
		public bool IsCheckBox { get; set; }
		public bool IsCheckBoxVisibility { get; set; }

		public bool IsSelectHeadFamily { get; set; }
		public bool IsSelectUseHeadAddress { get; set; }
		public bool IsEnabledFieldIsSelectUseHeadAddress => !IsSelectHeadFamily;


		private byte[] _pho;
		[JsonIgnoreAttribute]
		public byte[] Pho
		{
			get
			{
				if (_pho != null) return _pho;

				//return null;
				//Debug.WriteLine("ManagedThreadId=" + Thread.CurrentThread.ManagedThreadId);
				//var dt1 = DateTime.Now;
				var service = ServiceLocator.Current.GetInstance<IPatientsBusinessSharedService>();
				var photo = service.GetPatientPhoto(RowId);
				//var dt2 = DateTime.Now;
				//Debug.WriteLine(FirstName + "." + LastName + "=11=" + dt1.ToString("mm:hh:ss.fff"));
				//Debug.WriteLine(FirstName + "." + LastName + "=22=" + dt2.ToString("mm:hh:ss.fff"));
				//Debug.WriteLine(FirstName + "." + LastName + "=33=" + (dt2 - dt1).TotalMilliseconds);
				_pho = photo;
				return photo;

				//Thread.Sleep(1000);
				//var bytes = File.ReadAllBytes(@"E:\PROJECTS\Profibiz.PracticeManager\___SQL\_Photos\095cbda2-951b-40ae-a6cf-fa4d6410987b.png");
				//return bytes;
			}
		}



		public bool IsErrorInfoWork { get; set; }
		string IDataErrorInfo.Error
		{
			get
			{
				if (!IsErrorInfoWork) return null;
				return null;
			}
		}
		string IDataErrorInfo.this[string columnName]
		{
			get
			{
				if (!IsErrorInfoWork) return null;

				if (columnName == "FirstName")
				{
					if (string.IsNullOrEmpty(FirstName))
					{
						return "\"First Name\" is empty";
					}
				}
				else if (columnName == "LastName")
				{
					if (string.IsNullOrEmpty(LastName))
					{
						return "\"Last Name\" is empty";
					}
				}
				return null;
			}
		}

		public bool IsAddressChanged;
		void SubsribeAddressChange()
		{
			(this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (AddressFields.Contains(e.PropertyName))
				{
					IsAddressChanged = true;
				}
				if (e.PropertyName == nameof(IsChanged))
				{
					if (!IsChanged)
					{
						IsAddressChanged = false;
					}
				}
			};
		}

		public override string ToString()
		{
			return FullName;
		}



		//public object GetValue(string columnname)
		//{
		//	var prop = typeof(Patient).GetProperty(columnname);
		//	var val = prop.GetValue(this);
		//	return val;
		//}

		public static void CopyAddressFields(Patient src, Patient dst)
		{
			foreach (var prop in AddressFields.Select(q => typeof(Patient).GetProperty(q)))
			{
				var val = prop.GetValue(src);
				prop.SetValue(dst, val);
			}
		}


		public static string[] AddressFields = new[] 
		{
			nameof(Province1), nameof(Address1), nameof(City1), nameof(Postcode1),
			nameof(Province2), nameof(Address2), nameof(City2), nameof(Postcode2)
		};
	}
}
