using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCREEN_MRW.DTO
{
    public class Asset
    {
        public Voice voice { get; set; }
    }
    public class Voice
    {
        public string id { get; set; }
        public int mtime { get; set; }
        public int dtime { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public I18n18 i18n { get; set; }
        //public Behavior behavior { get; set; }
    }
    public class I18n18
    {
        public En en { get; set; }
        public Sp sp { get; set; }
        public Vi vi { get; set; }
    }

    public class Vi
    {
        public List<string> files { get; set; }
        public string folder { get; set; }
        public string format { get; set; }
        public List<Item> items { get; set; }
    }
    public class Item
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class En
    {
        public List<string> files { get; set; }
        public string folder { get; set; }
        public string format { get; set; }
        public List<Item> items { get; set; }
    }

    public class Sp
    {
        public List<string> files { get; set; }
        public string folder { get; set; }
        public string format { get; set; }
        public List<Item> items { get; set; }
    }




}
