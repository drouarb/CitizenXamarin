using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace citizen.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailPage : ContentPage
	{
        EventDetailModel viewModel;

		public EventDetailPage (EventsItem _event)
		{
			InitializeComponent();
            BindingContext = viewModel = new EventDetailModel(_event);
            viewModel.LoadDetailCommand.Execute(null);
		}
    }
}