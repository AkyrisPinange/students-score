using Microsoft.AspNetCore.Mvc;
using StudantScore.Services;
using StudantScore.Strategies;
using static StudantScore.Services.IAlunoService;

namespace StudantScore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : Controller
    {
        private readonly IAlunoService _alunoService;
        private readonly ISortingStrategy _bubbleSortStrategy;
        private readonly ISortingStrategy _quickSortStrategy;

        public AlunosController(IAlunoService alunoService, IEnumerable<ISortingStrategy> sortingStrategies)
        {
            _alunoService = alunoService;
            _bubbleSortStrategy = sortingStrategies.OfType<BubbleSortStrategy>().First();
            _quickSortStrategy = sortingStrategies.OfType<QuickSortStrategy>().First();
        }

        [HttpGet("aprovados")]
        public IActionResult GetAlunosAprovados()
        {
            return Ok(_alunoService.GetAlunosAprovados());
        }
        [HttpGet("reprovados")]
        public IActionResult GetAlunosReprovados()
        {
            return Ok(_alunoService.GetAlunosReprovados());
        }

        [HttpGet("melhor-aluno/{materia}")]
        public IActionResult GetMelhorAlunoPorMateria(string materia)
        {
            return Ok(_alunoService.GetMelhorAlunoPorMateria(materia));
        }

        [HttpGet("{matricula}")]
        public IActionResult GetAlunoPorMatricula(int matricula)
        {
            return Ok(_alunoService.GetAlunoPorMatricula(matricula));
        }

        [HttpGet("ordenar/bubble")]
        public IActionResult OrdenarAlunosPorMediaBubbleSort()
        {
            return Ok(_alunoService.OrdenarAlunosPorMedia(_bubbleSortStrategy));
        }

        [HttpGet("ordenar/quick")]
        public IActionResult OrdenarAlunosPorMediaQuickSort()
        {
            return Ok(_alunoService.OrdenarAlunosPorMedia(_quickSortStrategy));
        }
    }
}
