using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Reflection;

namespace AsposePoc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to convert (ESC = exit)");
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                using var licenseStream = File.Open("Aspose.PDF.NET.lic", FileMode.Open, FileAccess.Read);
                License license = new License();
                license.SetLicense(licenseStream);

                FontRepository.Sources.Add(new FolderFontSource(GetAbsolutePathFor(Path.Combine("Document", "Fonts"))));
                FontRepository.LoadFonts();
                

                using var xslFoStream = File.Open("result.fo", FileMode.Open, FileAccess.Read);
                using var document = new Document(xslFoStream, new XslFoLoadOptions());

                var font = FontRepository.OpenFont(GetAbsolutePathFor(Path.Combine("Fonts", "OpenSans.ttf")));

                document.Save($"result-{Guid.NewGuid()}.pdf", new PdfSaveOptions { DefaultFontName = "Open Sans Regular" });

                Console.WriteLine("Press any key to convert (ESC = exit)");
            }
        }

        public static string GetAbsolutePathFor(string relativePath)
        {
            return Path.Combine(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location)
                ?? throw new InvalidOperationException("GetExecutingAssembly location can not be null"),
                relativePath);
        }
    }
}