using System;
using System.Linq;


namespace Profibiz.PracticeManager.Model
{
	public interface IPatientsBusinessSharedService
	{
		byte[] GetPatientPhoto(Guid rowId);
	}
}
