﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BairroConnectAPI.Data;
using BairroConnectAPI.Models;

namespace BairroConnectAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriaController(DataContext context)
        {
            _context = context;
        }

        #region GetMethods

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Categoria> categorias = await _context.TB_CATEGORIA.ToListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar todas as categorias: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                Categoria categoria = await _context.TB_CATEGORIA.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound($"Categoria com id {id} não encontrada.");
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar a categoria com id {id}: {ex.Message}");
            }
        }

        #endregion

        #region PostMethods

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Categoria newCategoria)
        {
            try
            {
                _context.TB_CATEGORIA.Add(newCategoria);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = newCategoria.idCategoria }, newCategoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao adicionar a categoria: {ex.Message}");
            }
        }

        #endregion

        #region PutMethod

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria updatedCategoria)
        {
            try
            {
                if (id != updatedCategoria.idCategoria)
                {
                    return BadRequest("Id da categoria não corresponde ao id fornecido na requisição.");
                }

                _context.Entry(updatedCategoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao atualizar a categoria: {ex.Message}");
            }
        }

        #endregion

        #region DeleteMethod

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Categoria categoria = await _context.TB_CATEGORIA.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound($"Categoria com id {id} não encontrada.");
                }
             

                _context.TB_CATEGORIA.Remove(categoria);
                await _context.SaveChangesAsync();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao excluir a categoria: {ex.Message}");
            }
        }

        #endregion
    }
}
