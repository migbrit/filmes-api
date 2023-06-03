using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("Filme")]
    public class FilmeController : ControllerBase
    {
        List<Filme> filmes = new List<Filme>();

        int id = 0;

        [Route("Add")]
        [HttpPost]
        public void AddFilme(Filme filme)
        {
            id++;
            filme.Id = id;
            filmes.Add(filme);
            Console.WriteLine(filme.Titulo);
            Console.WriteLine(filme.Genero);
            Console.WriteLine(filme.Duracao);
        }

        [Route("Get")]
        [HttpGet("{id}")]
        public Filme GetFilmeById(int id)
        {
            return filmes.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
