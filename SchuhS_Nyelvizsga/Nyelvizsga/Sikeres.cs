using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyelvizsga
{
    class Sikeres
    {
        public string Nyelv;
        public int[] Sikerestmb = new int[10];

        public int OsszesNyelviVizsga1
        {
            get
            {
                int SikeresOsszes = 0;
                for (int i = 1; i < Sikerestmb.Length; i++)
                {
                    SikeresOsszes += Sikerestmb[i];
                }
                return SikeresOsszes;
            }            
        }
        public Sikeres(string sor)
        {
            var dbok = sor.Split(';');
            this.Nyelv = dbok[0];
            for (int i = 0; i < Sikerestmb.Length; i++)
            {
                Sikerestmb[i] = int.Parse(dbok[i + 1]);
            }
        }
    }
}
