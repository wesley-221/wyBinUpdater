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

        public UpdateItem(string name, string folderLocation)
        {
            Name = name;
            this.SshCommands = new List<string>();
            FolderLocation = folderLocation;
        }

        public UpdateItem(string name, string folderLocation, List<string> sshCommands) : this(name, folderLocation)
        {
            SshCommands = sshCommands;
        }

        public List<string> GetBatchFileCommands()
        {
            List<string> finalBatchCommands = new()
            {
                @"echo /c cd " + FolderLocation
            };

            SshCommands.ForEach(sshCommand => finalBatchCommands.Add(sshCommand));

            return finalBatchCommands;
        }
    }
}
