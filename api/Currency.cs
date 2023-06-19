namespace CurrencyRateProvider;

public record Currency
{
    public int Quantity { get; set; }
    public string RateDate { get; set; }
    public decimal Rate { get; set; }
    public string Link { get; set; }
}
