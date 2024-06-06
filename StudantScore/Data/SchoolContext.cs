using Microsoft.EntityFrameworkCore;
using StudantScore.Models;

namespace StudantScore.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Materia> Materias { get; set; }
    }
}
