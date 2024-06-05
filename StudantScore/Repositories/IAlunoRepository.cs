using StudantScore.Models;

namespace StudantScore.Repositories
{
    public interface IAlunoRepository
    {
        IEnumerable<Aluno> GetAll();
    }
}
