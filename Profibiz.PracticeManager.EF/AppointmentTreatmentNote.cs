//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Profibiz.PracticeManager.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppointmentTreatmentNote
    {
        public System.Guid RowId { get; set; }
        public System.Guid AppointmentRowId { get; set; }
        public Nullable<bool> InformedConsentReceived { get; set; }
        public bool Treatment { get; set; }
        public bool Assessment { get; set; }
        public Nullable<int> Subjectiveinfo { get; set; }
        public bool CervicalSpine { get; set; }
        public bool ThoracicSpine { get; set; }
        public bool LumbarSpine { get; set; }
        public bool AffectedJointsGlenohumeral { get; set; }
        public bool AffectedJointsElbow { get; set; }
        public bool AffectedJointsWrist { get; set; }
        public bool AffectedJointsHip { get; set; }
        public bool AffectedJointsKnee { get; set; }
        public bool AffectedJointsAnkle { get; set; }
        public Nullable<bool> AffectedDailyActivities { get; set; }
        public Nullable<int> Hydrotherapy { get; set; }
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
        public Nullable<System.Guid> CreatedByUserRowId { get; set; }
        public Nullable<System.Guid> UpdatedByUserRowId { get; set; }
        public Nullable<System.DateTime> CreatedByDateTime { get; set; }
        public Nullable<System.DateTime> UpdatedByDateTime { get; set; }
    
        public virtual AppointmentT Appointment { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ServiceProvider ServiceProvider1 { get; set; }
    }
}
