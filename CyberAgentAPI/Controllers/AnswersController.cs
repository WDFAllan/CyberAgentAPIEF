using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CyberAgentAPI.Models;
using CyberAgentAPI.Models.dtos;

namespace CyberAgentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {

        private readonly CyberAgentContext _context;
        public static Answers answer = new Answers();

        public AnswersController(CyberAgentContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answers>>> GetAnswers()
        {
            return await _context.Answers.Include(q=>q.User).ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answers>> GetAnswer(int id)
        {
            var answer = await _context.Answers.Include(q => q.User).SingleOrDefaultAsync(i => i.AnswerId == id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        [HttpGet("ByAnswer/{id}")]
        public async Task<ActionResult<IEnumerable<Answers>>> GetAnswersByAnswer(int id)
        {
            var history = _context.Answers.Include(q=>q.User)
                                        .Where(q => q.SurveyQuestionId == id)
                                        .ToList();


            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, Answers answer)
        {
            if (id != answer.AnswerId)
            {
                return BadRequest();
            }

            _context.Entry(answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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

        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Answers>> PostAnswer(DtoAnswer request)
        {

            answer.SurveyQuestionId = request.SurveyQuestionId;
            answer.Answer = request.Answer1;
            answer.Date = DateTime.Now;
            answer.UserId = request.UserId;
            answer.AnswerId = 0;

            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.AnswerId }, answer);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerId == id);
        }
    }
}
