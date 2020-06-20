using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace TrackService
{
    public class CompressorManager
    {
        FileSystemWatcher fsw = null;
        private readonly string directoryName;

        public CompressorManager()
        {
            try
            {
                var myDocsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                directoryName = File.ReadAllText(Path.Combine(myDocsFolderPath, "TrackService\\TrackServiceFolder.txt"));
            }
            catch
            {
                throw new Exception("Невозможно прочитать файл настроек");
            }
        }

        public void Start()
        {
            fsw = new FileSystemWatcher(directoryName);
            fsw.IncludeSubdirectories = true;
            fsw.Created += File_Created;
            fsw.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            fsw.EnableRaisingEvents = false;
            fsw.Dispose();
        }

        public void Pause()
        {
            fsw.EnableRaisingEvents = false;
        }

        public void Continue()
        {
            fsw.EnableRaisingEvents = true;
        }

        private void File_Created(object sender, FileSystemEventArgs e)
        {
            var extension = Path.GetExtension(e.FullPath);
            if (extension == ".jpg" || extension == ".jpeg")
            {
                Compress(e.FullPath);
            }
        }

        private void Compress(string fullPath)
        {
            //Ожидает доступа к файлу
            while (true)
            {
                try
                {
                    using (StreamReader stream = new StreamReader(fullPath))
                    {
                        break;
                    }
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }

            var bytes = File.ReadAllBytes(fullPath);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image image = Image.FromStream(ms);
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters encoderParams = new EncoderParameters(1)
                {
                    Param = new[] { new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 0L) }
                };

                using (MemoryStream ms2 = new MemoryStream())
                {
                    image.Save(ms2, jpgEncoder, encoderParams);
                    File.WriteAllBytes(fullPath, ms2.ToArray());
                }
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }
    }
}
