using System.ComponentModel.DataAnnotations;

namespace StudantScore.Models
{
    public class Aluno
    {
        [Key] 
        public int Matricula { get; set; }

        public required string Nome { get; set; }
       
        
        public required ICollection<Materia> Materias { get; set; }
    }
}
