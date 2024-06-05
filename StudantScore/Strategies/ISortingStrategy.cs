using StudantScore.Models;

namespace StudantScore.Strategies
{
    public interface ISortingStrategy
    {
        IEnumerable<Aluno> Sort(IEnumerable<Aluno> alunos);
    }
}
