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
    public class SurveyQuestionsController : ControllerBase
    {
        private readonly CyberAgentContext _context;

        public SurveyQuestionsController(CyberAgentContext context)
        {
            _context = context;
        }

        // GET: api/SurveyQuestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyQuestion>>> GetSurveyQuestions()
        {
            return await _context.SurveyQuestions.ToListAsync();
        }

        // GET: api/SurveyQuestions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyQuestion>> GetSurveyQuestion(int id)
        {
            var surveyQuestion = await _context.SurveyQuestions.FindAsync(id);

            if (surveyQuestion == null)
            {
                return NotFound();
            }

            return surveyQuestion;
        }

        [HttpGet("BySurvey/{id}")]
        public async Task<ActionResult<IEnumerable<SurveyQuestion>>> GetQuestionBySurvey(int id)
        {

            var surveyQuestion = _context.SurveyQuestions
                                                .Include(q => q.Question)
                                                .Include(q => q.Answers)
                                                .Where(q => q.SurveyId == id).ToList();
                                                

            if (surveyQuestion == null)
            {
                return NotFound();
            }

            return surveyQuestion;
        }

        

        // PUT: api/SurveyQuestions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyQuestion(int id, SurveyQuestion surveyQuestion)
        {
            if (id != surveyQuestion.SurveyQuestionId)
            {
                return BadRequest();
            }

            _context.Entry(surveyQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyQuestionExists(id))
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

        // POST: api/SurveyQuestions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SurveyQuestion>> PostSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            _context.SurveyQuestions.Add(surveyQuestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveyQuestion", new { id = surveyQuestion.SurveyQuestionId }, surveyQuestion);
        }

        // DELETE: api/SurveyQuestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyQuestion(int id)
        {
            var surveyQuestion = await _context.SurveyQuestions.FindAsync(id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }

            _context.SurveyQuestions.Remove(surveyQuestion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurveyQuestionExists(int id)
        {
            return _context.SurveyQuestions.Any(e => e.SurveyQuestionId == id);
        }
    }
}
