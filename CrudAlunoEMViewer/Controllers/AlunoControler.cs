using EMDomain;
using Microsoft.AspNetCore.Mvc;

namespace EMWeb.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAluno<Aluno> interfaceAluno;
        public AlunoController(IAluno<Aluno> aluno)
        {
            interfaceAluno = aluno;
        }

        public IActionResult Index()
        {
            List<Aluno> Alunos = new List<Aluno>();
            Alunos = interfaceAluno.ObtenhaTodosOsALunos().ToList();

            return View(Alunos);
        }

        [HttpGet]
        public IActionResult FichaDoAluno(int matricula)
        {
            if (matricula == null)
            {
                return NotFound("Matricula Não Encontrada");
            }

            Aluno aluno = interfaceAluno.ConsulteAluno(matricula);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);

        }

        [HttpGet]
        public IActionResult NovoAluno()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NovoAluno([Bind] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                interfaceAluno.AdicioneAluno(aluno);
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Editar(int matricula)
        {
            if (matricula == null)
            {
                return NotFound();
            }

            Aluno aluno = interfaceAluno.ConsulteAluno(matricula);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int matricula, [Bind] Aluno aluno)
        {
            if (matricula != aluno.Matricula)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                interfaceAluno.AtualizaAluno(aluno);
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Excluir(int matricula)
        {
            if (matricula == null)
            {
                return NotFound();
            }
            Aluno aluno = interfaceAluno.ConsulteAluno(matricula);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExclusaoConfirmada(int matricula)
        {
            interfaceAluno.ExcluaAluno(matricula);
            return RedirectToAction("Index");
        }

    }
}
