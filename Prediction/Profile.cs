using System;
using Newtonsoft.Json;

namespace Prediction
{
    public class Profile
    {
        public int id { get; set; }
        [JsonProperty("typeContratp")]
        public String typeContratp { get; set; }
        public String typeHabitation { get; set; }
        public String situationFamiliale { get; set; }
        public String predictionValue { get; set; }
        public String montantEmprunt { get; set; }
        public String nbEnfn { get; set; }

        public Profile()
        {}

        [JsonIgnore]
        public string Name
        {
            get { return "Profil " + id; }
        }
    }
}
