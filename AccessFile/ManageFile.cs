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
        /// <summary>
        /// DB file 검사
        /// </summary>
        /// <param name="dbFileName"></param> DB file 이름
        /// <returns></returns>
        public int checkDBFile(string dbFileName)
        {
            try
            {
                FileStream fs = new FileStream(dbFileName, FileMode.Open);
                fs.Close();
                //File.Create(dbFileName);
                return 0; // success
            }
            catch(FileNotFoundException e)
            {
                FileStream fs = new FileStream(dbFileName, FileMode.Create);
                fs.Close();
                return -4;
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
        /// <summary>
        /// 최근의 project를 txt file에서 읽어옴
        /// </summary>
        /// <param name="fileName"></param> txt file 이름
        /// <returns></returns> project명 : 성공, null : 실패
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
        /// <summary>
        /// 현재 적용된 project를 txt file에 저장
        /// </summary>
        /// <param name="fileName"></param> txt file 이름
        /// <param name="projectName"></param> 현재 적용된 project
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
        /// <summary>
        /// directory 생성
        /// </summary>
        /// <param name="path"></param> namespace
        /// <returns></returns> 0 : 성공, -2 : unknown
        public int createDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// directory 삭제
        /// </summary>
        /// <param name="path"></param> namespace
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
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
        /// <summary>
        /// directory 변경(not used)
        /// </summary>
        /// <param name="path"></param> namespace
        /// <param name="newPath"></param> 변경할 namepsace
        /// <returns></returns> 0 : 성공, -2 : unknown
        public int modifyDirectory(string path, string newPath)
        {
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(path);
                DirectoryInfo CDir = new DirectoryInfo(newPath);
                if (CDir.Exists)
                {
                    Directory.Delete(newPath, true);
                }
                if (Dir.Exists)
                {
                    while (true)
                    {
                        try
                        {
                            Dir.MoveTo(newPath);
                            break;
                        }
                        // 변경할 폴더의 하위폴더를 열고 있을 경우 예외발생
                        catch(IOException e)
                        {
                            // 폴더 창을 닫아야 함
                            System.Windows.Forms.MessageBox.Show("Close current folder's window");
                            continue;
                        }
                    }
                    Dir = new DirectoryInfo(newPath);
                }
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// cs file 생성
        /// </summary>
        /// <param name="path"></param> namespace(directory)
        /// <param name="fileName"></param> file명
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int createFile(string path, string fileName, string fields)
        {
            try
            {
                int point;
                int point2;
                string name;
                string type;
                string methods = "";
                string field;
                string lines = string.Format(
@"namespace {0} 
{{
    public class {1} 
    {{
        "
, path, fileName);
                if(fields != null && fields != "")
                {
                    point = fields.IndexOf("-");
                    point2 = fields.IndexOf(",");
                    while(point != -1)
                    {
                        name = fields.Substring(0, point);
                        type = fields.Substring(point + 1, point2 - point - 1);
                        lines += "private " + type + " " + name + ";\n\t\t";
                        field = name[0].ToString().ToUpper();
                        if(name.Length > 1)
                        {
                            field += name.Substring(1);
                        }
                        methods += "\n\t\tpublic " + type + " getset" + field + 
                            "\n\t\t{\n\t\t\tget { return " + name + 
                            "; }\n\t\t\tset { " + name + " = value; }\n\t\t}";
                        fields = fields.Substring(point2 + 1);
                        point = fields.IndexOf("-");
                        point2 = fields.IndexOf(",");
                    }
                }
                lines += methods;
                lines += "\n\t}\n}";
                File.WriteAllText(path.Replace(".", @"\") + @"\" + fileName + ".cs", lines);
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
        /// <summary>
        /// cs file 삭제(not used)
        /// </summary>
        /// <param name="path"></param> namespace(directory)
        /// <param name="fileName"></param> file명
        /// <returns></returns> 0 : 성공, -2 : unknown
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
        /// <summary>
        /// cs file 변경 (not used)
        /// </summary>
        /// <param name="path"></param> namespace(directory)
        /// <param name="fileName"></param> file명
        /// <param name="newFileName"></param> 변경할 file명
        /// <returns></returns> 0 : 성공, -2 : unknown
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
        /// <summary>
        /// cs file 존재여부 검사 (not usded)
        /// </summary>
        /// <param name="path"></param> namespace(directory)
        /// <param name="fileName"></param> file명
        /// <returns></returns> 존재여부
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
    }
}
