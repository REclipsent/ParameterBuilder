using System;
using System.Collections.Generic;
using System.Text;

namespace ParameterBuilder
{
    public class ParamBuilder
    {
        public IDictionary<string, object> ParamsDictionary { get; set; }
        public Uri BaseUri { get; set; }
        public ParamBuilder(IDictionary<string, object> ParamsDictionary, Uri BaseUri = null)
        {
            this.ParamsDictionary = ParamsDictionary;
            this.BaseUri = BaseUri;
        }
        public Uri GetUri() 
        {
            if (BaseUri is null) throw new ArgumentNullException($"Define {nameof(BaseUri)} in construction or by setting the property");

            return new Uri(CreateUri(BaseUri.ToString()));
        }
        public Uri GetUri(Uri baseUri)
        {
            return new Uri(CreateUri(baseUri.ToString()));
        }
        public string GetUriString()
        {
            if (BaseUri is null) throw new ArgumentNullException($"Define {nameof(BaseUri)} in construction or by setting the property");

            return CreateUri(BaseUri.ToString());
        }
        public string GetUriString(string baseUri)
        {
            return CreateUri(baseUri);
        }

        public string GetQuery()
        {
            return CreateQuery();
        }

        private string CreateQuery()
        {
            StringBuilder query = new StringBuilder();
            query.Append("?");

            int amountOfParams = ParamsDictionary.Count;

            int i = 1;
            foreach (var param in ParamsDictionary)
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
            uri.Append(CreateQuery());

            return uri.ToString();
        }
    }
}
