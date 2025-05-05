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
        public static string ViewKeyAllowedCharacters = "0123456789abcdefhp";

        public static string IsolateViewkey(string input)
        {
            // https://www.pornhub.com/view_video.php?viewkey=ph69c693d69e695
            // Why do we do it this way? In case they enter part of the url, like view_video.php?viewkey=ph69c693d69e695
            string result = input.Replace("https://", "");
            result = result.Replace("http://", "");
            result = result.Replace("www.", "");
            result = result.Replace("pornhub.com", "");
            result = result.Replace("/", "");
            result = result.Replace("view_video.php", "");
            result = result.Replace("?", "");
            result = result.Replace("viewkey", "");
            result = result.Replace("=", "");
            return result;
        }

        public static bool LooksLikeUrlOrViewkey(string input)
        {
            // https://www.pornhub.com/view_video.php?viewkey={viewkey}
            // or viewkey
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            string sanitized = IsolateViewkey(input); // Just strip it out, dont even bother matching

            return LooksLikeViewkey(sanitized);
        }

        public static bool LooksLikeViewkey(string input)
        {
            // 13 characters long
            // contains only 0123456789abcdef
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            if (input.Length != 13 && input.Length != 15)
            {
                return false;
            }
            string lower = input.ToLower();
            if (!lower.All(c => ViewKeyAllowedCharacters.Contains(c)))
            {
                return false;
            }
            return true;
        }

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
