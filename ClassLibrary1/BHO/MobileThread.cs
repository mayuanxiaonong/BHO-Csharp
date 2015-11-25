using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ClassLibrary1.BHO
{
    class MobileThread
    {

        private string s;
        public MobileThread(string s)
        {
            this.s = s;
        }

        public void http()
        {
            WebRequest request = null;
            WebResponse response = null;
            try
            {
                string url = "http://m.ip138.com/mobile.asp?mobile=" + s;
                request = WebRequest.Create(url);
                response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                string line = reader.ReadToEnd();
                System.Windows.Forms.MessageBox.Show("BHO " + s + "\r\n" + line);
                
            }
            catch (Exception)
            {
            }
            finally
            {
                if (response != null)
                {
                    try
                    {
                        response.Close();
                    }
                    catch (Exception)
                    { }
                }
            }
        }
    }
}
