using System.Collections.Generic;

namespace TestingCombinatoriale
{
    public class ColonnaMatrice
    {
        public int Id;
        public List<int> IDrigheCoperte;

        public ColonnaMatrice(int id, List<int> idrigheCoperte)
        {
            Id = id;
            IDrigheCoperte = idrigheCoperte;
        }
    }
}