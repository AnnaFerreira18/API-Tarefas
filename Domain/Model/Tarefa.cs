using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)] // Exemplo de limite de tamanho para o título
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public DateTime Data { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
