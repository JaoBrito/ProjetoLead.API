using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Data;
using ProjetoLead.API.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProjetoLead.API.Controllers
{
    [ApiController]
    [Route( "api/[controller]" )]
    public class LeadsController : ControllerBase
    {
        private readonly AppDbContext _context;
        //private readonly HttpClient _httpClient;

        public LeadsController( AppDbContext context )
        {
            _context = context;
            //_httpClient = new HttpClient();
        }

        //Pegar todos os Leads
        [HttpGet]
        public async Task<IActionResult> Get( )
        {
            return Ok( await _context.Leads.ToListAsync() );
        }

        //Pegar Lead por ID
        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetLeadById( int id )
        {
            var lead = await _context.Leads.FindAsync( id );

            if ( lead == null )
                return NotFound( "Lead não encontrado." );

            return Ok( lead );
        }

        //Criar um novo Lead sem a API do ViaCep
        [HttpPost]
        public async Task<IActionResult> CreateLead( [FromBody] LeadModel lead )
        {
            if ( lead == null )
                return BadRequest( "Os dados do Lead são obrigatórios." );

            // Validações
            if ( string.IsNullOrWhiteSpace( lead.Cnpj ) || !ValidarCnpj( lead.Cnpj ) )
                return BadRequest( "CNPJ inválido." );

            if ( string.IsNullOrWhiteSpace( lead.RazaoSocial ) )
                return BadRequest( "Razão Social é obrigatória." );

            if ( string.IsNullOrWhiteSpace( lead.Cep ) )
                return BadRequest( "CEP é obrigatório." );

            if ( string.IsNullOrWhiteSpace( lead.Endereco ) )
                return BadRequest( "Endereço é obrigatório." );

            if ( string.IsNullOrWhiteSpace( lead.Numero ) )
                return BadRequest( "Número do endereço é obrigatório." );

            if ( string.IsNullOrWhiteSpace( lead.Bairro ) )
                return BadRequest( "Bairro é obrigatório." );

            if ( string.IsNullOrWhiteSpace( lead.Cidade ) )
                return BadRequest( "Cidade é obrigatória." );

            if ( string.IsNullOrWhiteSpace( lead.Estado ) )
                return BadRequest( "Estado é obrigatório." );

            _context.Leads.Add( lead );
            await _context.SaveChangesAsync();

            return CreatedAtAction( nameof( GetLeadById ), new { id = lead.Id }, lead );
        }

        // Validação de CNPJ pelo módulo 11
        private bool ValidarCnpj( string cnpj )
        {
            cnpj = Regex.Replace( cnpj, "[^0-9]", "" );

            if ( cnpj.Length != 14 )
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring( 0, 12 );
            int soma = 0;

            for ( int i = 0; i < 12; i++ )
                soma += int.Parse( tempCnpj[ i ].ToString() ) * multiplicador1[ i ];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for ( int i = 0; i < 13; i++ )
                soma += int.Parse( tempCnpj[ i ].ToString() ) * multiplicador2[ i ];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith( digito1.ToString() + digito2.ToString() );
        }

        //Atualizar um Lead
        [HttpPut]
        public async Task<IActionResult> UpdateLead(int id, [FromBody] LeadModel leadAtualizado )
        {
            if ( leadAtualizado == null )
            {
                return BadRequest( "Os dados do lead são obrigatórios" );
            }
            var leadExistente = await _context.Leads.FindAsync( id );
            if ( leadExistente == null )
                return NotFound( "Lead não encontrado." );

            // Validações
            if ( string.IsNullOrWhiteSpace( leadAtualizado.Cnpj ) || !ValidarCnpj( leadAtualizado.Cnpj ) )
                return BadRequest( "CNPJ inválido." );

            if ( string.IsNullOrWhiteSpace( leadAtualizado.RazaoSocial ) )
                return BadRequest( "Razão Social é obrigatória." );

            if ( string.IsNullOrWhiteSpace( leadAtualizado.Cep ) )
                return BadRequest( "CEP é obrigatório." );

            if ( string.IsNullOrWhiteSpace( leadAtualizado.Endereco ) )
                return BadRequest( "Endereço é obrigatório." );

            if ( string.IsNullOrWhiteSpace( leadAtualizado.Numero ) )
                return BadRequest( "Número do endereço é obrigatório." );

            if ( string.IsNullOrWhiteSpace( leadAtualizado.Bairro ) )
                return BadRequest( "Bairro é obrigatório." );

            if ( string.IsNullOrWhiteSpace( leadAtualizado.Cidade ) )
                return BadRequest( "Cidade é obrigatória." );

            if ( string.IsNullOrWhiteSpace( leadAtualizado.Estado ) )
                return BadRequest( "Estado é obrigatório." );

            // Atualiza os dados
            leadExistente.Cnpj = leadAtualizado.Cnpj;
            leadExistente.RazaoSocial = leadAtualizado.RazaoSocial;
            leadExistente.Cep = leadAtualizado.Cep;
            leadExistente.Endereco = leadAtualizado.Endereco;
            leadExistente.Numero = leadAtualizado.Numero;
            leadExistente.Complemento = leadAtualizado.Complemento;
            leadExistente.Bairro = leadAtualizado.Bairro;
            leadExistente.Cidade = leadAtualizado.Cidade;
            leadExistente.Estado = leadAtualizado.Estado;

            await _context.SaveChangesAsync();

            return Ok( leadExistente );
        }

        //Deletar um lead pelo id
        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteLead( int id )
        {
            var lead = await _context.Leads.FindAsync( id );
            if ( lead == null )
                return NotFound( "Lead não encontrado." );

            _context.Leads.Remove( lead );
            await _context.SaveChangesAsync();

            return Ok( "Lead deletado com sucesso." );
        }

        ////Criar um novo Lead
        //[HttpPost]
        //public async Task<IActionResult> CreateLead( [FromBody] LeadModel lead )
        //{
        //    // ✅ Validação do CNPJ
        //    if ( !IsValidCnpj( lead.Cnpj ) )
        //    {
        //        return BadRequest( "CNPJ inválido." );
        //    }

        //    // ✅ Consulta ao ViaCEP
        //    var endereco = await GetEnderecoViaCep( lead.Cep );
        //    if ( endereco == null || string.IsNullOrEmpty( endereco.Logradouro ) )
        //    {
        //        return BadRequest( "CEP inválido ou não encontrado no ViaCEP." );
        //    }

        //    // ✅ Preenchendo os campos de endereço
        //    lead.Endereco = endereco.Logradouro;
        //    lead.Bairro = endereco.Bairro;
        //    lead.Cidade = endereco.Localidade;
        //    lead.Estado = endereco.Uf;

        //    // 🚨 Verificação antes de salvar
        //    if ( string.IsNullOrEmpty( lead.Endereco ) || string.IsNullOrEmpty( lead.Bairro ) ||
        //        string.IsNullOrEmpty( lead.Cidade ) || string.IsNullOrEmpty( lead.Estado ) )
        //    {
        //        return BadRequest( "Não foi possível obter todas as informações do endereço." );
        //    }

        //    // ✅ Salvando no banco
        //    _context.Leads.Add( lead );
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction( nameof( CreateLead ), new { id = lead.Id }, lead );
        //}

        //// 🔹 Validação do CNPJ - Módulo 11
        //private bool IsValidCnpj( string cnpj )
        //{
        //    cnpj = Regex.Replace( cnpj, "[^0-9]", "" ); // Remove caracteres não numéricos
        //    if ( cnpj.Length != 14 )
        //        return false;

        //    int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        //    int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        //    string tempCnpj = cnpj.Substring( 0, 12 );
        //    int soma = 0;

        //    for ( int i = 0; i < 12; i++ )
        //        soma += int.Parse( tempCnpj[ i ].ToString() ) * multiplicador1[ i ];

        //    int resto = (soma % 11);
        //    int primeiroDigito = resto < 2 ? 0 : 11 - resto;

        //    tempCnpj += primeiroDigito;
        //    soma = 0;

        //    for ( int i = 0; i < 13; i++ )
        //        soma += int.Parse( tempCnpj[ i ].ToString() ) * multiplicador2[ i ];

        //    resto = (soma % 11);
        //    int segundoDigito = resto < 2 ? 0 : 11 - resto;

        //    return cnpj.EndsWith( primeiroDigito.ToString() + segundoDigito.ToString() );
        //}

        //// 🔹 Consulta ao ViaCEP
        //private async Task<EnderecoModel?> GetEnderecoViaCep( string cep )
        //{
        //    using var client = new HttpClient();
        //    var response = await client.GetAsync( $"https://viacep.com.br/ws/{cep}/json/" );

        //    if ( !response.IsSuccessStatusCode )
        //    {
        //        return null;
        //    }

        //    var json = await response.Content.ReadAsStringAsync();
        //    var endereco = JsonSerializer.Deserialize<EnderecoModel>( json );

        //    // Se a API retornar "erro": true, significa que o CEP não existe
        //    if ( endereco == null || endereco.Erro )
        //    {
        //        return null;
        //    }

        //    return endereco;
        //}

        //// Modelo para mapear o retorno do ViaCEP
        //public class EnderecoModel
        //{
        //    public string? Logradouro { get; set; }
        //    public string? Bairro { get; set; }
        //    public string? Localidade { get; set; } // Cidade
        //    public string? Uf { get; set; } // Estado
        //    public bool Erro { get; set; } // Indica se o CEP é inválido
        //}
    }
}