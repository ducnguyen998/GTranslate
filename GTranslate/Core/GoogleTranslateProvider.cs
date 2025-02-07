using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GTranslate.Core
{
    public class GoogleTranslateProvider : BaseTranslateProvider
    {
        public override async Task<string> Translate(TranslateAgent agent)
        {
            var result = string.Empty;

            try
            {
                var url = this.GetTranslateUrl(agent);

                using (var httpClient = new HttpClient())
                {
                    result = await httpClient.GetStringAsync(url);
                }
            }
            catch (Exception e)
            {
                return "Exception : " + e.Message;
            }

            return await this.ParseJsonAsync(result);
        }


        protected override string GetTranslateUrl(TranslateAgent agent)
        {
            var url = String.Format(
            "https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
            Extension.GetEnumDescription(agent.From),
            Extension.GetEnumDescription(agent.To),
            Uri.EscapeUriString(agent.Input)
            );

            return url;
        }

        protected override async Task<string> ParseJsonAsync(string jsonData)
        {
            return await Task.Run(() => {
                var translateReslt = string.Empty;
                var translateItems = JsonConvert.DeserializeObject<List<dynamic>>(jsonData).FirstOrDefault();

                foreach (IEnumerable translationLineObject in translateItems)
                {
                    IEnumerator translationLineString = translationLineObject.GetEnumerator();
                    translationLineString.MoveNext();
                    translateReslt += string.Format(" {0}", Convert.ToString(translationLineString.Current));
                }

                if (translateReslt.Length > 1)
                {
                    translateReslt = translateReslt.Substring(1);
                }

                return translateReslt;
            });
        }
    }
}
