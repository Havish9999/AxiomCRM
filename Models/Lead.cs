using System.ComponentModel.DataAnnotations;

namespace AxiomCRM.Models;

public class Lead
{
    public int LeadId { get; set; }

    [Required]
    public string LeadName { get; set; } = "";

    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [Phone]
    public string Phone { get; set; } = "";

    public string CompanyName { get; set; } = "";

    public string Source { get; set; } = "Website";

    [Required]
    public string Status { get; set; } = "New";

    [Range(0, 100000000)]
    public decimal ExpectedValue { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public string AssignedTo { get; set; } = "";
}