using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTranslate.Core
{
    public abstract class BaseTranslateProvider : ITranslateProvider
    {
        public abstract Task<string> Translate(TranslateAgent agent);

        protected abstract string GetTranslateUrl(TranslateAgent agent);

        protected abstract Task<string> ParseJsonAsync(string jsonData);
    }
}
