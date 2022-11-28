﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace ConsoleApp.Services
{
    internal interface IClientCredentialService
    {
        Task<AuthenticationResult> GetAuthenticationResult(IEnumerable<string>? scopes = null);
        Task<string> GetAccessToken(IEnumerable<string>? scopes = null);
    }

    internal class ClientCredentialService : IClientCredentialService
    {
        private readonly ILogger<ClientCredentialService> _logger;
        private readonly IConfidentialClientApplication _confidentialClientApplication;

        public ClientCredentialService(
            ILogger<ClientCredentialService> logger,
            IOptions<ConfidentialClientApplicationOptions> confidentialClientApplicationOptions)
        {
            _logger = logger;
            _confidentialClientApplication = ConfidentialClientApplicationBuilder
                .CreateWithApplicationOptions(confidentialClientApplicationOptions.Value)
                .Build();
        }

        public async Task<string> GetAccessToken(IEnumerable<string>? scopes = null)
        {
            var result = await GetAuthenticationResult(scopes);
            return result.AccessToken;
        }

        public async Task<AuthenticationResult> GetAuthenticationResult(IEnumerable<string>? scopes = null)
        {
            AuthenticationResult? result = null;
            scopes ??= new[] { ".default" };
            try
            {
                _logger.LogDebug($"Acquiring token with scopes: '{string.Join(" ", scopes)}'");
                result = await _confidentialClientApplication
                    .AcquireTokenForClient(scopes)
                    .ExecuteAsync();
                _logger.LogDebug("Acquired token");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to acquire token");
                throw;
            }
            return result;
        }
    }
}

