﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeMonthly.Entities;

public class Country
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string CountryCode { get; set; } = string.Empty;
}
