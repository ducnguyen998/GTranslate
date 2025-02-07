using CommunityToolkit.Mvvm.ComponentModel;
using GTranslate.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTranslate
{
    public class MainWindowViewmodel : ObservableObject
    {
        public List<ETranslateEngine> TranslateEngineCollection { get; set; }

        public List<ELanguage> LanguageCollection { get; set; }

        public MainWindowViewmodel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.TranslateEngineCollection
                = Enum.GetValues(typeof(ETranslateEngine))
                .Cast<ETranslateEngine>()
                .ToList();
            this.LanguageCollection
                = Enum.GetValues(typeof(ELanguage))
                .Cast<ELanguage>()
                .ToList();

            this.DoChangeTranslateProvider();
            this.TranslateAgent = new TranslateAgent("hello", ELanguage.English, ELanguage.Vietnamese);
            this.TranslateAgent.PropertyChanged += (s, e) => DoTranslate();
        }

        public ETranslateEngine SelectedTranslateEngine
        {
            get
            {
                return selectedTranslateEngine;
            }
            set
            {
                if (selectedTranslateEngine != value)
                {
                    selectedTranslateEngine = value;
                    OnPropertyChanged();
                    DoChangeTranslateProvider();
                    DoTranslate();
                }
            }
        }

        public TranslateAgent TranslateAgent
        {
            get
            {
                return translateAgent;
            }
            set
            {
                if (translateAgent != value)
                {
                    translateAgent = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TranslateResult
        {
            get
            {
                return translateResult;
            }
            set
            {
                if (translateResult != value)
                {
                    translateResult = value;
                    OnPropertyChanged();
                }
            }
        }

        private void DoTranslate()
        {
            Task.Factory.StartNew(async () =>
            {
                this.TranslateResult = await this.translateProvider.Translate(this.translateAgent);
            });
        }

        private void DoChangeTranslateProvider()
        {
            switch (selectedTranslateEngine)
            {
                case ETranslateEngine.Google:
                    this.translateProvider = serviceProvider.GetRequiredService<GoogleTranslateProvider>();
                    break;
                case ETranslateEngine.Gemini:
                this.translateProvider = serviceProvider.GetRequiredService<GeminiTranslateProvider>();
                break;
            }
        }

        private readonly IServiceProvider serviceProvider;

        private ITranslateProvider translateProvider;

        private ETranslateEngine selectedTranslateEngine;

        private TranslateAgent translateAgent;

        private string translateResult;
    }
}
