using System;
using System.Drawing;
using System.Windows.Forms;
using Accord.Imaging.Converters;
using Accord.MachineLearning;
using Accord.Math;
using Accord.Statistics.Distributions.DensityKernels;
using LibreriaObjetos;
using ColorMine;
using AForge.Imaging.Filters;
using System.Collections.Generic;

namespace ProcImagen
{
    public class Procesamiento
    {
        public Procesamiento()
        {
            //var rgb = new Rgb();
            //var luv = rgb.To<Luv>();

        }

        public Bitmap runMeanShift(Bitmap imagenOriginal, Double hs, Double hr)
        {
            //FeatureImage resultado = new FeatureImage();
            int pixelSize = 3;

            // Retrieve the kernel bandwidth
            double sigma = hs*0.01;

            // Load original image

            //resultado.imagenOriginal = imagenOriginal;
            // Create converters
            ImageToArray imageToArray = new ImageToArray(min: -1, max: +1);
            ArrayToImage arrayToImage = new ArrayToImage(imagenOriginal.Width, imagenOriginal.Height, min: -1, max: +1);

            // Transform the image into an array of pixel values
            double[][] pixels; 
            imageToArray.Convert(imagenOriginal, out pixels);


            // Create a MeanShift algorithm using the given bandwidth
            // and a Gaussian density kernel as the kernel function:

            IRadiallySymmetricKernel kernel = new GaussianKernel(pixelSize);
            
            var meanShift = new MeanShift(pixelSize, kernel, sigma)
            {
                Tolerance = 0.05,
                MaxIterations = 100
            };
            
            
            // Compute the mean-shift algorithm until the difference 
            // in shift vectors between two iterations is below 0.05
            
            int[] idx = meanShift.Compute(pixels);


            // Replace every pixel with its corresponding centroid
            pixels.ApplyInPlace((x, i) => meanShift.Clusters.Modes[idx[i]]);

            // Show resulting image in the picture box
            Bitmap result;
            arrayToImage.Convert(pixels, out result);
            //resultado.imagenSegmentada = result;

            return result;
        }

        public Double[,] Alterno(Double [,] capaU, Int32 img1w, Int32 img1h) {

            // pasar a LUV
            // sacar capa U matriz MxN
            // pasar la capa U a matriz Alterno
            // Del Alterno sacar una matriz de posiciones mayor o menor a un dato
            Double[,] resultado = new Double[img1h, img1w];
            for (Int32 x = 0; x < img1w; x++)
                {
                    for (Int32 y = 0; y < img1h; y++)
                    {

                        resultado[y, x] = capaU[y, x];// Capa U está en la posición 1;
                    }
            }

            return resultado;
        }

        public Double[,] SacarMascara(Double[,] Alterno, Double umbral, Int32 tipoUmbral)
        {
            // recorrer todos los pixeles de la imagen alterna y verifica el umbral
            // si se cumple: añadir posicion a matriz resultado y,x 
            // resultado = (columna 1 = posicion fila alterno, columna 2 = columna de alterno)
            Int32 x = Alterno.GetLength(0);
            Int32 y = Alterno.GetLength(1);
            Double[,] resultado = new Double[y, x];


            for (x = 0; x < Alterno.GetLength(0); x++)
            {
                for (y = 0; y < Alterno.GetLength(1); y++)
                {
                    if ((Alterno[y, x] > umbral && tipoUmbral == 0) || (Alterno[y, x] < umbral && tipoUmbral == 1))
                    {
                        resultado[y, x] = 1;

                    }
                    else
                    {
                        resultado[y, x] = 0;
                    }

                }
            }
            return resultado;

        }

        public Double calcularUmbral(Double[,] Alterno){
            
            //GaussianBlur filter = new GaussianBlur(3,3);
            
            //filter.ApplyInPlace(Alterno);

            Double[,] L = Alterno; //filtroMediasMoviles(Alterno, 3,3);
            Double media = mediaPixeles(L);
            Double desviacion = Accord.Statistics.Tools.StandardDeviation(Accord.Statistics.Tools.StandardDeviation(L));

            Double ratio = desviacion/media;

            //calcular tamaño de la matriz Alterno
            Int32 H = Alterno.GetLength(0);//300;
            Int32 W = Alterno.GetLength(1);

            Int32 centroH = H/2;
            Int32 centroW = W/2;

            Int32 r = 10;
            Double[,] ventanaCentral = Matrix.Submatrix<Double>(L,centroH-r,centroH+r,centroW-r,centroW+r);
            Double mediaVentanaCentral = mediaPixeles(ventanaCentral);
            Double desviacionVentanaCentral = Accord.Statistics.Tools.StandardDeviation(Accord.Statistics.Tools.StandardDeviation(ventanaCentral));

            Double[,] p1 = Matrix.Submatrix<Double>(L,1,r,1,r);
            Double[,] p2 = Matrix.Submatrix<Double>(L,1,r,W-r,W);
            Double[,] p3 = Matrix.Submatrix<Double>(L,H-r,H,W-r,W);
            Double[,] p4 = Matrix.Submatrix<Double>(L,H-r,H,1,r);

            Double mediaEsquina = (mediaPixeles(p1) + mediaPixeles(p2) + mediaPixeles(p3) + mediaPixeles(p4))/4;

            Double tM = mediaVentanaCentral + desviacionVentanaCentral;
            Double tL = mediaVentanaCentral - desviacionVentanaCentral;
            if(mediaEsquina > tL && mediaEsquina<tM){
                return Matrix.Min<Double>(L);
            }else{
                return mediaEsquina + 2*ratio;
            }
        }

        private Double mediaPixeles(Double[,] matriz){
            Double resultado = 0;

            double[] means = Accord.Statistics.Tools.Mean(matriz);
            resultado = Accord.Statistics.Tools.Mean(means);

            return resultado;
        }

        public Double[] matrizCapaAOIaVector(Double[,] xR, Double[,] mascara)
        {
            Int32 x = xR.GetLength(0);
            Int32 y = xR.GetLength(1);
            List<Double> resultado = new List<Double>();
            Int32 punteroResultado = 0;
            // Loop through the images pixels to reset color.
            for (x = 0; x < xR.GetLength(0); x++)
            {
                for (y = 0; y < xR.GetLength(1); y++)
                {
                    if (mascara[y, x] == 1)
                    {
                        resultado.Add(xR[y, x]);
                        punteroResultado++;
                    }

                }
            }
            return resultado.ToArray();
        }

        public Double[] calcularFeatures(Double[] AOIxR, Double[] AOIxG, Double[] AOIxB, Double[] AOIxL, Double[] AOIxu, Double[] AOIxv, Double[] AOIxH, Double[] AOIxS, Double[] AOIxV, Double[] AOIxRn, Double[] AOIxGn, Double[] AOIxBn)
        {
            Double[] features = new Double[120];
            
            //Calcular las features con los vectores de entrada:


            return features;
        }
    }
}
