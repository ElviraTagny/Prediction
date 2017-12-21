using System;
namespace Prediction
{
    public class Profile
    {
        public String typeContratp { get; set; }
        public String typeHabitation { get; set; }
        public String situationFamiliale { get; set; }
        public String predictionValue { get; set; }
        public String montantEmprunt { get; set; }
        public String nbEnfn { get; set; }
        public int id { get; set; }
        public String Name { get; set; }
        public Profile()
        {
            Name = "Monsieur/Madame " + id;
        }
    }
}
