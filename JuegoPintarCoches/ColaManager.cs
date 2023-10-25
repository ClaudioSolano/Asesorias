using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPintarCoches
{
    public class ColaManager
    {
        private Queue<string> lstCola = new Queue<string>();

        public Queue<string> LstCola
        {
            get { return lstCola; }
        }

        public void AgregarColor(string color)
        {
            lstCola.Enqueue(color);
        }

        public void ResetCola()
        {
            lstCola.Clear();
        }
    }
}
