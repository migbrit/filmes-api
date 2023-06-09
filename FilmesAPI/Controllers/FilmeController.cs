using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [Route("updatefilme")]
        [HttpPut]
        public IActionResult UpdateFilme(int id,[FromBody] UpdateFilmeDto filmeDto)
        {
            Filme? filme = _context.TbFilmes.Where(x => x.Id == id).FirstOrDefault();

            if(filme == null)
                return NotFound();

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();
        }

        [Route("updatefilmepartial")]
        [HttpPatch]
        public IActionResult UpdateFilmePartial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            Filme? filme = _context.TbFilmes.Where(x => x.Id == id).FirstOrDefault();

            if (filme == null)
                return NotFound();

            var filmeToUpdate = _mapper.Map<UpdateFilmeDto>(filme);

            patch.ApplyTo(filmeToUpdate, ModelState);

            if(!TryValidateModel(filmeToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeToUpdate, filme);
            _context.SaveChanges();
            return NoContent();
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
