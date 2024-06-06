using StudantScore.Models;
using StudantScore.Strategies;

namespace StudantScore.Services
{
    public interface IAlunoService
    {       
             
            IEnumerable<Aluno> GetTodosAlunos();
            IEnumerable<Aluno> GetAlunosAprovados();
            IEnumerable<Aluno> GetAlunosReprovados();
            Aluno? GetMelhorAlunoPorMateria(string materia);
            Aluno? GetAlunoPorMatricula(int matricula);
            IEnumerable<Aluno> OrdenarAlunosPorMedia(ISortingStrategy sortingStrategy);
       
    }
}
