### What is this?
This is a small application written in C# that removes all metadata from image files in a directory and (optionally) its subdirectories, if the files are of a supported type. Currently, supported types are JPG and PNG.

### Why is this?
Sometimes it's useful to easily remove metadata from multiple images. Photoshop recently introduced a feature that adds metadata indicating an image was generated with AI if any AI-related tools were used during its creation. 
While the idea might seem reasonable, the current implementation is problematic. For instance, if you use AI to create a reference for inspiration and then continue working on the document, the metadata will persist. Even if you spend countless hours creating your own non-AI-generated content, the exported file will still indicate it was AI-generated, which can be misleading when shared on platforms like Instagram.

### What does it do?
It processes a directory and, optionally, its subdirectories, finding files of supported types. If a supported file is found, its metadata is removed.

### How do I use this?
1. Download a pre-built version from the Releases page, or download the source code and build it yourself. 
2. Optionally (but highly recommended), add the path to the built `.exe` file to your system PATH variable. If you don't, you'll need to provide the full path to the `.exe` file every time you run it.

To run the application:
- Open a terminal.
- Provide the full path to the built `.exe` file, or if it's added to the PATH variable, just type its name: `MetadataRemover`.

The application processes the current terminal directory by default. Before running, it's recommended to `cd` into the target directory. 

#### Options:
- `-d`: Perform a "dry run," which lists the files that would be processed without making actual changes.
- `-r`: Recurse into subdirectories and process files there as well (including nested subdirectories).

Note: By default, the application does **not** recurse into subdirectories unless the `-r` flag is specified.
