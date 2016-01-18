using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessDB
{
    public static class CheckCharacter
    {
        /// <summary>
        /// class명이나 field명의 적합성을 검사(특수문자 사용불가)
        /// </summary>
        /// <param name="str"></param> 검사할 문자열
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public static int checkString(string str, int flag)
        {
            try
            {
                if(flag == 0) // project or class name
                {
                    if(str[0] < 65 || str[0] > 90)
                    {
                        MessageBox.Show("The name must be started from upper case");
                        return -1;
                    }
                }
                else // field name
                {
                    if (str[0] < 97 || str[0] > 122)
                    {
                        MessageBox.Show("The name must be started from lower case");
                        return -1;
                    }
                }
                int i = 0;
                // 특수문자 등 검사
                while (str.Length > i)
                {
                    if (str[i] < 48 || (str[i] > 57 && str[i] < 65) || (str[i] > 90 && str[i] < 97) || str[i] > 122)
                    {
                        MessageBox.Show("It is only possible to use alphabets or numbers.");
                        return -1;
                    }
                    i++;
                }
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
        /// <summary>
        /// namespace 적합성을 검사(특수문자 사용불가)
        /// </summary>
        /// <param name="namespaceStr"></param> 검사할 namespace
        /// <returns></returns> 0 : 성공, -1 : 실패, -2 : unknown
        public static int checkNamespace(string namespaceStr)
        {
            try
            {
                int j = 0;
                // 특수문자 검사
                while (namespaceStr.Length > j)
                {
                    if (namespaceStr[j] < 46 || namespaceStr[j] == 47 || (namespaceStr[j] > 57 && namespaceStr[j] < 65) || (namespaceStr[j] > 90 && namespaceStr[j] < 97) || namespaceStr[j] > 122)
                    {
                        MessageBox.Show("It is only possible to use alphabets or numbers with dot.");
                        return -1;
                    }
                    j++;
                }
                int i;
                int point = namespaceStr.IndexOf(".");
                // dot(.) 표현의 적합성을 검사
                while(point != -1)
                {
                    if(point == 0 || point == namespaceStr.Length - 1)
                    {
                        MessageBox.Show("Invalid namespace form.");
                        return -1;
                    }
                    else if(namespaceStr[0] < 65 || namespaceStr[0] > 90)
                    {
                        MessageBox.Show("Every namespace must be started from upper case");
                        return -1;
                    }
                    i = 0;
                    while (namespaceStr.Length > i)
                    {
                        if (namespaceStr[i] < 46 || namespaceStr[i] == 47 || (namespaceStr[i] > 57 && namespaceStr[i] < 65) || (namespaceStr[i] > 90 && namespaceStr[i] < 97) || namespaceStr[i] > 122)
                        {
                            MessageBox.Show("It is only possible to use alphabets or numbers with dot.");
                            return -1;
                        }
                        i++;
                    }
                    namespaceStr = namespaceStr.Substring(point + 1);
                    point = namespaceStr.IndexOf(".");
                }
                // 마지막 남은 문자열 검사
                if (namespaceStr[0] < 65 || namespaceStr[0] > 90)
                {
                    MessageBox.Show("Every namespace must be started from upper case");
                    return -1;
                }
                i = 0;
                while (namespaceStr.Length > i)
                {
                    if (namespaceStr[i] < 46 || namespaceStr[i] == 47 || (namespaceStr[i] > 57 && namespaceStr[i] < 65) || (namespaceStr[i] > 90 && namespaceStr[i] < 97) || namespaceStr[i] > 122)
                    {
                        MessageBox.Show("It is only possible to use alphabets or numbers with dot.");
                        return -1;
                    }
                    i++;
                }
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
    }
}
