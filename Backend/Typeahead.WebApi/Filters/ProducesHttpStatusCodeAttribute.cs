using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Typeahead.WebApi.Filters
{
    public class ProducesHttpStatusCodeAttribute : ProducesResponseTypeAttribute
    {
        public ProducesHttpStatusCodeAttribute(HttpStatusCode statusCode)
            : base((int) statusCode)
        {
        }
    }
}