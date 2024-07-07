using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeMonthly.Entities;

public class Company
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Address Address { get; set; } //Where the company is located

    //public List<CompanyDeliveryCountry> DeliveryCountryList { get; set; } //List of countries the company delivers too
}
