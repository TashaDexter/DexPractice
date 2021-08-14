namespace BankSystem.Models
{
    public abstract class Currency
    {
        public string Type { get; set; }
        public double ValueInDollars { get; set; }
    }
}
