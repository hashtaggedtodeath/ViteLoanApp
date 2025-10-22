
using LoanAPI.Data;
using LoanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LoanApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanApplication>>> GetAll(
            [FromQuery] LoanStatus? status,
            [FromQuery] decimal? minAmount,
            [FromQuery] decimal? maxAmount,
            [FromQuery] int? minTerm,
            [FromQuery] int? maxTerm)
        {
            var query = _context.LoanApplications.AsQueryable();

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (minAmount.HasValue)
                query = query.Where(x => x.Amount >= minAmount.Value);

            if (maxAmount.HasValue)
                query = query.Where(x => x.Amount <= maxAmount.Value);

            if (minTerm.HasValue)
                query = query.Where(x => x.TermValue >= minTerm.Value);

            if (maxTerm.HasValue)
                query = query.Where(x => x.TermValue <= maxTerm.Value);

            return await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        
        [HttpPost]
        public async Task<ActionResult<LoanApplication>> Create(LoanApplication loan)
        {
            if (loan.Amount <= 0)
                return BadRequest("Сумма должна быть больше 0");
            if (loan.TermValue <= 0)
                return BadRequest("Срок займа должен быть больше 0");
            if (loan.InterestValue <= 0)
                return BadRequest("Процентная ставка должна быть больше 0");

            loan.Status = LoanStatus.Published;
            loan.CreatedAt = DateTimeOffset.UtcNow;
            loan.ModifiedAt = DateTimeOffset.UtcNow;
            loan.Number = $"LN-{Guid.NewGuid().ToString()[..8]}";

            _context.LoanApplications.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = loan.Id }, loan);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LoanApplication updatedLoan)
        {
            var loan = await _context.LoanApplications.FindAsync(id);
            if (loan == null)
                return NotFound();

            if (updatedLoan.Amount <= 0 || updatedLoan.TermValue <= 0 || updatedLoan.InterestValue <= 0)
                return BadRequest("Поля должны быть больше 0");

            loan.Amount = updatedLoan.Amount;
            loan.TermValue = updatedLoan.TermValue;
            loan.InterestValue = updatedLoan.InterestValue;
            loan.ModifiedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(loan);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _context.LoanApplications.FindAsync(id);
            if (loan == null)
                return NotFound();

            _context.LoanApplications.Remove(loan);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        [HttpPatch("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var loan = await _context.LoanApplications.FindAsync(id);
            if (loan == null)
                return NotFound();

            loan.Status = loan.Status == LoanStatus.Published
                ? LoanStatus.Unpublished
                : LoanStatus.Published;

            loan.ModifiedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(loan);
        }
    }
}
