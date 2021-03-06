using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace Profibiz.PracticeManager.EF
{
	public partial class PracticeManagerEntities
	{
		public PracticeManagerEntities(Guid? currentUserRowId, bool useLogFile) : this()
		{
			CurrentUserRowId = currentUserRowId;

			if (useLogFile)
			{
				this.Database.Log = (q) =>
				{
					System.Diagnostics.Debug.WriteLine(q);
					var path = ConfigurationSettings.AppSettings["LogFile.Database"];
					if (!string.IsNullOrEmpty(path))
					{
						File.AppendAllText(path, q);
					}
				};
			}

			int sqlCommandTimeOut;
			if (Int32.TryParse(ConfigurationSettings.AppSettings["SqlCommandTimeOut"], out sqlCommandTimeOut))
			{
				this.Database.CommandTimeout = sqlCommandTimeOut;
			}
		}

		public static PracticeManagerEntities GetConnection(Guid? currentUserRowId, bool useLogFile = true)
		{
			useLogFile = false;
			var _db = new EF.PracticeManagerEntities(currentUserRowId, useLogFile: useLogFile);
			return _db;
		}

		Guid? CurrentUserRowId { get; set; }

		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified))
			{
				if (entry.Entity is IEntityCreatedUpdated entity)
				{
					var now = DateTime.Now;

					if (entry.State == EntityState.Added)
					{
						entity.CreatedByDateTime = now;
						entity.CreatedByUserRowId = CurrentUserRowId;
						entity.UpdatedByDateTime = now;
						entity.UpdatedByUserRowId = CurrentUserRowId;
					}
					else if (entry.State == EntityState.Modified)
					{
						entity.UpdatedByDateTime = now;
						entity.UpdatedByUserRowId = CurrentUserRowId;
					}

				}
			}

			return base.SaveChanges();
		}

	}
}