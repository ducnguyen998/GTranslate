using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GTranslate.Core
{
    public class GeminiTranslateProvider : BaseTranslateProvider
    {
        public override async Task<string> Translate(TranslateAgent agent)
        {
            try
            {
                var jsonBody = this.GetJsonBody(agent);
                var url = this.GetTranslateUrl();

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Content = new StringContent(jsonBody, Encoding.UTF8);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.SendAsync(request).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var ret = responseBody.Substring(responseBody.IndexOf("\"text\": \"") + 9, responseBody.IndexOf("\"", responseBody.IndexOf("\"text\": \"") + 10) - responseBody.IndexOf("\"text\": \"") - 9);
                        return ret;
                    }
                    else
                    {
                        return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception e)
            {
                return "Exception : " + e.Message;
            }
        }

        protected override string GetTranslateUrl(TranslateAgent agent = null)
        {
            return $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.0-pro:generateContent?key={GEMINI_TRANSLATE_API_KEY}";
        }

        protected override async Task<string> ParseJsonAsync(string jsonData)
        {
            await Task.Delay(0);
            return string.Empty;
        }

        protected string GetJsonBody(TranslateAgent agent)
        {
            var jsonBody = $@"{{
                    ""contents"": [
                        {{
                            ""role"": """",
                            ""parts"": [
                                {{
                                    ""text"": ""Translate from {Extension.GetEnumDescription(agent.From)} to {Extension.GetEnumDescription(agent.To)} : {agent.Input}""
                                }}
                            ]
                        }}
                    ],
                    ""generationConfig"": {{
                        ""temperature"": 0.9,
                        ""topK"": 50,
                        ""topP"": 0.95,
                        ""maxOutputTokens"": 4096,
                        ""stopSequences"": []
                    }},
                    ""safetySettings"": [

                    ]
                }}";

            return jsonBody;
        }

        private const string GEMINI_TRANSLATE_API_KEY = "YOUR_API_KEY_HERE";
    }
}
