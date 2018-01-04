using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Prediction
{
    public partial class PredictionPage : ContentPage
    {
        RestService restService;

        public PredictionPage()
        {
            InitializeComponent();
            restService = new RestService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitData();
        }

        public async void InitData() {
            Profiles.ItemsSource = await restService.RefreshDataAsync();
            if(Profiles.ItemsSource == null){
                Profiles.IsVisible = false;
                await DisplayAlert("Network issue", "The list could not be retrieved. Please check your network connection.", "OK");
                //Error.IsVisible = true;
                //Error.Text = "Network issue. \nThe list of profiles can\'t be retrieved.";
            }
            else {
                Profiles.IsVisible = true;
                //Error.IsVisible = false;
            }
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Profile;
            ((ListView)sender).SelectedItem = null;
            var detailPage = new DetailPage(selectedItem.id);
            detailPage.BindingContext = selectedItem;
            await Navigation.PushAsync(detailPage);
        }

        async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormulairePage());
        }

        async void OnRefreshClicked(object sender, EventArgs e)
        {
            InitData();
        }


        //TODO set graphic resources to buttons

        async void OnDeleteRequested(object sender, System.EventArgs e){
            var res = await DisplayAlert("Attention", "Voulez-vous supprimer ce profil?", "OK", "Annuler");
            if(res){
                var b = (Button)sender;
                String id = b.CommandParameter.ToString();
                var result = await restService.DeleteProfileAsync(id);
                if(result) await DisplayAlert("Suppression réussie", "Le profil a bien été supprimé !", "OK");
                else await DisplayAlert("Erreur", "Une erreur est survenue lors de la suppresison de ce profil.", "OK");
                InitData();
            }
        }

        async void OnScoreRequested(object sender, System.EventArgs e){
            var b = (Button)sender;
            String id = b.CommandParameter.ToString();
            String result = await restService.GetScoreSignaletique(id.ToString());
            if(result.StartsWith("ERROR ")){
                await DisplayAlert("Erreur", result, "OK");
            }
            else await DisplayAlert("Score signalétique", "Votre score signalétique est: \n\n" + result, "OK");
        }

        async void OnPredictionRequested(object sender, System.EventArgs e){
            var b = (Button) sender;
            String id = b.CommandParameter.ToString();
            String result = await restService.GetPredictionML(id);
            if (result.StartsWith("ERROR "))
            {
                await DisplayAlert("Erreur", result, "OK");
            }
            else await DisplayAlert("Score ML", "Votre score ML est: \n\n" + result, "OK");
        }
    }
}
