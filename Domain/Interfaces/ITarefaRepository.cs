using Domain.Entities;
using System;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task<Tarefa> GetTarefaByIdAsync(int id);
        Task<IEnumerable<Tarefa>> GetTarefasByUserIdAsync(int userId);
        Task<IEnumerable<Tarefa>> GetAllTarefasAsync();
        Task AddTarefaAsync(Tarefa tarefa);
        Task UpdateTarefaAsync(Tarefa tarefa);
        Task DeleteTarefaAsync(int id);
    }
}
