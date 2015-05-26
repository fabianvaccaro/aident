using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MeanShift
{
    public class MeanShift
    {
        Image imp;
	    Int32 rad;
        Int32 rad2;
	    float radCol;
        float radCol2;
	    Int32 nPasses;
        Int32 pass;
        protected Int32 stack; //Numero de Capas
	    Boolean cancel = false;


        public int setup(String arg, Image imp) {
		    this.imp = imp;
		    if (imp == null) {
			    Console.WriteLine("ERROR", "Hay que abrir primero una imágen");
			    return 0;
		    }
		    stack = imp.getStack();
		    return 1; //return DOES_ALL; DOES_ALL Set this flag if the filter handles all types of images.
	    }

        public void run(Image ip, Int32 radio_fila, float radio_columna) {
		    pass++;

            rad = radio_fila;
            rad2 = radio_fila * radio_fila;
            radCol = radio_columna;
		    radCol2 = radCol*radCol;
            
		    if (cancel == false){
			    filterLCTImage(ip);
			    //IJ.showMessage("ImageJ", "Finalizado");
		    }
	    }

        public void filterLCTImage(Image ip)
        {

            int itype = imp.GetType();
            int h = ip.Height;
            int w = ip.Width;
            int s = stack;
            // Comprobación de los valores de la imágen
            // IJ.showMessage("Probando", "Alto:" + height);
            // IJ.showMessage("Probando", "Ancho:" + width);
            // IJ.showMessage("Probando", "Slices:" + slices);
            // Comprobamos el tipo de la imagen
            //IJ.showMessage("TIPO", "Tipo de la Imágen:" + itype);

            // Llamamos al algoritmo Mean Shift, dependiendo del tipo
            // realizaremos conversiones previas ya que el Mean Shift
            // trabaja con flotantes
            if (itype == 0)
            {
                //IJ.showMessage("BYTE", "Dentro del Byte");
                new StackConverter(imp).convertToGray32();
                MeanShiftFilter(ip, h, w, s);
                Segmentar(ip, h, w, s);
                new StackConverter(imp).convertToGray8();
            }
            else if (itype == 1)
            {
                //IJ.showMessage("SHORT", "Dentro del Short");
                new StackConverter(imp).convertToGray32();
                MeanShiftFilter(ip, h, w, s);
                Segmentar(ip, h, w, s);
                new StackConverter(imp).convertToGray16();
            }
            else if (itype == 2)
            {
                //IJ.showMessage("FLOAT", "Dentro del Float");
                MeanShiftFilter(ip, h, w, s);
                Segmentar(ip, h, w, s);
            }
        }

        public void MeanShiftFilter(Image ip, int height, int width, int slices) {
		String status = "[Means Shift Segment] Número de capas: " + slices;
		Console.WriteLine(status);
		//float [] pixelsResult = (float[]) ip.getPixels();
        Int32 longitudVector= height*width;
		float [][] pixelsf = new float[slices][];//[slices][longitudVector];
		
		
		// En cada fila estará una capa
		for (int i = 0; i < longitudVector; i++) {
			pixelsf[i]= (float[]) stack.getProcessor(i+1).getPixels();
		}
		
		// Clonación del objeto, trabajamos con clonado 
		// y finalmente guardamos en el inicial los cambios
		float [][] pixelsfc = (float [][]) pixelsf.clone();
		
		// Desplazamiento y número de iteraciones
		float shift = 0;
		int iters = 0;
		
		// Comienza el algoritmo
		for (int y = 0; y < height; y++) {
			// Barra de progreso
			//if (y%20 == 0) IJ.showProgress(y/(double)height);
			
			// Nos situamos en el pixel actual, pos 
			//(x < width e y < hight, para no pasarnos con el radio aunque el valor de este sea mayor)
			for (int x = 0; x < width; x++) {
				int xc = x;
				int yc = y;
				int xcOld, ycOld;
				//float YcOld; --> cOld
				float[] cOld = new float [slices];
				// Posicion actual
				int pos = y*width + x;
				//float Yc = --> c
				float[] yiq = new float [slices];
				float[] c = new float [slices];
				for (int i = 0; i < slices; i++) {
					yiq[i] = pixelsfc[i][pos];
					c[i] = yiq[i];
				}
				
				iters = 0;
				do {
					xcOld = xc;
					ycOld = yc;
					for (int i = 0; i < slices; i++) {
						cOld[i] = c[i];
					}
					
					float mx = 0;
					float my = 0;
					//float mI,mQ,mY = --> m
					float[] m = new float [slices];
					for (int i = 0; i < slices; i++) m[i] = 0;
					int num = 0;
					
					// Movimiento en el eje de las y
					for (int ry=-rad; ry <= rad; ry++) {
						int y2 = yc + ry;
						// Comprobamos que esté dentro de los valores permitidos para y
						if ((y2 >= 0) && (y2 < height)) {
							// Movimiento en el eje de las x
							for (int rx=-rad; rx <= rad; rx++) {
								int x2 = xc + rx;
								// Comprobamos que esté dentro de los valores permitidos para x
								if ((x2 >= 0) && (x2 < width)) {
									// Comprobación que está dentro de los límites de la circunferencia
									if (ry*ry + rx*rx < rad2) {
										// Ponemos los valores de ese punto en yiq para todas las capas
										for (int i = 0; i < slices; i++) yiq[i] = pixelsfc[i][y2*width + x2];
										//float I2,Q2,Y2 = --> V2
										//float dI,dQ,dY = --> d
										float [] V2 = new float[slices];
										float [] d = new float[slices];
										float suma = 0;
										for (int i = 0; i < slices; i++) {
											V2[i] = yiq[i];
											d[i] = c[i] - V2[i];
											suma += (d[i]*d[i]); 			
										}
										// Comprobamos que no sobrepase la distancia entre colores establecida
										if (suma <= radCol2) {
											mx += x2;
											my += y2;
											// En m[i], voy a ir sumando (añadiendo) los valores de los
											// pixeles que han cumplido lo anterior en cada una de las capas
											for (int i = 0; i < slices; i++) m[i] += V2[i];
											num++;
										}
									}
								}
							}
						}
					}
					float num_ = 1f/num;
					for (int i = 0; i < slices; i++) c[i] = m[i]*num_;
					xc = (int) (mx*num_+0.5);
					yc = (int) (my*num_+0.5);
					int dx = xc - xcOld;
					int dy = yc - ycOld;
					shift = dx*dx + dy*dy;
					//float dI,dQ,dY = --> d2
					float [] d2 = new float [slices];
					for (int i = 0; i < slices; i++) {
						d2[i] = c[i] - cOld[i];
						shift += d2[i]*d2[i];
					}
										
					iters++;
				} while ((shift > 0.05) && (iters < 100));
				// Cambiamos ahora el inicial, no el clonado
				// Actualizamos finalmente los valores de la imágen
				for (int i = 0; i < slices; i++)
					pixelsf[i][pos] = c[i];
			}
		}
	}


        // Calculo para la segmentación de la imagen
        // Después de aplicar el Mean Shift
        public void Segmentar(Image ip, int height, int width, int slices) {
		
		//float [] pixelsResult = (float[]) ip.getPixels();
		float [][] pixelsf = new float [slices][];
		
		
		// En cada fila estará una capa
		for (int i = 0; i < stack.getSize(); i++) {
			pixelsf[i]= (float[]) stack.getProcessor(i+1).getPixels();
		}
		
		// Clonación del objeto, trabajamos con clonado 
		// y finalmente guardamos en el inicial los cambios
		float [][] pixelsfc = (float [][]) pixelsf.clone();
		
		// Declaracion de los vectores que contendran todas las posiciones (x,y) a cambiar
		// Vector vector=new Vector();
        List<Int32> xcoord = new List<Int32>();
        List<Int32> ycoord = new List<Int32>(); 
		
		// Desplazamiento y número de iteraciones
		float shift = 0;
		int iters = 0;
		
		// Volvemos a aplicar el algoritmo, pero no modificamos nada hasta el final
		// dónde modificaremos aquellas posiciones con valores parecidos
		for (int y = 0; y < height; y++) {
			// Barra de progreso
			//if (y%20 == 0) IJ.showProgress(y/(double)height);
			
			// Nos situamos en el pixel actual, pos 
			//(x < width e y < hight, para no pasarnos con el radio aunque el valor de este sea mayor)
			for (int x = 0; x < width; x++) {
				int xc = x;
				int yc = y;
				int xcOld, ycOld;
				//float YcOld; --> cOld
				float[] cOld = new float [slices];
				// Posicion actual
				int pos = y*width + x;
				//float Yc = --> c
				float[] yiq = new float [slices];
				float[] c = new float [slices];
				for (int i = 0; i < slices; i++) {
					yiq[i] = pixelsfc[i][pos];
					c[i] = yiq[i];
				}
				
				// Insertamos la 'x' e 'y' a partir del cual se harán los cálculos 
				xcoord.Add(xc);
				ycoord.Add(yc);
				
				iters = 0;
				do {
					xcOld = xc;
					ycOld = yc;
					for (int i = 0; i < slices; i++) {
						cOld[i] = c[i];
					}
					
					float mx = 0;
					float my = 0;
					//float mI,mQ,mY = --> m
					float[] m = new float [slices];
					for (int i = 0; i < slices; i++) m[i] = 0;
					int num = 0;
					
					// Movimiento en el eje de las y
					for (int ry=-rad; ry <= rad; ry++) {
						int y2 = yc + ry;
						// Comprobamos que esté dentro de los valores permitidos para y
						if ((y2 >= 0) && (y2 < height)) {
							// Movimiento en el eje de las x
							for (int rx=-rad; rx <= rad; rx++) {
								int x2 = xc + rx;
								// Comprobamos que esté dentro de los valores permitidos para x
								if ((x2 >= 0) && (x2 < width)) {
									// Comprobación que está dentro de los límites de la circunferencia
									if (ry*ry + rx*rx < rad2) {
										// Ponemos los valores de ese punto en yiq para todas las capas
										for (int i = 0; i < slices; i++) yiq[i] = pixelsfc[i][y2*width + x2];
										//float I2,Q2,Y2 = --> V2
										//float dI,dQ,dY = --> d
										float [] V2 = new float[slices];
										float [] d = new float[slices];
										float suma = 0;
										for (int i = 0; i < slices; i++) {
											V2[i] = yiq[i];
											d[i] = c[i] - V2[i];
											suma += (d[i]*d[i]); 			
										}
										// Comprobamos que no sobrepase la distancia entre colores establecida
										if (suma <= radCol2) {
											mx += x2;
											my += y2;
											// En m[i], voy a ir sumando (añadiendo) los valores de los
											// pixeles que han cumplido lo anterior en cada una de las capas
											for (int i = 0; i < slices; i++) m[i] += V2[i];
											// Añadimos ese pixel a la lista de pixeles que se van a modificar
											xcoord.Add(x2);
											ycoord.Add(y2);
											// Incremento el número de pixeles que han cumplido las condiciones
											num++;
										}
									}
								}
							}
						}
					}
					float num_ = 1f/num;
					for (int i = 0; i < slices; i++) c[i] = m[i]*num_;
					xc = (int) (mx*num_+0.5);
					yc = (int) (my*num_+0.5);
					int dx = xc - xcOld;
					int dy = yc - ycOld;
					shift = dx*dx + dy*dy;
					//float dI,dQ,dY = --> d2
					float [] d2 = new float [slices];
					for (int i = 0; i < slices; i++) {
						d2[i] = c[i] - cOld[i];
						shift += d2[i]*d2[i];
					}
										
					iters++;
				} while ((shift > 0.05) && (iters < 100));
				// Cambiamos ahora el inicial, no el clonado
				// Actualizamos finalmente los valores de la imágen
				//for (int i = 0; i < slices; i++)
				//	pixelsf[i][pos] = c[i];
				
				// En vez de cambiar ahora un pixel cómo hacía antes, cambio todos los valores que se
				// han introducido en la lista de coordenas de x e y con el calor de la media
				// Cambiamos ahora los valores para ese pixel en la imagen inicial, no en la clonada
				// Actualizamos finalmente los valores de la imágen
				
				// Comprobé que el numero de elementos fuese el mismo.
				// IJ.showMessage("nº de elementos X", "Elementos: " + xcoord.size());
				// IJ.showMessage("nº de elementos Y", "Elementos: " + ycoord.size());
				
				// Actualizamos finalmente los valores de la imágen en la imagen inicial, no en la clonada
				int pos2 = 0;
				if (xcoord.size() == ycoord.size()) {
					// En cada capa, para cada una de las coordenadas (xcoord,ycoord) cambiamos su valor al de la media
					for (int i = 0; i < slices; i++) {
						for (int z = 0; z < xcoord.size(); z++) {
							pos2 = ycoord.elementAt(z)*width + xcoord.elementAt(z);
							pixelsf[i][pos2] = c[i];
						}
					}	
				}
				else Console.WriteLine("ERROR", "Los vectores de posiciones (xcoord,ycoord) NO son iguales");
				
				// Borramos ahora todos los elementos de las listas que contienes los valores a cambiar.
				// Tenemos que borrarlos porque si no se quedarían las listas con los valores de los pixeles
				// y cambiarían los valores de los mismos en la siguiente iteración.
				xcoord.removeAllElements();
				ycoord.removeAllElements();
				
				
			}
		}
	}

    }
}
