using System;
using System.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Bot
{
    public class IndexModel : PageModel
    {
        private readonly IOptions<ApplicationConfiguration> _optionsApplicationConfiguration;

        public string DebugLink { get; private set; }

        public string EmulatorDeepLink { get; private set; }

        public IndexModel(IOptions<ApplicationConfiguration> optionsApplicationConfiguration)
        {
            _optionsApplicationConfiguration = optionsApplicationConfiguration;
        }

        public void OnGet()
        {
            var botUrl = $"{ Request.Scheme }://{ Request.Host }/api/messages";
            DebugLink = botUrl;

            // construct emulator protocol URI
            var protocolUri = $"bfemulator://livechat.open?botUrl={ HttpUtility.UrlEncode(botUrl) }";
            var msaAppId = _optionsApplicationConfiguration.Value.MicrosoftAppId;
            var msaAppPw = _optionsApplicationConfiguration.Value.MicrosoftAppPassword;

            if (!string.IsNullOrEmpty(msaAppId))
            {
                protocolUri += $"&msaAppId={ HttpUtility.UrlEncode(msaAppId) }";
            }
            if (!string.IsNullOrEmpty(msaAppPw))
            {
                protocolUri += $"&msaPassword={ HttpUtility.UrlEncode(msaAppPw) }";
            }
            EmulatorDeepLink = protocolUri;
        }
    }
}