using EMDomain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EMDomain
{

    public class Aluno : IEntidade
    {


        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string? Cpf { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDeNascimento { get; set; }
        public EnumeradorSexo Sexo { get; set; }

        public Aluno() { }
    }
}
