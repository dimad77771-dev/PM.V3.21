using DevExpress.Mvvm;
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
	public partial class MedicalHistoryRecord
	{
		public MedicalHistoryRecord()
		{
		}

		public Guid RowId { get; set; }
		public Guid PatientRowId { get; set; }
		public DateTime RecordDate { get; set; }
		public bool Headaches { get; set; }
		public bool Migraines { get; set; }
		public bool VisionProblemsLoss { get; set; }
		public bool EarachesHearingLoss { get; set; }
		public bool JawProblems { get; set; }
		public bool Smoking { get; set; }
		public bool ChronicCough { get; set; }
		public bool Emphysema { get; set; }
		public bool Asthma { get; set; }
		public bool Bronchitis { get; set; }
		public bool ShortnessOfBreath { get; set; }
		public bool HighBloodPressure { get; set; }
		public bool LowBloodPressure { get; set; }
		public bool ChronicCongestiveHeartFailure { get; set; }
		public bool HistoryOfHeartDiseaseMl { get; set; }
		public bool PhlebitisVaricoseVeins { get; set; }
		public bool StrokeCva { get; set; }
		public bool PacemakerOrSimilarDevice { get; set; }
		public bool HeartAttack { get; set; }
		public bool CardiovascularOther { get; set; }
		public string CardiovascularOtherText { get; set; }
		public bool PregnantDue { get; set; }
		public string PregnantDueText { get; set; }
		public bool GynecologicalConditions { get; set; }
		public string GynecologicalConditionsText { get; set; }
		public bool Neck { get; set; }
		public bool MidBack { get; set; }
		public bool LowerBack { get; set; }
		public bool Shoulders { get; set; }
		public bool LegLeftRight { get; set; }
		public bool MusclesJointsOther { get; set; }
		public string MusclesJointsOtherText { get; set; }
		public string Infections { get; set; }
		public string SkinConditions { get; set; }
		public string SurgeryType { get; set; }
		public string SurgeryDate { get; set; }
		public string SurgeryCurrentSymptoms { get; set; }
		public string InjuryType { get; set; }
		public string InjuryDate { get; set; }
		public string InjuryCurrentSymptoms { get; set; }
		public bool DigestiveProblems { get; set; }
		public bool Constipation { get; set; }
		public bool Epilepsy { get; set; }
		public bool LossOfSensation { get; set; }
		public bool LiverGallBladderProblems { get; set; }
		public bool KidneyProblems { get; set; }
		public bool Diabetes { get; set; }
		public bool AllergiesHypersensitivityReaction { get; set; }
		public bool Cancer { get; set; }
		public bool Arthritis { get; set; }
		public bool Hemophilia { get; set; }
		public bool Osteoporosis { get; set; }
		public bool MentalIllness { get; set; }
		public bool InternalPinsWiresArtificialJoints { get; set; }
		public string OtherConditionsWhere { get; set; }
		public bool OtherConditionsOther { get; set; }
		public string OtherConditionsOtherText { get; set; }
		public string CurrentMedication { get; set; }
		public string MedicalDoctorName { get; set; }
		public string MedicalDoctorAddress { get; set; }
		public string MedicalDoctorPhone { get; set; }
		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
