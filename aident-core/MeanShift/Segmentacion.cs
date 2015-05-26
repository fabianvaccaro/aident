using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MeanShift
{
    class Segmentacion
    {
        private class RGBImage{
            public Image R{get;set;}
            public Image G{get;set;}
            public Image B{get;set;}
            public Int32 size{get;set;}
            public RGBImage(Image image){
                R=image;
                G=image;
                B=image;
                size = 3;// comprobar que hay tres capas
            }
        };
        // Recibe una imagen RGB y devuelve una ImagenLuv
        public Image RGB2Luv(RGBImage im){
             Image vuelta = null;
            if(im.size != 3){
                Console.WriteLine("Error: No tiene tres canales de color");
                return vuelta;
            }

            return vuelta;
        }




if ~isa(im,'float')
    im = im2single(im);
end
if (max(im(:)) > 1)
    im = im./255;
end

XYZ = [.4125 .3576 .1804; .2125 .7154 .0721; .0193 .1192 .9502];
Yn = 1.0;
Lt = .008856;
Up = 0.19784977571475;
Vp = 0.46834507665248;
imsiz = size(im);
im = permute(im,[3 1 2]);
im = reshape(im,[3 prod(imsiz(1:2))]);
xyz = reshape((XYZ*im)',imsiz);
x = xyz(:,:,1);
y = xyz(:,:,2);
z = xyz(:,:,3);

l0 = y./Yn;
l = l0;
l(l0>Lt) = 116.*(l0(l0>Lt).^(1/3)) - 16;
l(l0<=Lt) = 903.3*l0(l0<=Lt);
c = x + 15*y + 3 * z;
u = 4*ones(imsiz(1:2),class(im));
v = (9/15)*ones(imsiz(1:2),class(im));
u(c~=0) = 4*x(c~=0)./c(c~=0);
v(c~=0) = 9*y(c~=0)./c(c~=0);

u = 13*l.*(u-Up);
v = 13*l.*(v-Vp);

luvim = cat(3,l,u,v);

    }
}
