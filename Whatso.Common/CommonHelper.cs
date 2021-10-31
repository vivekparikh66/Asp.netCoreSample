using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Whatso.Common
{
    public class CommonHelper
    {
        public string Encrypt(string clearText)
        {
            if (clearText.Length == 0)
            {
                return clearText;
            }
            string EncryptionKey = Constants.EncryptionKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public string Decrypt(string cipherText)
        {
            if (cipherText.Length == 0)
            {
                return cipherText;
            }
            try
            {
                string EncryptionKey = Constants.EncryptionKey;
                byte[] cipherBytes = Convert.FromBase64String(cipherText.Replace(' ', '+'));
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch (Exception ex)
            {
               // dbConnection.WriteCriticalLog("ClsCommon_Decrypt Message :: " + ex.Message + " ::: StackTrace:: " + ex.StackTrace + "");
                return cipherText;
            }

        }
        public string DisplaySpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') | c == '-' || c == '_')
                {

                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Trim();
        }

        public void SetDisplayToast(Controller controller, string Message, string type, string title)
        {
            controller.TempData["AlertType"] = type;
            controller.TempData["AlertMsg"] = Message;
            controller.TempData["AlertTitle"] = title;
        }
        public void GetDisplayToast(Controller controller)
        {
            if (controller.TempData["AlertType"] != null && !String.IsNullOrEmpty(controller.TempData["AlertType"].ToString()))
            {
                controller.ViewBag.AlertType = controller.TempData["AlertType"];
                controller.TempData["AlertType"] = null;
            }
            else
            {
                controller.ViewBag.AlertType = "";
            }

            if (controller.TempData["AlertMsg"] != null && !String.IsNullOrEmpty(controller.TempData["AlertMsg"].ToString()))
            {
                controller.ViewBag.AlertMsg = controller.TempData["AlertMsg"];
                controller.TempData["AlertMsg"] = null;
            }
            else
            {
                controller.ViewBag.AlertMsg = "";
            }
            if (controller.TempData["AlertTitle"] != null && !String.IsNullOrEmpty(controller.TempData["AlertTitle"].ToString()))
            {
                controller.ViewBag.AlertTitle = controller.TempData["AlertTitle"];
                controller.TempData["AlertTitle"] = null;
            }
            else
            {
                controller.ViewBag.AlertTitle = "";
            }
        }
        public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public string getDOCMtime()
        {
            try
            {
                DateTime nonISD = DateTime.Now;

                //Change Time zone to ISD timezone
                TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                //DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);
                DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, myTZ);
                //ISDTime = DateTime.ParseExact(ISDTime,"dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture);
                string currentDateString = ISDTime.ToString("dd-MM-yyyy HH:mm:ss");

                string[] dt1 = currentDateString.Split(' ');
                string[] date = dt1[0].Split('-');
                string[] time = dt1[1].Split(':');

                return date[2] + "-" + date[1] + "-" + date[0] + " " + time[0] + ":" + time[1] + ":" + time[2];
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }
    }
}
