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

        /// <summary>
        /// 생성자
        /// </summary>
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
        /// <summary>
        /// 필드 1개에 대한 rows를 반환하는 select문(중복포함)
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="column"></param> 필드명(name, class, field)
        /// <param name="condition"></param> 조건문
        /// <returns></returns> 검색한 소문자 형태의 문자열
        public string[] getRows(string tName, string column, string condition)
        {
            try
            {
                strQuery = "select " + column + " from " + tName;
                if (condition != null)
                {
                    strQuery = strQuery + " where " + condition + " collate nocase";
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
        /// <summary>
        /// 필드 1개에 대한 rows를 반환하는 select문(중복제거)
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="column"></param> 필드명(name, class, field)
        /// <param name="condition"></param> 조건문
        /// <returns></returns> 검색한 소문자 형태의 문자열
        public string[] getRowsDistinct(string tName, string column, string condition)
        {
            try
            {
                strQuery = "select distinct " + column + " from " + tName;
                if (condition != null)
                {
                    strQuery = strQuery + " where " + condition + " collate nocase";
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
        /// <summary>
        /// 1행을 추가하는 insert문(namespace 필수)
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="name"></param> namespace명
        /// <param name="className"></param> class명
        /// <param name="field"></param> field명
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
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
                    // strQuery = strQuery + "NULL, ";
                    return -1;
                }
                if (className != null)
                {
                    strQuery = strQuery + "'" + className + "', ";
                }
                else
                {
                    strQuery = strQuery + "null, ";
                }
                if (field != null)
                {
                    strQuery = strQuery + "'" + field + "')";
                }
                else
                {
                    strQuery = strQuery + "null)";
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
        /// <summary>
        /// 1행을 삭제하는 delete문
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="condition"></param> 조건문
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int deleteRow(string tName, string condition)
        {
            try
            {
                if (condition == null)
                {
                    return -2;
                }
                strQuery = "delete from " + tName + " where " + condition + " collate nocase";
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
        /// <summary>
        /// 1행을 갱신하는 update문
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="set"></param> 갱신값
        /// <param name="condition"></param> 조건문
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int updateRow(string tName, string set, string condition)
        {
            try
            {
                if (condition == null)
                {
                    return -2;
                }
                strQuery = "update " + tName + " set " + set + " where " + condition + " collate nocase";
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
        /// <summary>
        /// project를 삭제하는 drop table문
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int deleteTable(string tName)
        {
            try
            {
                string[] rows = getRows("sqlite_master", "name", "type = 'table'");
                int i = 0;
                while (rows.Length > i)
                {
                    if (rows[i] == tName)
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
        /// <summary>
        /// project를 생성하는 create문
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="beforeTName"></param> 이전 project명(project명 변경 가능)
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int createTable(string tName, string beforeTName)
        {
            try
            {
                // 중복검사
                string[] rows = getRows("sqlite_master", "name", "type = 'table'");
                if (rows != null)
                {
                    int i = 0;
                    while (rows.Length > i)
                    {
                        if (rows[i] == tName)
                            return -1;
                        i++;
                    }
                }
                // table 생성
                if (beforeTName == null)
                {
                    strQuery = "create table " + tName + "(name varchar(50) not null, class varchar(50), field varchar(500))";
                }
                // table 변경
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
        /// <summary>
        /// project명을 변경하는 함수
        /// </summary>
        /// <param name="beforeTName"></param> 이전 project명
        /// <param name="afterTName"></param> 변경한 project명
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int changeTable(string beforeTName, string afterTName)
        {
            try
            {
                int result = createTable(afterTName, beforeTName);
                if (result != 0)
                    return result;
                result = deleteTable(beforeTName);
                return result;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// namespace 1개를 추가하는 insert문
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int insertNamespace(string tName, string path)
        {
            try
            {
                // namespace를 조각내어 검사
                string str = path;
                int point = str.LastIndexOf(".");
                string[] rows;
                while (point != -1)
                {
                    rows = getRows(tName, "name", "name = '" + str + "'");
                    try
                    {
                        // 중복검사
                        if(rows[0].ToLower() == str.ToLower())
                        {
                            if(str == path)
                            {
                                return -1; // 생성하고자 하는 namespace가 이미 존재
                            }
                            else
                            {
                                return 0; // 이후의 namespace는 검사할 필요가 없음
                            }
                        }
                    }
                    // 예외가 발생하면 해당 namespace가 없으므로 생성
                    catch (Exception e)
                    {
                        insertRow(tName, str, null, null);
                        str = str.Substring(0, point);
                        point = str.LastIndexOf(".");
                    }
                }
                // 마지막 namespace 조각처리
                rows = getRows(tName, "name", "name = '" + str + "'");
                try {
                    if (rows[0].ToLower() == str.ToLower())
                    {
                        if (str == path)
                        {
                            return -1; // 생성하고자 하는 namespace가 이미 존재
                        } 
                        else
                        {
                            return 0;
                        }
                    }
                }
                catch(Exception e)
                {
                    insertRow(tName, str, null, null);
                    return 0;
                }
                return -2;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// namespace를 삭제하는 delete문
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int deleteNamespace(string tName, string path)
        {
            try
            {
                string[] rows = getRows(tName, "name", "name = '" + path + "'");
                try
                {
                    // 존재여부 검사
                    if (rows.Length < 1)
                    {
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    return -1;
                }
                // 하위 namespace까지 모두 삭제
                int result = deleteRow(tName, "name like '" + path + ".%' or name = '" + path + "'");
                return result;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// namespace를 변경하는 함수
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <param name="newPath"></param> 변경할 namespace
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int changeNamespace(string tName, string path, string newPath)
        {
            try
            {
                // 해당 namespace의 하위 namespace도 모두 추출
                string[] rows = getRows(tName, "name", "name like '" + path + ".%' or name = '" + path + "'");
                int i = 0;
                try
                {
                    // 존재여부 검사
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
        /// <summary>
        /// class 1개를 추가하는 함수
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <param name="className"></param> class명
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int insertClass(string tName, string path, string className)
        {
            try
            {
                int result = -2;
                string[] row = getRows(tName, "class", "name = '" + path + "' and class = '" + className + "'");
                try
                {
                    // 중복검사
                    if(row.Length > 0 && row[0] != "")
                    {
                        return -1;
                    }
                }
                catch(Exception e)
                {
                }

                // namespace만 있고 class가 없을 경우부터 처리
                row = getRows(tName, "name", "name = '" + path + "' and class is null");
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

                // 위의 경우가 아닌 경우 새로 추가
                result = insertRow(tName, path, className, null);
                return result;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// class 1개를 삭제하는 함수
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <param name="className"></param> class명
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int deleteClass(string tName, string path, string className)
        {
            try
            {
                string[] rows = getRows(tName, "name", "name = '" + path + "' and class = '" + className + "'");
                try
                {
                    // 존재여부 검사
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
                return -1;
            }
            catch (Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// class명을 변경하는 함수
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <param name="className"></param> class명
        /// <param name="newClassName"></param> 변경할 class명
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int changeClass(string tName, string path, string className, string newClassName)
        {
            try
            {
                string[] rows = getRows(tName, "name", "name = '" + path + "' and class = '" + newClassName + "'");
                try
                {
                    // 중복검사
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
        /// <summary>
        /// field 1개를 추가하는 함수
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <param name="className"></param> class명
        /// <param name="fieldName"></param> field명
        /// <param name="fieldType"></param> field type
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int insertField(string tName, string path, string className, string fieldName, string fieldType)
        {
            try
            {
                int result = -2;
                // 해당 class의 field가 null인 것부터 처리
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

                // 위의 경우가 아닐 경우
                row = getRows(tName, "field", "name = '" + path + "' and class = '" + className + "' and (field like '" + fieldName + "-%' or field like '%," + fieldName + "-%')");
                try
                {
                    // 중복검사
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
        /// <summary>
        /// field 1개를 삭제하는 함수
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <param name="className"></param> class명
        /// <param name="fieldName"></param> field명
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int deleteField(string tName, string path, string className, string fieldName)
        {
            int result = -2;
            string[] row = getRows(tName, "field", "name = '" + path + "' and class = '" + className + "'");
            try
            {
                // 존재여부 검사
                if(row.Length > 0)
                {
                    try {
                        int point = row[0].IndexOf(fieldName);
                        if (point != -1)
                        {
                            if (point == 1) // field가 첫 번째에 위치할 경우
                            {
                                int point2 = row[0].IndexOf(",");
                                row[0] = row[0].Substring(point2);
                            }
                            else // 첫 번째가 아닐 경우
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
        /// <summary>
        /// field명을 변경하는 함수
        /// </summary>
        /// <param name="tName"></param> project명(table명)
        /// <param name="path"></param> namespace
        /// <param name="className"></param> class명
        /// <param name="oldFieldName"></param> 이전 field명
        /// <param name="newFieldName"></param> 변경할 field명
        /// <param name="fieldType"></param> 변경할 field type
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public int changeField(string tName, string path, string className, string oldFieldName, string newFieldName, string fieldType)
        {
            int point = oldFieldName.IndexOf("(");
            oldFieldName = oldFieldName.Substring(0, point);
            int result = -2;
            try
            {
                // 먼저 삭제 후
                result = deleteField(tName, path, className, oldFieldName);
                if(result != 0)
                {
                    return result;
                }
                // 삽입
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
