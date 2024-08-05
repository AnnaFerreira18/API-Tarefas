using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interface
{
    public interface ITarefaService
    {
        Task<Tarefa> GetTarefaByIdAsync(int id);
        Task<IEnumerable<Tarefa>> GetTarefasByUserIdAsync(int userId);
        Task<IEnumerable<Tarefa>> GetAllTarefasAsync();
        Task AddTarefaAsync(Tarefa tarefa);
        Task UpdateTarefaAsync(Tarefa tarefa);
        Task DeleteTarefaAsync(int id);
    }
}
