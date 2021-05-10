using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class ServiceProviderAssociation
    {
        public Guid RowId { get; set; }
        public Guid AssociationRowId { get; set; }
        public Guid ServiceProviderRowId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationExpiryDate { get; set; }
        public bool IsPrimary { get; set; }
        public ProfessionalAssociation ProfessionalAssociation { get; set; }
        public ServiceProvider ServiceProvider { get; set; }
     
    }
}
