using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTranslate
{
    public enum ELanguage
    {
        [Description("vi")]
        Vietnamese,
        [Description("en")]
        English,
        [Description("ko")]
        Korean,
        [Description("zh-CN")]
        Chinese
    }
    public enum ETranslateEngine
    {
        Google,
        Gemini
    }
}
