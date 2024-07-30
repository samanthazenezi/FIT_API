using Fit_API.Context;
using Fit_API.src.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fit_API.src.Controllers;

[ApiController]
[Route("Exercise")]
public class ExercisesController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ExercisesController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExercisesModel>>> GetExercises()
    {
        var exercises = await _context.Exercises.FindAsync();
        return Ok(exercises);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExercisesModel>> GetExercise(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);

        if (exercise == null)
        {
            return NotFound();
        }

        return Ok(exercise);
    }

    [HttpPost]
    public async Task<ActionResult> CreateExercise(ExercisesModel exercise)
    {
        _context.Exercises.Add(exercise);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetExercise", new { id = exercise.IdExercises }, exercise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutExercise(int id, ExercisesModel exercise)
    {
        if (exercise == null || id != exercise.IdExercises)
        {
            return BadRequest();
        }

        _context.Entry(exercise).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException)
        {
            if (!ExerciseExists(id))
            {
                return NotFound();
            } else
            {
                throw;
            }
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExercise(int id)
    {
        var exercise = await _context.Exercises.FindAsync(id);

        if (exercise == null)
        {
            return NotFound();
        }

        _context.Exercises.Remove(exercise);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private bool ExerciseExists(int id)
    {
        return _context.Users.Any(e => e.IdUser == id);
    }
}

