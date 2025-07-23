using CommunityToolkit.Maui.Storage;
using System.Text;

namespace WildlifeTrackerSystem
{
    /// <summary>
    /// Class responsible for file IO operations.
    /// </summary>
    public class FileManager
    {
        private IFileSaver fileSaver;

        public FileManager(IFileSaver fileSaver)
        {

            this.fileSaver = fileSaver;
        }

        /// <summary>
        /// Saves animals list in string format to a file chosen by the user.
        /// </summary>
        /// <param name="text">animals list</param>
        /// <param name="fileName"></param>
        /// <returns>the result of the save which will be evaluated with result.IsSuccessful()</returns>
        public async Task<FileSaverResult> SaveAnimalsToFile(string text, string fileName)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            using MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(text));
            return await fileSaver.SaveAsync(fileName, stream, cancellationTokenSource.Token);
        }

        /// <summary>
        /// Opens a file picker to allow the user to choose the file from which to read animal data.
        /// </summary>
        /// <returns>Task(true, fileContent) if read was successful</returns>
        public async Task<(bool, string)> ReadAnimalsFromJsonFile()
        {
            StringBuilder builder = new StringBuilder();
            string fileContent = string.Empty;
            bool okRead = false;


            var filePickResult = await FilePicker.PickAsync();
            if (filePickResult != null)
            {
                if (filePickResult.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                {
                    byte[] data = new byte[1024];
                    var stream = await filePickResult.OpenReadAsync();
                    int numBytesRead = await stream.ReadAsync(data, 0, 1024);

                    foreach (char c in data)
                    {
                        if (c != '\0')
                            builder.Append(c);
                    }
                    fileContent = builder.ToString();
                    okRead = true;
                }
            }
            return (okRead, fileContent);
        }

        /// <summary>
        /// Loads an image with file picker for animal profile pic.
        /// </summary>
        /// <returns>new image source if user selected an image, default if action was cancelled </returns>
        public async Task<ImageSource> ReadProfilePic()
        {
            ImageSource imageSource;
            var filePickResult = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Pick an image",
                FileTypes = FilePickerFileType.Images
            });

            if (filePickResult != null)
                imageSource = filePickResult.FullPath;
            else
                imageSource = ImageSource.FromFile("img_not_found.jpg");
            return imageSource;
        }
    }
}
