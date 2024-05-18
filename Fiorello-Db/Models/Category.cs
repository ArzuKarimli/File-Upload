using System.ComponentModel.DataAnnotations;

namespace Fiorello_Db.Models
{
    public class Category : BaseEntity
    {
       
        public string Name { get; set; }
        public ICollection<Product> Products { get; set;}
    }
}
