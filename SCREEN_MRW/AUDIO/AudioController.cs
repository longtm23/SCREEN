using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCREEN_MRW.AUDIO
{
    public class AudioController
    {
        #region AUDIO
        public Dictionary<string, string> loadFileAudio(string urlFolder, string lang)
        {
            var dic = new Dictionary<string, string>();
            var url = urlFolder + "\\" + lang;
            if (Directory.Exists(url))
            {
                var arrAudio = Directory.GetFiles(url, "*.mp3");
                char[] reg = { '.', '\\' };
                foreach (var iNum in arrAudio)
                {
                    var key = getFileName(iNum, reg);
                    dic.Add(key, iNum);
                }
            }
            else
            {
                MessageBox.Show("thư mục Audio không tồn tại!");
            }
            return dic;
        }

        public string getFileName(string url, char[] reg)
        {
            var str = url.Split(reg);
            return str[str.Length - 2];
        }
        #endregion
    }
}
