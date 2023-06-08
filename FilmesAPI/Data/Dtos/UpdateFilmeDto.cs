using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto
    {
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        [Required]
        [StringLength(20)]
        public string Genero { get; set; }
        [Required]
        [Range(70, 600)]
        public int Duracao { get; set; }
    }
}
