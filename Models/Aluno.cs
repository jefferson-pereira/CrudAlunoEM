using CrudAlunoEM.Models.Enums;


namespace CrudAlunoEM.Models
{
    public class Aluno
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public EnumeradorSexo Sexo { get; set; }

        public Aluno() { }

        public Aluno(int matricula, string nome, DateTime dataDeNascimento, EnumeradorSexo sexo)
        {
            Matricula = matricula;
            Nome = nome;
            DataDeNascimento = dataDeNascimento;
            Sexo = sexo;

        }

        public Aluno(int matricula, string nome, string? cpf, DateTime dataDeNascimento, EnumeradorSexo sexo)
        {
            Matricula = matricula;
            Nome = nome;
            Cpf = CpfEhValido(cpf) ? cpf : null;
            DataDeNascimento = dataDeNascimento;
            Sexo = sexo;
        }

        bool CpfEhValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            if (new string(tempCpf[0], tempCpf.Length) == tempCpf) return false;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
