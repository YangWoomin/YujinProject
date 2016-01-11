using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AccessFile
{
    public class ManageFile
    {
        private FileStream fs;
        private StreamReader sr;
        private StreamWriter sw;
        public int checkDBFile(string dbFileName)
        {
            try
            {
                fs = new FileStream(dbFileName, FileMode.OpenOrCreate);
                fs.Close();
                return 0; // success
            }
            catch (FileLoadException e)
            {
                return -2; // success but don't care
            }
            catch (IOException e)
            {
                return -3; // success, already used file, normally this case
            }
            catch (Exception e)
            {
                return -1; // unknown exception
            }
        }
        public string getCurrentProject(string fileName)
        {
            try
            {
                fs = new FileStream(fileName, FileMode.OpenOrCreate);
                sr = new StreamReader(fs);
                string projectName = sr.ReadLine();
                sr.Close();
                fs.Close();
                return projectName;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public void setCurrentProject(string fileName, string projectName)
        {
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(projectName);
                sw.Close();
                fs.Close();
            }
            catch(Exception e)
            {
            }
        }
        public int createDirectory(string path)
        {
            try {
                Directory.CreateDirectory(path);
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int deleteDirectory(string path)
        {
            try
            {
                if(Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    return 0;
                }
                return -1;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int modifyDirectory(string path, string newPath)
        {
            try
            {
                Directory.Move(path, newPath);
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int createFile(string path, string fileName)
        {
            try
            {
                fs = new FileStream(path + @"\" + fileName + ".cs", FileMode.CreateNew);
                return 0;
            }
            catch(IOException e)
            {
                return -1;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int deleteFile(string path, string fileName)
        {
            try
            {
                File.Delete(path + @"\" + fileName + ".cs");
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int modifyFile(string path, string fileName, string newFileName)
        {
            try
            {
                File.Move(path + @"\" + fileName + ".cs", path + @"\" + newFileName + ".cs");
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int createField(string path, string currentDir, string fileName, string fields)
        {
            try
            {
                fs = new FileStream(path + @"\" + fileName + ".cs", FileMode.OpenOrCreate);
                sw = new StreamWriter(fs, Encoding.UTF8);
                sw.WriteLine("namespace " + currentDir + "\n{");
                sw.WriteLine("public class " + fileName + "\n{");
                int point = fields.IndexOf("-");
                int point2 = fields.IndexOf(",");
                string name;
                string type;
                while(point != -1)
                {
                    name = fields.Substring(0, point);
                    type = fields.Substring(point+1, point2 - point - 1);
                    sw.WriteLine("private " + type + " " + name + ";");
                    sw.WriteLine("public " + type + " getset" + name + "\n{");
                    sw.WriteLine("get { return " + name + "; }");
                    sw.WriteLine("set { " + name + " = " + "getset" + name + "; }\n}");
                    fields = fields.Substring(point2 + 1);
                    point = fields.IndexOf("-");
                    point2 = fields.IndexOf(",");
                }
                sw.WriteLine("}\n}");
                sw.Close();
                fs.Close();
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
    }
}
