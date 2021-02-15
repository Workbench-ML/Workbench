using System;

namespace Workbench.Runtime.Plugin
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class WorkbenchPluginAttribute : Attribute
    {
        public string VersionString { get; set; }
        public int VersionMajor { get; private set; }
        public int VersionMinor { get; private set; }
        public int VersionRevision { get; private set; }
        public WorkbenchPluginAttribute()
        {
            Version version = Version.Parse(VersionString);
            VersionMajor = version.Major;
            VersionMinor = version.Minor;
            VersionRevision = version.Revision;
        }
    }
}
