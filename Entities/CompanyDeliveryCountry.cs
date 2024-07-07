using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeMonthly.Entities;

public class CompanyDeliveryCountry
{
    public int Id { get; set; } 
    public Company PrintDistributor { get; set; }
    public Country DeliveryCountry { get; set; }
}
