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
	public partial class TreatmentPlanRecord
	{
		public TreatmentPlanRecord()
		{
		}

		public Guid RowId { get; set; }
		public Guid PatientRowId { get; set; }
		public DateTime RecordDate { get; set; }
		public bool DiscussedWithClient { get; set; }
		public bool SleepDisturbance { get; set; }
		public bool AnxietyAttacks { get; set; }
		public bool ProlongedStanding { get; set; }
		public bool ProlongedSitting { get; set; }
		public bool Walking { get; set; }
		public bool LiftHeavyThings { get; set; }
		public bool Banding { get; set; }
		public bool Showering { get; set; }
		public bool DecreasePain { get; set; }
		public bool IncreaseRom { get; set; }
		public bool DecreaseTension { get; set; }
		public bool StressReduction { get; set; }
		public bool Relaxation { get; set; }
		public bool ImproveSleepByReducingTtension { get; set; }
		public bool SwedishMassageDeepTissue { get; set; }
		public bool SwedishMassageToleratedPressure { get; set; }
		public bool SwedishMassageLightPressure { get; set; }
		public bool AshiatsuMassageToleratedPressure { get; set; }
		public Nullable<int> Frequency { get; set; }
		public bool DurationFullBodyMassage { get; set; }
		public bool DurationBack { get; set; }
		public bool DurationArmLR { get; set; }
		public bool DurationLegLR { get; set; }
		public bool DurationFootLR { get; set; }
		public bool DurationNeck { get; set; }
		public bool DurationShoulders { get; set; }
		public bool DurationAbdominals { get; set; }
		public bool DurationFace { get; set; }
		public bool DurationHead { get; set; }
		public bool DurationOther { get; set; }
		public bool AreasFullBodyMassage { get; set; }
		public bool AreasBack { get; set; }
		public bool AreasArmLR { get; set; }
		public bool AreasLegLR { get; set; }
		public bool AreasFootLR { get; set; }
		public bool AreasNeck { get; set; }
		public bool AreasShoulders { get; set; }
		public bool AreasAbdominals { get; set; }
		public bool AreasFace { get; set; }
		public bool AreasHead { get; set; }
		public bool AreasOther { get; set; }
		public bool CSpineFlexion80_90_p { get; set; }
		public bool CSpineFlexion80_90_m { get; set; }
		public bool CSpineExtension70_p { get; set; }
		public bool CSpineExtension70_m { get; set; }
		public bool CSpineRotation70_90_p { get; set; }
		public bool CSpineRotation70_90_m { get; set; }
		public bool CSpineSideFlex20_45_p { get; set; }
		public bool CSpineSideFlex20_45_m { get; set; }
		public bool ArmFlexion100_180_p { get; set; }
		public bool ArmFlexion100_180_m { get; set; }
		public bool ArmExtension50_60_p { get; set; }
		public bool ArmExtension50_60_m { get; set; }
		public bool ArmAbduction170_180_p { get; set; }
		public bool ArmAbduction170_180_m { get; set; }
		public bool ArmAdduction50_75_p { get; set; }
		public bool ArmAdduction50_75_m { get; set; }
		public bool ArmLatRot80_90_p { get; set; }
		public bool ArmLatRot80_90_m { get; set; }
		public bool ArmMedRot60_100_p { get; set; }
		public bool ArmMedRot60_100_m { get; set; }
		public bool ArmHorizAddAbb130_p { get; set; }
		public bool ArmHorizAddAbb130_m { get; set; }
		public bool ElbowFlexion140_150_p { get; set; }
		public bool ElbowFlexion140_150_m { get; set; }
		public bool ElbowExtension0_10_p { get; set; }
		public bool ElbowExtension0_10_m { get; set; }
		public bool ElbowSupination90_p { get; set; }
		public bool ElbowSupination90_m { get; set; }
		public bool ElbowPronation80_90_p { get; set; }
		public bool ElbowPronation80_90_m { get; set; }
		public bool WristFlexion80_90_p { get; set; }
		public bool WristFlexion80_90_m { get; set; }
		public bool WristExtension70_90_p { get; set; }
		public bool WristExtension70_90_m { get; set; }
		public bool WristAbduction15_p { get; set; }
		public bool WristAbduction15_m { get; set; }
		public bool WristAdduction30_45_p { get; set; }
		public bool WristAdduction30_45_m { get; set; }
		public bool HipFlexion110_120_p { get; set; }
		public bool HipFlexion110_120_m { get; set; }
		public bool HipExtension10_15_p { get; set; }
		public bool HipExtension10_15_m { get; set; }
		public bool HipAbduction30_50_p { get; set; }
		public bool HipAbduction30_50_m { get; set; }
		public bool HipAdduction30_p { get; set; }
		public bool HipAdduction30_m { get; set; }
		public bool HipLatRot40_60degrees_p { get; set; }
		public bool HipLatRot40_60degrees_m { get; set; }
		public bool HipMedRot30_40degrees_p { get; set; }
		public bool HipMedRot30_40degrees_m { get; set; }
		public bool KneeFlexion0_135_p { get; set; }
		public bool KneeFlexion0_135_m { get; set; }
		public bool KneeExtension0_15_p { get; set; }
		public bool KneeExtension0_15_m { get; set; }
		public bool KneeLatRot30_40degrees_p { get; set; }
		public bool KneeLatRot30_40degrees_m { get; set; }
		public bool KneeMedRot20_30degrees_p { get; set; }
		public bool KneeMedRot20_30degrees_m { get; set; }
		public bool AnklePlantar50_p { get; set; }
		public bool AnklePlantar50_m { get; set; }
		public bool AnkleDorsi20_p { get; set; }
		public bool AnkleDorsi20_m { get; set; }
		public bool AnkleSupination45_60_p { get; set; }
		public bool AnkleSupination45_60_m { get; set; }
		public bool AnklePronation15_30_p { get; set; }
		public bool AnklePronation15_30_m { get; set; }
		public bool TMJKnuckleTest_p { get; set; }
		public bool TMJKnuckleTest_m { get; set; }
		public bool ApleyScratchTestFrozenShoulder_p { get; set; }
		public bool ApleyScratchTestFrozenShoulder_m { get; set; }
		public bool CrankTestShoulderDislocation_p { get; set; }
		public bool CrankTestShoulderDislocation_m { get; set; }
		public bool HawkinsKennedyImpingementTest_p { get; set; }
		public bool HawkinsKennedyImpingementTest_m { get; set; }
		public bool SupraspinatusTtestEmptyCanTest_p { get; set; }
		public bool SupraspinatusTtestEmptyCanTest_m { get; set; }
		public bool CrankTestShoulderDislocation2_p { get; set; }
		public bool CrankTestShoulderDislocation2_m { get; set; }
		public bool PainfulArcTestForGhj60_120SubacromialImpingement_p { get; set; }
		public bool PainfulArcTestForGhj60_120SubacromialImpingement_m { get; set; }
		public bool PainfulArcTestForGhj170_180ACJointCalcification_p { get; set; }
		public bool PainfulArcTestForGhj170_180ACJointCalcification_m { get; set; }
		public bool HalsteadManeuver_p { get; set; }
		public bool HalsteadManeuver_m { get; set; }
		public bool AllensManeuver_p { get; set; }
		public bool AllensManeuver_m { get; set; }
		public bool SpeedsTestBicepsTestOrStraight_ArmTest_p { get; set; }
		public bool SpeedsTestBicepsTestOrStraight_ArmTest_m { get; set; }
		public bool CozensTestTennisElbowOrLateralEpicondylitisTest_p { get; set; }
		public bool CozensTestTennisElbowOrLateralEpicondylitisTest_m { get; set; }
		public bool GolfersElbowMedialEpicondylitisTest_p { get; set; }
		public bool GolfersElbowMedialEpicondylitisTest_m { get; set; }
		public bool PhalensTestCarpalTunnelSyndrome_p { get; set; }
		public bool PhalensTestCarpalTunnelSyndrome_m { get; set; }
		public bool PhalensTestMedialNerveLesion_p { get; set; }
		public bool PhalensTestMedialNerveLesion_m { get; set; }
		public bool MurphysSignLunateBoneDislocation_p { get; set; }
		public bool MurphysSignLunateBoneDislocation_m { get; set; }
		public bool ReversPhalensTestPain_p { get; set; }
		public bool ReversPhalensTestPain_m { get; set; }
		public bool ShoulderDepressionTest_p { get; set; }
		public bool ShoulderDepressionTest_m { get; set; }
		public bool VertebralArteryTestOrCervicalQuadrantTest_p { get; set; }
		public bool VertebralArteryTestOrCervicalQuadrantTest_m { get; set; }
		public bool SlumpTestImplicatingImpingement_p { get; set; }
		public bool SlumpTestImplicatingImpingement_m { get; set; }
		public bool SlumpTestSciaticPain_p { get; set; }
		public bool SlumpTestSciaticPain_m { get; set; }
		public bool TrendelenburgTestThePelvisOnTheNon_StanceLegFalls_p { get; set; }
		public bool TrendelenburgTestThePelvisOnTheNon_StanceLegFalls_m { get; set; }
		public bool SlumpTestIncreaseTensionInTheNeuromeningealTract_p { get; set; }
		public bool SlumpTestIncreaseTensionInTheNeuromeningealTract_m { get; set; }
		public bool BragardsTestslrModificationSciaticNerve_p { get; set; }
		public bool BragardsTestslrModificationSciaticNerve_m { get; set; }
		public bool BragardsTestslrModificationPiriformis_p { get; set; }
		public bool BragardsTestslrModificationPiriformis_m { get; set; }
		public bool BragardsTestslrModificationHamstringTightness_p { get; set; }
		public bool BragardsTestslrModificationHamstringTightness_m { get; set; }
		public bool IpsilateralProneKineticTestIfThePsis_p { get; set; }
		public bool IpsilateralProneKineticTestIfThePsis_m { get; set; }
		public bool GilletsTestOrSacralFixationTest_p { get; set; }
		public bool GilletsTestOrSacralFixationTest_m { get; set; }
		public bool PatricksTestOrFaberTestOrFigure_FourTest_p { get; set; }
		public bool PatricksTestOrFaberTestOrFigure_FourTest_m { get; set; }
		public bool ThomasTestIfTheStraightLegRaisingUp_p { get; set; }
		public bool ThomasTestIfTheStraightLegRaisingUp_m { get; set; }
		public bool NobleCompressionTestChronicInflammationOfTheIliotibialBand_p { get; set; }
		public bool NobleCompressionTestChronicInflammationOfTheIliotibialBand_m { get; set; }
		public bool PiriformisTestPainResultsInTheButtockByPiriformisMuscle_p { get; set; }
		public bool PiriformisTestPainResultsInTheButtockByPiriformisMuscle_m { get; set; }
		public bool TripodSignIfTheClientExtendsTheTrunkToRelieveTheTension_p { get; set; }
		public bool TripodSignIfTheClientExtendsTheTrunkToRelieveTheTension_m { get; set; }
		public bool ObersTestTheUpperLegRemainsAbductedInTheAir_p { get; set; }
		public bool ObersTestTheUpperLegRemainsAbductedInTheAir_m { get; set; }
		public bool RectusFemorisContractureTestTheAngleOfUnflexedKneeNotRemain_p { get; set; }
		public bool RectusFemorisContractureTestTheAngleOfUnflexedKneeNotRemain_m { get; set; }
		public bool RectusFemorisContractureTestTheAngleOfUnflexedKneeNotRemain2_p { get; set; }
		public bool RectusFemorisContractureTestTheAngleOfUnflexedKneeNotRemain2_m { get; set; }
		public bool ElysTestOrTightRectusFemorisTest_p { get; set; }
		public bool ElysTestOrTightRectusFemorisTest_m { get; set; }
		public bool TrendelenburgTestIfClientIsUnableToCompleteTheMovement_p { get; set; }
		public bool TrendelenburgTestIfClientIsUnableToCompleteTheMovement_m { get; set; }
		public bool HamstringsContractureTestIfClientIsNotAbleToTouch_p { get; set; }
		public bool HamstringsContractureTestIfClientIsNotAbleToTouch_m { get; set; }
		public bool ValgusStressTestOrAbductionTest_p { get; set; }
		public bool ValgusStressTestOrAbductionTest_m { get; set; }
		public bool VarusStressTestOrAdductionTest_p { get; set; }
		public bool VarusStressTestOrAdductionTest_m { get; set; }
		public bool LachmanTestOrRitchieTestOrTrillatTestOrLachmanTrillat_p { get; set; }
		public bool LachmanTestOrRitchieTestOrTrillatTestOrLachmanTrillat_m { get; set; }
		public bool AnteriorDrawerTest_p { get; set; }
		public bool AnteriorDrawerTest_m { get; set; }
		public bool PosteriorDrawer_p { get; set; }
		public bool PosteriorDrawer_m { get; set; }
		public bool PosteriorSagSignOrGravityDrawerTest_p { get; set; }
		public bool PosteriorSagSignOrGravityDrawerTest_m { get; set; }
		public bool BounceHomeTestSharpPainOnTheJointLine_p { get; set; }
		public bool BounceHomeTestSharpPainOnTheJointLine_m { get; set; }
		public bool McmurrayTestLateralMeniscusPathology_p { get; set; }
		public bool McmurrayTestLateralMeniscusPathology_m { get; set; }
		public bool McmurrayTestMedialMeniscusPathology_p { get; set; }
		public bool McmurrayTestMedialMeniscusPathology_m { get; set; }
		public bool McmurrayTestPatelofemoralLateralTrackingSyndrome_p { get; set; }
		public bool McmurrayTestPatelofemoralLateralTrackingSyndrome_m { get; set; }
		public bool BounceHomeTestSharpPainOnTheJointLine2_p { get; set; }
		public bool BounceHomeTestSharpPainOnTheJointLine2_m { get; set; }
		public bool NeutralPositionOfTheTalusOrWeightBearingPosition_p { get; set; }
		public bool NeutralPositionOfTheTalusOrWeightBearingPosition_m { get; set; }
		public bool ThompsonsTest_p { get; set; }
		public bool ThompsonsTest_m { get; set; }
		public bool FeissLine_p { get; set; }
		public bool FeissLine_m { get; set; }
		public bool HomansSign_p { get; set; }
		public bool HomansSign_m { get; set; }
		public bool RemedialExercisesHeatPacksToTheAffectedArea { get; set; }
		public bool RemedialExercisesColdPacksToTheAffectedArea { get; set; }
		public bool RemedialExercisesContrastHydrotherapy { get; set; }
		public bool RemedialExercisesHotBathsWithEpsomSalt { get; set; }
		public bool RemedialExercisesSelfMassageOfTheAffectedArea { get; set; }
		public bool RemedialExercisesStretchingAndStrengtheningExercises { get; set; }
		public bool RemedialExercisesDemonstrated { get; set; }
		public bool RemedialExercisesDiaphragmaticBreathing { get; set; }
		public bool RemedialExercisesYoga { get; set; }
		public bool RemedialExercisesPilates { get; set; }
		public bool RemedialExercisesWalkingSwimmingCycling { get; set; }
		public bool RemedialExercisesNonWeightBearingExercisesDemonstrated { get; set; }
		public bool ContraindicationsRisks { get; set; }
		public int? ReassessmentScheduled { get; set; }
		public int? ReassessmentScheduledOther { get; set; }
		public int? Duration { get; set; }
		public bool ReferralsChiropractor { get; set; }
		public bool ReferralsPhysiotherapist { get; set; }
		public bool ReferralsAcupuncturist { get; set; }
		public string MedicalDoctorName { get; set; }
		public string MedicalDoctorAddress { get; set; }
		public string MedicalDoctorPhone { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public bool IsShowReassessmentScheduledOther => (ReassessmentScheduled == -1);

		public IEnumerable<ReassessmentScheduledEnumClass> ReassessmentScheduledEnum
		{
			get
			{
				return new[]
				{
					new ReassessmentScheduledEnumClass { Value = 5, Name = "5 Sessions" },
					new ReassessmentScheduledEnumClass { Value = 6, Name = "6 Sessions" },
					new ReassessmentScheduledEnumClass { Value = 10, Name = "10 Sessions" },
					new ReassessmentScheduledEnumClass { Value = -1, Name = "Other" },
				};
			}
		}
		public class ReassessmentScheduledEnumClass
		{
			public int? Value { get; set; }
			public string Name { get; set; }
		}

		public IEnumerable<FrequencyEnumClass> FrequencyEnum
		{
			get
			{
				return new[]
				{
					new FrequencyEnumClass { Value = 1, Name = "Weekly" },
					new FrequencyEnumClass { Value = 2, Name = "Twice a week" },
					new FrequencyEnumClass { Value = 3, Name = "Fortnightly" },
					new FrequencyEnumClass { Value = 4, Name = "Monthly" },
				};
			}
		}
		public class FrequencyEnumClass
		{
			public int? Value { get; set; }
			public string Name { get; set; }
		}


		public IEnumerable<DurationEnumClass> DurationEnum
		{
			get
			{
				return new[]
				{
					new DurationEnumClass { Value = 30, Name = "30 min" },
					new DurationEnumClass { Value = 45, Name = "45 min" },
					new DurationEnumClass { Value = 60, Name = "60 min" },
					new DurationEnumClass { Value = 75, Name = "75 min" },
					new DurationEnumClass { Value = 90, Name = "90 min" },
					new DurationEnumClass { Value = 120, Name = "120 min" },
				};
			}
		}
		public class DurationEnumClass
		{
			public int? Value { get; set; }
			public string Name { get; set; }
		}
	}
}
