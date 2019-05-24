using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Project.FileHandler
{
    public class ReadWrite
    {
        public ReadWrite()
        {

        }

        public void writeToFile(string path, Object data)
        {
            var json = new JavaScriptSerializer().Serialize(data);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(json);
            }
        }
    }
}
