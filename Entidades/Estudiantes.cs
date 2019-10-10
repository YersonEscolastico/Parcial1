using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Estudiantes
    {
        [Key]
        public int EstudianteId { get; set; }
        public string Nombres { get; set; }
        public DateTime Fecha { get; set; }

        public Estudiantes()
        {
            EstudianteId = 0;
            Nombres = string.Empty;
            Fecha = DateTime.Now;
        }
    }
}