﻿namespace CRS.WebApi.Services;
using CRS.WebApi.Models;
using CRS.WebApi.Data;

public class TaxCalculatorService
{
    private readonly TaxCalculatorFactory _taxCalculatorFactory;

    public TaxCalculatorService(TaxCalculatorFactory taxCalculatorFactory)
    {
        _taxCalculatorFactory = taxCalculatorFactory;
    }

    public decimal CalculateTax(decimal amount, string taxType)
    {
        ICalculator<decimal, int> calculator = _taxCalculatorFactory.GetTaxCalculator(taxType);
        return calculator.CalculateTaxWithRateFromDb(amount, (int)Enum.Parse(typeof(Data.TaxType), taxType.ToUpper()));
    }
}
