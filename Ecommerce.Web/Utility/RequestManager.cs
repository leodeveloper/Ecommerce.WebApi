using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using static Ecommerce.Web.Utility.UtilityEnum;

namespace Ecommerce.Web.Utility
{
    public class RequestManager : IGetRequestManager, IPostRequestManager, IPutRequestManager, IDeleteRequestManager
    {
        #region Public Method
        private readonly ILogger _ilogger;

        public RequestManager(ILogger<RequestManager> ilogger)
        {
            _ilogger = ilogger;
        }

        public string SendPostRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers)
        {
            try
            {
                HttpWebRequest request = GenerateRequest(uri, content, RequestMethod.Post, null, null, allowAutoRedirect, contentType, headers);
                HttpWebResponse response = GetResponse(request);
                string responseContent = GetResponseContent(response);
                return responseContent;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message, ex);
                return null;
            }

        }

        public string SendPutRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers)
        {
            try
            {
                HttpWebRequest request = GenerateRequest(uri, content, RequestMethod.Put, null, null, allowAutoRedirect, contentType, headers);
                HttpWebResponse response = GetResponse(request);
                string responseContent = GetResponseContent(response);
                return responseContent;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message, ex);
                return null;
            }

        }

        public string SendDeleteRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers)
        {
            try
            {
                HttpWebRequest request = GenerateRequest(uri, null, RequestMethod.Delete, null, null, allowAutoRedirect, contentType, headers);
                HttpWebResponse response = GetResponse(request);
                string responseContent = GetResponseContent(response);
                return responseContent;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message, ex);
                return null;
            }

        }

        public string SendGetRequest(string uri, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers)
        {
            try
            {
                HttpWebRequest request = GenerateRequest(uri, null, RequestMethod.Get, null, null, allowAutoRedirect, contentType, headers);
                HttpWebResponse response = GetResponse(request);
                string responseContent = GetResponseContent(response);
                return responseContent;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message, ex);
                return null;
            }

        }
        #endregion

        #region Private methods
        private string GetResponseContent(HttpWebResponse response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }
            Stream dataStream = null;
            StreamReader reader = null;
            string responseFromServer = null;

            try
            {
                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Cleanup the streams and the response.
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (dataStream != null)
                {
                    dataStream.Close();
                }
                response.Close();
            }

            return responseFromServer;
        }

        private HttpWebRequest GenerateRequest(string uri, string content, RequestMethod method, string login, string password, bool allowAutoRedirect, string contentType, HeadersDto headers)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/x-www-form-urlencoded";
            }
            // Create a request using a URL that can receive a post. 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            // Set the Method property of the request to POST.
            request.Method = method.ToString();
            request.AllowAutoRedirect = allowAutoRedirect;
            //Default proxy does not work thats why set to null
            request.Proxy = null;
            //request.Credentials = new NetworkCredential("muhammads", "Leo4580@");
            //request.Proxy.Credentials = new NetworkCredential("muhammads", "Leo4580@");

            if (headers != null)
            {
                if (headers.Headers?.Count > 0)
                {
                    foreach (var header in headers.Headers)
                    {
                        request.Headers.Add(header.Name + " " + header.Value);
                    }
                }
            }

            // If login is empty use defaul credentials
            if (string.IsNullOrEmpty(login))
            {
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
            else
            {
                request.Credentials = new NetworkCredential(login, password);
            }
            if (method == RequestMethod.Post)
            {
                // Convert POST data to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                // Set the ContentType property of the WebRequest.
                request.ContentType = contentType;
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();
            }
            return request;
        }


        private HttpWebResponse GetResponse(HttpWebRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webex)
            {
                string webExceptionMessage = webex.Message;
                if (webex?.Response != null)
                {
                    using (WebResponse webExceptionResponse = webex.Response)
                    {
                        if (webExceptionResponse != null)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)webExceptionResponse;
                            using (Stream data = webExceptionResponse.GetResponseStream())
                            using (var reader = new StreamReader(data))
                            {
                                webExceptionMessage = reader.ReadToEnd();
                            }
                        }
                    }
                }
                throw new Exception(webExceptionMessage, webex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return response;
        }
        #endregion
    }
}