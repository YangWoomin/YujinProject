using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessDB
{
    public class CheckCharacter
    {
        public int checkString(string str)
        {
            try {
                if (str[0] >= 48 && str[0] <= 57)
                {
                    MessageBox.Show("A number is not available for first character.");
                    return -3;
                }
                int i = 0;
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
        public int checkNamespace(string namespaceStr)
        {
            try
            {
                if (namespaceStr[0] >= 48 && namespaceStr[0] <= 57)
                {
                    MessageBox.Show("A number is not available for first character.");
                    return -3;
                }
                int j = 0;
                while (namespaceStr.Length > j)
                {
                    if (namespaceStr[j] < 46 || namespaceStr[j] == 47 || (namespaceStr[j] > 57 && namespaceStr[j] < 65) || (namespaceStr[j] > 90 && namespaceStr[j] < 97) || namespaceStr[j] > 122)
                    {
                        MessageBox.Show("It is only possible to use alphabets or numbers with dot.");
                        return -1;
                    }
                    j++;
                }
                int point = namespaceStr.IndexOf(".");
                while(point != -1)
                {
                    if(point == 0 || point == namespaceStr.Length - 1)
                    {
                        MessageBox.Show("Invalid namespace form.");
                        return -1;
                    }
                    if (namespaceStr[0] >= 48 && namespaceStr[0] <= 57)
                    {
                        MessageBox.Show("A number is not available for first character.");
                        return -3;
                    }
                    int i = 0;
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
                return 0;
            }
            catch(Exception e)
            {
                return -2;
            }
        }
    }
}
