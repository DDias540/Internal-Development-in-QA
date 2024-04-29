using Internal_Development_in_QA__SDET_;
using System;



class Program
{
    static void Main(string[] args)
    {
        //Checks whether arguments were provided
        if (args.Length != 4)
        {
            //1-MySourceFolder 2-MyReplicaFolder Time to create a replica - 30 log - C:\Logs\sync_log.txt
            Console.WriteLine("C:\\Teste_Task C:\\Teste_Task2 30 C:\\Logs");
            return;
        }

        string sourceFolderPath = args[0];
        string replicaFolderPath = args[1];
        int synchronizationIntervalInSeconds = int.Parse(args[2]);
        string logFilePath = args[3];

        //Start the synchrozination of the folders
        FolderSynchronizer.StartSynchronization(sourceFolderPath, replicaFolderPath, TimeSpan.FromSeconds(synchronizationIntervalInSeconds), logFilePath);

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
