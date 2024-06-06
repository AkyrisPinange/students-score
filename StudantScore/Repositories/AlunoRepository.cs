using Microsoft.EntityFrameworkCore;
using StudantScore.Data;
using StudantScore.Models;

namespace StudantScore.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly SchoolContext _context;
        public AlunoRepository(SchoolContext context) 
        {
            _context = context;
        }

        public IEnumerable<Aluno> GetAll() 
        {
            return _context.Alunos.Include(a => a.Materias);
        }

        public Aluno? GetByMatricula(int matricula)
        {
            return _context.Alunos.FirstOrDefault(a => a.Matricula == matricula);
        }
    }
}
