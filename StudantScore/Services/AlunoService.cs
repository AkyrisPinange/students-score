using Microsoft.EntityFrameworkCore;
using StudantScore.Models;
using StudantScore.Repositories;
using StudantScore.Strategies;

namespace StudantScore.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoService(IAlunoRepository alunoRepository) 
        {
            _alunoRepository = alunoRepository;
        }

        public IEnumerable<Aluno> GetTodosAlunos()
        {
            return _alunoRepository.GetAll();
        } 
        
        public IEnumerable<Aluno> GetAlunosAprovados()
        {
            return _alunoRepository.GetAll()
                .Where(a => a.Materias.All(m => m.Nota >= 60));
        }

        public IEnumerable<Aluno> GetAlunosReprovados()
        {
            return _alunoRepository.GetAll()
                .Where(a =>  a.Materias.Any(a => a.Nota < 60));
        }

        public Aluno? GetMelhorAlunoPorMateria(string materia) 
        {
            return _alunoRepository.GetAll()
                .SelectMany(a => a.Materias.Where(m => string.Equals(m.Nome, materia, 
                StringComparison.OrdinalIgnoreCase))
                .Select(m => new { Aluno = a, m.Nota }))
                .OrderByDescending(a => a.Nota)
                .FirstOrDefault()?.Aluno;
        }

        public Aluno? GetAlunoPorMatricula(int matricula)
        {
            return _alunoRepository.GetAll()
                .Where(a => a.Matricula == matricula)
                .FirstOrDefault();
        }


        public IEnumerable<Aluno> OrdenarAlunosPorMedia(ISortingStrategy strategy)
        {
            var alunos = _alunoRepository.GetAll();
            return strategy.Sort(alunos);
        }
    }
}
