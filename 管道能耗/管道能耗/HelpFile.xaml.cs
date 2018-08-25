using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Runtime.InteropServices;

using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;

namespace 管道能耗
{
    /// <summary>
    /// HelpFile.xaml 的交互逻辑
    /// </summary>
    public partial class HelpFile : System.Windows.Window
    {
        public HelpFile()
        {
            InitializeComponent();
            web.Navigate(new Uri(Directory.GetCurrentDirectory() + "/HelpFile/help.html"));
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        /**    WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            basePath = basePath.Replace("\\","/");
            string filePath = basePath+ "HelpFile/help.doc";
            Console.WriteLine(filePath);
            docViewer.Document = ConvertWordToXPS(filePath).GetFixedDocumentSequence();
            docViewer.FitToWidth();
    */
        }
      /**  private XpsDocument ConvertWordToXPS(string wordDocName)
        {
            FileInfo fi = new FileInfo(wordDocName);
            XpsDocument result = null;
            string xpsDocName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), fi.Name);
            xpsDocName = xpsDocName.Replace(".docx", ".xps").Replace(".doc", ".xps");
            Microsoft.Office.Interop.Word.Application wordApplication = new Microsoft.Office.Interop.Word.Application();
            try
            {
                if (!File.Exists(xpsDocName))
                {
                    wordApplication.Documents.Add(wordDocName);
                    Document doc = wordApplication.ActiveDocument;
                    doc.ExportAsFixedFormat(xpsDocName, WdExportFormat.wdExportFormatXPS, false, 
                        WdExportOptimizeFor.wdExportOptimizeForPrint, WdExportRange.wdExportAllDocument, 0, 0, 
                        WdExportItem.wdExportDocumentContent, true, true, 
                        WdExportCreateBookmarks.wdExportCreateHeadingBookmarks, true, true, false, Type.Missing);
                    result = new XpsDocument(xpsDocName, System.IO.FileAccess.Read);
                }

                if (File.Exists(xpsDocName))
                {
                    result = new XpsDocument(xpsDocName, FileAccess.Read);
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                wordApplication.Quit(WdSaveOptions.wdDoNotSaveChanges);
            }

            wordApplication.Quit(WdSaveOptions.wdDoNotSaveChanges);

            return result;
        }*/

    }
}
