using System;
using System.Collections.Generic;
using System.Text;

namespace Whatso.Model
{
    public class Accounts
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public int CompanyCountryId { get; set; }
        public int CompanyCityId { get; set; }
        public string GSTNo { get; set; }
        public int CompanyCurrencyId { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
    }
}
