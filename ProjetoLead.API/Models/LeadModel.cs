using System.ComponentModel.DataAnnotations;

namespace ProjetoLead.API.Models
{
    public class LeadModel
    {
        public int Id { get; set; }

        [Required]
        public string Cnpj { get; set; }

        [Required]
        public string RazaoSocial { get; set; }

        [Required]
        public string Cep { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public string Numero { get; set; }

        public string? Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }
    }
}
