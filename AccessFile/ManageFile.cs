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
        public int checkDBFile(string dbFileName)
        {
            try
            {
                FileStream fs = new FileStream(dbFileName, FileMode.OpenOrCreate);
                fs.Close();
                //File.Create(dbFileName);
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
                string[] lines = File.ReadAllLines(fileName);
                return lines[0];
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
                string[] lines = new string[1];
                lines[0] = projectName;
                File.WriteAllLines(fileName, lines);
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
        public int createFile(string path, string curDir, string fileName)
        {
            try
            {
                if (fileName != null && fileName != "")
                {
                    string[] lines = new string[3];
                    lines[0] = "namespace " + curDir + "\n{";
                    lines[1] = "public class " + fileName + "\n{";
                    lines[2] = "}\n}";
                    File.WriteAllLines(path + @"\" + fileName + ".cs", lines);
                    return 0;
                }
                return -1;
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
        public bool checkFile(string path, string fileName)
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                int i = 0;
                while(files.Length > i)
                {
                    if(fileName + ".cs" == files[i])
                    {
                        return true;
                    }
                    i++;
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public int createField(string path, string currentDir, string fileName, string fields)
        {
            try
            {
                int point = fields.IndexOf("-");
                int point2 = fields.IndexOf(",");
                string name;
                string type;
                while (point != -1)
                {
                    name = fields.Substring(0, point);
                    type = fields.Substring(point + 1, point2 - point - 1);
                    addField(path, fileName, name, type);
                    fields = fields.Substring(point2 + 1);
                    point = fields.IndexOf("-");
                    point2 = fields.IndexOf(",");
                }
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int addField(string path, string fileName, string fieldName, string type)
        {
            try
            {
                string[] lines = File.ReadAllLines(path + @"\" + fileName + ".cs");
                lines[lines.Length - 2] = "private " + type + " " + fieldName + ";\n" + "public " + type + " getset" + fieldName + "\n{\nget { return " + fieldName + "; }\nset { " + fieldName + " = getset" + fieldName + "; }\n}\n}";
                File.WriteAllLines(path + @"\" + fileName + ".cs", lines);
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int deleteField(string path, string fileName, string fieldName)
        {
            try
            {
                string[] lines = File.ReadAllLines(path + @"\" + fileName + ".cs");
                string[] newLines = new string[lines.Length - 6];
                int i = 0;
                int j = 0;
                int point;
                while(lines.Length > i)
                {
                    point = lines[i].IndexOf(fieldName);
                    if(point == -1)
                    {
                        newLines[j] = lines[i];
                    }
                    else
                    {
                        i = i + 6;
                        continue;
                    }
                    j++;
                    i++;
                }
                File.WriteAllLines(path + @"\" + fileName + ".cs", newLines);
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int modifyField(string path, string fileName, string oldFieldName, string newFieldName, string fieldType)
        {
            int result = -2;
            try
            {
                result = deleteField(path, fileName, oldFieldName);
                if(result != 0)
                {
                    return -2;
                }
                result = addField(path, fileName, newFieldName, fieldType);
                return result;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        public int checkField(string path, string fileName, string fields)
        {
            try
            {
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
    }
}
