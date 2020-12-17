using System;
using PoshtaBy.Domain.Common;

namespace PoshtaBy.Domain.Entities
{
    public class Agency : AuditableEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool SslEnabled { get; set; }

        public string SecureUrl { get; set; }

        public string Hosts { get; set; }

        public string DefaultLanguageId { get; set; }

        public string DefaultCountryId { get; set; }

        public string AgencyName { get; set; }

        public string AgencyAddress { get; set; }

        public string AgencyPhoneNumber { get; set; }

        public string AgencyVat { get; set; }

        public string AgencyEmail { get; set; }

        public string AgencyHours { get; set; }
    }
}
