using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using citizen.Models.Api;
using citizen.Services.Api;
using citizen.Services;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using System;

namespace citizen.ViewModels
{
    class ReportViewModel
    {
        public Command sendReportCommand { get; set; }
        public ReportService ReportService = new ReportService();
        bool IsBusy = false;

        public ReportViewModel()
        {
            sendReportCommand = new Command<ReportContentItem>(async (content) => await ExecuteSendReportCommand(content));
        }

        async Task ExecuteSendReportCommand(ReportContentItem content)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await ReportService.ReportPostAsync(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}

