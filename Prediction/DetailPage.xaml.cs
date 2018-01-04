using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Prediction
{
    public partial class DetailPage : ContentPage
    {
        private Profile profile;

        public DetailPage(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
            //var profile = (Profile) BindingContext;

            InitData(id.ToString());
        }

        public async void InitData(String id){
            RestService restService = new RestService();
            if (restService != null)
            {
                profile = await restService.GetProfileAsync(id);
            }
            Title = profile.Name;
            SituationFamille.Text = profile.situationFamiliale;
            TypeHabitation.Text = profile.typeHabitation;
            TypeContrat.Text = profile.typeContratp;
            if (profile.situationFamiliale == null) SituationFamille.IsVisible = false;
            if (profile.typeHabitation == null) TypeHabitation.IsVisible = false;
            if (profile.typeContratp == null) TypeContrat.IsVisible = false;
        }
    }
}