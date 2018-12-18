//------------------------------------------------------------------------------
// <copyright file="ConfigurationBuilderChain.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Configuration
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Xml;

    internal class ConfigurationBuilderChain : ConfigurationBuilder
    {
        List<ConfigurationBuilder> _builders;

        public List<ConfigurationBuilder> Builders { get { return _builders; } }

        public override void Initialize(string name, NameValueCollection config) {
            _builders = new List<ConfigurationBuilder>();
            base.Initialize(name, config);
        }

        public override XmlNode ProcessRawXml(XmlNode rawXml) {
            XmlNode processedXml = rawXml;
            foreach (ConfigurationBuilder b in _builders) {
                processedXml = b.ProcessRawXml(processedXml);
            }
            return processedXml;
        }

        public override ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection) {
            ConfigurationSection processedConfigSection = configSection;
            foreach (ConfigurationBuilder b in _builders) {
                processedConfigSection = b.ProcessConfigurationSection(processedConfigSection);
            }
            return processedConfigSection;
        }
    }
}
