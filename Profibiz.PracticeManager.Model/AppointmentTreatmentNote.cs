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
	public partial class AppointmentTreatmentNote
	{
		public AppointmentTreatmentNote()
		{
		}

		public Guid RowId { get; set; }
		public Guid AppointmentRowId { get; set; }
        public bool? InformedConsentReceived { get; set; }
        public bool Treatment { get; set; }
        public bool Assessment { get; set; }
        public int? Subjectiveinfo { get; set; }
        public bool CervicalSpine { get; set; }
        public bool ThoracicSpine { get; set; }
        public bool LumbarSpine { get; set; }
        public bool AffectedJointsGlenohumeral { get; set; }
        public bool AffectedJointsElbow { get; set; }
        public bool AffectedJointsWrist { get; set; }
        public bool AffectedJointsHip { get; set; }
        public bool AffectedJointsKnee { get; set; }
        public bool AffectedJointsAnkle { get; set; }
        public bool? AffectedDailyActivities { get; set; }
        public int? Hydrotherapy { get; set; }
        public bool AreasTreatedUpperBody { get; set; }
        public bool AreasTreatedLowerBody { get; set; }
        public bool AreasTreatedFullBody { get; set; }
        public bool AreasTreatedBack { get; set; }
        public bool AreasTreatedHipArea { get; set; }
        public bool AreasTreatedNeck { get; set; }
        public bool AreasTreatedShoulders { get; set; }
        public bool AreasTreatedAbdominals { get; set; }
        public bool AreasTreatedChest { get; set; }
        public bool AreasTreatedFace { get; set; }
        public bool AreasTreatedScalp { get; set; }
        public bool AreasTreatedArmLR { get; set; }
        public bool AreasTreatedLegLR { get; set; }
        public string AreasTreatedOther { get; set; }
        public bool StressReductionPain { get; set; }
        public bool ROMImprovRelaxation { get; set; }
        public bool TechniquesDeepTissue { get; set; }
        public bool TechniquesModeratePressure { get; set; }
        public bool TechniquesLightPressure { get; set; }
        public bool TechniquesSwedish { get; set; }
        public bool TechniquesThaiOil { get; set; }
        public bool TechniquesThai { get; set; }
        public bool TechniquesAshiatsu { get; set; }
        public bool TechniquesLymphaticDrainage { get; set; }
        public bool TechniquesStroking { get; set; }
        public bool TechniquesRocking { get; set; }
        public bool TechniquesEffleurage { get; set; }
        public bool TechniquesPetrissage { get; set; }
        public bool TechniquesTriggerPoint { get; set; }
        public bool TechniquesPressurePoints { get; set; }
        public bool TechniquesJointMobilization { get; set; }
        public bool TechniquesFriction { get; set; }
        public bool TechniquesPassiveStretching { get; set; }
        public bool FeedbackRelaxation { get; set; }
        public bool FeedbackStressReduction { get; set; }
        public bool FeedbackMuscleRelaxation { get; set; }
        public bool FeedbackIncreaseROM { get; set; }
        public bool FeedbackPainReduction { get; set; }
        public bool FeedbackPostureImprovement { get; set; }
        public bool FeedbackTenstionReduction { get; set; }
        public bool FeedbackStiffnessReduction { get; set; }
        public bool RecommendedHeatPacks { get; set; }
        public bool RecommendedColdPacks { get; set; }
        public bool RecommendedContrastHydrotherapy { get; set; }
        public bool RecommendedHotBaths { get; set; }
        public bool RecommendedSelfMassage { get; set; }
        public bool RecommendedStretching { get; set; }
        public bool RecommendedDiaphragmaticBreathing { get; set; }
        public bool RecommendedYoga { get; set; }
        public bool RecommendedPilates { get; set; }
        public bool RecommendedWalkingSwimmingCycling { get; set; }
        public bool RecommendedNonWeightBbearing { get; set; }
        public string Comments { get; set; }
		public bool IFC { get; set; }
		public bool Hotpack { get; set; }
		public bool Ultrasound { get; set; }
		public bool SoftTissueRelease { get; set; }
		public bool ShockwaveTherapy { get; set; }
		public bool ColdPack { get; set; }
		public bool Laser { get; set; }
		public bool Exercises { get; set; }

		public bool IsChanged { get; set; }
	}
}
