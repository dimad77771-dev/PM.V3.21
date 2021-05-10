using System;
using Microsoft.Practices.ServiceLocation;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Profibiz.PracticeManager.Model;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using System.IO;

namespace Profibiz.PracticeManager.Patients.BusinessService
{
	public class PatientsBusinessSharedService : IPatientsBusinessSharedService
	{
		static ConcurrentDictionary<Guid, byte[]> CachePhotos = null;// new ConcurrentDictionary<Guid, byte[]>();
		public byte[] GetPatientPhoto(Guid rowId)
		{
			byte[] photo;
			if (CachePhotos != null && CachePhotos.TryGetValue(rowId, out photo))
			{
				Debug.WriteLine("get from CachePhotos=" + rowId);
				return GetPhoto(photo);
			}

			var patientsBusinessService = ServiceLocator.Current.GetInstance<IPatientsBusinessService>();
			var task = patientsBusinessService.GetPatientPhoto(rowId);
			//task.Wait();
			photo = task.Result;

			if (CachePhotos != null)
			{
				CachePhotos.TryAdd(rowId, photo);
			}

			Debug.WriteLine("not from CachePhotos=" + rowId);
			return GetPhoto(photo);
		}

		private byte[] GetPhoto(byte[] photo)
		{
			if (photo == null || photo.Length == 0)
			{
				photo = CommonResources.photo_not_exists;
			}
			return photo;
		}
	}
}
