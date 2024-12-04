namespace MetadataRemover
{
    public class PatchingHandler
    {
        private List<IPatcher> patchers;
        private Dictionary<string, IPatcher> fileExtensionPatchers;

        public PatchingHandler()
        {
            patchers = new List<IPatcher>()
            {
                new JpegPatcher(),
                new PngPatcher(),
            };

            fileExtensionPatchers = new Dictionary<string, IPatcher>();

            foreach (IPatcher patcher in patchers)
            {
                List<string> fileExtensions = patcher.GetSupportedFileExtensions();

                foreach (string fileExtension in fileExtensions)
                {
                    fileExtensionPatchers.Add(fileExtension.ToLower(), patcher);
                }
            }
        }

        public bool GetCanPatchFileExtension(string fileExtension)
        {
            return fileExtensionPatchers.ContainsKey(fileExtension.ToLower());
        }

        public IPatcher GetPatcherByFileExtension(string fileExtension)
        {
            if (fileExtensionPatchers.TryGetValue(fileExtension, out IPatcher? patcher))
            {
                return patcher;
            }

            throw new InvalidOperationException($"Support for {fileExtension} not added!");
        }
    }
}
