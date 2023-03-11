using System.Reflection;

namespace OrderService.Domain.SeedWork
{
    public class Enumeration : IComparable
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Select(o => o.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration otherObj) { return false; }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherObj.Id);

            return typeMatches && valueMatches;
        }
        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

        public override int GetHashCode()  => Id.GetHashCode();
            
    }
}
