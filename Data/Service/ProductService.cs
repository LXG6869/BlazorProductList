using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorProductList.Data.Model;
using System.IO;

namespace BlazorProductList.Data.Service
{
    public class ProductService
    {
        private static  IEnumerable<Product> ProductList;

        private static readonly Dictionary<int,string> ProductNameList=new Dictionary<int, string>();

        private static  IEnumerable<string> VorlageList;

        private static IEnumerable<string> StatusList;

        private static IEnumerable<string> ImagePathList;

        public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
        public delegate TResult Func<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);


        //public delegate T GetStringFromList<T>(IEnumerable<T> input); 

        public ProductService()
        {
            StatusList = new List<string>() { "Veröffentlicht", "Deaktiviert" };
            VorlageList = new List<string>() { "Einfaches Produkt", "Gruppiertes Produkt (Varianten)" };
            ImagePathList = new List<string>() { "/images/p1.jpg", "/images/notfound.png" };
            ProductNameList.Add(1, "WDT Phosphor oral 500ml");
            ProductNameList.Add(2, "LumiVersion AKKU Leuchthalsband mit grünen KED 60cm");
            ProductNameList.Add(3, "Trixie Modern Art Sportdog, Vleine (L-XL), 2,0m/25mm");
            ProductNameList.Add(4, "EquiPower Biotion Liquid");
            ProductNameList.Add(5, "Trixie Modern Art Coffe , VLeune");
            ProductNameList.Add(6, "Royal Canin Neutered Cat Young male");
            ProductNameList.Add(7, "Trixe Modern Art Rose Heart Leine");
            ProductNameList.Add(8, "Trixe Kunstoffpflanzen mit Sandfuß ca 12cm, 6st.");
            ProductNameList.Add(9, "Dechra Denticur Kaustriplets mit Enzymen für kleine Hunde 173g");
            ProductNameList.Add(10, "Feliway Friends Nachfüllflaken 3 x 30 Tage Vorteilspack");

        }

        public Task<Product> GetProductDetails(int id)
        {
            Product newProduct = ProductList.SingleOrDefault(p => p.Id == id);
            return Task.FromResult(newProduct);
        }


        public async Task<IEnumerable<Product>> GetProductList()
        {
            Func<DateTime, DateTime, DateTime> d = delegate (DateTime a, DateTime b)
            {
                int rang = (b - a).Days;
                Random r = new Random();
                return a.AddDays(r.Next(rang));
            };
            Func<int, int, IEnumerable<string>, string> fe = delegate (int a, int b, IEnumerable<string> c)
            {
                Random rdm = new Random();
                return c.ElementAt(rdm.Next(a, b));
            };
            Func<int, int, int> c = delegate (int a, int b) {
                Random rdm = new Random();
                return rdm.Next(a, b);
            };
            Func<int, int, Dictionary<int, string>, string> fd = delegate (int a, int b, Dictionary<int, string> c)
            {
                Random rdm = new Random();
                return c[rdm.Next(a, b)];
            };
            if (ProductList!=null && ProductList.Count()>0)
            {
                return ProductList;
            }
            else
            {
                ProductList = await Task.FromResult(Enumerable.Range(1, 20)
    .Select(idx => new Product()
    {
        Id = idx,
        Identifizierer = CreateIdentifizierer(),
        ChangedAt = d(new DateTime(2021, 1, 1), DateTime.Now),
        CreatedAt = d(new DateTime(2021, 1, 1), DateTime.Now),
        Completeness = c(1, 100),
        Produktname = fd(1, 10, ProductNameList),
        Anbieter = "",
        BildPath = fe(0, 2, ImagePathList),
        Vorlage = fe(0, 2, VorlageList),
        Status = fe(0, 2, StatusList)
    }
));

                return ProductList;
            }


        }

        //public  Task<IEnumerable<Product>> GetList()
        //{
        //    Func<int, int, int> c = delegate (int a, int b) {
        //        Random rdm = new Random();
        //        return rdm.Next(a, b);
        //    };
        //    Func<DateTime, DateTime, DateTime> d = delegate (DateTime a, DateTime b)
        //        {
        //            int rang = (b - a).Days;
        //            Random r = new Random();
        //            return a.AddDays(r.Next(rang));
        //        };
        //    Func<int, int, IEnumerable<string>,string> fe = delegate (int a, int b, IEnumerable<string> c)
        //        {
        //            Random rdm = new Random();
        //            return c.ElementAt(rdm.Next(a, b));
        //        };
        //    Func<int, int,Dictionary<int,string>, string> fd = delegate (int a, int b, Dictionary<int, string> c)
        //    {
        //        Random rdm = new Random();
        //        return c[rdm.Next(a, b)];
        //    };
        //    for (int i = 0; i < 20; i++)
        //    {

        //        ProductList.Append(new Product()
        //        {
    
        //        }); ;

        //    }
        //     return Task.FromResult(Enumerable.Range(1, 20)
        //        .Select(idx => new Product()
        //        {
        //            Id = idx,
        //            Identifizierer = CreateIdentifizierer(),
        //            //Status = fe(0,2,StatusList),
        //            //Completeness = c(1,100),
        //            //Vorlage=fe(0,2,VorlageList),
        //            //ChangedAt=d(new DateTime(2021,1,1),DateTime.Now),
        //            //CreatedAt=d(new DateTime(2021,1,1),DateTime.Now),
        //            Anbieter = "",
        //            //Produktname=fd(1,10,ProductNameList),
        //            BildPath = ""
        //        }
        //    ));
        //}

        /// <summary>
        ///     create a random Number bw 1 to 100
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //private int CreateNewCompleteness()
        //{
        //    Random rdm = new Random();
        //    return rdm.Next(1,100);
        //}


        /// <summary>
        ///    random new status
        /// </summary>
        /// <returns> status String</returns>
        private string CreateNewStatus(IEnumerable<string> input)
        {
            Random rdm = new Random();
            return input.ElementAt(rdm.Next(0, 2));
        }
        /// <summary>
        ///    random Product indentifizierer
        /// </summary>
        /// <returns> string identifizierer</returns>
        private string CreateIdentifizierer()
        {
            string result = "";
            Random rdm = new Random();
            int idTypeNumber = rdm.Next(0, 2);

            switch (idTypeNumber)
            {
                case 0:
                    {
                        for (int i = 0; i < 13; i++)
                        {
                            result += rdm.Next(0, 9).ToString();
                        }
                    }
                    break;
                case 1:
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            result += rdm.Next(1, 9).ToString();
                        }
                    }
                    break;
                default:
                    break;
            }

            return idTypeNumber == 0 ? result : "gr-" + result;
        }
    }
}
