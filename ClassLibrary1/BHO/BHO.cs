using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Expando;
using Microsoft.JScript;
using System.Reflection;
using System.Net;

using SHDocVw;
using mshtml;

namespace ClassLibrary1.BHO
{

    [
        ComVisible(true),
        Guid("34e09a14-2cda-4334-8f02-48f450fc0560"),
        ClassInterface(ClassInterfaceType.None),
        ProgId("MyExtension"),
        ComDefaultInterface(typeof(IExtension))
        ]

    public class BHO : IObjectWithSite, IExtension
    {
        public static string BHO_KEY = @"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects";

        WebBrowser webBrowser;
        HTMLDocument document;

        public void OnDocumentComplete(object pDisp, ref object URL)
        {

            // count = 1;
            //System.Windows.Forms.MessageBox.Show("1 OnDocumentComplete");
            //  System.Windows.Forms.MessageBox.Show("Don't interrupt me. I am thinking...");
            // 
            document = (HTMLDocument)webBrowser.Document;


            // 注册外部js函数
            /*
            try
            {
                dynamic window = ((IHTMLDocument2)webBrowser.Document).parentWindow;
                IExpando windowEx = (IExpando)window;
                PropertyInfo myExtension = windowEx.GetProperty("myExtension", BindingFlags.Default);
                if (myExtension == null)
                {
                    myExtension = windowEx.AddProperty("myExtension");
                }
                myExtension.SetValue(windowEx, this, null);
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show("Error : " + e.ToString());
            }
            */

            //this.regExtJS();


            //document.parentWindow.execScript("$(\"#ajaxget\").click(function() {alert(\"3333333333\");});", "JavaScript");


            //document.parentWindow.execScript("$(\"#extfun\").click(function() {window.myExtension.Foo('bar');});");
            //document.parentWindow.execScript("$(\"#count\").val(\"" + str + "\");");
            //append("document" + count);

            count = 1;

        }

        public void regExtFun()
        {
            // 注册外部js函数
            try
            {
                dynamic window = ((IHTMLDocument2)webBrowser.Document).parentWindow;
                IExpando windowEx = (IExpando)window;
                PropertyInfo myExtension = windowEx.GetProperty("myExtension", BindingFlags.Default);
                if (myExtension == null)
                {
                    myExtension = windowEx.AddProperty("myExtension");
                }
                myExtension.SetValue(windowEx, this, null);
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show("Error : " + e.ToString());
            }
        }

        public void regExtJs()
        {
            //append("regJs");
            document = (HTMLDocument)webBrowser.Document;
            document.parentWindow.execScript("$(\"#extfun\").click(function() {window.myExtension.Foo('bar');});");
            document.parentWindow.execScript(
                @"$(""#mobile"").blur(function() {
                    window.myExtension.Mobile(this.value);
                });"
            );
        }

        public void regRefresh()
        {
            //append("regRefresh");
            HTMLDocument doc = webBrowser.Document as HTMLDocument;
            if (doc != null)
            {
                IHTMLWindow2 tmpWindow = doc.parentWindow;
                if (tmpWindow != null)
                {
                    HTMLWindowEvents2_Event events = (tmpWindow as HTMLWindowEvents2_Event);
                    try
                    {
                        events.onload -= new HTMLWindowEvents2_onloadEventHandler(RefreshHandler);
                    }
                    catch { }
                    events.onload += new HTMLWindowEvents2_onloadEventHandler(RefreshHandler);
                }
            }
        }

        public void regExtension()
        {
            document = (HTMLDocument)webBrowser.Document;
            IHTMLElement e = document.getElementById("_BHO_");
            append(""+(e == null));
            
            if(e == null)
            {
                IHTMLElement body = document.body;
                body.insertAdjacentHTML("afterBegin", "<input id=\"_BHO_\" type=\"text\" value=\""+ DateTime.Now.ToString()+"\" />");
                append("regExtension");
                this.regExtFun();
                this.regExtJs();
            }
            
        }

        public void OnDownloadBegin()
        {
            append("downloadbegin");
        }

        public void OnDownloadComplete()
        {
            
            //System.Windows.Forms.MessageBox.Show(""+(count++));

            append("download"+count);
                this.regExtension();
            if (count == 1) {




                

                //document.parentWindow.execScript("$(\"#count\").val(\"" + sss + "\");");



                //IHTMLElement body = document.body;
                //document.parentWindow.execScript("$(\"#ajaxget\").click(function() {alert(\"44444444\");});", "JavaScript");



            }


            this.regRefresh();

            count = count + 1;

        }

        public string tips = " ";
        public void append(string str)
        {
            tips = tips + str + " ";
            document = (HTMLDocument)webBrowser.Document;
            //document.parentWindow.execScript("$(\"#count\").val(\"" + tips + "\");");
        }

        // 刷新事件
        public void RefreshHandler(IHTMLEventObj e)
        {
            
            //append("refresh"+count);
            count = 1;
        }

        public void OnBeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
        {
            count = 1;
        }

        public int count = 1;

        public void Foo(string s)
        {
            /*
            System.Windows.Forms.MessageBox.Show("已拦截到按钮的点击操作");
            str = "";
            append("Foo");
            document = (HTMLDocument)webBrowser.Document;
            string ss = "";
            foreach (IHTMLInputElement e in document.getElementsByTagName("input"))
            {
                if(e.type.Equals("text") || e.type.Equals("password"))
                ss = ss + "类型："+e.type+" 值: "+ e.value+"\r\n";
            }
            System.Windows.Forms.MessageBox.Show("已拦截到按钮的点击操作。。。\r\n" + ss);
            */

            //string result = callHttp();
            System.Windows.Forms.MessageBox.Show("GET: " + s);
        }

        

        public void Mobile(string s)
        {
            MobileThread mt = new MobileThread(s);
            new System.Threading.Thread(new System.Threading.ThreadStart(mt.http)).Start();
        }

        public string callHttp(string url)
        {
            WebRequest request = null;
            WebResponse response = null;
            try
            {
                //string url = "http://m.ip138.com/mobile.asp?mobile=18932916778";
                //string url = "http://192.168.1.7:8080/get.jsp";
                request = WebRequest.Create(url);
                response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                string line = reader.ReadToEnd();
                return line;
            }
            catch (Exception)
            {
                return "";
            }finally
            {
                if(response != null)
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

        public int SetSite(object site)
        {
            if (site != null)
            {
                //System.Windows.Forms.MessageBox.Show("site != null");
                webBrowser = (WebBrowser)site;
                webBrowser.DocumentComplete += new DWebBrowserEvents2_DocumentCompleteEventHandler(this.OnDocumentComplete);
                webBrowser.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(this.OnBeforeNavigate2);
                webBrowser.DownloadBegin += new DWebBrowserEvents2_DownloadBeginEventHandler(this.OnDownloadBegin);
                webBrowser.DownloadComplete += new DWebBrowserEvents2_DownloadCompleteEventHandler(this.OnDownloadComplete);
                
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("site == null");
                
                webBrowser.DocumentComplete -= new DWebBrowserEvents2_DocumentCompleteEventHandler(this.OnDocumentComplete);
                webBrowser.BeforeNavigate2 -= new DWebBrowserEvents2_BeforeNavigate2EventHandler(this.OnBeforeNavigate2);
                webBrowser.DownloadBegin -= new DWebBrowserEvents2_DownloadBeginEventHandler(this.OnDownloadBegin);
                webBrowser.DownloadComplete -= new DWebBrowserEvents2_DownloadCompleteEventHandler(this.OnDownloadComplete);
                webBrowser = null;
                
            }
            return 0;
        }

        public int GetSite(ref Guid guid, out IntPtr ppvSite)
        {
            IntPtr punk = Marshal.GetIUnknownForObject(webBrowser);
            int hr = Marshal.QueryInterface(punk, ref guid, out ppvSite);
            Marshal.Release(punk);
            return hr;
        }

        [ComRegisterFunction]
        public static void RegisterBHO(Type type)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BHO_KEY, true);
            if (registryKey == null)
            {
                registryKey = Registry.LocalMachine.CreateSubKey(BHO_KEY);
            }
            

            string guid = type.GUID.ToString("B");
            RegistryKey ourKey = registryKey.OpenSubKey(guid, true);
            if (ourKey == null)
            {
                ourKey = registryKey.CreateSubKey(guid);
            }

            ourKey.SetValue("NoExplorer", 1);
            //ourKey.SetValue("Alright", 1);

            registryKey.Close();
            ourKey.Close();
            
        }

        [ComUnregisterFunction]
        public static void UnregisterBHO(Type type)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BHO_KEY, true);
            string guid = type.GUID.ToString("B");
            if (registryKey != null)
            {
                registryKey.DeleteSubKey(guid, false);
            }
        }

    }
}
