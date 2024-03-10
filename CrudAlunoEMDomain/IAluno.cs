namespace EMDomain
{
    public interface IAluno<T> where T : IEntidade
    {
        IEnumerable<Aluno> ObtenhaTodosOsALunos();
        void AdicioneAluno(Aluno aluno);
        void AtualizaAluno(Aluno aluno);
        Aluno ConsulteAluno(int matricula);
        void ExcluaAluno(int matricula);
    }
}
