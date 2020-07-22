using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using System.Windows.Forms;
//using DevExpress.ClipboardSource.SpreadsheetML;

namespace WebAppCoreNew.Common {
    public class AsposeCell {
        public static Stream LicenseAspose()
        {
            string xmlLicense = string.Empty;
            System.IO.Stream license;

            xmlLicense = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            xmlLicense = xmlLicense + "<License>";
            xmlLicense = xmlLicense + "<Data>";
            xmlLicense = xmlLicense + "<LicensedTo>FPT Information system</LicensedTo>";
            xmlLicense = xmlLicense + "<EmailTo>hoangnt16@fpt.com.vn</EmailTo>";
            xmlLicense = xmlLicense + "<LicenseType>Developer OEM</LicenseType>";
            xmlLicense = xmlLicense + "<LicenseNote>Limited to 1 developer.</LicenseNote>";
            xmlLicense = xmlLicense + "<OrderID>141002051606</OrderID>";
            xmlLicense = xmlLicense + "<UserID>275358</UserID>";
            xmlLicense = xmlLicense + "<OEM>This is a redistributable license</OEM>";
            xmlLicense = xmlLicense + "<Products>";
            xmlLicense = xmlLicense + "<Product>Aspose.Total Product Family</Product>";
            xmlLicense = xmlLicense + "</Products>";
            xmlLicense = xmlLicense + "<EditionType>Enterprise</EditionType>";
            xmlLicense = xmlLicense + "<SerialNumber>72d50801-1a9e-4899-905d-38307ee51a36</SerialNumber>";
            xmlLicense = xmlLicense + "<SubscriptionExpiry>20151015</SubscriptionExpiry>";
            xmlLicense = xmlLicense + "<LicenseVersion>3.0</LicenseVersion>";
            xmlLicense = xmlLicense + "<LicenseInstructions>http://www.aspose.com/corporate/purchase/license-instructions.aspx</LicenseInstructions>";
            xmlLicense = xmlLicense + "</Data>";
            xmlLicense = xmlLicense + "<Signature>ggrtgqzpRY7YE5HxnSYGg+B9m3i4x2jqVG2ywtqMZ9vEq1qQOLwOJ2q0v+kzxiOGEFUWWaF6KV4Bd14FcKRM5J/BA0HncoDpoHJGaSzyRNskFklKkZE80BiWwt30cZH88x9auvHpf+ppAdB6AY7+CFla4ciO1Jyt/2wQ5cKms1g=</Signature>";
            xmlLicense = xmlLicense + "</License>";
            license = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlLicense));
            return license;
        }
        public static string Excel2Pdf(string path)
        {
            ////string dataDir = RunExamples.GetDataDir(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            //try {
            //    // Get the template excel file path.
            //    string designerFile = "D://1.xls";

            //    // Specify the pdf file path.
            //    string pdfFile = "D://Output.pdf";

            //    // Open the template excel file
            //    Aspose.Cells.Workbook wb = new Aspose.Cells.Workbook(designerFile);

            //    // Save the pdf file.
            //    wb.Save(pdfFile, SaveFormat.Pdf);
            //}
            //catch (Exception e) {
            //    Console.WriteLine(e.Message);
            //    Console.ReadLine();
            //}
            return "";
        }
        public static void Print()
        {
            
            
            //Workbook workbook = new Workbook();
            Workbook workbook = new Workbook();
            workbook.LoadDocument("D://1.xlsx");
            // Load a document from a file.
            

            // Create an object that contains printer settings.
            PrinterSettings printerSettings = new PrinterSettings();

            // Define the printer to use.
            printerSettings.PrinterName = "Microsoft Print to PDF";
            printerSettings.PrintToFile = true;
            printerSettings.PrintFileName = "Documents\\PrintedDocument.pdf";

            // Specify that the first three pages should be printed.
            printerSettings.PrintRange = PrintRange.SomePages;
            //printerSettings.FromPage = 1;
            //printerSettings.ToPage = 3;

            // Set the number of copies to print.
            printerSettings.Copies = 1;

            // Print the workbook using the specified printer settings.
            workbook.Print(printerSettings);

        }
    }
}
