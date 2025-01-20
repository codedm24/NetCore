using DISampleLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinClient.Services
{
    internal class XamarinMessageService : IMessageService
    {
        private readonly IPageSerrvice _pageService;
        public XamarinMessageService(IPageSerrvice pageService) {
            _pageService = pageService;
        }

        public Task ShowMessageAsync(string message)
        {
            return _pageService.Page.DisplayAlert("Message", message,"Close");
        }
    }
}
