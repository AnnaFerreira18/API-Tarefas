using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly ApplicationDbContext _context;

        public TarefaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefa> GetTarefaByIdAsync(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasByUserIdAsync(int userId)
        {
            return await _context.Tarefas.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetAllTarefasAsync()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task AddTarefaAsync(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
                await _context.SaveChangesAsync();
            }
        }
    }
}
