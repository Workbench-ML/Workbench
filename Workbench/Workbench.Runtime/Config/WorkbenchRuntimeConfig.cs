using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Workbench.Infrastructure.DI;
using Workbench.Infrastructure.Services;

namespace Workbench.Runtime.Config
{
    [XmlRoot("Workbench")]
    public sealed class WorkbenchRuntimeConfigSection
    {
        [XmlAttribute("WorkbenchDirectory")]
        public string WorkbenchDirectory { get; set; }
        [XmlAttribute("PluginsSubdirectory")]
        public string PluginsSubdirectory { get; set; }

    }

    public class WorkbenchRuntimeConfig
    {
        private WorkbenchRuntimeConfigSection _configSection;

        private readonly ILoggingService _loggingService;
        private readonly IDirectoryService _directoryService;

        public string WorkbenchDirectory 
        { 
            get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
            _configSection.WorkbenchDirectory); 
        }

        public string WorkbenchPluginsDirectory {
            get => Path.Combine(WorkbenchDirectory,
            _configSection.PluginsSubdirectory);
        }

        public WorkbenchRuntimeConfig(IDependencyInjectionEngine di)
        {
            di.Inject(ref _loggingService);
            di.Inject(ref _directoryService);
        }

        public void Load(RuntimeConfigurationAttribute configAttribute)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var configFilePath = Path.Combine(baseDirectory, configAttribute.ConfigFile);
            var configFileInfo = _directoryService.GetFileAtPath(configFilePath);
            try
            {
                using(var fs = configFileInfo.OpenRead())
                {
                    InternalParseConfig(fs);
                }
            }
            catch(IOException ex)
            {
                _loggingService.Error("Failed to parse config file", ex);
            }
        }
        private void InternalParseConfig(FileStream fileStream)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                using (XmlReader xmlReader = XmlReader.Create(fileStream))
                {
                    doc.Load(xmlReader);
                }
            }
            catch(Exception ex)
            {
                _loggingService.Warn("Exception triggered when parsing XML config", ex);
                doc = null;
            }
            if(doc != null)
            {
                _loggingService.Info("Loading XML configuration");
                XmlNodeList configNodeList = doc.GetElementsByTagName("Workbench");
                if(configNodeList.Count == 0)
                {
                    _loggingService.Warn("XML configuration does not have have a <Workbench> element");
                }
                else if(configNodeList.Count > 1)
                {
                    _loggingService.Error("XML configuration malformed. More than one <Workbench> found");
                }
                else
                {
                    _configSection = InternalParseXMLConfig(configNodeList[0] as XmlElement);
                }
            }
        }

        private WorkbenchRuntimeConfigSection InternalParseXMLConfig(XmlElement element)
        {
            var serializer = new XmlSerializer(typeof(WorkbenchRuntimeConfigSection));
            using(var xmlNodeReader = new XmlNodeReader(element))
            {
                return (WorkbenchRuntimeConfigSection)serializer.Deserialize(xmlNodeReader);
            }
        }
    }
}
