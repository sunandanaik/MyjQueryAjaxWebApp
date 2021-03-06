//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyjQueryAjaxWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage ="Name field required")]
        public string Name { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public Nullable<int> Salary { get; set; }

        [DisplayName("Image")]
        public string ImagePath { get; set; }

        //Inorder to work with files in asp.net application use HttpPostedfilebase class.
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        public Employee()
        {
            ImagePath = "~/AppFiles/Images/Barbie.jpg";
        }
    }
}
