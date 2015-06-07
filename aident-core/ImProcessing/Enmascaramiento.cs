using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Accord.Imaging.Converters;
using System.Drawing;

namespace ImProcessing
{
    public class Enmascaramiento
    {

        public Double[][][] MascaraAOI(EstrucutraImagen imagen)
        {
            transformaciones tr = new transformaciones();
            Segmentar sg = new Segmentar();
            Int32 H = imagen.Original.Height;
            Int32 W = imagen.Original.Width;
            var FIMG = sg.Kmeans(imagen.Original);
            imagen.FIMG = FIMG;
            Double[][] luv1 = tr.RGBTO(FIMG, "LUV");
            Double[][] alterno = new Double[H][];
            Int32 cont = -1;
            for (var i = 0; i < H; i++ )
            {
                alterno[i] = new Double[W];
                for(var j = 0; j < W; j++)
                {
                    cont++;
                    Double[] pix = luv1[cont];
                    alterno[i][j] = pix[1];

                }
            }
            Double Umbral = CalcularUmbral(alterno);
            ImageToArray imageToArray = new ImageToArray(min: 0, max: 255);
            Double[][] pixels;
            imageToArray.Convert(imagen.Original, out pixels);
            
            //Listas temporales
            List<RGB> lrgb = new List<RGB>();
            List<nRGB> lnrgb = new List<nRGB>();
            List<LUV> lluv = new List<LUV>();
            List<HSB> lhsb = new List<HSB>();
            

            foreach (var unPixel in pixels)
            {
                RGB rgb = new RGB();
                rgb.Red = Convert.ToInt32(unPixel[0]);
                rgb.Green = Convert.ToInt32(unPixel[1]);
                rgb.Blue = Convert.ToInt32(unPixel[2]);

                //Conversiones
                LUV luv = new LUV();
                CIEXYZ xyz = new CIEXYZ();
                xyz = ColorSpaceHelper.RGBtoXYZ(rgb);
                luv = ColorSpaceHelper.XYZtoLUV(xyz.X, xyz.Y, xyz.Z);

                
                //Extraccion de pixeles del AOI
                if (Math.Abs(luv.U) > Umbral)
                {
                    nRGB nrgb = new nRGB();
                    HSB hsb = new HSB();
                    nrgb = ColorSpaceHelper.RGBtonRGB(rgb);
                    hsb = ColorSpaceHelper.RGBtoHSB(rgb);

                    lrgb.Add(rgb);
                    lnrgb.Add(nrgb);
                    lluv.Add(luv);
                    lhsb.Add(hsb);
                }

            }

            //Transformar listas en array
            Int32 tama = lrgb.Count; //cantidad de píxeles que componen el AOI
            //Double[][][] trans = new Double[tama][][];
            Double[][][] trans = new Double[4][][];
            trans[0] = new Double[3][];
            trans[1] = new Double[3][];
            trans[2] = new Double[3][];
            trans[3] = new Double[3][];
            foreach (var p in trans)
            {
                p[0] = new Double[tama];
                p[1] = new Double[tama];
                p[2] = new Double[tama];
            }

            for (var i = 0; i<tama; i++)
            {
                trans[0][0][i] = lrgb[i].Red;
                trans[0][1][i] = lrgb[i].Green;
                trans[0][2][i] = lrgb[i].Blue;

                trans[1][0][i] = lnrgb[i].Red;
                trans[1][1][i] = lnrgb[i].Green;
                trans[1][2][i] = lnrgb[i].Blue;

                trans[2][0][i] = lluv[i].L;
                trans[2][1][i] = lluv[i].U;
                trans[2][2][i] = lluv[i].V;

                trans[3][0][i] = lhsb[i].Hue;
                trans[3][1][i] = lhsb[i].Saturation;
                trans[3][2][i] = lhsb[i].Brightness;
            }


            //for (var i = 0; i < tama; i++)
            //{
            //    trans[i] = new Double[4][];
            //    trans[i][0] = new Double[3];
            //    trans[i][1] = new Double[3];
            //    trans[i][2] = new Double[3];
            //    trans[i][3] = new Double[3];

            //    trans[i][0][0] = lrgb[i].Red;
            //    trans[i][0][1] = lrgb[i].Green;
            //    trans[i][0][2] = lrgb[i].Blue;

            //    trans[i][1][0] = lnrgb[i].Red;
            //    trans[i][1][1] = lnrgb[i].Green;
            //    trans[i][1][2] = lnrgb[i].Blue;

            //    trans[i][2][0] = lluv[i].L;
            //    trans[i][2][1] = lluv[i].U;
            //    trans[i][2][2] = lluv[i].V;

            //    trans[i][3][0] = lhsb[i].Hue;
            //    trans[i][3][1] = lhsb[i].Saturation;
            //    trans[i][3][2] = lhsb[i].Brightness;

            //}

             var pixelesAOI = trans;



            return pixelesAOI;
        }

        private Double[] unArray(Double[][] dosArray)
        {
            List<Double> miLista = new List<double>();
            foreach (var xdf in dosArray)
            {
                foreach (var xxx in xdf)
                {
                    miLista.Add(xxx);
                }
            }
            return miLista.ToArray();
        }

        public Double CalcularUmbral(Double[][] alterno)
        {
            Int32 filas = alterno.GetLength(0);
            Int32 Columnas = alterno[0].GetLength(0);
            Double res = 0;
            Int32 fc = filas / 2;
            Int32 cc = Columnas / 2;
            Int32 r = 10;
            Double[][] centro = Accord.Math.Matrix.Submatrix(alterno, fc-r, fc+r, cc-r, cc+r);
            Double mediaCentro = Accord.Statistics.Tools.Mean(Accord.Statistics.Tools.Mean(centro));
            

            var centro1d = unArray(centro);
            var todoPixels = unArray(alterno);
            Double stdCentro = Accord.Statistics.Tools.StandardDeviation(centro1d);
            Double desviacion = Accord.Statistics.Tools.StandardDeviation(todoPixels);
            Double mediaTodo = Accord.Statistics.Tools.Mean(todoPixels);
            Double Minimo = Accord.Math.Matrix.Min(alterno);
            Double rats = desviacion / mediaTodo;

            Double[][] p1 = Accord.Math.Matrix.Submatrix(alterno, 0, r, 0, r);
            Double[][] p2 = Accord.Math.Matrix.Submatrix(alterno, 0, r, Columnas - r -1, Columnas -1);
            Double[][] p3 = Accord.Math.Matrix.Submatrix(alterno, filas - r -1, filas -1, Columnas - r -1, Columnas -1);
            Double[][] p4 = Accord.Math.Matrix.Submatrix(alterno, filas - r -1, filas -1, 0, r);

            Double[] esquinasCompletas = unArray(p1).Concat(unArray(p2)).ToArray().Concat(unArray(p3)).ToArray().Concat(unArray(p4)).ToArray();
            Double StdEsquina = Accord.Statistics.Tools.StandardDeviation(esquinasCompletas);

            Double mp1 = Accord.Statistics.Tools.Mean(unArray(p1));
            Double mp2 = Accord.Statistics.Tools.Mean(unArray(p2));
            Double mp3 = Accord.Statistics.Tools.Mean(unArray(p3));
            Double mp4 = Accord.Statistics.Tools.Mean(unArray(p4));

            Double mediaEsquinas = (mp1 + mp2 + mp3 + mp4) / 4;


            res = Math.Abs(mediaEsquinas) + 2 * Math.Abs(StdEsquina);

            
            
            return res;
        }
    }
}
