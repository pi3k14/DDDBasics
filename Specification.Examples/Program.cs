namespace Kodefabrikken.DDD.Specification.Examples
{
    class Program
    {
        static void Main(string[] _)
        {
            Console.WriteLine("Examples of how to use Specification");

            List<Product> products =
                [
                    new() { Id = 1, Name = "Product One", Description = "The first", Released = new DateTime(2000, 12, 25) },
                    new() { Id = 6, Name = "Six pack", Description = "Wouldn't you want one?" },
                    new() { Id = 7, Name = "The lucky one", Description = "This is getting tiresome", Released = DateTime.Now, EndOfLife = new DateTime(2030, 1, 1) },
                    new() { Id = 13, Name = "Last", Description = "The unlucky one", Released = new DateTime(1986, 1, 1), EndOfLife = new DateTime(1999, 12, 31) }
                ];

            Console.WriteLine("\nThese products has the name 'One' or 'lucky' or contains 'One' or 'lucky' in the description");
            foreach (var item in products.Where(ProductSearch("One", "lucky")))
            {
                Console.WriteLine("#{0} - {1}", item.Id, item.Name);
            }

            Console.WriteLine("\nThese products is currently selling");
            foreach (var item in products.Where(IsSelling))
            {
                Console.WriteLine("#{0} - {1}", item.Id, item.Name);
            }

            Console.WriteLine("Press <ENTER> to end");
            Console.ReadLine();
        }

        public static Specification<Product> ProductSearch(params string[] keywords)
        {
            Specification<Product> query = Specification<Product>.None;

            foreach (var item in keywords)
            {
                query = query.Or(p => p.Name == item || p.Description.Contains(item));
            }

            return query;
        }

        public static Specification<Product> IsSelling = new(p => p.Released != default && p.Released <= DateTime.Now 
                                                                  && (p.EndOfLife == default || p.EndOfLife > DateTime.Now));
    }
}
