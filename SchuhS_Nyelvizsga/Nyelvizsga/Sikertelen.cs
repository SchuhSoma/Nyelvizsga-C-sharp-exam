using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvizsga
{
    class Sikertelen
    {
        public string Nyelv;
        public int[] Sikertelentmb = new int[10];
        public int OsszesNyelviVizsga2
        {
            get
            {
                int SikertelenOsszes = 0;
                for (int i = 1; i < Sikertelentmb.Length; i++)
                {
                    SikertelenOsszes += Sikertelentmb[i];
                }
                return SikertelenOsszes;
            }
        }
        public Sikertelen(string sor)
        {
            var dbok = sor.Split(';');
            this.Nyelv = dbok[0];            
            for (int i = 0; i < Sikertelentmb.Length; i++)
            {
                Sikertelentmb[i] = int.Parse(dbok[i + 1]);
            }
        }
    }
}
