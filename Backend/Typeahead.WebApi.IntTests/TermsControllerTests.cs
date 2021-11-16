using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Typeahead.WebApi.Model;
using Xunit;

namespace Typeahead.WebApi.IntTests
{
    public class TermsControllerTests : IntegrationTestsBase<Startup>
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("v")]
        [InlineData("ve")]
        [InlineData("nonexistingword")]
        [InlineData("verylongstringabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        public async Task GetFilteredTermsNotFound(string filter)
        {
            var response = await Client.GetAsync("/terms/" + filter);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("micr", 10)]
        public async Task GetFilteredTermsSuccess(string filter, int expectedCount)
        {
            var response = await Client.GetAsync("/terms/" + filter);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var rawContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Term>>(rawContent);
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(expectedCount);
            result.Should().NotContain(x => !x.Name.Contains(filter));
        }
    }
}