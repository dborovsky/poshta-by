using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PoshtaBy.Domain.Entities;



namespace PoshtaBy.Application
{
    public interface IAgencyContext
    {
        Agency CurrentAgency { get; }

        Task<Agency> SetCurrentAgency();

        Task SetAgencyCookie(string Id);
    }
}
