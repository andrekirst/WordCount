﻿using System.Diagnostics.CodeAnalysis;

namespace WordCount.Abstractions.SystemAbstractions.Resources
{
    [ExcludeFromCodeCoverage]
    public class ResourceManager : IResourceManager
    {
        private readonly System.Resources.ResourceManager _resourceManager;

        public ResourceManager()
        {
            _resourceManager = new System.Resources.ResourceManager(
                baseName: "WordCount.Resources.Resource",
                assembly: typeof(Program).Assembly);
        }

        public string GetString(string name, System.Globalization.CultureInfo cultureInfo) =>
            _resourceManager.GetString(
                name: name,
                culture: cultureInfo);
    }
}
