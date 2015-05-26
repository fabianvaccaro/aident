using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaObjetos
{
    [DataContract]
    [Serializable]
    public class FeatureImage
    {
        // Declaración de variables
        [DataMember]
        public Int32 alto { set; get; }
        [DataMember]
        public Int32 ancho { set; get; }
        [DataMember]
        public Image imagenOriginal { set; get; }
        [DataMember]
        public Image imagenSegmentada { set; get; }
        [DataMember]
        public Int32[,] etiquetas { set; get; }
        [DataMember]
        public Int32[,] marcas { set; get; }
        [DataMember]
        public Double[] vectorCaracteristicas { set; get; }

        public FeatureImage(Bitmap Org)
        {
            this.ancho = Org.Size.Width;
            this.alto = Org.Size.Height;
            this.imagenOriginal = Org;
            this.imagenSegmentada = imagenOriginal;

            this.etiquetas = new Int32[alto, ancho];
            this.marcas = etiquetas;
            this.vectorCaracteristicas = new Double[120];
        }

    }
}
