using System.Reflection.Emit;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;
using Timer = System.Windows.Forms.Timer;

namespace JuegoPintarCoches
{
    public partial class Form1 : Form
    {
        private ColaManager colaManager = new ColaManager();
        private Timer timer = new Timer();
        private Timer timerImagen = new Timer();
        private string colorActual = string.Empty;
        private int aciertos = 0;
        private int tiempoEntreValores = 20000; // Inicialmente 20 segundos
        private int recordAciertos = 0;
        private bool juegoIniciado = false; // Para validar si el juego ha comenzado

        private Label[] labels;

        public Form1()
        {
            InitializeComponent();

            btnEncaje.Click += BotonColor_Click;
            btnDurazno.Click += BotonColor_Click;
            btnPlateado.Click += BotonColor_Click;
            btnCafe.Click += BotonColor_Click;
            btnKaki.Click += BotonColor_Click;
            btnOro.Click += BotonColor_Click;
            btnMostaza.Click += BotonColor_Click;
            btnLimon.Click += BotonColor_Click;
            btnAguaMarina.Click += BotonColor_Click;
            btnGrisOscuro.Click += BotonColor_Click;
            btnVerdeAgua.Click += BotonColor_Click;
            btnAzulMarino.Click += BotonColor_Click;
            btnSalmon.Click += BotonColor_Click;
            btnLadrillo.Click += BotonColor_Click;
            btnCarmesi.Click += BotonColor_Click;
            btnPurpura.Click += BotonColor_Click;

            timer.Interval = tiempoEntreValores;
            timerImagen.Interval = 1000; // 1 segundo

            timer.Tick += AgregarColorALaCola;
            timerImagen.Tick += (sender, e) =>
            {
                pictureBox6.Image = null;
                label6.Visible = false;
                timerImagen.Stop();
            };

            // Inicializa el arreglo de etiquetas
            labels = new Label[] { label1, label2, label3, label4, label5 };
            pictureBox6.Visible = false;
            btnDetener.Click += btnDetener_Click;
        }
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (!juegoIniciado)
            {
                juegoIniciado = true;
                colaManager.ResetCola();
                lstCola.DataSource = colaManager.LstCola.ToArray();
                timer.Start();
                lblAciertos.Text = "Aciertos: 0";
                lblMensaje.Visible = false;
                label6.Visible = false;
                aciertos = 0;
                recordAciertos = 0;
                ActualizarRecord();
                ReiniciarImagenes();
                ReiniciarTextos();
                btnDetener.Visible = true; // Mostrar el botón "Detener"
            }
            else
            {
                lblMensaje.Text = "Toca el botón Iniciar para comenzar";
            }
        }
        private void btnDetener_Click(object sender, EventArgs e)
        {
            if (juegoIniciado)
            {
                colaManager.ResetCola();
                ReiniciarImagenes();
                ReiniciarTextos();
                timer.Stop();
                timerImagen.Stop();
                pictureBox6.Image = null;
                pictureBox6.Visible = false;
                juegoIniciado = false;
                btnDetener.Visible = false;
            }
        }

        private void AgregarColorALaCola(object sender, EventArgs e)
        {
            string color = ColorGenerator.GetRandomColor();
            colaManager.AgregarColor(color);
            lstCola.DataSource = colaManager.LstCola.ToArray();
            ActualizarImagenes();
            ActualizarTextos();

            if (colaManager.LstCola.Count >= 5)
            {
                Perdiste();
            }

            // Reducir el tiempo entre valores cada 3 aciertos
            if (aciertos > 0 && aciertos % 3 == 0)
            {
                if (tiempoEntreValores == 20000)
                    tiempoEntreValores -= 4000; // 16 segundos
                else if (tiempoEntreValores == 16000)
                    tiempoEntreValores -= 4000; // 12 segundos
                else if (tiempoEntreValores == 12000)
                    tiempoEntreValores -= 4000; // 8 segundos
                else if (tiempoEntreValores == 8000)
                    tiempoEntreValores -= 4000; // 4 segundos
                else if (tiempoEntreValores == 4000)
                    tiempoEntreValores -= 2000; // 2 segundos
                else if (tiempoEntreValores == 2000)
                    tiempoEntreValores -= 500; // 1.5 segundos
                else if (tiempoEntreValores == 1500)
                    tiempoEntreValores -= 500; // 1 segundos
                else if (tiempoEntreValores == 1000)
                    tiempoEntreValores -= 500; // .5 segundos
                else if (tiempoEntreValores == 500)
                    tiempoEntreValores -= 250; // .25segundos
                else if (tiempoEntreValores == 250)
                    tiempoEntreValores -= 249; // 0.1segundos

                timer.Interval = tiempoEntreValores;
            }
            if (tiempoEntreValores == 0.1)
            {
                Ganaste();
            }
        }


        private void BotonColor_Click(object sender, EventArgs e)
        {
            if (juegoIniciado)
            {
                Button boton = (Button)sender;
                string colorBoton = boton.Text;

                if (colaManager.LstCola.Count > 0)
                {
                    string primerColorCola = colaManager.LstCola.Peek();
                    if (primerColorCola == colorBoton)
                    {
                        // Mostrar la imagen correspondiente en pictureBox6
                        MostrarImagenEnPictureBox6(primerColorCola);

                        colaManager.LstCola.Dequeue();
                        lstCola.DataSource = colaManager.LstCola.ToArray();
                        lblAciertos.Text = "Aciertos: " + (++aciertos);
                        lblMensaje.Visible = false;
                        label6.Visible = true;
                        ActualizarRecord();
                        ActualizarImagenes();
                        ActualizarTextos();

                        if (tiempoEntreValores == 0.1)
                        {
                            Ganaste();
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Inténtalo de nuevo";
                        lblMensaje.Visible = true;
                    }
                }
            }
            else
            {
                lblMensaje.Text = "Toca el botón Iniciar para comenzar";
            }
        }
        private void MostrarImagenEnPictureBox6(string color)
        {
            switch (color)
            {
                case "Encaje":
                    pictureBox6.Image = Properties.Resources.Encaje;
                    break;
                case "Durazno":
                    pictureBox6.Image = Properties.Resources.Durazno;
                    break;
                case "Plateado":
                    pictureBox6.Image = Properties.Resources.Plateado;
                    break;
                case "Cafe":
                    pictureBox6.Image = Properties.Resources.Cafe;
                    break;
                case "Kaki":
                    pictureBox6.Image = Properties.Resources.Kaki;
                    break;
                case "Oro":
                    pictureBox6.Image = Properties.Resources.Oro;
                    break;
                case "Mostaza":
                    pictureBox6.Image = Properties.Resources.Mostaza;
                    break;
                case "Limon":
                    pictureBox6.Image = Properties.Resources.Limon;
                    break;
                case "AguaMarina":
                    pictureBox6.Image = Properties.Resources.AguaMarina;
                    break;
                case "GrisOscuro":
                    pictureBox6.Image = Properties.Resources.GrisOscuro;
                    break;
                case "VerdeAgua":
                    pictureBox6.Image = Properties.Resources.VerdeAgua;
                    break;
                case "AzulMarino":
                    pictureBox6.Image = Properties.Resources.AzMarino;
                    break;
                case "Salmon":
                    pictureBox6.Image = Properties.Resources.Salmon;
                    break;
                case "Ladrillo":
                    pictureBox6.Image = Properties.Resources.Ladrillo;
                    break;
                case "Carmesi":
                    pictureBox6.Image = Properties.Resources.Carmesi;
                    break;
                case "Purpura":
                    pictureBox6.Image = Properties.Resources.Purpura;
                    break;
            }

            pictureBox6.Visible = true;

            // Iniciar un temporizador para ocultar la imagen después de 0.5 segundos
            timerImagen.Start();
        }
        private void Perdiste()
        {
            colaManager.ResetCola();
            ReiniciarImagenes();
            ReiniciarTextos();
            timer.Stop();
            timerImagen.Stop();
            pictureBox6.Image = null;
            pictureBox6.Visible = false;
            MessageBox.Show("Perdiste");
            juegoIniciado = false;
            btnDetener.Visible = false;
        }

        private void Ganaste()
        {
            colaManager.ResetCola();
            ReiniciarImagenes();
            ReiniciarTextos();
            timer.Stop();
            timerImagen.Stop();
            pictureBox6.Image = null;
            pictureBox6.Visible = false;
            MessageBox.Show("Ganaste");
            juegoIniciado = false;
            btnDetener.Visible = false;
        }


        private void ActualizarRecord()
        {
            if (aciertos > recordAciertos)
            {
                recordAciertos = aciertos;
                lblRecord.Text = "Record: " + recordAciertos;
            }
        }

        private void ActualizarImagenes()
        {
            ReiniciarImagenes();
            for (int i = 0; i < colaManager.LstCola.Count; i++)
            {
                string color = colaManager.LstCola.ToArray()[i];
                switch (i)
                {
                    case 0:
                        pictureBox1.Image = (color == "NoColor") ? null : Properties.Resources.NoColor;
                        break;
                    case 1:
                        pictureBox2.Image = (color == "NoColor") ? null : Properties.Resources.NoColor;
                        break;
                    case 2:
                        pictureBox3.Image = (color == "NoColor") ? null : Properties.Resources.NoColor;
                        break;
                    case 3:
                        pictureBox4.Image = (color == "NoColor") ? null : Properties.Resources.NoColor;
                        break;
                    case 4:
                        pictureBox5.Image = (color == "NoColor") ? null : Properties.Resources.NoColor;
                        break;
                }
            }
        }

        private void ReiniciarImagenes()
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
        }
        private void ActualizarTextos()
        {
            ReiniciarTextos();
            for (int i = 0; i < colaManager.LstCola.Count; i++)
            {
                string color = colaManager.LstCola.ToArray()[i];
                labels[i].Text = (color == "NoColor") ? string.Empty : color;
            }
        }

        private void ReiniciarTextos()
        {
            foreach (Label label in labels)
            {
                label.Text = string.Empty;
            }
        }
    }
}