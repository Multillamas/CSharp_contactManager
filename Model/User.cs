using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        int m_Iduser = 0;
        string m_Username = string.Empty;
        string m_Userpassword = string.Empty;

        #region Propertiers
        public int Iduser {
            get { return m_Iduser; }
            set { m_Iduser = value; }
        }

        public string Username {
            get { return m_Username; }
            set { m_Username = value; }
        }

        public string Userpassword {
            get { return m_Userpassword; }
            set { m_Userpassword = value; }
        }

    #endregion Propertiers

    }
}
