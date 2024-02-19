using CrudAlunoEM.Models.Enums;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace CrudAlunoEM.Models
{
    public class AlunoDAL : IAlunoDAL
    {
        string StringConexao = @"User=SYSDBA;PassWord=masterkey;Database=C:\Users\jephb\Desktop\CrudAlunoEM\Bd\PROJETO.FB4;DataSource=localhost;Port=3050;Dialect=3";

        IEnumerable<Aluno> IAlunoDAL.GetAllAlunos()
        {
            List<Aluno> alunos = new List<Aluno>();
            using (FbConnection conexao = new FbConnection(StringConexao))
            {
                FbCommand comando = new FbCommand("SELECT MATRICULA, NOME, SEXO, NASCIMENTO, CPF FROM TBALUNO", conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                FbDataReader leia = comando.ExecuteReader();

                while (leia.Read())
                {
                    Aluno aluno = new Aluno();
                    aluno.Matricula = Convert.ToInt32(leia["MATRICULA"]);
                    aluno.Nome = leia["NOME"].ToString();
                    aluno.Sexo = (EnumeradorSexo)Convert.ToInt32(leia["SEXO"]);
                    aluno.DataDeNascimento = Convert.ToDateTime(leia["NASCIMENTO"]);
                    aluno.Cpf = leia["CPF"].ToString();
                    alunos.Add(aluno);
                }
                conexao.Close();
            }
            return alunos;
        }

        void IAlunoDAL.AdicioneAluno(Aluno aluno)
        {
            using (FbConnection conexao = new FbConnection(StringConexao))
            {
                string comandoSQL = ("INSERT INTO TBALUNO (MATRICULA, NOME, SEXO, NASCIMENTO, CPF)" +
                                        "VALUES (@MATRICULA, @NOME, @SEXO, @NASCIMENTO, @CPF)");

                FbCommand comando = new FbCommand(comandoSQL, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@MATRICULA", aluno.Matricula);
                comando.Parameters.AddWithValue("@NOME", aluno.Nome);
                comando.Parameters.AddWithValue("@SEXO", aluno.Sexo);
                comando.Parameters.AddWithValue("@NASCIMENTO", aluno.DataDeNascimento);
                comando.Parameters.AddWithValue("@CPF", aluno.Cpf);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        void IAlunoDAL.AtualizaAluno(Aluno aluno)
        {
            using (FbConnection conexao = new FbConnection(StringConexao))
            {
                string comandoSQL = ("UPDATE TBALUNO SET NOME = @NOME, SEXO = @SEXO, " +
                                        "NASCIMENTO = @NASCIMENTO, CPF = @CPF WHERE MATRICULA = @MATRICULA");

                FbCommand comando = new FbCommand(comandoSQL, conexao);
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@MATRICULA", aluno.Matricula);
                comando.Parameters.AddWithValue("@NOME", aluno.Nome);
                comando.Parameters.AddWithValue("@SEXO", aluno.Sexo);
                comando.Parameters.AddWithValue("@NASCIMENTO", aluno.DataDeNascimento);
                comando.Parameters.AddWithValue("@CPF", aluno.Cpf);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        Aluno IAlunoDAL.ConsulteAluno(int matricula)
        {
            Aluno aluno = new Aluno();
            using (FbConnection conexao = new FbConnection(StringConexao))
            {
                string alunoConsultaSQL = ("SELECT * FROM TBALUNO WHERE MATRICULA = " + matricula);
                FbCommand comando = new FbCommand(alunoConsultaSQL, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                FbDataReader leia = comando.ExecuteReader();

                while (leia.Read())
                {
                    aluno.Matricula = Convert.ToInt32(leia["MATRICULA"]);
                    aluno.Nome = leia["NOME"].ToString();
                    aluno.Sexo = (EnumeradorSexo)Convert.ToInt32(leia["SEXO"]);
                    aluno.DataDeNascimento = Convert.ToDateTime(leia["NASCIMENTO"]);
                    aluno.Cpf = leia["CPF"].ToString();

                }
                conexao.Close();

            }

            return (aluno);
        }

        public void ExcluaAluno(int matricula)
        {
            using (FbConnection conexao = new FbConnection(StringConexao))
            {
                string comandoSQL = ("DELETE FROM TBALUNO WHERE MATRICULA = " + matricula);
                FbCommand comando = new FbCommand(comandoSQL, conexao);
                comando.CommandType = CommandType.Text;

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
