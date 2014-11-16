using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace FFT
{

    //Дефиниране на структура от комплексен тип на данните
    struct COMPLEX
    {
        public double real, imag;
        public COMPLEX(double x, double y)
        {
            real = x;
            imag = y;
        }
        public float Magnitude()
        {
            return ((float)Math.Sqrt(real * real + imag * imag));
        }
        public float Phase()
        {
            return ((float)Math.Atan(imag / real));
        }
    }

   
    class Fourie
    {
        public Bitmap Object;
        public int[,] SivoIzobrajenie;
        int Width, Height;
        int nx, ny;
        COMPLEX[,] Fourier;              // Масив на амплитудата за обратна трансформация
        public COMPLEX[,] FFTShifted;    // Shifted FFT 
        public COMPLEX[,] Izhod;        
        public float[,] FourierMagnitude;
        public float[,] FourierPhase;
        float[,] FFTMagnitudeLog;         // Вход за Фурие амплитуда
        float[,] FFTPhaseLog;            //  Вход за Фурие фазата
        public int[,] FFTMagniNormaliziran;     //  Scale 0-1  Нормализирана амплитуда
        public int[,] FFTPhaseNormaliziran;// Scale 0-1   Нормализирана фаза
        //public COMPLEX[,] FFTNormal;
        public Bitmap FourierPlot;       // Генериране на Фурие амплитуда
        public Bitmap PhasePlot;         // Генериране на Фурие фаза
        //--------------------------------------------------------------
        //--------------------------------------------------------------



          
        public Fourie(Bitmap Input)
        {
            Object = Input;
            Width = nx = Input.Width;
            Height= ny = Input.Height;
            ReadImage();
        }

         public Fourie(int[,] Input)
        {
            SivoIzobrajenie = Input;
            Width = nx = Input.GetLength(0);
            Height = ny = Input.GetLength(1);
        }

         //Контруктор за обратното преобразувание на Фурие
        public Fourie(COMPLEX[,] Input)
        {
            nx = Width = Input.GetLength(0);
            ny = Height = Input.GetLength(1);
            Fourier = Input;

        }

        //Преобразува изображението в масив
        private void ReadImage()
        {
            int i, j;
            SivoIzobrajenie = new int[Width, Height];  //[редове, колони]
            Bitmap image = Object;
            BitmapData bitmapData1 =
               image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        SivoIzobrajenie[j, i] = (int)((imagePointer1[0] +
                           imagePointer1[1] + imagePointer1[2]) / 3.0);
                        
                        imagePointer1 += 4;
                    }
                    
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            image.UnlockBits(bitmapData1);
            return;
        }

        //Визуализира масива като изображение
        public Bitmap Displayimage(int[,] image)
        {
            int i, j;
            Bitmap output = new Bitmap(image.GetLength(0), image.GetLength(1));
            BitmapData bitmapData1 = output.LockBits(new Rectangle(0, 0, image.GetLength(0), image.GetLength(1)),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        imagePointer1[0] = (byte)image[j, i];
                        imagePointer1[1] = (byte)image[j, i];
                        imagePointer1[2] = (byte)image[j, i];
                        imagePointer1[3] = 255;
                      
                        imagePointer1 += 4;
                    }
                    
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }
            }
            output.UnlockBits(bitmapData1);
            return output;

        }

        //Изчислява бързото преобразувание на Фурие от входното изображение
        public void ForwardFFT()
        {
            //Инициализира масива за преубразованието на Фурие
            int i, j;
            Fourier = new COMPLEX[Width, Height];
            Izhod = new COMPLEX[Width, Height];
            //Копира данните за изображението в комплексния масив
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    Fourier[i, j].real = (double)SivoIzobrajenie[i, j];
                    Fourier[i, j].imag = 0;
                }
            //Извиква функцията за бързото преобразование на Фурие
            Izhod = BPF2D(Fourier, nx, ny, 1);
            return;
        }

        //Променя БПФ на изображението
        public void FFTShift()
        {
            int i, j;
            FFTShifted = new COMPLEX[nx, ny];

            for (i = 0; i <= (nx / 2) - 1; i++)
                for (j = 0; j <= (ny / 2) - 1; j++)
                {
                    FFTShifted[i + (nx / 2), j + (ny / 2)] = Izhod[i, j];
                    FFTShifted[i, j] = Izhod[i + (nx / 2), j + (ny / 2)];
                    FFTShifted[i + (nx / 2), j] = Izhod[i, j + (ny / 2)];
                    FFTShifted[i, j + (nx / 2)] = Izhod[i + (nx / 2), j];
                }

            return;
        }



        //генерира изображение за да може да се изведе на дисплея
        public void FFTPlot(COMPLEX[,] Output)
        {
            int i, j;
            float max;

            FFTMagnitudeLog = new float[nx, ny];
            FFTPhaseLog = new float[nx, ny];

            FourierMagnitude = new float[nx, ny];
            FourierPhase = new float[nx, ny];

            FFTMagniNormaliziran = new int[nx, ny];
            FFTPhaseNormaliziran = new int[nx, ny];

            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FourierMagnitude[i, j] = Output[i, j].Magnitude();
                    FourierPhase[i, j] = Output[i, j].Phase();
                    FFTMagnitudeLog[i, j] = FourierMagnitude[i, j];
                    FFTPhaseLog[i, j] = FourierPhase[i, j];
                }
            //Генерира bitmap на амплитуда
            max = FFTMagnitudeLog[0, 0];
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    if (FFTMagnitudeLog[i, j] > max)
                        max = FFTMagnitudeLog[i, j];
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTMagnitudeLog[i, j] = FFTMagnitudeLog[i, j] / max;
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTMagniNormaliziran[i, j] = (int)(500 * FFTMagnitudeLog[i, j]);
                }
            
            FourierPlot = Displayimage(FFTMagniNormaliziran);

            //генерира bitmap на фазата
            FFTPhaseLog[0, 0] = 0;
            max = FFTPhaseLog[1, 1];
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    if (FFTPhaseLog[i, j] > max)
                        max = FFTPhaseLog[i, j];
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTPhaseLog[i, j] = FFTPhaseLog[i, j] / max;
                }
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    FFTPhaseNormaliziran[i, j] = (int)(250 * FFTPhaseLog[i, j]);
                }
            
            PhasePlot = Displayimage(FFTPhaseNormaliziran);


        }

       
       //изчислява обратното преобразувание на Фурие 
        public void InverseFFT()
        {
            
            int i, j;

            //Извиква правото преобразувание на Фурие 
            Izhod = new COMPLEX[nx, ny];
            Izhod = BPF2D(Fourier, nx, ny, -1);

            Object = null;  
           
            for (i = 0; i <= Width - 1; i++)
                for (j = 0; j <= Height - 1; j++)
                {
                    SivoIzobrajenie[i, j] = (int)Izhod[i, j].Magnitude();

                }
            Object = Displayimage(SivoIzobrajenie);
            using (StreamWriter writer = new StreamWriter(@"C:\Users\7zereh\Documents\Visual Studio 2013\Projects\FastFourieTransform\grey image.txt"))
            {
                foreach (var value in SivoIzobrajenie)
                {
                    writer.WriteLine(value);
                }
                
            }
            using (StreamWriter writer = new StreamWriter(@"C:\Users\7zereh\Documents\Visual Studio 2013\Projects\FastFourieTransform\magnitude.txt"))
            {
                foreach (var value in FourierMagnitude)
                {
                    writer.WriteLine(value);
                }

            }
            using (StreamWriter writer = new StreamWriter(@"C:\Users\7zereh\Documents\Visual Studio 2013\Projects\FastFourieTransform\phase.txt"))
            {
                foreach (var value in FourierPhase)
                {
                    writer.WriteLine(value);
                }

            }
            return;

        }

        
        //----------------------------------------------------------------------------------
        //Преобразувание на Фурие
        //----------------------------------------------------------------------------------
       
        

        public COMPLEX[,] BPF2D(COMPLEX[,] c, int nx, int ny, int dir)
        {
            int i, j;
            int m;// Степен на 2 за текущия номер от точки
            double[] real;
            double[] imag;
            COMPLEX[,] output;
            output = c; 
            // Преобразува редовете
            real = new double[nx];
            imag = new double[nx];

            for (j = 0; j < ny; j++)
            {
                for (i = 0; i < nx; i++)
                {
                    real[i] = c[i, j].real;
                    imag[i] = c[i, j].imag;
                }
                // Извиква едномерната Фурие трансформация за редовете
                m = (int)Math.Log((double)nx, 2);// Намиране степента на 2 за текущия номер от точки
                BPF1D(dir, m, ref real, ref imag);

                for (i = 0; i < nx; i++)
                {
                   
                    output[i, j].real = real[i];
                    output[i, j].imag = imag[i];
                }
            }
           
            // Преобразува колоните  
            real = new double[ny];
            imag = new double[ny];

            for (i = 0; i < nx; i++)
            {
                for (j = 0; j < ny; j++)
                {
                   
                    real[j] = output[i, j].real;
                    imag[j] = output[i, j].imag;
                }
                // Извиква едномерната Фурие трансформация за колоните
                m = (int)Math.Log((double)ny, 2);//Намиране степента на 2 за текущия номер от точки
                BPF1D(dir, m, ref real, ref imag);
                for (j = 0; j < ny; j++)
                {
                    
                    output[i, j].real = real[j];
                    output[i, j].imag = imag[j];
                }
            }

          
            return (output);
        }
        //-------------------------------------------------------------------------
        //Изчисляване на едномерният случай на бързото преобразувание на Фурие
        private void BPF1D(int dir, int m, ref double[] x, ref double[] y)
        {
            long nn, i, i1, j, k, i2, l, l1, l2;
            double c1, c2, tx, ty, t1, t2, u1, u2, z;
            //Пресмятане броя на точките
            nn = 1;
            for (i = 0; i < m; i++)
                nn *= 2;
            
            i2 = nn >> 1;
            j = 0;
            for (i = 0; i < nn - 1; i++)
            {
                if (i < j)
                {
                    tx = x[i];
                    ty = y[i];
                    x[i] = x[j];
                    y[i] = y[j];
                    x[j] = tx;
                    y[j] = ty;
                }
                k = i2;
                while (k <= j)
                {
                    j -= k;
                    k >>= 1;
                }
                j += k;
            }
            // Изчислява преобразуванието на Фурие
            c1 = -1.0;
            c2 = 0.0;
            l2 = 1;
            for (l = 0; l < m; l++)
            {
                l1 = l2;
                l2 <<= 1;
                u1 = 1.0;
                u2 = 0.0;
                for (j = 0; j < l1; j++)
                {
                    for (i = j; i < nn; i += l2)
                    {
                        i1 = i + l1;
                        t1 = u1 * x[i1] - u2 * y[i1];
                        t2 = u1 * y[i1] + u2 * x[i1];
                        x[i1] = x[i] - t1;
                        y[i1] = y[i] - t2;
                        x[i] += t1;
                        y[i] += t2;
                    }
                    z = u1 * c1 - u2 * c2;
                    u2 = u1 * c2 + u2 * c1;
                    u1 = z;
                }
                c2 = Math.Sqrt((1.0 - c1) / 2.0);
                if (dir == 1)
                    c2 = -c2;
                c1 = Math.Sqrt((1.0 + c1) / 2.0);
            }
          
            if (dir == 1)
            {
                for (i = 0; i < nn; i++)
                {
                    x[i] /= (double)nn;
                    y[i] /= (double)nn;

                }
            }

            return;
        }

    }
}
