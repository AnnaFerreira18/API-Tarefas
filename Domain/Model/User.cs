using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Senha { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>(); // Inicialização da coleção
    }
}
