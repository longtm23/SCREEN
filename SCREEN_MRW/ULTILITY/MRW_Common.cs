using SCREEN_MRW.DTO;
using System;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.ULTILITY
{
    public class MRW_Common
    {
        public static ConfigMrw GetConfigByFileConfig()
        {
            try
            {
                Serializer ser = new Serializer();
                string path = string.Empty;
                string xmlInputData = string.Empty;
                string xmlOutputData = string.Empty;

                path = Directory.GetCurrentDirectory() + @"\config.xml";
                if (!System.IO.File.Exists(path))
                {
                    return null;
                }
                xmlInputData = File.ReadAllText(path);
                ConfigMrw customer2 = ser.Deserialize<ConfigMrw>(xmlInputData);
                xmlOutputData = ser.Serialize<ConfigMrw>(customer2);
                return customer2;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
