namespace Internal_Development_in_QA__SDET_
{
    class FolderSynchronizer
    {

        //Method to start the periodic synchronization 
        public static void StartSynchronization(string sourceFolderPath, string replicaFolderPath, TimeSpan synchronizationInterval, string logFilePath)
        {

            // Execute de synchrozination or get a message error
            var timer = new System.Threading.Timer(_ =>
            {
                try
                {
                    SynchronizeFolders(sourceFolderPath, replicaFolderPath, logFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during synchronization: {ex.Message}");
                }
            }, null, TimeSpan.Zero, synchronizationInterval);
        }

        //Method to synchronize folders
        private static void SynchronizeFolders(string sourceFolderPath, string replicaFolderPath, string logFilePath)
        {
            //Check if existis
            if (!Directory.Exists(sourceFolderPath))
            {
                Console.WriteLine($"Source folder '{sourceFolderPath}' does not exist.");
                return;
            }

            //Create the folder if not
            if (!Directory.Exists(replicaFolderPath))
            {
                Directory.CreateDirectory(replicaFolderPath);
                Console.WriteLine($"Replica folder '{replicaFolderPath}' created.");
            }

            string[] sourceFiles = Directory.GetFiles(sourceFolderPath);

            //Synchronize files to the new folder replica   
            foreach (string sourceFile in sourceFiles)
            {
                string fileName = Path.GetFileName(sourceFile);
                string replicaFilePath = Path.Combine(replicaFolderPath, fileName);

                if (!File.Exists(replicaFilePath))
                {
                    //Copy from origin folder to the folder replica
                    File.Copy(sourceFile, replicaFilePath);
                    Console.WriteLine($"File '{fileName}' copied to replica folder.");
                    LogToFile(logFilePath, $"File '{fileName}' copied to replica folder.");
                }
            }

            //Get the list of archives
            string[] replicaFiles = Directory.GetFiles(replicaFolderPath);

            foreach (string replicaFile in replicaFiles)
            {
                string fileName = Path.GetFileName(replicaFile);
                string sourceFilePath = Path.Combine(sourceFolderPath, fileName);

                if (!File.Exists(sourceFilePath))
                {
                    File.Delete(replicaFile);
                    Console.WriteLine($"File '{fileName}' removed from replica folder.");
                    LogToFile(logFilePath, $"File '{fileName}' removed from replica folder.");
                }
            }
        }

        //Method to record in a log file
        private static void LogToFile(string logFilePath, string message)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.WriteLine($"{DateTime.Now} - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
