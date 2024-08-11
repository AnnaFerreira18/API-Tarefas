using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interface;

namespace Application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Tarefa> GetTarefaByIdAsync(int id)
        {
            try
            {
                return await _tarefaRepository.GetTarefaByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasByUserIdAsync(int userId)
        {
            try
            {
                return await _tarefaRepository.GetTarefasByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Tarefa>> GetAllTarefasAsync()
        {
            try
            {
                return await _tarefaRepository.GetAllTarefasAsync();
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

        public async Task AddTarefaAsync(Tarefa tarefa)
        {
            try
            {
                if (tarefa == null)
                {
                    throw new ArgumentNullException(nameof(tarefa));
                }

                await _tarefaRepository.AddTarefaAsync(tarefa);
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
                if (tarefa == null)
                {
                    throw new ArgumentNullException(nameof(tarefa));
                }

                var existingTarefa = await _tarefaRepository.GetTarefaByIdAsync(tarefa.Id);
                if (existingTarefa == null)
                {
                    throw new InvalidOperationException("Tarefa not found");
                }

                await _tarefaRepository.UpdateTarefaAsync(tarefa);
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
                var existingTarefa = await _tarefaRepository.GetTarefaByIdAsync(id);
                if (existingTarefa == null)
                {
                    throw new InvalidOperationException("Tarefa not found");
                }

                await _tarefaRepository.DeleteTarefaAsync(id);
            }
            catch (Exception ex)
            {
                throw; 
            }
        }
    }
}
