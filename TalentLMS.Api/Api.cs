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
        private readonly string _auth;
        private readonly bool _logging;

        public TalentApi(string talentLmsApiRoot, string apiKey, bool logging = false)
        {
            _talentLmsApiRoot = talentLmsApiRoot;
            _logging = logging;

            string credentials = $"{apiKey}:"; // talent only uses api key as username without password
            byte[] bytes = Encoding.ASCII.GetBytes(credentials);
            string _auth = Convert.ToBase64String(bytes);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(serviceProvider => new AuthHeaderHandler(_auth));
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
        private readonly string _auth;

        public AuthHeaderHandler(string auth)
        {
            _auth = auth;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _auth);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}