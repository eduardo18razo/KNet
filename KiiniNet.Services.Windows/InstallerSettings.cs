using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace KiiniNet.Services.Windows
{
    [RunInstaller(true)]
    public partial class InstallerSettings : System.Configuration.Install.Installer
    {
        public InstallerSettings()
        {
            InitializeComponent();
        }
    }
}
