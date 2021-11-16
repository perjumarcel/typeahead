using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Typeahead.WebApi.IntTests
{
    public abstract class IntegrationTestsBase<TStartup> : IAsyncLifetime where TStartup : class
    {
        private readonly WebApplicationFactory<TStartup> _factory;

        protected virtual string GetTestSettingsJsonPath()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.integrationtests.json");
            return configPath;
        }

        protected IntegrationTestsBase() : this(typeof(TStartup).Assembly.GetName().Name)
        {
        }

        protected IntegrationTestsBase(string projectRelativePath)
        {
            _factory = new AsyncFriendlyWebApplicationFactory<TStartup>().WithWebHostBuilder(
                b =>
                {
                    b.UseSolutionRelativeContentRoot(projectRelativePath).UseEnvironment("Local").ConfigureAppConfiguration(
                        (ctx, builder) =>
                        {
                            var configPath = GetTestSettingsJsonPath();
                            if (!string.IsNullOrWhiteSpace(configPath))
                            {
                                builder.AddJsonFile(configPath, optional: true);
                            }
                        });
                });

            Client = _factory.CreateDefaultClient(new Uri("http://example.com/"));
            Services = _factory.Services;
        }

        protected IServiceProvider Services { get; }

        protected HttpClient Client { get; }

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task DisposeAsync()
        {
            _factory.Dispose();
            return Task.CompletedTask;
        }

        private class AsyncFriendlyWebApplicationFactory<T> : WebApplicationFactory<T> where T : class
        {
            protected override IHost CreateHost(IHostBuilder builder)
            {
                var host = builder.Build();
                Task.Run(() => host.StartAsync()).GetAwaiter().GetResult();
                return host;
            }
        }
    }
}