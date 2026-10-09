using COMP2139_ICE.Data;
using COMP2139_ICE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_ICE.Controllers;

public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Projects.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description")] Project project)
    {
        if (!ModelState.IsValid) return View(project);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Name,Description")] Project model)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();
        if (!ModelState.IsValid) return View(project);

        project.Name = model.Name;
        project.Description = model.Description;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return NotFound();
        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
