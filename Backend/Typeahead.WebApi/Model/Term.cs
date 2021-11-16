using System;

namespace Typeahead.WebApi.Model
{
    public class Term
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || ! (obj is Term term))
                return false;
            return Id == term.Id && Name == term.Name;
        }

        protected bool Equals(Term other)
        {
            return Id == other.Id && Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}