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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitData();
        }

        public async void InitData() {
            restService = new RestService();
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
            Navigation.PushAsync(new FormulairePage());
        }



        //TODO set graphic resources to buttons

        async void OnDeleteRequested(object sender, System.EventArgs e){
            var b = (Button)sender;
            String id = b.CommandParameter.ToString();
            await restService.DeleteProfileAsync(id);
            InitData();
        }

        async void OnScoreRequested(object sender, System.EventArgs e){
            var b = (Button)sender;
            String id = b.CommandParameter.ToString();
            String result = await restService.GetScoreSignaletique(id.ToString());
            if(result.StartsWith("ERROR ")){
                await DisplayAlert("Score ML - Error", result, "OK");
            }
            else await DisplayAlert("Score signalétique", "Votre score signalétique est: " + result, "OK");
        }

        async void OnPredictionRequested(object sender, System.EventArgs e){
            var b = (Button) sender;
            String id = b.CommandParameter.ToString();
            String result = await restService.GetPredictionML(id);
            if (result.StartsWith("ERROR "))
            {
                await DisplayAlert("Score ML - Error", result, "OK");
            }
            else await DisplayAlert("Score ML", "Votre score ML est: " + result, "OK");
        }
    }
}
