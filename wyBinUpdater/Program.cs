using System.Diagnostics;
using System.Runtime.CompilerServices;
using wyBinUpdater;

internal class Program
{
    private static void Main(string[] args)
    {
        List<UpdateItem> updateItems = new()
        {
            new UpdateItem("wyBin-front-end", "E:\\Projects\\wyBin", new()
            {
                "cmd /c yarn install",
                "CALL ng build --configuration production",
                "@echo off",
                "echo cd /var/www/wybin.xyz/public_html >> sftp.txt",
                "echo put -r %cd%\\dist\\wybin /var/www/wybin.xyz/public_html >> sftp.txt",
                "echo quit >> sftp.txt",
                "sftp -b sftp.txt root@wybin.xyz",
                "pause"
            }),
            new UpdateItem("wyBin-back-end", "E:\\Projects\\wyBin\\api", new()
            {
                "CALL .\\gradlew build",
                "@echo off",
                "echo put %cd%\\build\\libs\\wyBin-0.0.1-SNAPSHOT.jar /opt/wybin/wyBin.jar >> sftp.txt",
                "echo quit >> sftp.txt",
                "sftp -b sftp.txt root@wybin.xyz",
                "DEL sftp.txt"
            }),
            new UpdateItem("DirkBot", "E:\\Projects\\DirkBot", new()
            {
                "CALL .\\gradlew build",
                "@echo off",
                "echo put %cd%\\build\\libs\\Dirk-0.0.1-SNAPSHOT.jar /opt/dirk/DirkBot.jar >> sftp.txt",
                "echo quit >> sftp.txt",
                "sftp -b sftp.txt root@wybin.xyz",
                "DEL sftp.txt"
            }),
            new UpdateItem("wyBinBot", "E:\\Projects\\wyBinBot", new() 
            {
                "CALL .\\gradlew build",
                "@echo off",
                "echo put %cd%\\build\\libs\\wyBinBot-1.0-SNAPSHOT.jar /opt/wyBinBot/wyBinBot.jar >> sftp.txt",
                "echo quit >> sftp.txt",
                "sftp -b sftp.txt root@wybin.xyz",
                "DEL sftp.txt"
            })
        };

        ShowMenu(updateItems, true);

        int input;

        while (!int.TryParse(Console.ReadLine(), out input) || (input <= 0 || input > updateItems.Count))
        {
            ShowMenu(updateItems, false);
        }

        UpdateItem foundUpdateItem = updateItems.ElementAt(input - 1);

        string tempDirectory = Path.Combine(Path.GetTempPath(), "wyBinUpdaterTempFolder");
        string tmpFileName = @tempDirectory + "\\" + foundUpdateItem.Name + ".bat";
        Directory.CreateDirectory(tempDirectory);

        Console.WriteLine(tmpFileName);

        using (StreamWriter sw = File.CreateText(tmpFileName))
        {
            foreach (string batchCommand in foundUpdateItem.SshCommands)
            {
                sw.WriteLine(batchCommand);
            }
        }

        ExecuteCommand(tmpFileName, foundUpdateItem.FolderLocation);

        Directory.Delete(tempDirectory, true);
    }

    private static void ShowMenu(List<UpdateItem> updateItems, bool correctInput)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("   wyBin updater");
        Console.WriteLine();

        for (int i = 0; i < updateItems.Count; i++)
        {
            Console.WriteLine("   {0}. {1}", i + 1, updateItems[i].Name);
        }

        if (correctInput)
        {
            Console.Write("\nSelect what application to update: ");
        }
        else
        {
            Console.WriteLine("\nInvalid input, select what application to update: ");
        }
    }

    private static void ExecuteCommand(string command, string directory)
    {
        var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
        {
            WorkingDirectory = directory
        };

        var process = Process.Start(processInfo);
        process.WaitForExit();

        Console.WriteLine("ExitCode: {0}", process.ExitCode);
        process.Close();
    }
}