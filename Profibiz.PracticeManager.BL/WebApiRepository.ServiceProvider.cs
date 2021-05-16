using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Data.Entity;
using EntityFramework.Extensions;
using LinqKit;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public IEnumerable<DTO.ServiceProvider> GetServiceProviderList(Guid? rowId, Guid? professionalAssociationRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.ServiceProviderV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (professionalAssociationRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => db.ServiceProviderAssociations.Any(z => z.AssociationRowId == professionalAssociationRowId && z.ServiceProviderRowId == q.RowId));
			}

            var list = db
				.ServiceProviderVs
				.Include(q => q.ServiceProviderServices)
				.Include(q => q.ServiceProviderAssociations)
				.Where(wh.Expand())
				.ToArray();


			var options = AutoMapperHelper.CreateOptions()
				.AddIncludeProp<DTO.ServiceProvider>((q) => q.ServiceProviderAssociations)
				.AddIncludeProp<DTO.ServiceProvider>((q) => q.ServiceProviderServices);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, 
				typeof(EF.ServiceProviderV), typeof(EF.ServiceProviderAssociation), typeof(EF.ServiceProviderService));
			var ret = mapper.Map<List<DTO.ServiceProvider>>(list);
			return ret;
		}



		public void UpdateServiceProviderCore(DTO.ServiceProvider entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(DTO.ServiceProvider),
					typeof(DTO.ServiceProviderAssociation),
					typeof(DTO.ServiceProviderService));

				if (!isDelete)
				{
					var row = mapper.Map<EF.ServiceProvider>(entity);
					var entry = db.Entry(row);
					entry.State = state;
					db.SaveChangesEx();
				}


				var serviceProviderAssociations = db.ServiceProviderAssociations.Where(q => q.ServiceProviderRowId == entity.RowId);
				serviceProviderAssociations.Delete();
				var serviceProviderServices = db.ServiceProviderServices.Where(q => q.ServiceProviderRowId == entity.RowId);
				serviceProviderServices.Delete();
				if (isDelete)
				{
					db.ServiceProviders.Where(q => q.RowId == entity.RowId).Delete();
				}

				if (!isDelete)
				{
					var eServiceProviderAssociations = mapper.Map<List<EF.ServiceProviderAssociation>>(entity.ServiceProviderAssociations);
					db.ServiceProviderAssociations.AddRange(eServiceProviderAssociations);
					var eServiceProviderServices = mapper.Map<List<EF.ServiceProviderService>>(entity.ServiceProviderServices);
					db.ServiceProviderServices.AddRange(eServiceProviderServices);
					db.SaveChangesEx();
				}

				scope.Complete();
			}

		}
	}
}
