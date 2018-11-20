using System;
using System.Collections.Specialized;
using citizen.Models.Api;
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

        public PollDetailsPage(PollDetailsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            viewModel.PollChoices.CollectionChanged += HandleChoiceChange;
        }

        private void HandleChoiceChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var choiceObj in e.NewItems)
                {
                    var choice = choiceObj as PollChoice;
                    Console.WriteLine(choice.Text);
                    var PrimaryColor = (Color)App.Current.Resources["NavigationPrimary"];
                    var button = new Flex.Controls.FlexButton();
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

                    button.TouchedDown += HandleChoiceSelected;
                    
                    PollChoicesGrid.Children.Add(button, 1, _choiceCount++);
                }

                SubmitGrid.IsVisible = true;
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
                Console.WriteLine("Nothing selected");
                return;
            }
            
            Console.WriteLine("Selected " + selected);
        }

        private void HandleChoiceSelected(object sender, EventArgs e)
        {
            var triggeredButton = sender as Flex.Controls.FlexButton;
            if (triggeredButton == null)
                return;
            
            foreach (var item in PollChoicesGrid.Children)
            {
                var button = item as Flex.Controls.FlexButton;
                if (button == null || button == triggeredButton)
                    continue;

                button.IsToggled = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.PollChoices.Count == 0)
                viewModel.LoadChoicesCommand.Execute(null);
        }
    }
}