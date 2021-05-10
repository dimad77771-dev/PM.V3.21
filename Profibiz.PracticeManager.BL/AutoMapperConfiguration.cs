using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DTO = Profibiz.PracticeManager.DTO;
using EF = Profibiz.PracticeManager.EF;


namespace Profibiz.PracticeManager.BL
{
	public static class AutoMapperConfiguration
    {


		public static void Configure()
        {
            //Mapper.CreateMap<EF.PatientsListView, DTO.PatientsListView>();
            //Mapper.CreateMap<EF.ProfessionalAssociation, DTO.ProfessionalAssociation>();
            //Mapper.CreateMap<EF.ServiceProviderAssociation, DTO.ServiceProviderAssociation>();

            //Mapper.CreateMap<EF.ServiceProvider, DTO.ServiceProvider>();
            //Mapper.CreateMap<EF.InsuranceCoverageService, DTO.InsuranceCoverageService>();
            //Mapper.CreateMap<EF.InsuranceCoverageHolderService, DTO.InsuranceCoverageHolderService>();
            //Mapper.CreateMap<EF.InsuranceCoverageHolder, DTO.InsuranceCoverageHolder>();
            //Mapper.CreateMap<EF.InsuranceCoverage, DTO.InsuranceCoverage>();
            //Mapper.CreateMap<EF.InsuranceProvider, DTO.InsuranceProvider>();
            //Mapper.CreateMap<EF.MedicalServicesOrSupply, DTO.MedicalServicesOrSupply>();

            //Mapper.CreateMap<EF.Patient, DTO.Patient>();





            //Mapper.Initialize(cfg => cfg.CreateMap<EF.PatientsListView, DTO.PatientsListView>());
            //Mapper.Initialize(cfg => cfg.CreateMap<EF.ProfessionalAssociation, DTO.ProfessionalAssociation>());

            // DO NOT USE INITIALIZE IT HAS SOME BUG IN IT 

            /*
            //Mapper.Initialize(cfg => cfg.CreateMap<EF.PatientsListView, DTO.PatientsListView>());
            //Mapper.AssertConfigurationIsValid();

            Mapper.Initialize(
                    cfg => cfg.CreateMap<EF.ProfessionalAssociation, DTO.ProfessionalAssociation>()
                   .ForSourceMember(s => s.ServiceProviderAssociations, d => d.Ignore())
                   );
            Mapper.AssertConfigurationIsValid();

            Mapper.Initialize(
                cfg => cfg.CreateMap<EF.ServiceProviderAssociation, DTO.ServiceProviderAssociation>()
                .ForSourceMember(s => s.ServiceProvider, d => d.Ignore())
                .ForMember(d => d.ServiceProvider, d => d.Ignore())
                .ForSourceMember(s => s.ProfessionalAssociation, d => d.Ignore())
                .ForMember(d => d.ProfessionalAssociation, d => d.Ignore())
            );
            Mapper.AssertConfigurationIsValid();


            Mapper.Initialize(
                cfg => cfg.CreateMap<EF.ServiceProvider, DTO.ServiceProvider>()
                .ForSourceMember(s => s.ServiceProvider, d => d.Ignore())
                .ForMember(d => d.ServiceProviderAssociations, d => d.Ignore())
                );
            Mapper.AssertConfigurationIsValid();
            

            Mapper.Initialize(cfg => cfg.CreateMap<EF.MedicalCondition, DTO.MedicalCondition>()
                .ForSourceMember( s => s.PatientMedicalConditions, t => t.Ignore())
                );
            Mapper.AssertConfigurationIsValid();

            Mapper.Initialize(cfg => cfg.CreateMap<EF.MedicalServicesOrSupply, DTO.MedicalServicesOrSupply>()
            .ForSourceMember( s => s.InsuranceCoverageServices, t => t.Ignore())
            );
            Mapper.AssertConfigurationIsValid();

            Mapper.Initialize(cfg => cfg.CreateMap<EF.InsuranceProvider, DTO.InsuranceProvider>());
            Mapper.AssertConfigurationIsValid();


            Mapper.Initialize(cfg => cfg.CreateMap<EF.InsuranceCoverageHolderService, DTO.InsuranceCoverageHolderService>()
            .ForSourceMember(s => s.InsuranceCoverageHolder, t => t.Ignore())
            .ForSourceMember(s => s.InsuranceCoverageService, t => t.Ignore())
            .ForMember(s => s.InsuranceCoverageHolder, t => t.Ignore())
            .ForMember(s => s.InsuranceCoverageService, t => t.Ignore())
            );

            Mapper.AssertConfigurationIsValid();

            Mapper.Initialize(cfg => cfg.CreateMap<EF.InsuranceCoverageService, DTO.InsuranceCoverageService>()
            .ForSourceMember(s => s.InsuranceCoverageHolderServices, t => t.Ignore())
            .ForMember(s => s.InsuranceCoverageHolderServices, t => t.Ignore())
            .ForSourceMember(x => x.MedicalServicesOrSupply, t => t.Ignore())
            .ForMember(x => x.MedicalServicesOrSupply, t => t.Ignore())
            .ForSourceMember(y => y.InsuranceCoverage, t => t.Ignore())
            .ForMember(y => y.InsuranceCoverage, t => t.Ignore())
           );
            
            Mapper.AssertConfigurationIsValid();


            Mapper.Initialize(cfg => cfg.CreateMap<EF.InsuranceCoverage, DTO.InsuranceCoverage>()
            .ForSourceMember( s => s.InsuranceCoverageHolders, t => t.Ignore())
            .ForSourceMember(s => s.InsuranceProvider, t => t.Ignore())
            .ForSourceMember(s => s.InsuranceCoverageServices, t => t.Ignore())
            .ForMember(s => s.InsuranceCoverageHolders, t => t.Ignore())
            .ForMember(s => s.InsuranceProvider, t => t.Ignore())
            .ForMember(s => s.InsuranceCoverageServices, t => t.Ignore())
            );
            Mapper.AssertConfigurationIsValid();


            Mapper.Initialize(cfg => cfg.CreateMap<EF.InsuranceCoverageHolder, DTO.InsuranceCoverageHolder>()
                    .ForSourceMember(s => s.InsuranceCoverage, t => t.Ignore())
                    .ForSourceMember(s => s.Patient, t => t.Ignore())
                    .ForSourceMember(s => s.InsuranceCoverageHolderServices, t => t.Ignore())
                    .ForMember(s => s.InsuranceCoverage, t => t.Ignore())
                    .ForMember(s => s.Patient, t => t.Ignore())
                    .ForMember(s => s.InsuranceCoverageHolderServices, t => t.Ignore())
                    );
            Mapper.AssertConfigurationIsValid();

            Mapper.Initialize(cfg => cfg.CreateMap<EF.InsuranceCoverageHolderService, DTO.InsuranceCoverageHolderService>()
                    .ForSourceMember(s => s.InsuranceCoverageHolder, t => t.Ignore())
                    .ForSourceMember(s => s.InsuranceCoverageService, t => t.Ignore())
                    .ForMember(s => s.InsuranceCoverageHolder, t => t.Ignore())
                    .ForMember(s => s.InsuranceCoverageService, t => t.Ignore())

            );
            Mapper.AssertConfigurationIsValid();


            Mapper.Initialize(cfg => cfg.CreateMap<EF.PatientMedicalCondition, DTO.PatientMedicalCondition>()
            .ForSourceMember(s => s.MedicalCondition, t => t.Ignore())
            .ForMember(s => s.MedicalCondition, t => t.Ignore())
            );
            Mapper.AssertConfigurationIsValid();

            Mapper.Initialize(cfg => cfg.CreateMap<EF.Patient, DTO.Patient>()
            .ForSourceMember(s => s.InsuranceCoverageHolders, t => t.Ignore())
            .ForMember(s => s.InsuranceCoverageHolders, t => t.Ignore())
            );
            Mapper.AssertConfigurationIsValid();
            */

        }
    }
}