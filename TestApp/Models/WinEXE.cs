using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class WinEXE : IRVEXE
    {
        const string myUrl = "https://apps.autodesk.com/RVT/en/Detail/Index?id=3837838607913367957";

        public WinEXE()
        {
            TitleValue = $"Version Info: {GetUrlData(url: myUrl)}";
        }

        public Action ToDo()
        {
            return new Action(RevitStart);
        }

        public string TitleValue { get; set; }
        public string LabelContent { get; set; } = "Введите текст...";
        public string TextBoxValue { get; set; } = "Введите текст...";
        public string ButtonContent { get; set; } = "Запустить Revit";

        private void RevitStart() => Process.Start(GetRevit());

        /// <summary>
        /// Поиск Revit на компьютере и путей к нему
        /// </summary>
        /// <returns></returns>
        private static string GetRevit()
        {
            string value64 = string.Empty;
            RegistryKey autodeskRegKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            autodeskRegKey = autodeskRegKey.OpenSubKey("SOFTWARE\\Autodesk");

            var revitsRegKey = autodeskRegKey?.OpenSubKey("Revit");
            var list1 = revitsRegKey.GetSubKeyNames();

            try
            {
                foreach (string str in list1)
                {
                    var refKey = revitsRegKey?.OpenSubKey(str);
                    var list2 = refKey.GetSubKeyNames();

                    foreach (string str1 in list2.Where(x => x.Split('-')[0] == "REVIT"))
                    {
                        var refSubKey = refKey?.OpenSubKey(str1);
                        return $"{refSubKey.GetValue("InstallationLocation", string.Empty).ToString()}revit.exe";
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// Поиск версии плагина на сайте
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetUrlData(string url)
        {
            try
            {
                using (HttpClientHandler hdl = new HttpClientHandler { })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        using (HttpResponseMessage resp = clnt.GetAsync(url).Result)
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                var html = resp.Content.ReadAsStringAsync().Result;
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(html);

                                    HtmlNodeCollection versions = doc.DocumentNode.SelectNodes(".//div[@class='download-info-wrapper']//div[@class='download-info break-word']"); //<div class="download-info break-word">
                                    if (versions != null && versions.Count > 0)
                                    {
                                        foreach (var nd in versions)
                                        {
                                            HtmlNode versionProp = nd.SelectSingleNode(".//div[@class='property']");

                                            if (versionProp.InnerHtml == "Version Info:")
                                            {
                                                HtmlNode versionNum = nd.SelectSingleNode(".//div[@class='value']");
                                                return versionNum.InnerHtml;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
