﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR3_EQ5_TM.Componentes
{
    class Grafica : Panel
    {
        // Variables globales

        private int[] numerosAleatorios; // Guarda los numerosAleatorios generados aleatoriamente
        private RectangleF[] rectangulosGraficados; // Grafica los numerosAleatorios del arreglo
        private int aux, avx, ba, bb, bc, bd, be, rectanguloMenor = -1, rectanguloMayor = -1, max, nv;
        private double pv;
        private bool band;
        private string tipo;
        string opcional;
        // Gráficos
        private Pen p;
        private Graphics g;
        private Brush pincelPrincipal, pincelSecundario;
        private Color colorPrimario, colorSecundario, colorFondo;

        public int Nv { get => nv; set => nv = value; }
        public string Tipo { get => tipo; set => tipo = value; }

        public Grafica()
        // : base(parametros pero no tiene)
        {
            Colores();
            this.DoubleBuffered = true;
            this.Width = 150;
            this.Height = 100;
            this.Paint += new PaintEventHandler(Grafica_Paint);
            this.BackColor = colorFondo;

            int[] valoresIniciales = new int[] { 0,0,0 };

            max = valoresIniciales.Length;
            numerosAleatorios = valoresIniciales;
            this.Refresh();
            DibujarRectangulos();
        }
        public Grafica(int tam, string a)
        {
            Colores();
            this.DoubleBuffered = true;
            this.Width = 150;
            this.Height = 100;
            this.Paint += new PaintEventHandler(Grafica_Paint);
            this.BackColor = colorFondo;
            //this.DoubleBuffered = true;
            //int[] valoresIniciales;
            max = tam;
            //numerosAleatorios = valoresIniciales;
            Inicios(tam);
            this.Refresh();
            DibujarRectangulos();

        }
        public void ActualizarDatos(int[] valores) 
        {
            int[] valoresIniciales =(int[]) valores.Clone();

            max = valoresIniciales.Length;
            numerosAleatorios = valoresIniciales;
            this.Refresh();
            DibujarRectangulos();
        }
        public void Inicios(int tam)
        {
            int Tam=tam;
            int[] Rand;
            Random Alea = new Random();
            int Num;
            int i = 0;
            Rand = new int[Tam];
            while (i < Tam)
            {
                Num = Alea.Next(1, 30);
                Rand[i] = Num;
                i++;
            }
            numerosAleatorios = Rand;
        }

        public Grafica(int alto, int ancho, int[] valores)
        // : base(parametros pero no tiene) 
        {
            Colores();
            this.DoubleBuffered = true;
            this.Width = 150;
            this.Height = 100;
            this.Paint += new PaintEventHandler(Grafica_Paint);
            this.BackColor = colorFondo;
            numerosAleatorios = valores;
            max = valores.Length;
            this.Refresh();
            DibujarRectangulos();
        }

        // Graficación

        private void Colores()
        {
            if(colorPrimario.IsEmpty && colorSecundario.IsEmpty)
            {
                colorPrimario = new Color();
                colorPrimario = Color.DarkOrange;
                colorSecundario = new Color();
                colorSecundario = Color.FromArgb(0, 255, 151);
                colorFondo = new Color();
                colorFondo = Color.FromArgb(27, 38, 49);
            }
            else if(!colorPrimario.IsEmpty && colorSecundario.IsEmpty)
            {
                colorPrimario = new Color();
                colorPrimario = Color.DarkOrange;
                colorFondo = new Color();
                colorFondo = Color.FromArgb(27, 38, 49);
            }
            else if(colorPrimario.IsEmpty && !colorSecundario.IsEmpty)
            {
                colorSecundario = new Color();
                colorSecundario = Color.FromArgb(0, 255, 151);
                colorFondo = new Color();
                colorFondo = Color.FromArgb(27, 38, 49);
            }
        }

        private void DibujarRectangulos()
        {

            rectangulosGraficados = new RectangleF[numerosAleatorios.Length]; // Crea el arreglo de rectangulos
            lock (rectangulosGraficados)
            {
                float Gros;
                float Py;
                //for (int numRec = 0; numRec < numerosAleatorios.Length; numRec++) // Genera los rectangulos con la lista de numerosAleatorios
                //{
                //    if (numRec == 0)
                //        rectangulosGraficados[numRec] = new Rectangle(0, ((this.Height) / max * (numRec)), this.Width / max * numerosAleatorios[numRec], (this.Height) / (numerosAleatorios.Length)-1);
                //    else
                //        rectangulosGraficados[numRec] = new Rectangle(0, ((this.Height) / max * (numRec)), this.Width / max * numerosAleatorios[numRec], (this.Height) /(numerosAleatorios.Length)-1);
                //}
                for (int numRec = 0; numRec < numerosAleatorios.Length; numRec++) // Genera los rectangulos con la lista de numerosAleatorios
                {
                    Py = (this.Height / max * numRec);
                    Gros = ((this.Height / numerosAleatorios.Length) - 1);
                    if (numRec == 0)
                        rectangulosGraficados[numRec] = new RectangleF(0, Convert.ToSingle(Py), this.Width / max * numerosAleatorios[numRec], Convert.ToSingle(Gros));
                    else
                        rectangulosGraficados[numRec] = new RectangleF(0, Convert.ToSingle(Py), this.Width / max * numerosAleatorios[numRec], Convert.ToSingle(Gros));
                }
            }
            
        }

        private void Animacion(int k, int w)
        {
            try
            {
                rectanguloMenor = k;
            rectanguloMayor = w;
            float distancia = rectangulosGraficados[rectanguloMayor].Top - rectangulosGraficados[rectanguloMayor].Top;
            float yk = rectangulosGraficados[rectanguloMenor].Top,
                yw = rectangulosGraficados[rectanguloMayor].Top;
            
                while (rectangulosGraficados[rectanguloMenor].Top < yw)
                {

                    rectangulosGraficados[rectanguloMenor].Location = new PointF(0, (rectangulosGraficados[rectanguloMenor].Top) + this.Height / max * 1);
                    rectangulosGraficados[rectanguloMayor].Location = new PointF(0, (rectangulosGraficados[rectanguloMayor].Top) - this.Height / max * 1);

                    this.Invoke(new MethodInvoker(Refresh));

                    //g.FillRectangle(pincelSecundario, rectangulosGraficados[rectanguloMenor]);
                    //g.FillRectangle(pincelSecundario, rectangulosGraficados[rectanguloMayor]);
                    if (opcional == "Merge" || opcional == "Heap")
                    {
                        Thread.Sleep(1);
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }

                    //Invalidate();
                }
                //g.FillRectangle(pincelPrincipal, rectangulosGraficados[rectanguloMayor]);
                //g.FillRectangle(pincelPrincipal, rectangulosGraficados[rectanguloMayor]);
                rectanguloMenor = -1;
                rectanguloMayor = -1;
            }
            catch (IndexOutOfRangeException Ex)
            {
                //MessageBox.Show(Ex.Message);
            }


        } // Anima xd
        
        #region Metodos de grafiacion
        public void CambioColor(string cambio, Color color)
        {
            switch (cambio)
            {
                case "Principal":
                    colorPrimario = new Color();
                    colorPrimario = color;
                    break;
                case "Secundario":
                    colorSecundario = new Color();
                    colorSecundario = color;
                    break;
            }
        }
        #endregion

        // Ordenación

        public void Ordenar(string métodoSelecionado)
        {
            opcional = métodoSelecionado;
            switch (métodoSelecionado)
            {
                case "Burbuja Tradicional":
                    BurbujaT();
                    break;
                case "Burbuja Mejorada":
                    BurbujaM();
                    break;
                case "Quicksort":
                    Quicksort(0, numerosAleatorios.Length - 1);
                    break;
                case "Shell":
                    Shell();
                    break;
                case "Inserción Simple":
                    Baraja();
                    break;
                case "Inserción Binaria":
                    Binario();
                    break;
                case "Selección":
                    Seleccion();
                    break;
                case "Merge":
                    Merge(numerosAleatorios, 0, numerosAleatorios.Length-1);
                    break;
                case "Heap":
                    Heap(numerosAleatorios);
                    break;
                default:
                    MessageBox.Show("Selecione un método de ordenamiento");
                    break;
            }
            this.Invoke(new MethodInvoker(Refresh));
            //Refresh();
        }

        // Métodos

        #region Código de los métodos

        public void BurbujaT()
        {
            // Ordena la lista
            for (ba = 1; ba < numerosAleatorios.Length; ba++)
            {
                for (bb = numerosAleatorios.Length - 1; bb >= ba; bb--)
                {
                    if (numerosAleatorios[bb - 1] > numerosAleatorios[bb]) // Intercambio de datos
                    {
                        aux = numerosAleatorios[bb - 1];
                        Animacion(bb - 1, bb);
                        numerosAleatorios[bb - 1] = numerosAleatorios[bb];
                        numerosAleatorios[bb] = aux;
                        DibujarRectangulos();
                    }
                }
            }
        }              // Burbuja tradicional // Sí
        public void BurbujaM()
        {
            // Ordena la lista
            band = true; bd = 0;
            do
            {
                bd++;
                band = true;
                for (ba = 0; ba < numerosAleatorios.Length - bd; ba++)
                {
                    if (numerosAleatorios[ba] > numerosAleatorios[ba + 1])
                    {
                        aux = numerosAleatorios[ba];
                        Animacion(ba, ba + 1);
                        numerosAleatorios[ba] = numerosAleatorios[ba + 1];
                        numerosAleatorios[ba + 1] = aux;
                        DibujarRectangulos();
                        band = false;
                    }
                }
            }
            while (!band);
        }              // Burbuja mejorado // Sí
        public void Shell()
        {
            // Ordena la lista
            ba = numerosAleatorios.Length / 2;
            while (ba > 0)
            {
                band = true;
                while (band)
                {
                    band = false;
                    bb = 1;
                    while (bb <= (numerosAleatorios.Length - ba))
                    {
                        if (numerosAleatorios[bb - 1] > numerosAleatorios[(bb - 1) + ba]) // Intercambio de datos
                        {
                            aux = numerosAleatorios[(bb - 1) + ba];
                            Animacion(bb - 1, (bb - 1) + ba);
                            numerosAleatorios[(bb - 1) + ba] = numerosAleatorios[bb - 1];
                            numerosAleatorios[(bb - 1)] = aux;
                            DibujarRectangulos();
                            band = true;
                        }
                        bb++;
                    }
                }
                ba = ba / 2;
            }
        }                 // Shell // Sí
        public void Quicksort(int p, int u)
        {
            // Ordena la lista
            bc = (p + u) / 2;
            pv = numerosAleatorios[bc];
            ba = p;
            bb = u;
            do
            {
                while (numerosAleatorios[ba] < pv) ba++;
                while (numerosAleatorios[bb] > pv) bb--;
                if (ba <= bb)
                {
                    aux = numerosAleatorios[ba];
                    Animacion(ba, bb);
                    numerosAleatorios[ba] = numerosAleatorios[bb];
                    numerosAleatorios[bb] = aux;
                    DibujarRectangulos();
                    ba++;
                    bb--;
                }
            } while (ba <= bb);

            if (p < bb)
            {
                Quicksort(p, bb);
            }
            if (ba < u)
            {
                Quicksort(ba, u);
            }

        } // Quicksort // Sí
        public void Baraja()
        {
            // Ordena la lista
            for (ba = 0; ba < numerosAleatorios.Length; ba++)
            {
                aux = numerosAleatorios[ba];
                bc = ba - 1;
                while ((bc >= 0) && (aux < numerosAleatorios[bc])) // Intercambio de numerosAleatorios
                {
                    Animacion(bc, bc + 1);
                    numerosAleatorios[bc + 1] = numerosAleatorios[bc];
                    DibujarRectangulos();
                    bc--;
                }
                numerosAleatorios[bc + 1] = aux;
            }
        }                // Inserción directa // Sí
        public void Binario()
        {
            // Ordena la lista

            for (int ba = 0; ba < numerosAleatorios.Length; ba++)
            {
                aux = numerosAleatorios[ba];
                avx = ba;
                be = 0;
                bd = ba - 1;
                while (be <= bd)
                {
                    bc = (be + bd) / 2;
                    if (aux <= numerosAleatorios[bc])
                    {
                        bd = bc - 1;
                    }
                    else
                    {
                        be = bc + 1;
                    }
                }
                for (int bb = ba - 1; bb >= be; bb--)
                {
                    numerosAleatorios[bb + 1] = numerosAleatorios[bb];
                }
                Animacion(be, avx);
                numerosAleatorios[be] = aux;
                DibujarRectangulos();
            }
        }               // Inserción binaria // Sí
        //Seleccion
        public void Seleccion()
        {
            int min, i, j, auxiliar;
            int nums = numerosAleatorios.Length;
            for (i = 0; i < nums - 1; i++)
            {
                min = i;
                for (j = i + 1; j < nums; j++)
                {
                    if (numerosAleatorios[j] < numerosAleatorios[min])
                        min = j;
                }
                auxiliar = numerosAleatorios[i];
                numerosAleatorios[i] = numerosAleatorios[min];
                numerosAleatorios[min] = auxiliar;
                Animacion(i, numerosAleatorios[min]);
                DibujarRectangulos();
            }
        }
        //Merge
        public void Merge(int[] x, int desde, int hasta)
        {
            //Condicion de parada
            if (desde == hasta)
                return;
            //Calculo la mitad del array
            int mitad = (desde + hasta) / 2;
            //Voy a ordenar recursivamente la primera mitad
            //y luego la segunda
            Merge(x, desde, mitad);
            Merge(x, mitad + 1, hasta);
            //Mezclo las dos mitades ordenadas
            int[] aux = MergeSort(x, desde, mitad, mitad + 1, hasta);
            Array.Copy(aux, 0, x, desde, aux.Length);
            for (int i = 0; i < numerosAleatorios.Length; i++)
            {
                Animacion(i, numerosAleatorios[i]);
                DibujarRectangulos();
            }
        }
        public int[] MergeSort(int[] x, int desde1, int hasta1, int desde2, int hasta2)
        {
            int a = desde1;
            int b = desde2;
            int[] result = new int[hasta1 - desde1 + hasta2 - desde2 + 2];

            for (int i = 0; i < result.Length; i++)
            {
                if (b != x.Length)
                {
                    if (a > hasta1 && b <= hasta2)
                    {
                        result[i] = x[b];
                        b++;
                    }
                    if (b > hasta2 && a <= hasta1)
                    {
                        result[i] = x[a];
                        a++;
                    }
                    if (a <= hasta1 && b <= hasta2)
                    {
                        if (x[b] <= x[a])
                        {
                            result[i] = x[b];
                            b++;
                        }
                        else
                        {
                            result[i] = x[a];
                            a++;
                        }
                    }
                }
                else
                {
                    if (a <= hasta1)
                    {
                        result[i] = x[a];
                        a++;
                    }
                }
            }
            return result;
        }
        //Heap
        public void Heap(int[] array)
        {
            int tamanioArray = array.Length;
            for (int p = (tamanioArray - 1) / 2; p >= 0; p--)
                Maximos(array, tamanioArray, p);

            for (int i = array.Length - 1; i > 0; i--)
            {
                int temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                tamanioArray--;
                Maximos(array, tamanioArray, 0);
                for (int k = 0; k < numerosAleatorios.Length; k++)
                {
                    Animacion(k, numerosAleatorios[k]);
                    DibujarRectangulos();
                }
            }
        }
        public void Maximos(int[] array, int tamanyo, int posicion)
        {
            int izquierda = (posicion + 1) * 2 - 1;
            int derecha = (posicion + 1) * 2;
            int mayor = 0;

            if (izquierda < tamanyo && array[izquierda] > array[posicion])
                mayor = izquierda;
            else
                mayor = posicion;

            if (derecha < tamanyo && array[derecha] > array[mayor])
                mayor = derecha;

            if (mayor != posicion)
            {
                int temp = array[posicion];
                array[posicion] = array[mayor];
                array[mayor] = temp;

                Maximos(array, tamanyo, mayor);
            }
        }
        #endregion

        // Eventos

        private void Grafica_Paint(object sender, PaintEventArgs e)
        {
            // Crea los graficos
            g = e.Graphics;
            p = new Pen(new SolidBrush(Color.Black));
            pincelPrincipal = new SolidBrush(colorPrimario);
            pincelSecundario = new SolidBrush(colorSecundario);

            // Dibuja rectangulo
            g.DrawRectangles(p, rectangulosGraficados);
            g.FillRectangles(pincelPrincipal, rectangulosGraficados);

            if (rectanguloMenor != -1 && rectanguloMayor != -1)
            {
                g.FillRectangle(pincelSecundario, rectangulosGraficados[rectanguloMenor]);
                g.FillRectangle(pincelSecundario, rectangulosGraficados[rectanguloMayor]);
            }
        }
    }
}