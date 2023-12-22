using e_commerce_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace e_commerce_API.Services
{
    public interface IWatchService
    {
        public List<Watch> GetAllWatches();
        public JsonResult CheckoutWatches(List<string> watches);
    }
    public class WatchService : IWatchService
    {
      
        public WatchService()
        {
        }

        public List<Watch> GetAllWatches() =>
            new()
            {
                new() {
                    Watch_ID = 1,
                    Watch_Name = "Rolex",
                    Unit_Price = 100,
                    Discount = new Discount
                    {
                        Disc_Quantity = 3,
                        Disc_Price = 200
                    }
                },
                new()
                {
                    Watch_ID = 2,
                    Watch_Name = "Michael Kors",
                    Unit_Price = 80,
                    Discount = new Discount
                    {
                        Disc_Quantity = 2,
                        Disc_Price = 120
                    }
                },
                new()
                {
                    Watch_ID = 3,
                    Watch_Name = "Swatch",
                    Unit_Price = 50,
                    Discount = new Discount
                    {
                        Disc_Quantity = null,
                        Disc_Price = null
                    }
                },
                new()
                {
                    Watch_ID = 4,
                    Watch_Name = "Casio",
                    Unit_Price = 30,
                    Discount = new Discount
                    {
                        Disc_Quantity = null,
                        Disc_Price = null
                    }
                },

            };

        public JsonResult CheckoutWatches(List<string> watches)
        {
            try
            {
                String Output = "";
                decimal? totalprice = 0;
                if(watches != null)
                {
                    if(watches.Count > 0)
                    {
                        List<int> ints = watches
                        .Select(s => Int32.TryParse(s, out int n) ? n : (int?)null)
                        .Where(n => n.HasValue)
                        .Select(n => n.Value)
                        .ToList();

                        List<Watch> WatchesList = new List<Watch>();
                        WatchesList = GetAllWatches();


                        if (ints != null)
                        {
                            if (ints.Count > 0)
                            {
                                var intresult = ints.GroupBy(n => n)
                                    .Select(c => new { key = c.Key, total = c.Count() });

                                //List<Watch> UpdWatchesList = new List<Watch>();
                                var UpdWatchesList = from x in WatchesList
                                                     join y in intresult
                                                     on x.Watch_ID equals y.key
                                                     select new
                                                     {
                                                         x.Watch_ID,
                                                         x.Watch_Name,
                                                         x.Unit_Price,
                                                         x.Discount,
                                                         ord_Quantity = y.total
                                                     };
                                foreach (var x in UpdWatchesList)
                                {

                                    if (x.Discount.Disc_Quantity != null && x.Discount.Disc_Price != null)
                                    {
                                        if (x.Discount.Disc_Quantity > 0 && x.Discount.Disc_Price > 0)
                                        {
                                            if (x.ord_Quantity >= x.Discount.Disc_Quantity)
                                            {
                                                int q = Math.DivRem(x.ord_Quantity, (int)x.Discount.Disc_Quantity, out int r);
                                                totalprice += ((q * x.Discount.Disc_Price) + (r * x.Unit_Price));
                                            }
                                            else
                                            {
                                                totalprice += (x.Unit_Price * x.ord_Quantity);
                                            }

                                        }
                                        else
                                        {
                                            totalprice += (x.Unit_Price * x.ord_Quantity);
                                        }
                                    }
                                    else
                                    {
                                        totalprice += (x.Unit_Price * x.ord_Quantity);
                                    }
                                }
                            }
                            else
                            {
                                return new JsonResult("Invalid input");
                            }
                            Output = "price:" + string.Format("{0}", totalprice);

                        }
                        else
                        {
                            return new JsonResult("Invalid input");
                        }
                        return new JsonResult(Output);

                    }
                    else
                    {
                        return new JsonResult("Invalid input");
                    }

                }
                else
                {
                    return new JsonResult("Invalid input");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }
    }
}