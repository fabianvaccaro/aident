using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Accord.Imaging;
using Accord.Math;
using AForge;
using Accord.Imaging.Converters;
using Accord.Statistics.Distributions.DensityKernels;
using Accord.MachineLearning;

namespace ImProcessing
{
    public class Segmentar
    {
        public EstrucutraImagen MnShft(Bitmap Original, Double Hs, Double Hr)
        {
            EstrucutraImagen resultante = new EstrucutraImagen(Original);
            resultante.Original = Original;

            int pixelsize = 3;
            Double sigma = Hs * 0.01;
            ImageToArray im2array = new ImageToArray(min: -1, max: +1);
            ArrayToImage array2im = new ArrayToImage(Original.Width, Original.Height, min: -1, max: +1);
            Double[][] Pixels;
            im2array.Convert(Original, out Pixels);
            IRadiallySymmetricKernel kernel = new GaussianKernel(pixelsize);

            var meanshift = new MeanShift(pixelsize, kernel, sigma)
            {
                Tolerance = 0.05,
                MaxIterations = 100,
                Bandwidth = Hr
            };

            Int32[] idx = meanshift.Compute(Pixels);
            Pixels.ApplyInPlace((x, i) => meanshift.Clusters.Modes[idx[i]]);
            Bitmap temporal;
            array2im.Convert(Pixels, out temporal);
            resultante.FIMG = temporal;


            return resultante;
        }
        public Bitmap Kmeans(Bitmap Original)
        {
            Int32 K = 10;
            ImageToArray im2array = new ImageToArray(min: -1, max: +1);
            ArrayToImage array2im = new ArrayToImage(Original.Width, Original.Height, min: -1, max: +1);
            Double[][] Pixels;
            im2array.Convert(Original, out Pixels);
            KMeans kmedios = new KMeans(K, Distance.SquareEuclidean)
            {
                Tolerance = 0.05
            };

            Int32[] idx = kmedios.Compute(Pixels);
            Pixels.ApplyInPlace((x, i) => kmedios.Clusters.Centroids[idx[i]]);
            Bitmap temporal;
            array2im.Convert(Pixels, out temporal);

            return temporal;
        }
    }
}
