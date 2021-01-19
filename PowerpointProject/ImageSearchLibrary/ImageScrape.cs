/// Copyright © 2021, Katelyn Rule 
/// License: GPL3
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace PowerpointProject
{

    /// There exists 3rd party libraries that you can use, 
    /// but the free ones are broken.
    /// This is free, but is not guaranteed to work forever,
    /// but it worked when I uploaded it. 

    public class ImageSearch
    {
        /// <summary>
        /// Simple request formatter.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string FormatMultipleSearchEntries(string key)
        {
            string ret = "";
            bool bLastWhite = false;
            foreach (var c in key.Trim())
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!bLastWhite)
                        ret += "+";
                    bLastWhite = true;
                }
                else
                {
                    ret += c;
                    bLastWhite = false;
                }
            }

            return ret;
        }

        /// <summary>
        /// Cheezy Bing Image Search Screen scraper
        /// </summary>
        /// <param name="key">single word key for search</param>
        /// <returns></returns>
        public static List<string> Search(string key, string suffix = "jpg")
        {
            try
            {
                key = FormatMultipleSearchEntries(key);
                var urls = new List<string>();

                // Make Request
                var queryURL =
                    "https://www.bing.com/images/search?q=" + key + "&qs=n&pq=" + key +
                    "&sc=8-5&first=1&tsc=ImageBasicHover";

                var request = (HttpWebRequest)HttpWebRequest.Create(queryURL);

                // Get Response
                var response = (HttpWebResponse)request.GetResponse();
                var htmlSource = new StreamReader(response.GetResponseStream());
                var r = htmlSource.ReadToEnd();

                // We don't care about parsing this mess. All we want is some
                // jpg links that came back. Returned clickable image responses are in hrefs.
                // So we will find them and strip out the URLs and return them.
                foreach (Match match in Regex.Matches(r, "href=\"([^\"]*)\""))
                {
                    var url = match.Groups[1].Value;
                    if (url.Contains(suffix) && url.Substring(url.Length - 3) == suffix)
                    {
                        urls.Add(url);
                    }
                }

                return urls;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
