﻿namespace CRS.WebApi.Services
{
    using CRS.WebApi.Models;

    public class PropertyTaxCalculator : ICalculator
    {
        private readonly CrsdbContext _context;

        public PropertyTaxCalculator(CrsdbContext context)
        {
            _context = context;
        }

        public decimal CalculateTax(decimal amount, decimal rate)
        {
            return amount * rate;
        }
        
        public decimal CalculateTaxWithRateFromDb(decimal amount, int taxTypeId)
        {
            var taxType = _context.TaxTypes.FirstOrDefault(t => t.Id == taxTypeId);
            if (taxType == null)
            {
                throw new Exception("TaxType not found.");
            }

            return CalculateTax(amount, taxType.Rate);
        }
    }
}