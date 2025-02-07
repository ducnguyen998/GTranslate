using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTranslate.Core
{
    public interface ITranslateProvider
    {
        Task<string> Translate(TranslateAgent agent);
    }
}
