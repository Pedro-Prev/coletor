using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HotSpotAPI.Models.Data;
using HotSpotAPI.Models.Models;
using HotSpotAPI.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotSpotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotSpotController : ControllerBase
    {
        private readonly ILogger<HotSpotController> _logger;
        private readonly HotSpotContext _hotspotContext;

        //CONSTRUCTOR

        public HotSpotController(HotSpotContext hotspotContext, ILogger<HotSpotController> logger)
        {
            _hotspotContext = hotspotContext;
            _logger = logger;
        }



        //MÉTODOS

        private bool CadastroExists(int id)
        {
            return _hotspotContext.Cadastros.Any(c => c.Id == id);
        }


        private static CadastroDTO CadastroToDTO(Cadastro cadastro) =>
            new CadastroDTO
            {
                Id = cadastro.Id,
                Nome = cadastro.Nome,
                Email = cadastro.Email,
                Telefone = cadastro.Telefone
            };



        // -------------------------------GET-------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadastroDTO>>> GetCadastros()
        {
            return await _hotspotContext.Cadastros
                .Select(cadastro => CadastroToDTO(cadastro))
                .ToListAsync();
        }


        // -------------------------------GET BY ID-------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<CadastroDTO>> GetCadastroById(int id)
        {
            var cadastro = await _hotspotContext.Cadastros.FindAsync(id);

            if (cadastro == null)
            {
                return NotFound();
            }

            return CadastroToDTO(cadastro);
        }


        // -------------------------------POST-------------------------------
        [HttpPost]
        public async Task<ActionResult<CadastroDTO>> PostCadastro(CadastroDTO cadastroDto)
        {

            var cadastro = new Cadastro
            {
                Nome = cadastroDto.Nome,
                Email = cadastroDto.Email,
                Telefone = cadastroDto.Telefone
            };

            _hotspotContext.Cadastros.Add(cadastro);
            await _hotspotContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCadastros), new { id = cadastro.Id }, CadastroToDTO(cadastro));
        }


        // -------------------------------PUT-------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCadastro(int id, CadastroDTO cadastroDto)
        {
            if (id != cadastroDto.Id)
            {
                return BadRequest();
            }

            var cadastro = await _hotspotContext.Cadastros.FindAsync(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            cadastro.Nome = cadastroDto.Nome;
            cadastro.Email = cadastroDto.Email;
            cadastro.Telefone = cadastroDto.Telefone;

            try
            {
                await _hotspotContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CadastroExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }


        // -------------------------------DELETE-------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadastro(int id)
        {
            var cadastro = await _hotspotContext.Cadastros.FindAsync(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            _hotspotContext.Cadastros.Remove(cadastro);
            await _hotspotContext.SaveChangesAsync();

            return NoContent();
        }
    }
}