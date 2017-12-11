//Written by CZL,2008.10.20��Wuhan

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
//int  d=10;   
    
//d.ToString("x")   //���x��Ϊ�أ������ͱ���ˣ���λ���ַ����ˣ�   
    
//int x=Convert.ToInt32(d.ToString("x"),16);�����ѣ������Ƶ��ַ�����أ������Ƶģ�

namespace BaseLibs
{
    //2ά���սṹ
    struct Struct2D
    {
        public string m_sName;
        public string m_sValue;       
    }
    class BaseLib
    {
               
        public static string GetAFolder(string strDefaultFolder=null)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Please select the data folder";
            folderBrowserDialog.SelectedPath = strDefaultFolder;
            folderBrowserDialog.ShowNewFolderButton = false;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return null;
            }
            string folderName = folderBrowserDialog.SelectedPath;
            if (!Directory.Exists(folderName)) return null;
            return folderName;
        }       
    }
    //��дINI�ļ�
    class IniFile
    {
        [DllImport("kernel32.dll")]
        //����˵��:(ini�ļ��еĶ���,INI�еĹؼ���,INI�йؼ��ֵ���ֵ,INI�ļ�������·��) 
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll")]
        //����˵��:(INI�ļ��еĶ���,INI�еĹؼ���,INI�޷���ȡʱ��ȱʡֵ,��ȡ��ֵ,��ֵ��С,INI�ļ�������·��) 
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        
        public IniFile()
        {
        }
        public static long Set(string section, string key, string val, string filePath)
        {
           return WritePrivateProfileString( section, key, val, filePath);
        }
        public static long Get(string section, string key, string def, StringBuilder retVal, int size, string filePath)
        {
           return GetPrivateProfileString(section,key,def,retVal,size,filePath);
        }        
    }
    class SystemInfo
    {
        public static string GetWindowsTempPath()
        {
            return Path.GetTempPath();//windows temp
        }
        public static string GetExePath()
        {
            return Application.StartupPath; 
        }
        public static string GetDataPath()
        {
            return Application.StartupPath + "\\data\\";
        }
        public static string GetSysPath()
        {
            return Application.StartupPath + "\\sys\\";
        }
        public static string GetRuleDBFile()
        {
            return Application.StartupPath + "\\Rules.mdb";
        }
        public static string GetStdDBFile()
        {
            return Application.StartupPath + "\\std.mdb";
        }
        public static string GetIniFile()
        {
            return Application.StartupPath + "\\sys.ini";
        }
        public static string GetPathFileNameNoExt(string strFullName)//c:\temp\abc.exe  -> c:\temp\abc
        {
            return Path.GetDirectoryName(strFullName) + "\\" + Path.GetFileNameWithoutExtension(strFullName);
        }
        public static string GetPathFileNameWithPrefixNoExt(string strFullName)//c:\temp\abc.exe  -> c:\temp\BHabc
        {
            return Path.GetDirectoryName(strFullName) + "\\" + "BH" + Path.GetFileNameWithoutExtension(strFullName);
        }
        //strAddedDir=""Ϊ�ղ��ڵ�ǰ�ļ������½��ļ���,
        //strPrefix=""Ϊ�ղ���ǰ׺,
        //strNewExt=""Ϊ�ձ���ԭ���ĺ�׺
        public static string GetPathFileNameWithAddedDir(string strFullName,string strAddedDir,string strPrefix,string strNewExt)
        {
            if (strNewExt.Length != 0)
            {
                if (strAddedDir.Length != 0)
                {
                    return Path.GetDirectoryName(strFullName) + "\\" + strAddedDir + "\\" + strPrefix + Path.GetFileNameWithoutExtension(strFullName) + strNewExt;
                }
                else
                {
                    return Path.GetDirectoryName(strFullName) + "\\" + strPrefix + Path.GetFileNameWithoutExtension(strFullName) + strNewExt;
                }
            }
            else
            {
                if (strAddedDir.Length != 0)
                {
                    return Path.GetDirectoryName(strFullName) + "\\" + strAddedDir + "\\" + strPrefix + Path.GetFileName(strFullName);
                }
                else
                {
                    return Path.GetDirectoryName(strFullName) + "\\" + strPrefix + Path.GetFileName(strFullName);
                }
            }            
        }

        public static string[] GetFilesInFolder(string sExt)//sExt = ".dwg"
        {
            string folderName = BaseLib.GetAFolder("");
            if (folderName!=null) return null;
            if (sExt.IndexOf(".") == -1)
            {
                sExt = "*." + sExt;
            }
            else
            {
                sExt = "*" + sExt;
            }
            string[] strFiles = Directory.GetFiles(folderName,sExt);
            return strFiles;
        }
        public static string[] GetFilesInFolder(string strFolder, string sExt)//strFolder = "c:\temp",sExt = ".dwg"
        {
            if (!Directory.Exists(strFolder)) return null;
            if (sExt.IndexOf(".") == -1)
            {
                sExt = "*." + sExt;
            }
            else
            {
                sExt = "*" + sExt;
            }
            string[] strFiles = Directory.GetFiles(strFolder, sExt);
            return strFiles;
        }

    }
    class FileInfo
    {
        string sFullName;//ȫ·����c:\temp\abc.exe
        string sDirectory;//·��c:\temp
        string sPathFileName;//·���ļ���c:\temp\abc(�޺�׺)
        string sFileName;//�ļ���abc.exe
        string sFileNameNoExt;//�ļ���abc
        string sExt;//��׺ .exe(���е�)
        string sRoot;//��Ŀ¼c:\\
        public FileInfo(string strFullName)
        {
            sFullName = strFullName;
            //string s = Path.GetFullPath(sFullName);//c:\temp\abc.exe
            sDirectory = Path.GetDirectoryName(strFullName);//c:\temp
            sExt = Path.GetExtension(strFullName);//.exe
            sFileName = Path.GetFileName(strFullName);//abc.exe
            sFileNameNoExt = Path.GetFileNameWithoutExtension(strFullName);//abc
            sRoot = Path.GetPathRoot(strFullName);//c:\\  
            sPathFileName = sDirectory + "\\" + sFileNameNoExt;//·���ļ���c:\temp\abc(�޺�׺)
        }
        public string GetFullName()//c:\temp\abc.exe
        {
            return sFullName;
        }
        public string GetDirectory()//c:\temp
        {
            return sDirectory;
        }
        public string GetPathFileName()//·���ļ���c:\temp\abc(�޺�׺)
        {
            return sPathFileName;
        }
        public string GetFileName()//abc.exe
        {
            return sFileName;
        }
        public string GetFileNameNoExt()//abc
        {
            return sFileNameNoExt;
        }
        public string GetExt()//.ext
        {
            return sExt;
        }
        public string GetRoot()//c:\
        {
            return sRoot;
        }
    }
   
}

