using CrudAlunoEMDomain;
using CrudAlunoEMDomain.Enums;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace CrudAlunoEMRepository
{
    public class MapeadorAluno : IAluno<Aluno>
    {
        public void AdicioneAluno(Aluno aluno)
        {
            using FbConnection conexao = BDAluno.UseInstancia().UseConexao();
            {
                string sql = ("UPDATE TBALUNO (NOME, SEXO, NASCIMENTO, CPF)" +
                                       "VALUES (@NOME, @SEXO, @NASCIMENTO, @CPF)");

                FbCommand comando = new(sql, conexao) {CommandType = CommandType.Text};

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

        public void AtualizaAluno(Aluno aluno)
        {
            using FbConnection conexao = BDAluno.UseInstancia().UseConexao();
            {
                string sql = ("INSERT INTO TBALUNO (MATRICULA, NOME, SEXO, NASCIMENTO, CPF)" +
                                       "VALUES (@MATRICULA, @NOME, @SEXO, @NASCIMENTO, @CPF)");

                FbCommand comando = new(sql, conexao) { CommandType = CommandType.Text };


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

        public Aluno ConsulteAluno(int matricula)
        {
            Aluno aluno = new();

            using FbConnection conexao = BDAluno.UseInstancia().UseConexao();
            {
                string sql = $"SELECT MATRICULA, NOME, SEXO, NASCIMENTO, CPF FROM TBALUNO WHERE MATRICULA = '{matricula}'";
                
                FbCommand comando = new(sql, conexao) { CommandType = CommandType.Text };

                conexao.Open();
                FbDataReader leia = comando.ExecuteReader();

                while(leia.Read())
                {
                    aluno.Matricula = Convert.ToInt32(leia["MATRICULA"]);
                    aluno.Nome = leia["NOME"].ToString();
                    aluno.Sexo = (EnumeradorSexo)Convert.ToInt32(leia["SEXO"]);
                    aluno.DataDeNascimento = Convert.ToDateTime(leia["NASCIMENTO"]);
                    aluno.Cpf = leia["CPF"].ToString();
                }
                conexao.Close();

                return aluno;
            }
        }

        public void ExcluaAluno(int matricula)
        {
            using FbConnection conexao = BDAluno.UseInstancia().UseConexao();
            {
                string sql = $"DELETE FROM TBALUNO WHERE MATRICULA = '{matricula}'";
                
                FbCommand comando = new(sql, conexao) { CommandType = CommandType.Text };

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Aluno> ObtenhaTodosOsALunos()
        {

            List<Aluno> alunos = [];

            using FbConnection conexao = BDAluno.UseInstancia().UseConexao();
            {
                string sql = $"SELECT MATRICULA, NOME, SEXO, NASCIMENTO, CPF FROM TBALUNO";

                FbCommand comando = new(sql, conexao) { CommandType = CommandType.Text };

                conexao.Open();
                FbDataReader leia = comando.ExecuteReader();

                while (leia.Read())
                {
                    Aluno aluno = new()
                    {
                        Matricula = Convert.ToInt32(leia["MATRICULA"]),
                        Nome = leia["NOME"].ToString(),
                        Sexo = (EnumeradorSexo)Convert.ToInt32(leia["SEXO"]),
                        DataDeNascimento = Convert.ToDateTime(leia["NASCIMENTO"]),
                        Cpf = leia["CPF"].ToString()
                    };
                    alunos.Add(aluno);
                }
                conexao.Close();

                return alunos;
            }
        }
        
    }
}
