using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ClassLibrary1.BHO
{
    class Test
    {
        static void main(string[] args)
        {
            WebRequest webRequest = WebRequest.Create("");
            WebResponse webResponse = webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            string line = reader.ReadToEnd();
            webResponse.Close();
        }
    }
}
