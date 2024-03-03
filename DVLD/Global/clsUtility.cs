using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Global
{
    public class clsUtility
    {
        public static string GenerateGUID()
        {
            Guid NewGuid = Guid.NewGuid();

            return NewGuid.ToString();
        }

        public static string ReplaceFileNameWithGUID(string FileSource)
        {
            string FileName = FileSource;
            FileInfo fileInfo = new FileInfo(FileName);
            string Extension = fileInfo.Extension;
            return GenerateGUID() + Extension;
        }

        public static bool CreateImageFolderIfDoesNotExist(string FolderPath)
        {
            if(!Directory.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);

                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error Creating Folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            return true;
        }

        public static bool CopyImageToProjectImagesFolder(ref string FileSource)
        {
            string DestinationFolder = @"C:\Users\omerm\Desktop\OMS\PROGRAMING\C# Language\Programming Advices\DVLDProject\People-Images\";

            if (!CreateImageFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFile = DestinationFolder + ReplaceFileNameWithGUID(FileSource);

            try
            {
                File.Copy(FileSource, DestinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            FileSource = DestinationFile;
            return true;
        }

    }
}
