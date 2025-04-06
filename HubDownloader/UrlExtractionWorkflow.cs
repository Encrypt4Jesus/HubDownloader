using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace HubDownloader
{
    public static class UrlExtractionWorkflow
    {
        public static event NewResultsAvailableEventHandler NewResultsAvailable;

        public static WebDriverService WebdriverInstance
        {
            get
            {
                if (_webdriverInstance == null)
                {
                    _webdriverInstance = new WebDriverService();
                    Driver = _webdriverInstance.Driver;
                }
                return _webdriverInstance;
            }
        }
        private static WebDriverService _webdriverInstance = null;

        public static bool IsProcessing
        {
            get { return _isProcessing; }
        }
        private static bool _isProcessing = false;
        private static object _lockObject = new object();

        private static IWebDriver Driver;

        private static bool IsShutdown
        {
            get
            {
                if (_webdriverInstance == null)
                {
                    return true;
                }
                else
                {
                    return _webdriverInstance.IsShutdown;
                }
            }
        }

        public static void Shutdown()
        {
            if (Driver != null)
            {
                Driver.Dispose();
                Driver = null;
            }
            if (_webdriverInstance != null)
            {
                if (!_webdriverInstance.IsShutdown)
                {
                    _webdriverInstance.Shutdown();
                }
                _webdriverInstance = null;

                LoggingBuffer.Dispose();
            }
        }

        private static void SetIsProcessing(bool value)
        {
            lock (_lockObject)
            {
                _isProcessing = value;
            }
        }

        private static void RaiseNewResultsAvailable(UrlExtractionResultObject results)
        {
            NewResultsAvailable?.Invoke(null, new NewResultsAvailableEventArgs(results));
        }

        private static bool IsFirstRun = true;
        private static ThreadLocal<StringBuilder> LoggingBuffer = new ThreadLocal<StringBuilder>(() => new StringBuilder());

        public static UrlExtractionResultObject ExtractUrlFromViewKey(string viewkey, RequestedVideoQuality requestedQuality, FallBackVideoQuality fallBackVideoQuality)
        {
            if (IsFirstRun)
            {
                IsFirstRun = false;
                _webdriverInstance = new WebDriverService();
                Driver = _webdriverInstance.Driver;
            }

            string resultUrl = null;
            string resultName = null;
            LoggingBuffer.Value.Clear();
            UrlExtractionResultObject result = new UrlExtractionResultObject();
            try
            {
                SetIsProcessing(true);

                string requestUri = string.Format(InternalSettings.HubAddressFormat, viewkey);

                if (IsShutdown)
                {
                    result.ErrorReason = ExtractionWorkflowResult.WebdriverDisposed;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }

                // Get JSON Metadata about the video
                string scriptTag_InnerHtml = UrlExtractionHelperMethods.ExtractHtmlElementByXPath(Driver, requestUri, InternalSettings.JsonMetadata_XPath);
                if (scriptTag_InnerHtml == null)
                {
                    result.ErrorReason = ExtractionWorkflowResult.JsonMetadataNotFound;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }

                if (IsShutdown)
                {
                    result.ErrorReason = ExtractionWorkflowResult.WebdriverDisposed;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }
                string saveFileName = Driver.Title.Replace(" ", "_");
                saveFileName = new string(saveFileName.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
                int lastWordIndex = saveFileName.LastIndexOf('_');
                saveFileName = saveFileName.Substring(0, lastWordIndex);
                resultName = saveFileName;

                string json = UrlExtractionHelperMethods.ExtractJsonFromJavaScript(scriptTag_InnerHtml, InternalSettings.Json1_Extract_StartToken, InternalSettings.Json1_Extract_EndToken).Trim(';');
                if (json == null)
                {
                    result.ErrorReason = ExtractionWorkflowResult.JsonExtractionFailure;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }

                LoggingBuffer.Value.AppendLine("Extracted JSON from landing page:");
                LoggingBuffer.Value.AppendLine();
                LoggingBuffer.Value.AppendLine("<JSON>");
                LoggingBuffer.Value.AppendLine(json);
                LoggingBuffer.Value.AppendLine("</JSON>");
                LoggingBuffer.Value.AppendLine();

                // Parse JSON, extract media URLs
                Dictionary<string, string> Video_QualityKey_UrlValue_Dictionary = UrlExtractionHelperMethods.ExtractMediaUrlsFromJson(json);
                if (Video_QualityKey_UrlValue_Dictionary == null)
                {
                    result.ErrorReason = ExtractionWorkflowResult.JsonExtractionFailure;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }

                LoggingBuffer.Value.AppendLine();
                LoggingBuffer.Value.AppendLine("Extracted Media URLs:");
                LoggingBuffer.Value.AppendLine(string.Join(Environment.NewLine, Video_QualityKey_UrlValue_Dictionary.Select(kvp => kvp.Value)));
                LoggingBuffer.Value.AppendLine();

                string nextResource = Video_QualityKey_UrlValue_Dictionary[InternalSettings.MediaAddressDictionary_Key]; // The URL with the key = "[]" is for the .mp4 resource

                LoggingBuffer.Value.AppendLine("Selected Resource URL:");
                LoggingBuffer.Value.AppendLine(nextResource);
                LoggingBuffer.Value.AppendLine();

                if (IsShutdown)
                {
                    result.ErrorReason = ExtractionWorkflowResult.WebdriverDisposed;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }
                // Get the JSON 
                string videoJson = UrlExtractionHelperMethods.GetHtmlPageSource(Driver, nextResource);
                if (videoJson == null)
                {
                    result.ErrorReason = ExtractionWorkflowResult.JsonMetadataNotFound;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }

                LoggingBuffer.Value.AppendLine("2nd JSON (raw):");
                LoggingBuffer.Value.AppendLine(videoJson);
                LoggingBuffer.Value.AppendLine();

                string json2 = UrlExtractionHelperMethods.ExtractJsonFromJavaScript(videoJson, InternalSettings.Json2_Extract_StartToken, InternalSettings.Json2_Extract_EndToken) + "]";
                if (json2 == null)
                {
                    result.ErrorReason = ExtractionWorkflowResult.JsonExtractionFailure;
                    result.DebugInformation = LoggingBuffer.Value;
                    return result;
                }

                LoggingBuffer.Value.AppendLine("2nd JSON (extracted):");
                LoggingBuffer.Value.AppendLine(json2);
                LoggingBuffer.Value.AppendLine();

                Dictionary<string, string> VideoQualityKey_UrlValue_Dictionary = new Dictionary<string, string>();

                // Place into dictionary
                JArray jArrVids = Newtonsoft.Json.Linq.JArray.Parse(json2);
                foreach (JToken obj in jArrVids)
                {
                    string qualityKey = obj[InternalSettings.JsonMediaSection_QualityProperty].ToString();
                    string urlValue = obj[InternalSettings.JsonMediaSection_VideoProperty].ToString();
                    VideoQualityKey_UrlValue_Dictionary.Add(qualityKey, urlValue);
                }

                // Attempt to get the URL corresponding to the quality requested.
                string key = UrlExtractionHelperMethods.GetEnumDescription(requestedQuality);
                if (!VideoQualityKey_UrlValue_Dictionary.ContainsKey(key))
                {
                    var kvpArray = VideoQualityKey_UrlValue_Dictionary.ToArray();
                    if (fallBackVideoQuality == FallBackVideoQuality.Lowest)
                    {
                        key = kvpArray.First().Key;
                    }
                    else
                    {
                        key = kvpArray.Last().Key;
                    }
                }

                // Grab the URL to download from the Dictionary
                resultUrl = VideoQualityKey_UrlValue_Dictionary[key];
                resultUrl = resultUrl.Replace("&amp;", "&");

                LoggingBuffer.Value.AppendLine("Download:");
                LoggingBuffer.Value.AppendLine(resultName);
                LoggingBuffer.Value.AppendLine("@");
                LoggingBuffer.Value.AppendLine(resultUrl);
                LoggingBuffer.Value.AppendLine();
            }
            catch
            {
                result.ErrorReason = ExtractionWorkflowResult.Unknown;
                result.DebugInformation = LoggingBuffer.Value;
                return result;
            }
            finally
            {
                SetIsProcessing(false);
            }

            result = new UrlExtractionResultObject(resultName, resultUrl);
            RaiseNewResultsAvailable(result);
            return result;
        }
    }
}
