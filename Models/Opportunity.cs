using System.ComponentModel.DataAnnotations;

namespace AxiomCRM.Models;

public class Opportunity
{
    public int OpportunityId { get; set; }

    [Required]
    public string OpportunityName { get; set; } = "";

    public int? CustomerId { get; set; }

    [Range(0.01, 100000000)]
    public decimal Amount { get; set; }

    public string Stage { get; set; } = "Qualification";

    [Range(0, 100)]
    public int Probability { get; set; }

    [DataType(DataType.Date)]
    public DateTime ExpectedCloseDate { get; set; } =
        DateTime.Today.AddDays(7);

    public string Status { get; set; } = "Open";

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string AssignedTo { get; set; } = "";
}