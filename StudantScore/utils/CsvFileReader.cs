using CsvHelper.Configuration;
using CsvHelper;
using StudantScore.Models;
using System.Globalization;

namespace StudantScore.utils
{
    public class CsvFileReader
    {
        private readonly string _filePath;

        public CsvFileReader(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Aluno> ReadAlunosFromCsv()
        {
            var alunos = new List<Aluno>();

            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Ignore header validation
                MissingFieldFound = null // Ignore missing field validation
            }))
            {
                var records = csv.GetRecords<dynamic>().ToList();

                foreach (var record in records)
                {
                    var aluno = new Aluno
                    {
                        Matricula = Int32.Parse(record.matricula),
                        Nome = record.nome,
                    };

                    var materias = new List<Materia>
                    {
                        new Materia { Nome = "Matematica", Nota = int.Parse(record.matematica), Aluno = aluno },
                        new Materia { Nome = "Portugues", Nota = int.Parse(record.portugues), Aluno = aluno },
                        new Materia { Nome = "Biologia", Nota = int.Parse(record.biologia), Aluno = aluno },
                        new Materia { Nome = "Quimica", Nota = int.Parse(record.quimica), Aluno = aluno }
                    };

                    aluno.Materias = materias;
                    alunos.Add(aluno);
                }
            }

            return alunos;
        }
    }
}

