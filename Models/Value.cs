using Microsoft.EntityFrameworkCore;

namespace Datingapp.API.Models
{
    public class Value 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PhoneNo{get;set;}
    }
}