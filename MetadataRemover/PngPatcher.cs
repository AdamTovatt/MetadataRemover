using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace MetadataRemover
{
    internal class PngPatcher : IPatcher
    {
        private List<string> supportedFileExtensions = new List<string> { ".png" };

        public List<string> GetSupportedFileExtensions()
        {
            return supportedFileExtensions;
        }   

        public Stream PatchAwayExif(Stream inStream, Stream outStream)
        {
            // Load the PNG image from the input stream
            using (Image<Rgba32> image = Image.Load<Rgba32>(inStream))
            {
                // Normalize the DPI (optional: set to 72x72 for digital)
                image.Metadata.HorizontalResolution = 72;
                image.Metadata.VerticalResolution = 72;

                // Save the PNG image without metadata
                PngEncoder encoder = new PngEncoder
                {
                    CompressionLevel = PngCompressionLevel.DefaultCompression,
                    SkipMetadata = true // Strip all metadata
                };

                image.Save(outStream, encoder);
            }

            // Return the output stream
            outStream.Seek(0, SeekOrigin.Begin);
            return outStream;
        }
    }
}
