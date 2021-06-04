using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DTO = Profibiz.PracticeManager.DTO;
using EF = Profibiz.PracticeManager.EF;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Profibiz.PracticeManager.BL
{
	public static class AutoMapperHelper
	{
		public static IMapper GetMapper(Action<IMapperConfiguration> configureAction = null, bool ignoreGlobalProfile = false)
		{
			MapperConfiguration config;
			if (configureAction == null)
			{
				config = new MapperConfiguration(q => { });
			}
			else
			{
				config = new MapperConfiguration(configureAction);
			}
			if (!ignoreGlobalProfile)
			{
				(config as IMapperConfiguration).AddProfile<GlobalAutoMapperProfile>();
			}
			return config.CreateMapper();
		}


		public static IMapper GetPocoMapperWithOptions(Options options = null, params string[] typenames)
		{
			var config = new MapperConfiguration(q =>
			{
				foreach (var typename in typenames)
				{
					var typeDTO = FindDTOType(typename);
					var typeEF = FindEFType(typename);
					q.CreateMap(typeEF, typeDTO).Poco(typeDTO, options);
					q.CreateMap(typeDTO, typeEF).Poco(typeEF, options);

					q.CreateMap(typeDTO, typeDTO).Poco(typeDTO, options);
					q.CreateMap(typeEF, typeEF).Poco(typeEF, options);
				}

				if (options != null && options.CreateMissingTypeMaps)
				{
					q.CreateMissingTypeMaps = true;
				}
			});
			return config.CreateMapper();
		}

		public static IMapper GetPocoMapper(params Type[] types)
		{
			return GetPocoMapperWithOptions(null, types);
		}

		public static IMapper GetPocoMapperWithOptions(Options options = null, params Type[] types)
		{
			return GetPocoMapperWithOptions(options, types.Select(q => q.Name).ToArray());
		}

		public static Type FindDTOType(string typename)
		{
			var type = typeof(DTO.Patient).Assembly.GetTypes().SingleOrDefault(q => q.Name == typename);
            if (type == null && typename.EndsWith("V"))
            {
                type = typeof(DTO.Patient).Assembly.GetTypes().SingleOrDefault(q => q.Name == typename.Substring(0, typename.Length - 1));
            }
            if (type == null && typename.EndsWith("T"))
            {
                type = typeof(DTO.Patient).Assembly.GetTypes().SingleOrDefault(q => q.Name == typename.Substring(0, typename.Length - 1));
            }
            if (type == null) throw new Exception("type not found:" + typename);
			return type;
		}
		public static Type FindEFType(string typename)
		{
			var type = typeof(EF.Patient).Assembly.GetTypes().SingleOrDefault(q => q.Name == typename);
            if (type == null && typename.EndsWith("V"))
            {
                type = typeof(EF.Patient).Assembly.GetTypes().SingleOrDefault(q => q.Name == typename.Substring(0, typename.Length - 1));
            }
            if (type == null) throw new Exception("type not found:" + typename);
            return type;
		}

		

		public class GlobalAutoMapperProfile : Profile
		{
			protected override void Configure()
			{
				CreateMap<EF.PatientsListView, DTO.PatientsListView>();
				CreateMap<EF.ProfessionalAssociation, DTO.ProfessionalAssociation>();
				CreateMap<EF.ServiceProviderAssociation, DTO.ServiceProviderAssociation>();

				CreateMap<EF.ServiceProvider, DTO.ServiceProvider>();
				CreateMap<EF.InsuranceCoverageService, DTO.InsuranceCoverageService>();
				CreateMap<EF.InsuranceCoverageHolderService, DTO.InsuranceCoverageHolderService>();
				CreateMap<EF.InsuranceCoverageHolder, DTO.InsuranceCoverageHolder>();
				CreateMap<EF.InsuranceCoverage, DTO.InsuranceCoverage>();
				CreateMap<EF.InsuranceProvider, DTO.InsuranceProvider>();
				CreateMap<EF.MedicalServicesOrSupply, DTO.MedicalServicesOrSupply>();

				CreateMap<EF.Patient, DTO.Patient>();
			}
		}

		public static IMappingExpression Poco(this IMappingExpression mappingExpression, Type type, Options options)
		{
			var props = type.GetProperties();
			foreach (var prop in props)
			{
				var ptype = prop.PropertyType;
				var ignore = false;
				if (!IsPocoype(ptype))
				{
					ignore = true;
				}
				if (options != null && options.IncludeProp.Any(q => q.TypeName == type.Name && q.PropertyName == prop.Name))
				{
					ignore = false;
				}

				if (options?.ExcludeCreatedUpdatedColumns == true)
				{
					if (new[] { "CreatedByUserRowId", "UpdatedByUserRowId", "CreatedByDateTime", "UpdatedByDateTime" }.Contains(prop.Name))
					{
						ignore = true;
					}
				}

				if (ignore)
				{
					mappingExpression.ForMember(prop.Name, z => z.Ignore());
				}
			}
			return mappingExpression;
		}

		public static IMappingExpression<TSource, TDestination> Poco<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
		{
			var type = typeof(TDestination);
			var props = type.GetProperties();
			foreach (var prop in props)
			{
				var ptype = prop.PropertyType;
				if (!IsPocoype(ptype))
				{
					mappingExpression.ForMember(prop.Name, z => z.Ignore());
				}
			}
			return mappingExpression;
		}


		static bool IsPocoype(Type ptype)
		{
			return
			(
				ptype == typeof(byte[]) ||
				ptype == typeof(string) ||

				ptype == typeof(Nullable<bool>) ||
				ptype == typeof(Nullable<byte>) ||
				ptype == typeof(Nullable<decimal>) ||
				ptype == typeof(Nullable<double>) ||
				ptype == typeof(Nullable<float>) ||
				ptype == typeof(Nullable<int>) ||
				ptype == typeof(Nullable<long>) ||
				ptype == typeof(Nullable<short>) ||
				ptype == typeof(Nullable<DateTime>) ||
				ptype == typeof(Nullable<DateTimeOffset>) ||
				ptype == typeof(Nullable<Guid>) ||
				ptype == typeof(Nullable<TimeSpan>) ||

				ptype == typeof(bool) ||
				ptype == typeof(byte) ||
				ptype == typeof(decimal) ||
				ptype == typeof(double) ||
				ptype == typeof(float) ||
				ptype == typeof(int) ||
				ptype == typeof(long) ||
				ptype == typeof(short) ||
				ptype == typeof(DateTime) ||
				ptype == typeof(DateTimeOffset) ||
				ptype == typeof(Guid) ||
				ptype == typeof(TimeSpan) ||

				false
			);
		}


		public class Options
		{
			public List<IncludeProp> IncludeProp = new List<IncludeProp>();

			public Options AddIncludeProp<T>(Expression<Func<T, object>> propertyExpression)
			{
				var includeProp = new IncludeProp
				{
					TypeName = typeof(T).Name,
					PropertyName = MapperReflectionHelper.FindProperty(propertyExpression).Name,
				};
				IncludeProp.Add(includeProp);
				return this;
			}

			public bool CreateMissingTypeMaps { get; set; }

			public bool ExcludeCreatedUpdatedColumns { get; set; } = false;
		}

		public static Options CreateOptions()
		{
			return new Options();
		}

		public class IncludeProp
		{
			public string TypeName { get; set; }
			public string PropertyName { get; set; }
		}
	}
}