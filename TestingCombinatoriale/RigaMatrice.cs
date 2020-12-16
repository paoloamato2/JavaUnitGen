using System.Collections.Generic;

namespace TestingCombinatoriale
{
    public class RigaMatrice
    {
        public int Id;
        public List<int> IDcolonneCoperte;

        public RigaMatrice(int id, List<int> idcolonneCoperte)
        {
            Id = id;
            IDcolonneCoperte = idcolonneCoperte;
        }
    }
}