using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatso.Model;

namespace Whatso.App.Models
{
    public class CompanyDetailsModel
    {
        public CompanyDetailsModel()
        {
            CompanyDetail = new Accounts();
        }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
        public Accounts CompanyDetail { get; set; }
    }
}
