using ClassTrack.Data;
using ClassTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassTrack.Controllers;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;

    // ASP.NET Core injects the DbContext registered in Program.cs.
    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Student - list all students.
    public async Task<IActionResult> Index()
    {
        var students = await _context.Students.ToListAsync();
        return View(students);
    }

    // GET: /Student/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: /Student/Create - show the empty form.
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Student/Create
    // [Bind] lists the only fields accepted from the form, so users cannot
    // set Id themselves (protection against "over-posting").
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FirstName,Surname")] Student student)
    {
        if (!ModelState.IsValid)
        {
            return View(student); // redisplay the form with validation messages
        }

        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: /Student/Edit/5 - show the form filled with the existing record.
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: /Student/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname")] Student student)
    {
        // The id in the URL must match the id in the form.
        if (id != student.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(student);
        }

        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Someone deleted the row while it was being edited.
            if (!await _context.Students.AnyAsync(s => s.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: /Student/Delete/5 - confirmation page.
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: /Student/Delete/5 - the confirmed deletion.
    // Same URL as the GET above, so ActionName("Delete") is needed; the C# method
    // must have a different name because both take (int id).
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
