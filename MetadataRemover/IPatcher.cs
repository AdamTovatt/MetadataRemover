namespace MetadataRemover
{
    public interface IPatcher
    {
        public Stream PatchAwayExif(Stream inStream, Stream outStream);
        public List<string> GetSupportedFileExtensions();
    }
}
