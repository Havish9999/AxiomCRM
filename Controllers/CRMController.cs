using AxiomCRM.Data;
using AxiomCRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AxiomCRM.Controllers;

public class CRMController : Controller
{
    private readonly ApplicationDbContext _context;

    public CRMController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        ViewBag.Customers = _context.Customers.Count();
        ViewBag.Leads = _context.Leads.Count();
        ViewBag.Opportunities = _context.Opportunities.Count();
        ViewBag.FollowUps = _context.FollowUps.Count();
        ViewBag.Activities = _context.Activities.Count();

        return View();
    }

    public IActionResult Customers()
    {
        var customers = _context.Customers
            .OrderByDescending(x => x.CustomerId)
            .ToList();

        return View(customers);
    }

    [HttpGet]
    public IActionResult CreateCustomer()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateCustomer(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        customer.Status = "Active";
        customer.CreatedDate = DateTime.Now;

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return RedirectToAction(nameof(Customers));
    }

    [HttpGet]
    public IActionResult EditCustomer(int id)
    {
        var customer = _context.Customers
            .FirstOrDefault(x => x.CustomerId == id);

        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditCustomer(Customer customer)
    {
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        var existingCustomer = _context.Customers
            .FirstOrDefault(x => x.CustomerId == customer.CustomerId);

        if (existingCustomer == null)
        {
            return NotFound();
        }

        existingCustomer.CustomerName = customer.CustomerName;
        existingCustomer.Email = customer.Email;
        existingCustomer.Phone = customer.Phone;
        existingCustomer.CompanyName = customer.CompanyName;
        existingCustomer.Address = customer.Address;
        existingCustomer.City = customer.City;
        existingCustomer.State = customer.State;
        existingCustomer.Status = customer.Status;

        _context.SaveChanges();

        return RedirectToAction(nameof(Customers));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _context.Customers
            .FirstOrDefault(x => x.CustomerId == id);

        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return RedirectToAction(nameof(Customers));
    }

    public IActionResult Audit()
    {
        var logs = _context.AuditLogs
            .OrderByDescending(x => x.AuditLogId)
            .ToList();

        return View(logs);
    }

    public IActionResult Reports()
    {
        ViewBag.Customers = _context.Customers.Count();

        ViewBag.Leads = _context.Leads.Count();

        ViewBag.Opportunities = _context.Opportunities.Count();

        ViewBag.Won = _context.Opportunities
            .Count(x => x.Status == "Won");

        ViewBag.Lost = _context.Opportunities
            .Count(x => x.Status == "Lost");

        ViewBag.PipelineValue = _context.Opportunities
            .Where(x => x.Status != "Lost")
            .AsEnumerable()
            .Sum(x => x.Amount);

        return View();
    }
}