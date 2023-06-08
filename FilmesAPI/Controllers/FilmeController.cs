using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{

    [ApiController]
    [Route("api")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [Route("addfilme")]
        [HttpPost]
        public IActionResult AddFilme([FromBody] AddFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.TbFilmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetFilmeById), new { id = filme.Id }, filme);
        }

        [Route("getfilmes")]
        [HttpGet]
        public IEnumerable<Filme> GetFilmes(int skip, int take)
        {
            return _context.TbFilmes.Skip(skip).Take(take);
        }

        [Route("getfilme")]
        [HttpGet]
        public IActionResult GetFilmeById(int id)
        {
            Filme? filme = _context.TbFilmes.FirstOrDefault(x => x.Id == id);

            if(filme == null) return NotFound();

            return Ok(filme);
        }

    }
}
