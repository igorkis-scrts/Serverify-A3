using System;
using System.IO;
using System.Net;

namespace Interchangeable.Helpers
{
    /// <summary>
    /// Simple helper class to download my github avatar :(
    /// </summary>
    public class DownloadHelper
    {
        /// <summary>
        /// D
        /// </summary>
        /// <param name="url">Url to file</param>
        /// <returns>Array of bytes with content</returns>
        public Stream DownloadFile(string url)
        {
            try
            {
                var request = WebRequest.Create(url);
                var response = request.GetResponse();

                return response.GetResponseStream();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
