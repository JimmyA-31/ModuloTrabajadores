using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModuloTrabajadores.Models
{
    public class Trabajador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios")]
        [StringLength(100)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [StringLength(100)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        [StringLength(20)]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio")]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [StringLength(10)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }

        // Guardamos solo el nombre del archivo
        public string? Foto { get; set; }

        // NO se mapea a la BD
        [NotMapped]
        public IFormFile? FotoArchivo { get; set; }
    }
}
