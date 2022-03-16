using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CyberAgentAPI.Models;

namespace CyberAgentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyQuestionCategoriesController : ControllerBase
    {
        private readonly CyberAgentContext _context;

        public SurveyQuestionCategoriesController(CyberAgentContext context)
        {
            _context = context;
        }

        // GET: api/SurveyQuestionCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyQuestionCategory>>> GetSurveyQuestionCategories()
        {
            return await _context.SurveyQuestionCategories.ToListAsync();
        }

        // GET: api/SurveyQuestionCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyQuestionCategory>> GetSurveyQuestionCategory(int id)
        {
            var surveyQuestionCategory = await _context.SurveyQuestionCategories.FindAsync(id);

            if (surveyQuestionCategory == null)
            {
                return NotFound();
            }

            return surveyQuestionCategory;
        }

        // PUT: api/SurveyQuestionCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyQuestionCategory(int id, SurveyQuestionCategory surveyQuestionCategory)
        {
            if (id != surveyQuestionCategory.SurveyQuestionCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(surveyQuestionCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyQuestionCategoryExists(id))
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

        // POST: api/SurveyQuestionCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SurveyQuestionCategory>> PostSurveyQuestionCategory(SurveyQuestionCategory surveyQuestionCategory)
        {
            _context.SurveyQuestionCategories.Add(surveyQuestionCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveyQuestionCategory", new { id = surveyQuestionCategory.SurveyQuestionCategoryId }, surveyQuestionCategory);
        }

        // DELETE: api/SurveyQuestionCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyQuestionCategory(int id)
        {
            var surveyQuestionCategory = await _context.SurveyQuestionCategories.FindAsync(id);
            if (surveyQuestionCategory == null)
            {
                return NotFound();
            }

            _context.SurveyQuestionCategories.Remove(surveyQuestionCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurveyQuestionCategoryExists(int id)
        {
            return _context.SurveyQuestionCategories.Any(e => e.SurveyQuestionCategoryId == id);
        }
    }
}
