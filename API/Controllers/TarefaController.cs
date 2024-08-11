using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            var tarefas = await _tarefaService.GetAllTarefasAsync();
            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return Ok(tarefa);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefasByUserId(int userId)
        {
            var tarefas = await _tarefaService.GetTarefasByUserIdAsync(userId);
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            await _tarefaService.AddTarefaAsync(tarefa);
            return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            await _tarefaService.UpdateTarefaAsync(tarefa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            await _tarefaService.DeleteTarefaAsync(id);
            return NoContent();
        }
    }
}
