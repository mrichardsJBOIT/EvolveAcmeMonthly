using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeMonthly.Entities;

public class Address
{
    public int Id { get; set; }

    public AddressTypes Type { get; set; }

    // ToDo: Should map to Company or Customer, but needs entity type hierarchy mapping
    // 'https://learn.microsoft.com/en-us/ef/core/modeling/inheritance'
    // public object Owner { get; set; }  
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public Country Country { get; set; }

}

public enum AddressTypes
{
    Delivery,
    Company,
    Cancelled
}
