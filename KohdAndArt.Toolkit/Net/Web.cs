using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KohdAndArt.Toolkit.Net
{
    public class Web
    {
        class Result
        {
            public string data;
            public EventWaitHandle waiter;
        }

        public string DownloadHtmlAsString(string url)
        {
            return DownloadHtmlAsString(new Uri(url));
        }

        public string DownloadHtmlAsString(Uri url)
        {
            var r = new Result();
            r.waiter = new ManualResetEvent(false);
            var wc = new WebClient();
            wc.DownloadStringCompleted += (sender, e) =>
            {
                Result r2 = e.UserState as Result;
                r2.data = e.Result;
                r2.waiter.Set();
            };
            //webClient.DownloadStringCompleted += DownloadCompletedEvent;

            wc.DownloadStringAsync(url, r);
            r.waiter.WaitOne();
            return r.data;
        }

        //void DownloadCompletedEvent(object sender, DownloadStringCompletedEventArgs e)
        //{
        //    Result r = e.UserState as Result;
        //    r.data = e.Result;
        //    r.waiter.Set();
        //}
    }
}
