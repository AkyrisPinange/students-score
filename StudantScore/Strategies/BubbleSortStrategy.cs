using StudantScore.Models;

namespace StudantScore.Strategies
{
    public class BubbleSortStrategy : ISortingStrategy
    {
        public IEnumerable<Aluno> Sort(IEnumerable<Aluno> alunos)
        {
            var alunoLista = alunos.ToList();
            bool trocou;
            do
            {
                trocou = false;
                for (int j = 0; j < alunoLista.Count - 1; j++)
                {
                    if (alunoLista[j].Materias.Average(m => m.Nota) > alunoLista[j + 1].Materias.Average(m => m.Nota))
                    {
                        var temp = alunoLista[j];
                        alunoLista[j] = alunoLista[j + 1];
                        alunoLista[j + 1] = temp;
                        trocou = true;
                    }
                }
            } while (trocou);
            return alunoLista;
        }
    }
}

