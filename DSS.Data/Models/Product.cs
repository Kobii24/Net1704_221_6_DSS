﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DSS.Data.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public string Name { get; set; }

    public long MainDiamondId { get; set; }

    public long ExtraDiamondId { get; set; }

    public long NumberExDiamond { get; set; }

    public long DiamondShellId { get; set; }

    public long Quantity { get; set; }

    public int Size { get; set; }

    public bool Status { get; set; }

    public double Amount { get; set; }

    public double Fee { get; set; }

    public double TotalAmount { get; set; }
}