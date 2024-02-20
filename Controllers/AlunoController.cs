using CrudAlunoEM.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrudAlunoEM.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoDAL interfaceAluno;
        public AlunoController(IAlunoDAL aluno)
        {
            interfaceAluno = aluno;
        }

        public IActionResult Index()
        {
            List<Aluno> Alunos = new List<Aluno>();
            Alunos = interfaceAluno.GetAllAlunos().ToList();

            return View(Alunos);
        }

        [HttpGet]
        public IActionResult Details(int matricula)
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                interfaceAluno.AdicioneAluno(aluno);
                return RedirectToAction("Index");
            }
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Edit(int matricula)
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
        public IActionResult Edit(int matricula, [Bind] Aluno aluno)
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
        public IActionResult Delete(int matricula)
        {
            if (matricula == null||matricula == 0)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int matricula)
        {
            interfaceAluno.ExcluaAluno(matricula);
            return RedirectToAction("Index");
        }

    }
}
