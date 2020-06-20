using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace TrackService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            string parameter = "TrackServiceSource\" \"TrackServiceLog";
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\"";
            base.OnBeforeInstall(savedState);
        }
    }
}
