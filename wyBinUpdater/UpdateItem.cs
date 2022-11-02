using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wyBinUpdater
{
    internal class UpdateItem
    {
        public string Name { get; set; }
        public string FolderLocation { get; set; }
        public List<String> SshCommands { get; set; }
    }
}
