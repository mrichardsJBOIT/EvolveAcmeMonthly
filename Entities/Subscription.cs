using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeMonthly.Entities;

public class Subscription
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int Id { get; set; }
    public Publication Publication { get; set; }

    public Customer Customer { get; set; }

    public Address DeliveryAddress { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public SubscriptionStatus Status { get; set; }

}

public enum SubscriptionStatus
{
    Active,
    Cancelled,
    Onhold
}
