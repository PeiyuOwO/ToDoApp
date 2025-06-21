using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

public class TodoController : Controller
{
    private readonly TaskService _service;

    public TodoController(TaskService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var tasks = _service.GetAll().OrderBy(t => t.Date).ToList();
        return View(tasks);
    }

    
    public IActionResult Create(TodoTask task)
    {
        if (!ModelState.IsValid) return View(task);

        var tasks = _service.GetAll();
        task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
        tasks.Add(task);
        _service.SaveAll(tasks);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var task = _service.GetAll().FirstOrDefault(t => t.Id == id);
        return task == null ? NotFound() : View(task);
    }

    [HttpPost]
    public IActionResult Edit(TodoTask updated)
    {
        var tasks = _service.GetAll();
        var task = tasks.FirstOrDefault(t => t.Id == updated.Id);
        if (task == null) return NotFound();

        task.Name = updated.Name;
        task.Date = updated.Date;
        _service.SaveAll(tasks);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var tasks = _service.GetAll();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        tasks.Remove(task);
        _service.SaveAll(tasks);
        return RedirectToAction("Index");
    }

    public IActionResult ToggleDone(int id)
    {
        var tasks = _service.GetAll();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsDone = !task.IsDone;
            _service.SaveAll(tasks);
        }
        return RedirectToAction("Index");
    }
}
