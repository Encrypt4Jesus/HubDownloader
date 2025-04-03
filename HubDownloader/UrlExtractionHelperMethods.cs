using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace HubDownloader
{
    public static class UrlExtractionHelperMethods
    {
        public static string ExtractHtmlElementByXPath(IWebDriver driver, string resourceUrl, string xpath)
        {
            try
            {
                driver.Navigate().GoToUrl(resourceUrl);

                // XPath: //*[@id="player"]/script[1]
                var elementByXPath = driver.FindElement(By.XPath(xpath));
                string xpath_InnerHtml = elementByXPath.GetAttribute("innerText"); // innerHTML

                return xpath_InnerHtml.Trim();
            }
            catch
            {
                return null;
            }
        }


        public static string ExtractJsonFromJavaScript(string scriptTag_InnerHtml, string startToken, string endToken)
        {
            try
            {
                int startIndex = scriptTag_InnerHtml.IndexOf(startToken, StringComparison.OrdinalIgnoreCase);
                if (startIndex == -1)
                {
                    return null;
                }

                string trimmed = scriptTag_InnerHtml.Remove(0, startIndex);

                int endIndex = trimmed.LastIndexOf(endToken);
                if (endIndex == -1)
                {
                    return null;
                }

                return trimmed.Substring(0, endIndex).Trim();
            }
            catch
            {
                return null;
            }
        }


        public static Dictionary<string, string> ExtractMediaUrlsFromJson(string json)
        {
            try
            {
                JObject jObj = Newtonsoft.Json.Linq.JObject.Parse(json);

                if (!jObj.ContainsKey(InternalSettings.JsonMediaSection_PropertyName))
                {
                    return null;
                }

                JToken? jArr = jObj[InternalSettings.JsonMediaSection_PropertyName];
                return JTokenToDictionary(jArr);
            }
            catch
            {
                return null;
            }
        }

        public static Dictionary<string, string> JTokenToDictionary(JToken jArr)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (JToken obj in jArr)
            {
                result.Add(obj[InternalSettings.JsonMediaSection_QualityProperty].ToString(), obj[InternalSettings.JsonMediaSection_VideoProperty].ToString());
            }

            return result;
        }

        public static string GetHtmlPageSource(IWebDriver driver, string resourceUrl)
        {
            try
            {
                driver.Navigate().GoToUrl(resourceUrl);
                return driver.PageSource;
            }
            catch
            {
                return null;
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
