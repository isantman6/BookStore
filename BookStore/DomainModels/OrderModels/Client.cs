using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.DomainModels.OrderModels
{
    public class Client:IEnumerable
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "שדה חובה")]
        public string FullName { get; set; }

        public shippingAddress Address { get; set; }

        [Required(ErrorMessage ="שדה חובה")]
        [EmailAddress(ErrorMessage ="מייל לא תקין")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "שדה חובה")]
        [Phone(ErrorMessage ="מספר לא תקין")]
        public string PhonNumber { get; set; }
        
        public List<Order> orders { get; set; }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this; 
        }
    }
}