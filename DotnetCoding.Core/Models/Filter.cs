namespace DotnetCoding.Core.Models;

public class Filter
{
    public string? ProductName { get; set; } = "";
    public double? fromPrice { get; set; } = 0;
    public double? toPrice { get; set; }= 0;
    public DateTime? fromDate { get; set; } = null;
    public DateTime? toDate { get; set; } = null;
}
