using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudantScore.Services;
using StudantScore.Strategies;

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

        [HttpGet("alunos")]
        [AllowAnonymous]
        
        public IActionResult GetTodosAlunos()
        {
            return Ok(_alunoService.GetTodosAlunos());
        }

        [HttpGet("aprovados")]
        [AllowAnonymous]
        public IActionResult GetAlunosAprovados()
        {
            return Ok(_alunoService.GetAlunosAprovados());
        }

        [HttpGet("reprovados")]
        [AllowAnonymous]
        public IActionResult GetAlunosReprovados()
        {
            return Ok(_alunoService.GetAlunosReprovados());
        }

        [HttpGet("melhor-aluno/{materia}")]
        [AllowAnonymous]
        public IActionResult GetMelhorAlunoPorMateria(string materia)
        {
            return Ok(_alunoService.GetMelhorAlunoPorMateria(materia));
        }

        [HttpGet("{matricula}")]
        [AllowAnonymous]
        public IActionResult GetAlunoPorMatricula(int matricula)
        {
            return Ok(_alunoService.GetAlunoPorMatricula(matricula));
        }

        [HttpGet("ordenar/bubble")]
        [AllowAnonymous]
        public IActionResult OrdenarAlunosPorMediaBubbleSort()
        {
            return Ok(_alunoService.OrdenarAlunosPorMedia(_bubbleSortStrategy));
        }

        [HttpGet("ordenar/quick")]
        [AllowAnonymous]
        public IActionResult OrdenarAlunosPorMediaQuickSort()
        {
            return Ok(_alunoService.OrdenarAlunosPorMedia(_quickSortStrategy));
        }
    }
}
