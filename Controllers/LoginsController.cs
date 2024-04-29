using Microsoft.AspNetCore.Mvc;
using BairroConnectAPI.Models;
using BairroConnectAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BairroConnectAPI.Utils;

namespace BairroConnectAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LoginsController : ControllerBase
    {
        private readonly List<Logins> _login;
        private readonly DataContext _context;

        public LoginsController(DataContext context)
        {
            _login = new List<Logins>();
            _context = context;
        }

        #region ComandosGET
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Logins> lista = await _context.TB_LOGINS.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar todos os logins: {ex.Message}");
            }

        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetSingle(int idPessoa)
        {

            try
            {
                Logins l = await _context.TB_LOGINS.FirstOrDefaultAsync(lbusca => lbusca.idPessoa == idPessoa);

                return Ok(l);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar o login com id {idPessoa}: {ex.Message}");
            }
        }

        [HttpGet("GetByEmail")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                // Procura o login com o email fornecido no banco de dados
                var login = await _context.TB_LOGINS.FirstOrDefaultAsync(l => l.email == email);
                if (login != null)
                {
                    // Se o login for encontrado, retorna um código de status 200 OK com o login
                    return Ok(login);
                }
                else
                {
                    // Se o login não for encontrado, retorna um código de status 404 Not Found
                    return NotFound($"Login com email '{email}' não encontrado.");
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna um código de status 500 Internal Server Error com uma mensagem de erro detalhada
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao buscar o login pelo email: {ex.Message}");
            }
        }
        #endregion

        #region ComandosPOST
        [AllowAnonymous]
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLogin([FromBody] Logins newLogin)
        {
            try
            {
                // Verifica se já existe um login com o mesmo email
                var existingLogin = await _context.TB_LOGINS.FirstOrDefaultAsync(l => l.email == newLogin.email);
                if (existingLogin != null)
                {
                    return Conflict($"Já existe um login registrado com o email '{newLogin.email}'.");
                }

                // Criptografa a senha
                Criptografia.CriarPasswordHash(newLogin.senha, out byte[] hash, out byte[] salt);
                newLogin.senha = string.Empty;
                newLogin.PasswordHash = hash;
                newLogin.PasswordSalt = salt;

                // Adiciona o novo login ao contexto do banco de dados
                _context.TB_LOGINS.Add(newLogin);
                await _context.SaveChangesAsync();

                // Retorna um código de status 201 Created com o novo login criado
                return CreatedAtAction(nameof(GetSingle), new { idPessoa = newLogin.idPessoa }, newLogin);
            }
            catch (Exception ex)
            {
                // Retorna um código de status 500 Internal Server Error em caso de erro
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao adicionar o login: {ex.Message}");
            }
        }
        #endregion

        #region ComandosPUT
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLogin(int idPessoa, [FromBody] Logins updatedLogin)
        {
            try
            {
                // Verifica se o login com o ID fornecido existe no banco de dados
                var existingLogin = await _context.TB_LOGINS.FindAsync(idPessoa);
                if (existingLogin == null)
                {
                    return NotFound($"Login com id {idPessoa} não encontrado.");
                }

                // Atualiza as propriedades do login existente com os novos valores fornecidos
                existingLogin.nome = updatedLogin.nome;
                existingLogin.sobrenome = updatedLogin.sobrenome;
                existingLogin.email = updatedLogin.email;
                existingLogin.dataNasc = updatedLogin.dataNasc;
                existingLogin.tipoConta = updatedLogin.tipoConta;

                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();

                // Retorna um código de status 200 OK com o login atualizado
                return Ok(existingLogin);
            }
            catch (Exception ex)
            {
                // Retorna um código de status 500 Internal Server Error em caso de erro
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao atualizar o login: {ex.Message}");
            }
        }

        [HttpPut("UpdateSenha")]
        public async Task<IActionResult> AlterarSenhaLogin(Logins credenciais)
        {
            try
            {
                Logins login = await _context.TB_LOGINS
                    .FirstOrDefaultAsync(x => x.nome.ToLower().Equals(credenciais.nome.ToLower()));

                if (login == null)
                    throw new System.Exception("Usuário não encontrado.");

                Criptografia.CriarPasswordHash(credenciais.senha, out byte[] hash, out byte[] salt);
                login.PasswordHash = hash;
                login.PasswordSalt = salt;

                _context.TB_LOGINS.Update(login);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region ComandosDELETE
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLogin(int idPessoa)
        {
            try
            {
                // Procura o login com o ID fornecido no banco de dados
                var loginToRemove = await _context.TB_LOGINS.FindAsync(idPessoa);
                if (loginToRemove == null)
                {
                    // Se o login não for encontrado, retorna um código de status 404 Not Found
                    return NotFound($"Login com id {idPessoa} não encontrado.");
                }

                // Remove o login do contexto do banco de dados
                _context.TB_LOGINS.Remove(loginToRemove);
                await _context.SaveChangesAsync();

                // Retorna um código de status 200 OK com o login removido
                return Ok(loginToRemove);
            }
            catch (Exception ex)
            {
                // Em caso de erro, retorna um código de status 500 Internal Server Error com uma mensagem de erro detalhada
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao excluir o login: {ex.Message}");
            }
        }
        #endregion
    }
}