using AxiomCRM.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomCRM.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ApiController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public ApiController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet("customers")]
    public async Task<IActionResult> Customers()
    {
        var customers = await _db.Customers
            .Select(x => new
            {
                x.CustomerId,
                x.CustomerName,
                x.Email,
                x.Phone,
                x.CompanyName,
                x.Status
            })
            .ToListAsync();

        return Ok(customers);
    }

    [HttpGet("leads")]
    public async Task<IActionResult> Leads()
    {
        return Ok(await _db.Leads.ToListAsync());
    }

    [HttpGet("opportunities")]
    public async Task<IActionResult> Opportunities()
    {
        return Ok(await _db.Opportunities.ToListAsync());
    }

    [HttpGet("reports/pipeline")]
    public async Task<IActionResult> Pipeline()
    {
        var pipeline =
            await _db.Opportunities
                .Where(x => x.Status == "Open")
                .SumAsync(x => (decimal?)x.Amount) ?? 0;

        return Ok(new
        {
            pipeline
        });
    }
}