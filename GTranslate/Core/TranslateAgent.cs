using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTranslate.Core
{
    public class TranslateAgent : ObservableObject
    {
        public TranslateAgent(string input, ELanguage from, ELanguage to)
        {
            Input = input;
            From = from;
            To = to;
        }

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                if (input != value)
                {
                    input = value;
                    OnPropertyChanged();
                }
            }
        }

        public ELanguage From
        {
            get
            {
                return from;
            }
            set
            {
                if (from != value)
                {
                    from = value;
                    OnPropertyChanged();
                }
            }
        }

        public ELanguage To
        {
            get
            {
                return to;
            }
            set
            {
                if (to != value)
                {
                    to = value;
                    OnPropertyChanged();
                }
            }
        }



        private string input;

        private ELanguage from;

        private ELanguage to;
    }
}
