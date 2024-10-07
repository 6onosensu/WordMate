using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordMate
{
    public class Word
    {
        public Guid Id { get; set; }
        public string EngWord { get; set; }
        public string RusWord { get; set; }
        public string Meaning { get; set; }
        public string Image { get; set; }
        public string LinkToCambridgeDictionary { get; set; }
        public string Pronunciation { get; set; }
    }
}
