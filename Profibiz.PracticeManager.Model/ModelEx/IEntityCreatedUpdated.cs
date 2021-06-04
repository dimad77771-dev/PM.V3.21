using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Profibiz.PracticeManager.Model
{
	public interface IEntityCreatedUpdated
	{
		Guid? CreatedByUserRowId { get; set; }
		Guid? UpdatedByUserRowId { get; set; }
		DateTime? CreatedByDateTime { get; set; }
		DateTime? UpdatedByDateTime { get; set; }
	}

	public static class EntityCreatedUpdatedExtension
	{
		public static string GetCreatedUpdatedString(this IEntityCreatedUpdated arg)
		{
			var vals = new List<string>();

			if (arg.CreatedByDateTime != null)
			{
				var fullName = LookupDataProvider.FindServiceProvider(arg.CreatedByUserRowId)?.FullName;
				vals.Add("Created: " + fullName + " - " + arg.CreatedByDateTime.Value.ToString(@"MM\/dd\/yyyy") + " " + arg.CreatedByDateTime.Value.ToString("t"));
			}

			if (arg.UpdatedByDateTime != null && arg.UpdatedByDateTime != arg.CreatedByDateTime)
			{
				var fullName = LookupDataProvider.FindServiceProvider(arg.UpdatedByUserRowId)?.FullName;
				vals.Add("Last modified: " + fullName + " - " + arg.UpdatedByDateTime.Value.ToString(@"MM\/dd\/yyyy") + " " + arg.UpdatedByDateTime.Value.ToString("t"));
			}

			return string.Join("; ", vals);
		}

	}

}