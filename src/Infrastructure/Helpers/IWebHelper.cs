using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoshtaBy.Infrastructure.Helpers
{
    public interface IWebHelper
    {
        string GetAgencyLocation(bool? useSsl = null);

        string GetAgencyHost(bool useSsl);

        bool IsRequestAvailable();

        bool IsCurrentConnectionSecured();

        string GetThisPageUrl(bool includeQueryString);

        string GetThisPageUrl(bool includeQueryString, bool useSsl);

        string GetRawUrl(HttpRequest request);

        bool IsLocalRequest(HttpRequest req);

        bool IsPostBeingDone { get; set; }

        bool IsRequestBeingRedirected { get; }

        void RestartAppDomain();
    }
}
