using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.DomainModels.OrderModels
{
    public class shippingAddress:IEnumerable
    {
        public string City { get; set; }

        public string Street { get; set; }
       
        public int HouseNumber { get; set; }
        [DataType(DataType.PostalCode)]
        public string ZipNumber { get; set; }

        //IEnumerator and IEnumerable require these methods.
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        //IEnumerator
        //public bool MoveNext()
        //{
        //    position++;
        //    return (position < carlist.Length);
        //}

        ////IEnumerable
        //public void Reset()
        //{ position = 0; }

        ////IEnumerable
        //public object Current
        //{
        //    get { return carlist[position]; }
        //}
    }
}