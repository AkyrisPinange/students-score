using StudantScore.Models;

namespace StudantScore.Strategies
{
    public class BubbleSortStrategy : ISortingStrategy
    {
        public IEnumerable<Aluno> Sort(IEnumerable<Aluno> alunos)
        {
            var alunoList = alunos.ToList();
            bool trocou;
            do
            {
                trocou = false;
                for (int j = 0; j < alunoList.Count - 1; j++)
                {
                    if (alunoList[j].Materias.Average(m => m.Nota) > alunoList[j + 1].Materias.Average(m => m.Nota))
                    {
                        var temp = alunoList[j];
                        alunoList[j] = alunoList[j + 1];
                        alunoList[j + 1] = temp;
                        trocou = true;
                    }
                }
            } while (trocou);
            return alunoList;
        }
    }
}

