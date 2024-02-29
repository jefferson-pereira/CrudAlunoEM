using FirebirdSql.Data.FirebirdClient;

namespace CrudAlunoEMRepository
{
    public class BDAluno
    {
        readonly string ConexaoBD = @"User=SYSDBA;PassWord=masterkey;Database=C:\Users\jephb\Desktop\CrudAlunoEM\Bd\Projeto.FB4;DataSource=localhost;Port=3050;Dialect=3";

        private static readonly BDAluno instanciaBD = new();

        private BDAluno() { }

        public static BDAluno UseInstancia() { return instanciaBD; }

        public FbConnection UseConexao()
        {
            string Config = ConexaoBD;
            return new FbConnection(Config);
        }
    }
}
