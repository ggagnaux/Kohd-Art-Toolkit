#region Copyright (c) 2017 G. Gagnaux, https://github.com/ggagnaux/Kohd-Art-Toolkit
/*
Kohd & Art Toolkit - A toolkit of general classes/methods for .NET and C#

Copyright (c) 2017 G. Gagnaux, https://github.com/ggagnaux/Kohd-Art-Toolkit

Permission is hereby granted, free of charge, to any person obtaining a copy of 
this software and associated documentation files (the "Software"), to deal in the 
Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
and to permit persons to whom the Software is furnished to do so, subject to the 
following conditions:

The above copyright notice and this permission notice shall be included in 
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
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
