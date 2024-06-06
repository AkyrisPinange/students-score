using CsvHelper.Configuration;
using CsvHelper;
using StudantScore.Models;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
                    List<Materia> materias = new List<Materia>();

                    var aluno = new Aluno
                    {
                        Matricula = Int32.Parse(record.matricula),
                        Nome = record.nome,
                        Materias = materias
                    };

                     materias = new List<Materia>
                    {
                        new Materia { Nome = "Matematica", Nota = int.Parse(record.matematica)},
                        new Materia { Nome = "Portugues", Nota = int.Parse(record.portugues)},
                        new Materia { Nome = "Biologia", Nota = int.Parse(record.biologia) },
                        new Materia { Nome = "Quimica", Nota = int.Parse(record.quimica)}
                    };

                    aluno.Materias = materias;
                    alunos.Add(aluno);
                }
            }

            return alunos;
        }
    }
}

