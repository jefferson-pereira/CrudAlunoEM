namespace CrudAlunoEM.Models
{
    public interface IAlunoDAL
    {
        IEnumerable<Aluno> GetAllAlunos();
        void AdicioneAluno(Aluno aluno);
        void AtualizaAluno(Aluno aluno);
        Aluno ConsulteAluno(int matricula);
        void ExcluaAluno(int matricula);
    }
}
