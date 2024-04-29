using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BairroConnectAPI.Data;
using BairroConnectAPI.Models;

namespace BairroConnectAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class OrgEventosController : ControllerBase
    {
        private readonly DataContext _context;

        public OrgEventosController(DataContext context)
        {
            _context = context;
        }

        #region ComandosGET 

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<OrgEventos> orgEventosList = await _context.TB_ORGEVENTOS.ToListAsync();
                return Ok(orgEventosList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar todos os organizadores de eventos: {ex.Message}");
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                OrgEventos orgEvento = await _context.TB_ORGEVENTOS.FindAsync(id);
                if (orgEvento == null)
                {
                    return NotFound($"Organizador de eventos com id {id} não encontrado.");
                }
                return Ok(orgEvento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar o organizador de eventos com id {id}: {ex.Message}");
            }
        }
        #endregion

        #region ComandosPOST 

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] OrgEventos newOrgEventos)
        {
            try
            {
                _context.TB_ORGEVENTOS.Add(newOrgEventos);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = newOrgEventos.idOrganizador }, newOrgEventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao adicionar o organizador de eventos: {ex.Message}");
            }
        }
        #endregion

        #region ComandosPUT 
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] OrgEventos updatedOrgEventos)
        {
            try
            {
                if (id != updatedOrgEventos.idOrganizador)
                {
                    return BadRequest("Id do organizador de eventos não corresponde ao id fornecido na requisição.");
                }

                _context.Entry(updatedOrgEventos).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar o organizador de eventos: {ex.Message}");
            }
        }
        #endregion

        #region ComandosDELETE 

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                OrgEventos orgEvento = await _context.TB_ORGEVENTOS.FindAsync(id);
                if (orgEvento == null)
                {
                    return NotFound($"Organizador de eventos com id {id} não encontrado.");
                }

                _context.TB_ORGEVENTOS.Remove(orgEvento);
                await _context.SaveChangesAsync();

                return Ok(orgEvento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao excluir o organizador de eventos: {ex.Message}");
            }
        }
        #endregion
    }
}