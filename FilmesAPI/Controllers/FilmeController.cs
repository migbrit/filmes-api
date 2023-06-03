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
        public void AddFilme(Filme filme)
        {
            id++;
            filme.Id = id;
            filmes.Add(filme);
        }

        [Route("getfilmes")]
        [HttpGet]
        public List<Filme> GetFilmes(int skip, int take)
        {
            return filmes.Skip(skip).Take(take).ToList();
        }

        [Route("getfilme")]
        [HttpGet]
        public Filme? GetFilmeById(int id)
        {
            Filme? filme = filmes.FirstOrDefault(x => x.Id == id);
            return filme;
        }

    }
}
