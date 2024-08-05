using Domain.Interfaces;
using Domain.Entities;
using System;
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
            return await _tarefaRepository.GetTarefaByIdAsync(id);
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasByUserIdAsync(int userId)
        {
            return await _tarefaRepository.GetTarefasByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Tarefa>> GetAllTarefasAsync()
        {
            return await _tarefaRepository.GetAllTarefasAsync();
        }

        public async Task AddTarefaAsync(Tarefa tarefa)
        {
            await _tarefaRepository.AddTarefaAsync(tarefa);
        }

        public async Task UpdateTarefaAsync(Tarefa tarefa)
        {
            await _tarefaRepository.UpdateTarefaAsync(tarefa);
        }

        public async Task DeleteTarefaAsync(int id)
        {
            await _tarefaRepository.DeleteTarefaAsync(id);
        }
    }
}

