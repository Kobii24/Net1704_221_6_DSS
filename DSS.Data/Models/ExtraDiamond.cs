﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DSS.Data.Models;

public partial class ExtraDiamond
{
    public int ExtraDiamondId { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public bool? Status { get; set; }

    public decimal? Carat { get; set; }

    public string Color { get; set; }

    public string Cut { get; set; }

    public string Describe { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}