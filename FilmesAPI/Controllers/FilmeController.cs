using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{

    [ApiController]
    [Route("api")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();
        private static int id = 0;

        [Route("addfilme")]
        [HttpPost]
        public IActionResult AddFilme([FromBody] Filme filme)
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(GetFilmeById), new { id = filme.Id }, filme);
        }

        [Route("getfilmes")]
        [HttpGet]
        public IEnumerable<Filme> GetFilmes(int skip, int take)
        {
            return filmes.Skip(skip).Take(take).ToList();
        }

        [Route("getfilme")]
        [HttpGet]
        public IActionResult GetFilmeById(int id)
        {
            Filme? filme = filmes.FirstOrDefault(x => x.Id == id);
            if(filme == null) return NotFound();
            return Ok(filme);
        }

    }
}
