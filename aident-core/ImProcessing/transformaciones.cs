using Accord.Imaging.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using Accord.Math;

namespace ImProcessing
{
    /*
     * 
     * [COLUMNA , FILA, CANAL]
     * 
     * 
     * 
     * */
    public class transformaciones
    {
        private Double[] recom(Double[] xm, String AA)
        {
            Double[] resultado = new Double[3];
            Double R = xm[0];
            Double G = xm[1];
            Double B = xm[2];

            RGB rgb = new RGB();
            rgb.Red = (Int32)R;
            rgb.Green = (Int32)G;
            rgb.Blue = (Int32)B;
            switch (AA)
            {
                case "XYZ":
                    CIEXYZ XYZ = ColorSpaceHelper.RGBtoXYZ(rgb);
                    resultado[0] = XYZ.X;
                    resultado[ 1] = XYZ.Y;
                    resultado[2] = XYZ.Z;
                    break;
                case "HSL":
                    HSL hsl = ColorSpaceHelper.RGBtoHSL(rgb);
                    resultado[0] = hsl.Hue;
                    resultado[1] = hsl.Saturation;
                    resultado[2] = hsl.Luminance;
                    break;
                case "LAB":
                    CIELab lab = ColorSpaceHelper.RGBtoLab(rgb);
                    resultado[0] = lab.L;
                    resultado[1] = lab.A;
                    resultado[2] = lab.B;
                    break;
                case "LUV":
                    CIEXYZ xxx = ColorSpaceHelper.RGBtoXYZ(rgb);
                    LUV cieluv = ColorSpaceHelper.XYZtoLUV(xxx.X, xxx.Y, xxx.Z);
                    resultado[0] = cieluv.L;
                    resultado[1] = cieluv.U;
                    resultado[2] = cieluv.V;
                    break;
                default:
                    resultado = xm;
                    break;
            }
            return resultado;

        }


        public Double[][] RGBTO(Bitmap ImagenOriginal, String A)
        {
            ImageToArray imageToArray = new ImageToArray(min: 0, max: 255);
            ArrayToImage arrayToImage = new ArrayToImage(ImagenOriginal.Width, ImagenOriginal.Height, min: 0, max: 255);

            ImageToMatrix imageToMatrix = new ImageToMatrix(min: 0, max: 255);

            Double[][] pixels;
            imageToArray.Convert(ImagenOriginal, out pixels);

            pixels.ApplyInPlace((x,i) => recom(x,A) );
            return pixels;


        }
    }
}
