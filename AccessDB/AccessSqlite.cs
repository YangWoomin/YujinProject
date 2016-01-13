using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SQLite;
using AccessFile;

namespace AccessDB
{
    public class AccessSqlite
    {
        private static string strQuery;
        private static SQLiteConnection conn;
        private static SQLiteCommand cmd;
        private int check;

        public AccessSqlite()
        {
            conn = new SQLiteConnection("Data Source = db.db");
            ManageFile mf = new ManageFile();
            check = mf.checkDBFile("db.db");
            if(check != -1)
            {
                conn.Open();
            }
        }
        private void synchronizeDirectory()
        {
            ManageFile mf = new ManageFile();
            
        }
        public int getCheck()
        {
            return check;
        }
        public string[] getRows(string tName, string column, string condition)
        {
            try
            {
                strQuery = "select " + column +" from " + tName;
                if (condition != null)
                {
                    strQuery = strQuery + " where " + condition;
                }
                strQuery = strQuery + " order by " + column + " asc";
                cmd = new SQLiteCommand(strQuery, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                cmd.Dispose();
                DataTable dt = new DataTable();
                dt.Load(reader);
                string[] rows = new string[dt.Rows.Count];
                int i = 0;
                while (rows.Length > i)
                {
                    rows[i] = dt.Rows[i][0].ToString();
                    i++;
                }
                reader.Close();
                return rows;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string[] getRowsDistinct(string tName, string column, string condition)
        {
            try
            {
                strQuery = "select distinct " + column + " from " + tName;
                if (condition != null)
                {
                    strQuery = strQuery + " where " + condition;
                }
                strQuery = strQuery + " order by " + column + " asc";
                cmd = new SQLiteCommand(strQuery, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();
                cmd.Dispose();
                DataTable dt = new DataTable();
                dt.Load(reader);
                string[] rows = new string[dt.Rows.Count];
                int i = 0;
                while (rows.Length > i)
                {
                    rows[i] = dt.Rows[i][0].ToString();
                    i++;
                }
                reader.Close();
                return rows;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int insertRow(string tName, string name, string className, string field)
        {
            try
            {
                strQuery = "insert into " + tName + " values(";
                if (name != null)
                {
                    strQuery = strQuery + "'" + name + "', ";
                }
                else
                {
                    strQuery = strQuery + "NULL, ";
                }
                if (className != null)
                {
                    strQuery = strQuery + "'" + className + "', ";
                }
                else
                {
                    strQuery = strQuery + "NULL, ";
                }
                if (field != null)
                {
                    strQuery = strQuery + "'" + field + "')";
                }
                else
                {
                    strQuery = strQuery + "NULL)";
                }
                cmd = new SQLiteCommand(strQuery, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int deleteRow(string tName, string condition)
        {
            try
            {
                strQuery = "delete from " + tName;
                if (condition != null)
                {
                    strQuery = strQuery + " where " + condition;
                }
                cmd = new SQLiteCommand(strQuery, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int updateRow(string tName, string set, string condition)
        {
            try
            {
                strQuery = "update " + tName + " set " + set;
                if (condition != null)
                {
                    strQuery = strQuery + " where " + condition;
                }
                cmd = new SQLiteCommand(strQuery, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int deleteTable(string tName)
        {
            try
            {
                string[] rows = getRows("sqlite_master", "name", "type = 'table'");
                int i = 0;
                while (rows.Length > i)
                {
                    if (string.Compare(rows[i], tName) == 0)
                    {
                        strQuery = "drop table " + tName;
                        cmd = new SQLiteCommand(strQuery, conn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        return 0;
                    }
                    i++;
                }
                return -1;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int createTable(string tName, string beforeTName)
        {
            try
            {
                string[] rows = getRows("sqlite_master", "name", "type = 'table'");
                if (rows != null)
                {
                    int i = 0;
                    while (rows.Length > i)
                    {
                        if (string.Compare(rows[i], tName) == 0)
                            return -1;
                        i++;
                    }
                }
                if (beforeTName == null)
                {
                    strQuery = "create table " + tName + "(name varchar(50) not null, class varchar(50), field varchar(500))";
                }
                else
                {
                    strQuery = "create table " + tName + " as select * from " + beforeTName;
                }
                cmd = new SQLiteCommand(strQuery, conn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int changeTable(string beforeTName, string afterTName)
        {
            try
            {
                int result = createTable(afterTName, beforeTName);
                if (result != 0)
                    return result;
                result = deleteTable(beforeTName);
                if (result != 0)
                    return result;
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int insertNamespace(string tName, string path)
        {
            try
            {
                string str = path;
                int point = str.LastIndexOf(".");
                while (point != -1)
                {
                    int result = 0;
                    string[] rows = getRows(tName, "name", "name = '" + str + "'");
                    int i = 0;
                    int flag = 0;
                    try
                    {
                        while (rows.Length > i)
                        {
                            if (rows[i] == str)
                            {
                                flag = 1;
                                if (str == path)
                                {
                                    return -1;
                                }
                            }
                            i++;
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    finally
                    {
                        if (flag == 0)
                        {
                            result = insertRow(tName, str, null, null);
                        }
                        str = str.Substring(0, point);
                        point = str.LastIndexOf(".");
                    }
                }
                string[] rows2 = getRows(tName, "name", "name = '" + str + "'");
                int flag2 = 0;
                int j = 0;
                try
                {
                    while (rows2.Length > j)
                    {
                        if (rows2[j] == str)
                        {
                            flag2 = 1;
                            if (str == path)
                            {
                                return -1;
                            }
                            j++;
                        }
                    }
                }
                catch (Exception e)
                {
                }
                finally
                {
                    if (flag2 == 0)
                    {
                        int result = insertRow(tName, str, null, null);
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int deleteNamespace(string tName, string path)
        {
            try
            {
                string[] rows = getRows(tName, "name", "name = '" + path + "'");
                try
                {
                    if (rows.Length < 1)
                    {
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    return -1;
                }
                int result = deleteRow(tName, "name like '" + path + ".%' or name = '" + path + "'");
                return result;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int changeNamespace(string tName, string path, string newPath)
        {
            try
            {
                string[] rows = getRows(tName, "name", "name like '" + path + ".%' or name = '" + path + "'");
                int i = 0;
                try
                {
                    while (rows.Length > i)
                    {
                        string newRow = rows[i];
                        newRow = newPath + rows[i].Substring(path.Length);
                        updateRow(tName, "name = '" + newRow + "'", "name = '" + rows[i] + "'");
                        i++;
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int insertClass(string tName, string path, string className)
        {
            try
            {
                int result;
                string[] row = getRows(tName, "name", "name = '" + path + "' and class is null");
                try
                {
                    if (row.Length > 0)
                    {
                        result = updateRow(tName, "class = '" + className + "'", "name = '" + path + "' and class is null");
                        return result;
                    }
                }
                catch (Exception e)
                {
                }
                string[] rows = getRows(tName, "name", "name = '" + path + "' and class = '" + className + "'");
                try
                {
                    if (rows.Length > 0)
                    {
                        return -1;
                    }
                }
                catch (Exception e)
                {
                }
                result = insertRow(tName, path, className, null);
                if (result == 0)
                {
                    return result;
                }
                else
                {
                    return result;
                }
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int deleteClass(string tName, string path, string className)
        {
            try
            {
                string[] rows = getRows(tName, "name", "name = '" + path + "' and class = '" + className + "'");
                try
                {
                    if (rows.Length > 0)
                    {
                        int result = deleteRow(tName, "name = '" + path + "' and class = '" + className + "'");
                        return result;
                    }
                }
                catch (Exception e)
                {
                    return -1;
                }
                return -2;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int changeClass(string tName, string path, string className, string newClassName)
        {
            try
            {
                string[] rows = getRows(tName, "name", "name = '" + path + "' and class = '" + newClassName + "'");
                try
                {
                    if (rows.Length > 0)
                    {
                        return -1;
                    }
                }
                catch (Exception e)
                {
                }
                int result = updateRow(tName, "class = '" + newClassName + "'", "name = '" + path + "' and class = '" + className + "'");
                return result;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int insertField(string tName, string path, string className, string fieldName, string fieldType)
        {
            try
            {
                int result = -2;
                string[] row = getRows(tName, "name", "name = '" + path + "' and class = '" + className + "' and field is null");
                try
                {
                    if(row.Length > 0)
                    {
                        result = updateRow(tName, "field = '" + fieldName + "-" + fieldType + ",'", "name = '" + path + "' and class = '" + className + "'");
                        return 0;
                    }
                }
                catch(Exception e)
                {
                }
                row = getRows(tName, "field", "name = '" + path + "' and class = '" + className + "' and (field like '" + fieldName + "-%' or field like '%," + fieldName + "-%')");
                try
                {
                    if (row.Length > 0)
                    {
                        return -1;
                    }
                }
                catch (Exception e)
                { 
                }
                row = getRows(tName, "field", "name = '" + path + "' and class = '" + className + "'");
                fieldName = row[0] + fieldName + "-" + fieldType + ",";
                result = updateRow(tName, "field = '" + fieldName + "'", "name = '" + path + "' and class = '" + className + "'");
                return result;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        public int deleteField(string tName, string path, string className, string fieldName)
        {
            int result = -2;
            string[] row = getRows(tName, "field", "name = '" + path + "' and class = '" + className + "'");
            try
            {
                if(row.Length > 0)
                {
                    try {
                        int point = row[0].IndexOf(fieldName);
                        if (point != -1)
                        {
                            if (point == 1)
                            {
                                int point2 = row[0].IndexOf(",");
                                row[0] = row[0].Substring(point2);
                            }
                            else
                            {
                                string temp = row[0].Substring(0, point);
                                row[0] = row[0].Substring(point);
                                int point2 = row[0].IndexOf(",");
                                row[0] = row[0].Substring(point2 + 1);
                                row[0] = temp + row[0];
                            }
                            result = updateRow(tName, "field = '" + row[0] + "'", "name = '" + path + "' and class = '" + className + "'");
                            return result;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch(Exception e)
                    {
                        return result;
                    }
                }
                return -1;
            }
            catch(Exception e)
            {
                return -1;
            }
        }
        public int changeField(string tName, string path, string className, string oldFieldName, string newFieldName, string fieldType)
        {
            int point = oldFieldName.IndexOf("(");
            oldFieldName = oldFieldName.Substring(0, point);
            int result = -2;
            try
            {
                result = deleteField(tName, path, className, oldFieldName);
                if(result != 0)
                {
                    return result;
                }
                result = insertField(tName, path, className, newFieldName, fieldType);
                return result;
            }
            catch(Exception e)
            {
                return result;
            }
        }
    }
}
