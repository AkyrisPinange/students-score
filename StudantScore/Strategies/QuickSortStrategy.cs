using StudantScore.Models;

namespace StudantScore.Strategies
{
    public class QuickSortStrategy : ISortingStrategy
    {
        public IEnumerable<Aluno> Sort(IEnumerable<Aluno> alunos)
        {
            var alunoList = alunos.ToList();
            QuickSort(alunoList, 0, alunoList.Count - 1);
            return alunoList;
        }

        private void QuickSort(List<Aluno> alunos, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(alunos, low, high);

                QuickSort(alunos, low, pi - 1);
                QuickSort(alunos, pi +1, high);
            }
        }

        private int Partition(List<Aluno> alunos, int low, int high)
        {
            double? pivot = alunos[high].Materias.Average(m => m.Nota);
            int i = (low - 1);

            for (int j = low; j <= high; j++) 
            {
                if (alunos[j].Materias.Average(m => m.Nota) < pivot)
                {
                    i++;
                    var temp = alunos[i];
                    alunos[i] = alunos[j];
                    alunos[j] = temp;
                }
            }

            var temp1 = alunos[i + 1];
            alunos[i + 1] = alunos[high];
            alunos[high] = temp1;

            return i + 1;
        }
    }
}
