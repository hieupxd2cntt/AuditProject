using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppClient.Interface;
using Core.Common;
using Core.Controllers;
using Core.Entities;
using Core.Utils;
using Core.Base;
using System.ServiceModel;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using DevExpress.XtraLayout;
using System.Globalization;

namespace AppClient.Controls
{
    public partial class ucUploadFile : ucModule
    {        
        private List<string> AttachmentFiles = new List<string>();
        private List<string> checkedListFiles = new List<string>();

        public ucUploadFile()
        {
            InitializeComponent();
            Program.FileName = String.Empty;            
        }
        protected override void InitializeGUI(DevExpress.Skins.Skin skin)
        {
            base.InitializeGUI(skin);
        }
        public ModuleInfo MaintainInfo
        {
            get
            {
                return (ModuleInfo)ModuleInfo;
            }
        }
        protected override void BuildButtons()
        {
#if DEBUG
            SetupContextMenu(mainLayout);
            SetupSaveLayout(mainLayout);
#endif
        }

        private void btnFilename_Click(object sender, ButtonPressedEventArgs e)
       {            
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = IMPORTMASTER.ATTACKED_FILE_EXTENSIONS;
            
            if (ModuleInfo.ExecuteMode == Core.CODES.DEFMOD.EXECMODE.SINGLE_INSTANCE)
                openFile.Multiselect = false;
            else
                openFile.Multiselect = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                AttachmentFiles.AddRange(openFile.FileNames);
                checkedListBoxControl1.Items.Clear();
                int i = 0;
                foreach (var strFile in AttachmentFiles)
                {
                    checkedListBoxControl1.Items.Add(strFile, true);                                       
                    i++;
                }                                        
            }
        }
        public static string convertToUnSign2(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
        private  void btnUpload_Click(object sender, EventArgs e)
        {
            Execute();
        }
        public override void Execute()
        {
            Program.blCheckFile = false;
            base.Execute();
            var ctrlSA = new SAController();
            try
            {
                User userInfo = new User();
                ctrlSA.GetSessionUserInfo(out userInfo);

                checkedListFiles.Clear();
                for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
                {
                    if (checkedListBoxControl1.GetItemChecked(i) == true)
                    {
                        checkedListFiles.Add(checkedListBoxControl1.GetItemValue(i).ToString());
                    }
                }
                
                if (checkedListFiles.Count > 0)
                {
                    // Tao file Zip                    
                    //var outPathname = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + RandomString(10, false) + ".zip"; ;
                    //FileStream fsOut = File.Create(outPathname);
                    //ZipOutputStream zipStream = new ZipOutputStream(fsOut);
                    //zipStream.SetLevel(9); //0-9, 9 being the highest level of compression                    
                    foreach (var filename in checkedListFiles)
                    {
                        
                        if ( !string.IsNullOrEmpty(filename))
                        {
                            FileInfo fi = new FileInfo(filename);
                            string entryName = System.IO.Path.GetFileName(filename);

                            ///////////////// MLDedit
                            var nameinforderup = convertToUnSign2(entryName);
                            string[] filestyle = nameinforderup.Split('.');
                            nameinforderup = "";
                            for (int i = 0; i < filestyle.Length - 1; i++)
                            {
                                nameinforderup += filestyle[i];
                            }
                            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                            nameinforderup = textInfo.ToTitleCase(nameinforderup); //War And Peace
                            char[] delimiters = new char[] { '/', '.', ',' };
                            string[] parts = nameinforderup.Split(delimiters,
                                 StringSplitOptions.RemoveEmptyEntries);
                            nameinforderup = "";
                            for (int i = 0; i < parts.Length; i++)
                            {
                                // Console.WriteLine(parts[i]);
                                nameinforderup += parts[i];
                            }
                            string[] words1 = nameinforderup.Split(' ');
                            nameinforderup = "";
                            for (int x = 0; x < words1.Length; x++)
                            {
                                nameinforderup += words1[x];
                            }
                            nameinforderup = nameinforderup + "." + filestyle[filestyle.Length - 1];
                            entryName = nameinforderup;

                            /////////



                            //entryName = ZipEntry.CleanName(entryName);
                            //ZipEntry newEntry = new ZipEntry(entryName);
                            //newEntry.DateTime = fi.LastWriteTime;

                            //newEntry.Size = fi.Length;
                            //zipStream.PutNextEntry(newEntry);

                            //// Zip the file in buffered chunks                                
                            //byte[] buffer = new byte[4096];
                            //using (FileStream streamReader = File.OpenRead(filename))
                            //{
                            //    StreamUtils.Copy(streamReader, zipStream, buffer);
                            //}
                            //zipStream.CloseEntry();                            
                        }
                        var _streamAttr = new FileStream(filename, FileMode.Open, FileAccess.Read);
                        prgUploadFile.Value = 0;
                        var fileUpload = new UploadFileStream(_streamAttr);
                        fileUpload.OnUploadStatusChanged += fileUpload_OnUploadStatusChanged;

                        FileUpload upload = new FileUpload();

                        upload.KeyID = userInfo.Username;
                        upload.ModID = ModuleInfo.ModuleID;
                        upload.FileName = System.IO.Path.GetFileName(filename);
                        upload.UploadStream = fileUpload;

                        ctrlSA.SaveFile(upload);

                        Program.FileName = Program.FileName + System.IO.Path.GetFileName(filename);
                        _streamAttr.Dispose();
                    }
                    //zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
                    //zipStream.Close();
                    //var _streamAttr = new FileStream(outPathname, FileMode.Open, FileAccess.Read);                    
                }
                              
                if (checkedListBoxControl1.Items.Count > 0)
                    Program.blCheckFile = true;
                CloseModule();                
            }
            catch (Exception ex)
            {
                CloseModule();
                ShowError(ex);
            }
        }
              
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                sb.Append(c);
            }
            if (lowerCase)
                return sb.ToString().ToLower();
            return sb.ToString();

        }
        private void fileUpload_OnUploadStatusChanged(object obj, UploadFileStream.UploadStatusArgs e)
        {
            prgUploadFile.Value = (Int32)(e.Uploaded * 100 / e.Length);
        }       
                
        private void ucUploadFile_Load(object sender, EventArgs e)
        {                       
                
        }
        public LayoutControl CommonLayout
        {
            get { return mainLayout; }
        }

        public string CommonLayoutStoredData
        {
            get { return Language.Layout; }
        }           
    }
}

