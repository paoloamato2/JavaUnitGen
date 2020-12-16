namespace TestingCombinatoriale
{
    public class ComboBoxItem
    {
        public int Id;
        public string Nome;

        public ComboBoxItem(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}