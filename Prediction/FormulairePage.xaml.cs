using System;
using System.Collections.Generic;
using Prediction.Tools;
using Xamarin.Forms;

namespace Prediction
{
    public partial class FormulairePage : ContentPage
    {
        class CustomEntry : Entry
        {
            public String Name { get; set; }
            public CustomEntry()
            {
                Keyboard = Keyboard.Numeric;
                MinimumWidthRequest = 100;
            }
        }

        private DropDownPicker DropSituationFamille, DropHabitation, DropTypeContrat, DropSecteurActivite/*, DropProfession*/;
        RestService restService;
        Profile newProfile;
        CustomEntry nbEnfantsEntry = new CustomEntry();
        CustomEntry montantEmpruntEntry = new CustomEntry();

        public FormulairePage()
        {
            InitializeComponent();
            restService = new RestService();

            Title = "Nouveau profil";
            double iosWidthRequest = 200;
            double androidWidthRequest = 220;
            double winPhoneWidthRequest = 200;
            float iosFontSize = 10;
            float androidFontSize = 14;
            float winPhoneFontSize = 10;

            this.DropSituationFamille = new DropDownPicker
            {
                //WidthRequest = Device.OnPlatform(iosWidthRequest, androidWidthRequest, winPhoneWidthRequest),
                HorizontalOptions=LayoutOptions.FillAndExpand,
                HeightRequest = 40,
                DropDownHeight = 150,
                Title = "Situation familiale",
                SelectedText = "",
                FontSize = Device.OnPlatform(iosFontSize, androidFontSize, winPhoneFontSize),
                CellHeight = 40,
                SelectedBackgroundColor = Color.FromRgb(0, 70, 172),
                SelectedTextColor = Color.White,
                BorderColor = Color.Purple,
                ArrowColor = Color.Blue
            };

            this.DropHabitation = new DropDownPicker
            {
                //WidthRequest = Device.OnPlatform(iosWidthRequest, androidWidthRequest, winPhoneWidthRequest),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 40,
                DropDownHeight = 150,
                Title = "Habitation principale",
                FontSize = Device.OnPlatform(iosFontSize, androidFontSize, winPhoneFontSize),
                CellHeight = 40,
                SelectedBackgroundColor = Color.FromRgb(0, 70, 172),
                SelectedTextColor = Color.White
            };

            this.DropTypeContrat = new DropDownPicker
            {
                //WidthRequest = Device.OnPlatform(iosWidthRequest, androidWidthRequest, winPhoneWidthRequest),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 40,
                DropDownHeight = 150,
                Title = "Type de contrat",
                FontSize = Device.OnPlatform(iosFontSize, androidFontSize, winPhoneFontSize),
                CellHeight = 40,
                SelectedBackgroundColor = Color.FromRgb(0, 70, 172),
                SelectedTextColor = Color.White
            };

            this.DropSecteurActivite = new DropDownPicker
            {
                //WidthRequest = Device.OnPlatform(iosWidthRequest, androidWidthRequest, winPhoneWidthRequest),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 40,
                DropDownHeight = 150,
                Title = "Secteur d'activité",
                FontSize = Device.OnPlatform(iosFontSize, androidFontSize, winPhoneFontSize),
                CellHeight = 40,
                SelectedBackgroundColor = Color.FromRgb(0, 70, 172),
                SelectedTextColor = Color.White
            };

            var dataSituationFam = new List<string>();
            dataSituationFam.Add("Aucune");
            dataSituationFam.Add("Célibataire");
            dataSituationFam.Add("Marié(e)");
            dataSituationFam.Add("Concubin(e)");
            dataSituationFam.Add("Séparé(e)");
            dataSituationFam.Add("Veuf(ve)");
            dataSituationFam.Add("Divorcé(e)");

            var dataHabitation = new List<string>();
            dataHabitation.Add("Aucune");
            dataHabitation.Add("Propriétaire");
            dataHabitation.Add("Locataire payant");
            dataHabitation.Add("Locataire gratuit");
            dataHabitation.Add("Locataire HLM");
            dataHabitation.Add("Locataire meublé");
            dataHabitation.Add("Logé par la famille");
            dataHabitation.Add("Nomade");

            var dataTypeContrat = new List<string>();
            dataTypeContrat.Add("Aucun");
            dataTypeContrat.Add("CDI");
            dataTypeContrat.Add("CDD");
            dataTypeContrat.Add("Intérim");
            dataTypeContrat.Add("Autre");

            var dataSecteurActiv = new List<string>();
            dataSecteurActiv.Add("Pêche, Construction, Agriculture");
            dataSecteurActiv.Add("Transports et communications");
            dataSecteurActiv.Add("Immobilier, location et services aux entreprises");
            dataSecteurActiv.Add("Commerce, réparations automobile et d'articles domestiques");
            dataSecteurActiv.Add("Industrie, EDF");
            dataSecteurActiv.Add("Education");
            dataSecteurActiv.Add("Services collectifs, sociaux et personnels");
            dataSecteurActiv.Add("Service (Santé, Administration, Ambassade)");
            dataSecteurActiv.Add("Activités financières");
            dataSecteurActiv.Add("Autre");

            this.DropHabitation.Source = dataHabitation;
            this.DropTypeContrat.Source = dataTypeContrat;
            this.DropSecteurActivite.Source = dataSecteurActiv;
            this.DropSituationFamille.Source = dataSituationFam;

            this.Content =
                new ScrollView {
                Content = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Padding = 20,
                    BackgroundColor = Color.White,
                    Children = {
                    new Label {
                        Text = "Situation familiale"
                    },
                    this.DropSituationFamille,
                    new Label {
                        Text = "Habitation principale",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    this.DropHabitation,
                    new Label {
                        Text = "Type de contrat",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    this.DropTypeContrat,
                    new Label {
                        Text = "Secteur d'activité",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    this.DropSecteurActivite,
                    new Label {
                        Text = "Profession",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Default,
                        WidthRequest = Device.OnPlatform(iosWidthRequest, androidWidthRequest, winPhoneWidthRequest)
                    },
                    //this.DropProfession,
                    new Label {
                        Text = "Age",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new Label {
                        Text = "Salaire mensuel net",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new Label {
                        Text = "Autres revenus mensuels",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new Label {
                        Text = "Année d'embauche",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new Label {
                        Text = "Loyer/Crédit immobilier de résidence principale",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new Label {
                        Text = "Loyer/Crédit immobilier de résidence secondaire",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new Label {
                        Text = "Montant charges",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new Label {
                        Text = "Autres crédits",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                        montantEmpruntEntry,
                    new Label {
                        Text = "Nombre d'enfants",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                        nbEnfantsEntry,
                    new Label {
                        Text = "Nombre de personnes rattachées au dossier",
                            Margin = new Thickness (0, 10, 0, 0)
                    },
                    new Entry {
                        Keyboard = Keyboard.Numeric
                    },
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.EndAndExpand,
                            Margin = new Thickness (0, 10, 0, 0),
                        Children = {
                            new Button
                            {
                                    Text = "Créer",
                                BackgroundColor = Color.RoyalBlue,
                                TextColor = Color.White,
                                    WidthRequest = 90,
                                Command = new Command(async () => {
                                    newProfile = new Profile();
                                    newProfile.situationFamiliale = DropSituationFamille.SelectedItem.ToString();
                                    newProfile.typeContratp = DropTypeContrat.SelectedItem.ToString();
                                    newProfile.typeHabitation = DropHabitation.SelectedItem.ToString();
                                        newProfile.nbEnfn = nbEnfantsEntry.Text;
                                        newProfile.montantEmprunt = montantEmpruntEntry.Text;
                                        if(await restService.SaveProfileAsync(newProfile, true)){
                                            var res = await DisplayAlert("Profil ajouté !", "Le nouveau profil a bien été sauvegardé.", null, "OK");
                                            if(!res) await Navigation.PopAsync();
                                        }
                                        else await DisplayAlert("Erreur", "Une erreur est survenue. Veuillez vérifier tous les champs.", "OK");
                                })
                            },
                            new Button
                            {
                                Text = "Annuler",
                                BackgroundColor = Color.Gray,
                                TextColor = Color.White,
                                    WidthRequest = 90,
                                Command = new Command(async () => {
                                    await Navigation.PopAsync();
                                })
                            }
                        }
                    }
                }
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.DropHabitation.OnSelected += DropHabitationSelected;
            this.DropTypeContrat.OnSelected += DropTypeContratSelected;
            this.DropSecteurActivite.OnSelected += DropSecteurActiviteSelected;
            this.DropSituationFamille.OnSelected += DropSituationFamilleSelected;

            if (Device.OS == TargetPlatform.iOS)
            {
                DropDownPicker.AddTapEvents();
                DropDownPicker.OnTapFrom += OnTapFrom;
            }
        }

        protected override void OnDisappearing()
        {
            if (Device.OS == TargetPlatform.iOS)
            {
                DropDownPicker.OnTapFrom -= OnTapFrom;
                DropDownPicker.RemoveEvents();
            }

            this.DropHabitation.OnSelected -= DropHabitationSelected;
            this.DropTypeContrat.OnSelected -= DropTypeContratSelected;
            this.DropSecteurActivite.OnSelected -= DropSecteurActiviteSelected;
            this.DropSituationFamille.OnSelected -= DropSituationFamilleSelected;

            base.OnDisappearing();
        }

        private void OnTapFrom(object sender, DropDownTapArgs e)
        {
            this.DropHabitation.DoHideDropDownOnTap(e);
            this.DropTypeContrat.DoHideDropDownOnTap(e);
            this.DropSecteurActivite.DoHideDropDownOnTap(e);
            this.DropSituationFamille.DoHideDropDownOnTap(e);
        }

        private void DropHabitationSelected(object sender, string e)
        {
            System.Diagnostics.Debug.WriteLine("selected text change DropHabitation: " + e);
        }

        private void DropTypeContratSelected(object sender, string e)
        {
            System.Diagnostics.Debug.WriteLine("selected text change DropTypeContrat: " + e);
        }

        private void DropSecteurActiviteSelected(object sender, string e)
        {
            System.Diagnostics.Debug.WriteLine("selected text change DropSecteurActivite: " + e);
        }

        private void DropSituationFamilleSelected(object sender, string e)
        {
            System.Diagnostics.Debug.WriteLine("selected text change DropSituationFamille: " + e);
        }
    }
}
