using System.Collections.Generic;
using System.Linq;
using media_library_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace media_library_api.Controllers {
    [Route ("api/[controller]")]
    public class MovieController : Controller {
        private readonly MovieContext _context;

        public MovieController (MovieContext context) {
            _context = context;

            if (_context.MovieItems.Count () == 0) {
                _context.MovieItems.Add (new MovieItem { Name = "Item1" });
                _context.SaveChanges ();
            }
        }

        [HttpGet]
        public IEnumerable<MovieItem> GetAll () {
            return _context.MovieItems.ToList ();
        }

        [HttpGet ("{id}", Name = "GetMovie")]
        public IActionResult GetById (long id) {
            var item = _context.MovieItems.FirstOrDefault (t => t.Id == id);
            if (item == null) {
                return NotFound ();
            }
            return new ObjectResult (item);
        }

        [HttpPost]
        public IActionResult Create ([FromBody] MovieItem item) {
            if (item == null) {
                return BadRequest ();
            }

            _context.MovieItems.Add (item);
            _context.SaveChanges ();

            return CreatedAtRoute ("GetMovie", new { id = item.Id }, item);
        }

        [HttpPut ("{id}")]
        public IActionResult Update (long id, [FromBody] MovieItem item) {
            if (item == null || item.Id != id) {
                return BadRequest ();
            }

            var todo = _context.MovieItems.FirstOrDefault (t => t.Id == id);
            if (todo == null) {
                return NotFound ();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.MovieItems.Update (todo);
            _context.SaveChanges ();
            return new NoContentResult ();
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete (long id) {
            var todo = _context.MovieItems.FirstOrDefault (t => t.Id == id);
            if (todo == null) {
                return NotFound ();
            }

            _context.MovieItems.Remove (todo);
            _context.SaveChanges ();
            return new NoContentResult ();
        }
    }
}