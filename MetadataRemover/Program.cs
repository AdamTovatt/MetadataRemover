namespace MetadataRemover
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            bool recurse = args.Any(x => x.ToLower().Contains("r"));
            bool dry = args.Any(x => x.ToLower().Contains("d"));

            Console.WriteLine("Will clean in the current directory: " + currentDirectory);

            Console.WriteLine("Recurse: " + recurse);
            Console.WriteLine("Dry run: " + dry);

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            ProcessDirectory(currentDirectory, recurse, dry);

            Console.WriteLine("Processing complete.");
        }

        private static void ProcessDirectory(string directory, bool recurse = false, bool dry = false)
        {
            PatchingHandler patchingHandler = new PatchingHandler();

            if (recurse)
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    ProcessDirectory(subDirectory, recurse, dry);
                }
            }

            // Get all files in the directory
            foreach (string inputFilePath in Directory.GetFiles(directory))
            {
                string baseFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                string extension = Path.GetExtension(inputFilePath);

                // Only process .jpg or .jpeg files (you can adjust this filter as needed)
                if (patchingHandler.GetCanPatchFileExtension(extension))
                {
                    // Construct the output file path in the same directory
                    string outputFilePath = Path.Combine(directory, $"{baseFileName}_processed{extension}");

                    try
                    {
                        if (!dry)
                        {
                            // Process the file by removing EXIF data
                            using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
                            using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                            {
                                patchingHandler.GetPatcherByFileExtension(extension).PatchAwayExif(inputFileStream, outputFileStream);
                            }

                            // Copy the processed file back to the original file path
                            File.Copy(outputFilePath, inputFilePath, true);

                            // Delete the temporary processed file
                            File.Delete(outputFilePath);

                            Console.WriteLine($"Processed: {inputFilePath}");
                        }
                        else
                        {
                            Console.WriteLine($"Would process: {inputFilePath}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing file {inputFilePath}: {ex.Message}");
                    }
                }
            }
        }
    }
}
