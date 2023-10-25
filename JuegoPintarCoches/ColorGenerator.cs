using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoPintarCoches
{
    public class ColorGenerator
    {
        private static Random random = new Random();

        private static string[] colors = { "Encaje", "Durazno", "Plateado", "Cafe", "Kaki", "Oro", "Mostaza", "Limon", "AguaMarina", "GrisOscuro", "VerdeAgua", "AzulMarino",      "Salmon", "Ladrillo", "Carmesi", "Purpura" };

        public static string GetRandomColor()
        {
            int index = random.Next(colors.Length);
            return colors[index];
        }
    }
}
