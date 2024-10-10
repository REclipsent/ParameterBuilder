using System;
using System.Collections.Generic;
using System.Text;

namespace ParameterBuilder
{
    public class ParamBuilder
    {
        /// <summary>
        ///     Dictionary of String Keys representing the query parameter and Object Values representing the value of the query parameter
        /// </summary>
        public IDictionary<string, object> ParamsDictionary { get; set; }
        /// <summary>
        ///     Uri that gets appended to the front of the query parameters in certain methods 
        /// </summary>
        public Uri BaseUri { get; set; }
        /// <summary>
        ///     Initialize a new instance of the ParamBuilder class.
        /// </summary>
        /// <param name="ParamsDictionary"></param>
        /// <param name="BaseUri"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ParamBuilder(IDictionary<string, object> ParamsDictionary, Uri BaseUri = null)
        {
            if (ParamsDictionary is null) throw new ArgumentNullException(nameof(ParamsDictionary));

            this.ParamsDictionary = ParamsDictionary;
            this.BaseUri = BaseUri;
        }
        /// <summary>
        ///     Creates Uri with passed BaseUri
        /// </summary>
        /// <returns>Uri of endpoint with query parameters</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Uri GetUri()
        {
            if (BaseUri is null) throw new InvalidOperationException($"Define {nameof(BaseUri)} in construction or by setting the property");

            return new Uri(CreateUri(BaseUri.ToString()));
        }
        /// <summary>
        ///     Creates Uri with passed BaseUri
        /// </summary>
        /// <param name="baseUri">Uri of endpoint</param>
        /// <returns>Uri of endpoint with query parameters</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Uri GetUri(Uri baseUri)
        {
            if (baseUri is null) throw new ArgumentNullException(nameof(baseUri));

            return new Uri(CreateUri(baseUri.ToString()));
        }
        /// <summary>
        ///     Creates String version of the Uri with passed BaseUri
        /// </summary>
        /// <returns>String version of the Uri of endpoint with query parameters</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetUriString()
        {
            if (BaseUri is null) throw new InvalidOperationException($"Define {nameof(BaseUri)} in construction or by setting the property");

            return CreateUri(BaseUri.ToString());
        }
        /// <summary>
        ///     Creates Uri with passed BaseUri
        /// </summary>
        /// <param name="baseUri">Uri of endpoint</param>
        /// <returns>Uri of endpoint with query parameters</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string GetUriString(string baseUri)
        {
            if (baseUri is null) throw new ArgumentNullException(nameof(baseUri));

            return CreateUri(baseUri);
        }
        /// <summary>
        /// Creates just the query part of the Uri
        /// </summary>
        /// <returns>Query String</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetQuery()
        {
            if (ParamsDictionary is null) throw new InvalidOperationException();

            return CreateQuery(ParamsDictionary);
        }
        /// <summary>
        /// Creates just the query part of the Uri
        /// </summary>
        /// <param name="paramsDictionary">Dictionary of String Keys representing the query parameter and Object Values representing the value of the query parameter</param>
        /// <returns>Query String</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetQuery(IDictionary<string, object> paramsDictionary)
        {
            if (paramsDictionary is null) throw new ArgumentNullException(nameof(paramsDictionary));

            return CreateQuery(paramsDictionary);
        }
        private static string CreateQuery(IDictionary<string, object> paramsDictionary)
        {
            StringBuilder query = new StringBuilder();
            query.Append("?");

            int amountOfParams = paramsDictionary.Count;

            int i = 1;
            foreach (var param in paramsDictionary)
            {
                if (param.Key is null) continue;

                query.Append(param.Key.Trim());
                query.Append("=");
                query.Append(param.Value.ToString().Trim());

                if (i++ < amountOfParams)
                {
                    query.Append("&");
                }
            }

            return query.ToString();
        }
        private string CreateUri(string baseUri)
        {
            StringBuilder uri = new StringBuilder();

            uri.Append(baseUri);
            uri.Append(CreateQuery(ParamsDictionary));

            return uri.ToString();
        }
    }
}
