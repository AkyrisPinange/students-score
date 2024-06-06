namespace StudantScore.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public  string? Nome { get; set; }
        public int? Nota { get; set; }
        public int AlunoId { get; set; }
    }
}
