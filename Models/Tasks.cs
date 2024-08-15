using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Mangement.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Pending|In Progress|Completed", ErrorMessage = "Status must be Pending, In Progress, or Completed.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Priority is required.")]
        [RegularExpression("Low|Medium|High", ErrorMessage = "Priority must be Low, Medium, High.")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "AssignedTo is required.")]

        public int? AssignedTo { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //[ForeignKey("AssignedTo")]
       // public User User { get; set; }
    }
}
