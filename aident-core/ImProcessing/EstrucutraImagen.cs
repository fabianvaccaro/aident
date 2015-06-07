using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Accord.Imaging;
using Accord.Math;
using AForge;

namespace ImProcessing
{
    public class EstrucutraImagen
    {
        public Int32 ancho { get; set; }
        public Int32 alto { get; set; }
        public Bitmap Original { get; set; }
        public Bitmap FIMG { get; set; } //Imagen segmentada
        public Int32[,] Labels { get; set; }
        public Int32[,] Marcas { get; set; }
        public Double[] Features {get;set;}
        public Double[][][] pixelesAOI { get; set; }
        public Int32 numCiclos { get; set; }

        public EstrucutraImagen(Bitmap Org)
        {
            numCiclos = 0;
            ancho = Org.Size.Width;
            alto = Org.Size.Height;
            Original = Org;
            FIMG = Original;
            Labels = new Int32[alto, ancho];
            Marcas = Labels;
            Features = new Double[84];

            Features = computarCaracteristicas();

        }

        private Double[] computarCaracteristicas()
        {
            Enmascaramiento enm = new Enmascaramiento();
            FeatureExtraction fet = new FeatureExtraction();
            var AOI = enm.MascaraAOI(this);
            pixelesAOI = AOI;
            List<Double> featurelist = new List<Double>();

            foreach (var esCol in AOI)
            {
                foreach(var chann in esCol)
                {
                    Double[] hps = fet.F_HPeaks(chann);

                    featurelist.Add(fet.F_Mean(chann));
                    featurelist.Add(fet.F_Var(chann));
                    featurelist.Add(hps[0]);
                    featurelist.Add(hps[1]);
                    featurelist.Add(fet.F_HVar(chann));
                    featurelist.Add(fet.F_HSkws(chann));
                    featurelist.Add(fet.F_HEner(chann));
                    //featurelist.Add(fet.F_HEntro(chann));
                }
            }
            var arrT = featurelist.ToArray();

            return arrT;
            
        }

    }
}
