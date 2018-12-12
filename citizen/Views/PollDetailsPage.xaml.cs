using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Services.Api;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using citizen.ViewModels;

namespace citizen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PollDetailsPage : ContentPage
    {
        PollDetailsViewModel viewModel;
        private int _choiceCount = 0;
        private bool AlreadyVoted;

        public PollDetailsPage(String uuid)
        {
            InitializeComponent();

            BindingContext = this.viewModel = new PollDetailsViewModel(uuid);
            viewModel.PollChoices.CollectionChanged += HandleChoiceChange;
            viewModel.VoteCommand.CanExecuteChanged += HandleVoteExecuted;
            viewModel.LoadCommand.CanExecuteChanged += HandleLoadChange;
        }

        public PollDetailsPage(PollDetailsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            viewModel.PollChoices.CollectionChanged += HandleChoiceChange;
            viewModel.VoteCommand.CanExecuteChanged += HandleVoteExecuted;
        }

        public void HandleLoadChange(object sender, EventArgs e)
        {
            if (viewModel.Poll == null)
            {
                PollName.Text = "Erreur, impossible de trouver cette consultation";
                LoadingActivityIndicator.IsVisible = false;
                LoadingActivityIndicator.IsRunning = false;
                return;
            }
            PollName.Text = viewModel.Poll.Proposition;
            PollDetails.Text = viewModel.Poll.Details;
        }

        private void HandleChoiceChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var choiceObj in e.NewItems)
                {
                    var choice = choiceObj as PollChoice;
                    Console.WriteLine(choice.Text);
                    var PrimaryColor = (Color) App.Current.Resources["NavigationPrimary"];
                    var button = new Flex.Controls.FlexButton();

                    if (AlreadyVoted)
                    {
                        button.IsEnabled = false;
                        button.Opacity = 100;
                    }
                    
                    button.HorizontalOptions = LayoutOptions.Fill;
                    button.Text = choice.Text;
                    button.CornerRadius = 15;
                    button.HeightRequest = 50;
                    //Default color
                    button.ForegroundColor = PrimaryColor;
                    button.BackgroundColor = Color.White;

                    //Pressed color
                    button.HighlightBackgroundColor = PrimaryColor;
                    button.HighlightForegroundColor = Color.White;

                    //Define the border
                    button.BorderColor = PrimaryColor;
                    button.HighlightBorderColor = PrimaryColor;
                    button.BorderThickness = 3;

                    button.ToggleMode = true;
                    button.IsToggled = choice.Selected;
                    if (choice.Selected)
                    {
                        foreach (var bb in PollChoicesGrid.Children)
                        {
                            var b = bb as Flex.Controls.FlexButton;
                            if (b == null)
                                continue;
                            b.IsEnabled = false;
                            b.Opacity = 100;
                            b.HighlightBackgroundColor = PrimaryColor;
                            b.HighlightForegroundColor = Color.White;
                            b.ForegroundColor = PrimaryColor;
                            b.BackgroundColor = Color.White;
                        }
                        AlreadyVoted = true;
                    }

                    button.TouchedDown += HandleChoiceSelected;

                    PollChoicesGrid.Children.Add(button, 1, _choiceCount++);
                }
            }
        }

        private void HandleSubmit(object sender, EventArgs e)
        {
            int i = 0;
            int selected = -1;

            foreach (var item in PollChoicesGrid.Children)
            {
                var button = item as Flex.Controls.FlexButton;
                if (button == null)
                    continue;

                if (button.IsToggled)
                    selected = i;
                i++;
            }

            if (selected == -1)
            {
                //TODO Nothing selected
                return;
            }

            if (!viewModel.IsBusy)
            {
                SubmitButton.Text = "";
                viewModel.VoteCommand.Execute(selected);
            }
        }

        private void HandleVoteExecuted(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void HandleChoiceSelected(object sender, EventArgs e)
        {
            var triggeredButton = sender as Flex.Controls.FlexButton;
            if (triggeredButton == null)
                return;

            if (AlreadyVoted)
            {
                triggeredButton.IsToggled = !triggeredButton.IsToggled;
                return;
            }

            foreach (var item in PollChoicesGrid.Children)
            {
                var button = item as Flex.Controls.FlexButton;
                if (button == null || button == triggeredButton)
                    continue;

                button.IsToggled = false;
            }

            if (!SubmitGrid.IsVisible)
            {
                PollStackLayout.SizeChanged += HandleSubmitAppear;
                SubmitGrid.IsVisible = true;
            }
        }

        public void HandleSubmitAppear(object sender, EventArgs e)
        {
            PollScrollView.ScrollToAsync(0, PollStackLayout.Height - PollScrollView.Height, true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.PollChoices.Count == 0)
                viewModel.LoadCommand.Execute(null);
        }
    }
}