using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudAssessment.Models
{
    public class BookModelView
    {
        [Display(Name = "Book ID")]
        public int book_id { get; set; }

        [Required(ErrorMessage = "Title is required."), Display(Name = "Title")]
        public string title { get; set; }

        [Required(ErrorMessage = "Author is required."), Display(Name = "Author")]
        public string author { get; set; }


        [Required(ErrorMessage = "Expenses is required."), Display(Name = "Price")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public string price { get; set; }

        //[Required(ErrorMessage = "Quantity Available is required."), Display(Name = "Quantity Available")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number"), Display(Name = "Quantity Available")]
        public int quantity_available { get; set; }
    }
}