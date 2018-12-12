using citizen.Models.Api;
using citizen.Views.Cells;
using Xamarin.Forms;

namespace citizen.Helpers
{
    public class ThreadDetailsTemplateSelector : DataTemplateSelector
    {
        private DataTemplate selfTemplate;
        
        public ThreadDetailsTemplateSelector()
        {
            selfTemplate = new DataTemplate(typeof(ThreadDetailsSelfViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as PostItem;
            if (messageVm == null)
                return null;

            return selfTemplate;
        }
    }
}