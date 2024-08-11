using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)] // Limite para o nome de usuário
        public string Username { get; set; }

        [Required]
        [StringLength(100)] // Limite para a senha, ajustável conforme necessidade
        public string Senha { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>(); // Inicialização da coleção
    }
}
