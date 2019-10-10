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
    public class ServiciosDetalle
    {
        [Key]
        public int DetalleID { get; set; }
        public int EvaluacionID { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }

        public ServiciosDetalle()
        {
            this.DetalleID = 0;
            this.EvaluacionID = 0;
            this.Cantidad = 0;
            this.Precio = 0;
            this.Importe = 0;
        }

        public ServiciosDetalle(int detalleID, int evaluacionID, decimal cantidad, decimal precio, decimal importe)
        {
            DetalleID = detalleID;
            EvaluacionID = evaluacionID;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }
    }
}