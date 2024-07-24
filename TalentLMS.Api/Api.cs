using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using TalentLMS.Api.Courses;
using Microsoft.Extensions.DependencyInjection;

namespace TalentLMS.Api
{

    public partial interface ITalentApi
    {
        HttpClient Client { get; }  // refit magic
    }

    public class TalentApi {

        private readonly string _talentLmsApiRoot;
        private readonly string _apiKey;
        private readonly bool _logging;

        public TalentApi(string talentLmsApiRoot, string apiKey, bool logging = false)
        {
            _talentLmsApiRoot = talentLmsApiRoot;
            _apiKey = apiKey;
            _logging = logging;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(serviceProvider => new AuthHeaderHandler(_apiKey));
            services.AddTransient<HttpLoggingHandler>();
            var refitClient = 
            services.AddRefitClient<ITalentApi>().ConfigureHttpClient(services =>
            {
                services.BaseAddress = new Uri(_talentLmsApiRoot);
            }).AddHttpMessageHandler<AuthHeaderHandler>();

            if (_logging)
            {
                refitClient.AddHttpMessageHandler<HttpLoggingHandler>();
            }
        }
    }

    class AuthHeaderHandler : DelegatingHandler
    {
        private readonly string _apiKey;

        public AuthHeaderHandler(string apiKey)
        {
            _apiKey = apiKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _apiKey);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}