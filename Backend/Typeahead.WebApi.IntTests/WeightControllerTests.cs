using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Typeahead.WebApi.Data;
using Typeahead.WebApi.Model;
using Xunit;

namespace Typeahead.WebApi.IntTests
{
    public class WeightControllerTests : IntegrationTestsBase<Startup>
    {
        [Theory]
        [InlineData(5, "mic")]
        [InlineData(2, "mic")]
        [InlineData(4, "mic")]
        [InlineData(2, "micr")]
        [InlineData(3, "micr")]
        public async Task IncreaseWeightNoContent(int termId, string input)
        {
            var currentWeight = await WeightCount(termId, input);

            var response = await PostAsJsonAsync(termId, input);
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var newWeight = await WeightCount(termId, input);
            newWeight.Count.Should().Be(currentWeight.Count + 1);
        }

        [Theory]
        [InlineData(0, "mic")]
        [InlineData(-1, "mic")]
        [InlineData(1, "")]
        [InlineData(1, "m")]
        [InlineData(1, "mi")]
        [InlineData(1, null)]
        [InlineData(1, "verylongscringabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")]
        public async Task IncreaseWeightBadRequest(int termId, string input)
        {
            var response = await PostAsJsonAsync(termId, input);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private async Task<Weight> WeightCount(int termId, string input)
        {
            var options = Services.GetService<TermsDataOptions>();
            if (options == null)
            {
                throw new NullReferenceException(nameof(options));
            }

            var repository = new TestTermsRepository(options);
            return await repository.GetWeight(termId, input);
        }

        private async Task<HttpResponseMessage> PostAsJsonAsync(int termId, string input)
        {
            var weightInput = new WeightInput { TermId = termId, Input = input };
            var json = JsonConvert.SerializeObject(weightInput);
            var stringContent = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await Client.PostAsync("/weight", stringContent);
            return response;
        }
    }
}