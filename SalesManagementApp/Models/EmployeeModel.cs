using System.ComponentModel.DataAnnotations;

namespace SalesManagementApp.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100,ErrorMessage = "{0} Bölümü {2} ile {1} arasında karakter içermelidir.", MinimumLength =2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} Bölümü {2} ile {1} arasında karakter içermelidir.", MinimumLength = 2)]
        public string LastName { get; set; }
       
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int? ReportToEmpId { get; set; }
        public string ImagePath { get; set; }
        public int EmployeeJobTitleId { get; set; }
       
    }
}
