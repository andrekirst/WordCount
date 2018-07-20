﻿using System.Linq;
using WordCount.Abstractions.SystemAbstractions.Globalization;
using WordCount.Abstractions.SystemAbstractions.Resources;
using WordCount.Helpers;
using WordCount.Interfaces.Language;
using WordCount.Models.Results;

namespace WordCount.Implementations.Language
{
    public class LanguageResource : ILanguageResource
    {
        private readonly ICultureInfo _cultureInfo;
        private readonly IResourceManager _resourceManager;
        private readonly ILanguageDecision _languageDecision;

        public LanguageResource(
            ILanguageDecision languageDecision,
            ICultureInfo cultureInfo,
            IResourceManager resourceManager)
        {
            _cultureInfo = cultureInfo;
            _resourceManager = resourceManager;
            _languageDecision = languageDecision;
        }

        public string GetResourceStringById(string resourceIdent)
        {
            DecideLanguageResult languageDecision = _languageDecision.DecideLanguage();

            string mappedLanguageCulture = LanguageToCultureMapping.Mappings[key: languageDecision.Language];

            System.Globalization.CultureInfo currentCultureInfo = _cultureInfo.GetCultureInfo(culture: mappedLanguageCulture);

            return _resourceManager.GetString(
                name: resourceIdent,
                cultureInfo: currentCultureInfo);
        }

        public int DetectLongestResourceString(string[] resourceIdents)
        {
            return resourceIdents.Max(selector: s => GetResourceStringById(resourceIdent: s).Length);
        }
    }
}