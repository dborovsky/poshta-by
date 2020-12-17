using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using PoshtaBy.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PoshtaBy.Application;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace PoshtaBy.Infrastructure.Helpers
{
    public partial class WebHelper : IWebHelper
    {
        private const string NullIpAddress = "::1";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HostingConfig _hostingConfig;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public WebHelper(
            IHttpContextAccessor httpContextAccessor,
            HostingConfig hostingConfig,
            IHostApplicationLifetime applicationLifetime
            )
        {
            _hostingConfig = hostingConfig;
            _httpContextAccessor = httpContextAccessor;
            _applicationLifetime = applicationLifetime;
        }

        public virtual string GetAgencyLocation(bool? useSsl = null)
        {
            var agencyLocation = string.Empty;

            //get store host
            var agencyHost = GetAgencyHost(useSsl ?? IsCurrentConnectionSecured());
            if (!string.IsNullOrEmpty(agencyHost))
            {
                //add application path base if exists
                agencyLocation = IsRequestAvailable() ? $"{agencyHost.TrimEnd('/')}{_httpContextAccessor.HttpContext.Request.PathBase}" : agencyHost;
            }

            //if host is empty (it is possible only when HttpContext is not available), use URL of a store entity configured in admin area
            if (string.IsNullOrEmpty(agencyHost) && DataSettingsHelper.DatabaseIsInstalled())
            {
                var currentStore = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IAgencyContext>().CurrentAgency;
                if (currentStore != null)
                    agencyLocation = !currentStore.SslEnabled ? currentStore.Url : currentStore.SecureUrl;
                else
                    throw new Exception("Current agency cannot be loaded");
            }

            //ensure that URL is ended with slash
            agencyLocation = $"{agencyLocation.TrimEnd('/')}/";

            return agencyLocation;
        }


        public virtual string GetAgencyHost(bool useSsl)
        {
            if (!IsRequestAvailable())
                return string.Empty;

            var hostHeader = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Host];
            if (StringValues.IsNullOrEmpty(hostHeader))
                return string.Empty;

            var storeHost = $"{(useSsl ? Uri.UriSchemeHttps : Uri.UriSchemeHttp)}://{hostHeader.FirstOrDefault()}";

            storeHost = $"{storeHost.TrimEnd('/')}/";

            return storeHost;
        }

        public virtual bool IsRequestAvailable()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request == null)
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public virtual bool IsCurrentConnectionSecured()
        {
            if (!IsRequestAvailable())
                return false;

            if (_hostingConfig.UseHttpClusterHttps)
                return _httpContextAccessor.HttpContext.Request.Headers["HTTP_CLUSTER_HTTPS"].ToString().Equals("on", StringComparison.OrdinalIgnoreCase);

            if (_hostingConfig.UseHttpXForwardedProto)
                return _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-Proto"].ToString().Equals("https", StringComparison.OrdinalIgnoreCase);

            return _httpContextAccessor.HttpContext.Request.IsHttps;
        }

        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            bool useSsl = IsCurrentConnectionSecured();
            return GetThisPageUrl(includeQueryString, useSsl);
        }
                
        public virtual string GetThisPageUrl(bool includeQueryString, bool useSsl)
        {
            if (!IsRequestAvailable())
                return string.Empty;

            var url = GetAgencyHost(useSsl).TrimEnd('/');

            url += includeQueryString ? GetRawUrl(_httpContextAccessor.HttpContext.Request)
                : $"{_httpContextAccessor.HttpContext.Request.PathBase}{_httpContextAccessor.HttpContext.Request.Path}";

            return url.ToLowerInvariant();
        }

        public virtual string GetRawUrl(HttpRequest request)
        {
            var rawUrl = request.HttpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;

            if (string.IsNullOrEmpty(rawUrl))
                rawUrl = $"{request.PathBase}{request.Path}{request.QueryString}";

            return rawUrl;
        }

        public virtual bool IsLocalRequest(HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (IsIpAddressSet(connection.RemoteIpAddress))
            {
                return IsIpAddressSet(connection.LocalIpAddress)
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }

        protected virtual bool IsIpAddressSet(IPAddress address)
        {
            return address != null && address.ToString() != NullIpAddress;
        }

        public virtual bool IsPostBeingDone
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Items["poshta.IsPOSTBeingDone"] == null)
                    return false;

                return Convert.ToBoolean(_httpContextAccessor.HttpContext.Items["poshta.IsPOSTBeingDone"]);
            }
            set
            {
                _httpContextAccessor.HttpContext.Items["poshta.IsPOSTBeingDone"] = value;
            }
        }

        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                var response = _httpContextAccessor.HttpContext.Response;
                int[] redirectionStatusCodes = { 301, 302 };
                return redirectionStatusCodes.Contains(response.StatusCode);
            }
        }

        public virtual void RestartAppDomain()
        {
            _applicationLifetime.StopApplication();
        }
    }
}
