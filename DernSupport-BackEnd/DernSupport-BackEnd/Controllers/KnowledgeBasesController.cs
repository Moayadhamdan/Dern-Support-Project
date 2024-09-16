using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DernSupport_BackEnd.Data;
using DernSupport_BackEnd.Models;

namespace DernSupport_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnowledgeBasesController : ControllerBase
    {
        private readonly DernSupportDbContext _context;

        public KnowledgeBasesController(DernSupportDbContext context)
        {
            _context = context;
        }

        // GET: api/KnowledgeBases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KnowledgeBase>>> GetKnowledgeBases()
        {
          if (_context.KnowledgeBases == null)
          {
              return NotFound();
          }
            return await _context.KnowledgeBases.ToListAsync();
        }

        // GET: api/KnowledgeBases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KnowledgeBase>> GetKnowledgeBase(int id)
        {
          if (_context.KnowledgeBases == null)
          {
              return NotFound();
          }
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(id);

            if (knowledgeBase == null)
            {
                return NotFound();
            }

            return knowledgeBase;
        }

        // PUT: api/KnowledgeBases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKnowledgeBase(int id, KnowledgeBase knowledgeBase)
        {
            if (id != knowledgeBase.KnowledgeBaseId)
            {
                return BadRequest();
            }

            _context.Entry(knowledgeBase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KnowledgeBaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KnowledgeBases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KnowledgeBase>> PostKnowledgeBase(KnowledgeBase knowledgeBase)
        {
          if (_context.KnowledgeBases == null)
          {
              return Problem("Entity set 'DernSupportDbContext.KnowledgeBases'  is null.");
          }
            _context.KnowledgeBases.Add(knowledgeBase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKnowledgeBase", new { id = knowledgeBase.KnowledgeBaseId }, knowledgeBase);
        }

        // DELETE: api/KnowledgeBases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKnowledgeBase(int id)
        {
            if (_context.KnowledgeBases == null)
            {
                return NotFound();
            }
            var knowledgeBase = await _context.KnowledgeBases.FindAsync(id);
            if (knowledgeBase == null)
            {
                return NotFound();
            }

            _context.KnowledgeBases.Remove(knowledgeBase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KnowledgeBaseExists(int id)
        {
            return (_context.KnowledgeBases?.Any(e => e.KnowledgeBaseId == id)).GetValueOrDefault();
        }
    }
}
