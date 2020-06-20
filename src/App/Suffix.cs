using System;
using System.Linq;

namespace App
{
    //Enummeration pattern
    public class Suffix : Entity
    {
        public static readonly Suffix Jr = new Suffix(new Guid("0e9fbb79-6c71-42ff-942f-74bdd9213681"), "Jr");
        public static readonly Suffix Sr = new Suffix(new Guid("0e9fbb79-6c71-42ff-942f-74bdd9213682"), "Sr");

        public static readonly Suffix[] AllSuffixes = { Jr, Sr };

        public string Name { get; }

        protected Suffix()
        {
        }

        private Suffix(Guid id, string name)
            : base(id)
        {
            Name = name;
        }

        public static Suffix FromId(Guid id)
        {
            return AllSuffixes.SingleOrDefault(x => x.Id == id);
        }
    }
}