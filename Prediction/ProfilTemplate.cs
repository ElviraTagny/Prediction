using System;

using Xamarin.Forms;

namespace Prediction
{
    public class ProfilTemplate // : ContentPage
    {
        /*public ProfilTemplate()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }*/

        public ProfilTemplate()
        {
        }

        public String Name { get; set; }
        public String TypeContrat { get; set; }
        public String TypeHabitation { get; set; }
        public String SituationFamille { get; set; }
        public int MontantEmprunt { get; set; }
        public int EnfantsACharge { get; set; }
        public int Num { get; set; }
    }
}

