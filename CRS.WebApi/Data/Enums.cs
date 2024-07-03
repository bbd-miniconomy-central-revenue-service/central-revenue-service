using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRS.WebApi.Data;

public enum TaxPayerType
{
    INDIVIDUAL = 1,
    BUSINESS
}

public enum TaxStatus
{
    ACTIVE = 1,
    INACTIVE
}

public enum TaxType
{
    INCOME = 1,
    VAT,
    PROPERTY
}