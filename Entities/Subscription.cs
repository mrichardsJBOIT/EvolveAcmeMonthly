using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeMonthly.Entities;

public class Subscription
{
    public int Id { get; set; }
    public Publication Publication { get; set; }

    public Customer Customer { get; set; }

    public Address DeliveryAddress { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; }

}
