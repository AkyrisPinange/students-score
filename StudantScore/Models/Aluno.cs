using System.ComponentModel.DataAnnotations;

namespace StudantScore.Models
{
    public class Aluno
    {
        [Key]
        public int Matricula { get; set; }

        public  string Nome { get; set; }
       
        public  ICollection<Materia> Materias { get; set; }
    }
}
