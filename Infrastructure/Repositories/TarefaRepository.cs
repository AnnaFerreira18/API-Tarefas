using Domain.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.Tarefas
                                 .Include(t => t.User) 
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasByUserIdAsync(int userId)
        {
            return await _context.Tarefas
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetAllTarefasAsync()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task AddTarefaAsync(Tarefa tarefa)
        {
            try
            {
                await _context.Tarefas.AddAsync(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateTarefaAsync(Tarefa tarefa)
        {
            try
            {
                var existingTarefa = await _context.Tarefas.FindAsync(tarefa.Id);
                if (existingTarefa == null)
                {
                    throw new InvalidOperationException("Tarefa not found");
                }

                _context.Entry(existingTarefa).CurrentValues.SetValues(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteTarefaAsync(int id)
        {
            try
            {
                var tarefa = await _context.Tarefas.FindAsync(id);
                if (tarefa != null)
                {
                    _context.Tarefas.Remove(tarefa);
                    await _context.SaveChangesAsync();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw; 
            }
        }
    }
}
