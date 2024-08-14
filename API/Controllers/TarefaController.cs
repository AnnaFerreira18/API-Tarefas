using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                var tarefas = await _tarefaService.GetAllTarefasAsync();
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            try
            {
                var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
                if (tarefa == null)
                {
                    return NotFound();
                }
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefasByUserId(int userId)
        {
            try
            {
                var tarefas = await _tarefaService.GetTarefasByUserIdAsync(userId);
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest("Tarefa is null");
            }

            try
            {
                await _tarefaService.AddTarefaAsync(tarefa);
                return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, [FromBody] Tarefa tarefa)
        {
            if (tarefa == null || id != tarefa.Id)
            {
                return BadRequest("Tarefa is null or ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _tarefaService.UpdateTarefaAsync(tarefa);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception message for debugging
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            try
            {
                await _tarefaService.DeleteTarefaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
