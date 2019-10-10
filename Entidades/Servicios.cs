using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Servicios
    {
        [Key]
        public int EvaluacionID { get; set; }
        public int EstudianteID { get; set; }
        [ForeignKey("EstudianteID")]
        public virtual Estudiantes Estudiantes { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public virtual List<ServiciosDetalle> Detalles { get; set; }


        public Servicios()
        {
            EvaluacionID = 0;
            EstudianteID = 0;
            this.Total = 0;
            Fecha = DateTime.Now;
            Detalles = new List<ServiciosDetalle>();
        }

        public Servicios(int evaluacionID, int estudianteID, decimal total, DateTime fecha, List<ServiciosDetalle> detalles)
        {
            EvaluacionID = evaluacionID;
            EstudianteID = estudianteID;
            Total = total;
            Fecha = fecha;
            Detalles = detalles;
        }

        public void RemoverDetalle(int Index)
        {
            this.Detalles.RemoveAt(Index);
        }

    }
}