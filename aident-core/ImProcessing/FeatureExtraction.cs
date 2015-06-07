using Accord.Statistics.Visualizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.Math;

namespace ImProcessing
{

    /*
     * A partir de algoritmos de análisis de la información de los píxeles:
        •	El valor medio de los píxeles. (Mean)
        •	La varianza del valor de los píxeles. (Var)
       A partir de algoritmos de análisis de histogramas de color:
        •	La  varianza del histograma. (HVar)
        •	La asimetría del histograma. (HSkws)
        •	La energía del histograma. (HEner)
        •	La entropía del histograma. (HEntro)
     *      La posición de los dos picos más altos en el histograma. (HP1, HP2)
        •	La altura de los dos picos más altos en el histograma. (HV1, HV2)
     * 
     * */
    public class FeatureExtraction
    {
        public Double F_Mean(Double[] pix)
        {
            var res = Accord.Statistics.Tools.Mean(pix);
            return res;
        }

        public Double F_Var(Double[] pix)
        {
            var res = Accord.Statistics.Tools.Variance(pix);
            return res;
        }

        public Double F_HMean (Double[] pix)
        {
            Double res = new Double();
            Histogram hs = new Histogram();
            hs.Compute(pix, numberOfBins:100);
            res = Accord.Statistics.Tools.Mean(hs.Values);
            return res;
        }

        public Double F_HVar(Double[] pix)
        {
            Double res = new Double();
            Histogram hs = new Histogram();
            hs.Compute(pix, numberOfBins: 100);
            res = Accord.Statistics.Tools.Variance(hs.Values);
            return res;
        }

        public Double F_HSkws(Double[] pix)
        {
            Double res = new Double();
            Histogram hs = new Histogram();
            hs.Compute(pix, numberOfBins: 100);
            Double[] vals = Array.ConvertAll(hs.Values, x => (double)x);
            res = Accord.Statistics.Tools.Skewness(vals, F_HMean(pix));
            return res;
        }
        public Double F_HEner(Double[] pix)
        {
            Double res = new Double();
            Histogram hs = new Histogram();
            hs.Compute(pix, numberOfBins: 100);
            Double[] cuadrados = Array.ConvertAll(hs.Values, x => (double)x);
            cuadrados.ApplyInPlace((x) => x*x);
            res = cuadrados.Sum();
             
            return res;
        }

        public Double F_HEntro(Double[] pix)
        {
            Double res = new Double();
            res = 0;
            Histogram hs = new Histogram();
            hs.Compute(pix, numberOfBins: 100);
            Double[] vals = Array.ConvertAll(hs.Values, x => (double)x);
            foreach (var p in vals)
            {
                res = res + p * (Math.Log(p, 2));
            }

            //res = Accord.Statistics.Tools.Entropy(vals);
            return res*-1;
        }

        /// <summary>
        /// Devuelve los picos más altos del histograma: P1, V1, P2, V2
        /// </summary>
        /// <param name="pix">Array de Pixeles del AOI de UNA SOLA CAPA, sin morfologia</param>
        /// <returns></returns>
        public Double[] F_HPeaks(Double[] pix)
        {
            Double[] res = new Double[4];
            Histogram hs = new Histogram();
            hs.Compute(pix, numberOfBins: 100);
            Double[] vals = Array.ConvertAll(hs.Values, x => (double)x);
            Double[] pes = Array.ConvertAll(hs.Edges, x => (double)x);
            var MaxVal = vals.Max();
            var bi = hs.Bins;

            var xdf = (from arecord in bi
                           orderby  arecord.Value descending
                           select new{
                               arecord
                           }).FirstOrDefault();
            res[0] = (xdf.arecord.Range.Min + xdf.arecord.Range.Min)/2;
            res[1] = xdf.arecord.Value;

            return res;
        }


    }
}
