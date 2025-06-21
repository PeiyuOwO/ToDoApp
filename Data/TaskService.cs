using TodoApp.Models;
using System.Text.Json;

namespace TodoApp.Data;

public class TaskService
{
    private readonly string _filePath = "Data/tasks.json";

    public List<TodoTask> GetAll() =>
        File.Exists(_filePath)
        ? JsonSerializer.Deserialize<List<TodoTask>>(File.ReadAllText(_filePath)) ?? new()
        : new();

    public void SaveAll(List<TodoTask> tasks) =>
        File.WriteAllText(_filePath, JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
}
