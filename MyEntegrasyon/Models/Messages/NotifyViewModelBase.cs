﻿namespace MyEntegrasyon.Models.Messages
{
    public class NotifyViewModelBase<T>
    {

        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeout { get; set; }


        public NotifyViewModelBase()
        {
            Header = "Yönlendiriliyorsunuz! ";
            Title = "Geçersiz İşlem";
            IsRedirecting = true;
            RedirectingUrl = "/Home/Default";
            RedirectingTimeout = 20000;
            Items = new List<T>();
        }
    }
}
