using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELREORS
{
    class status
    {
        public int statusM;

        public status(int statusM)
        {
            this.statusM = statusM;
        }
        public void setStatusM(int statusM)
        {
            this.statusM = statusM;
        }
        public int getStatusM()
        {
            return statusM;
        }
    }
}
