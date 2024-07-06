using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeMonthly.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Subscription>? Subscriptions { get; set; }

}
