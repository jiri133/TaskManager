using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TaskManager.Models
{
    public class TaskItem : IValidatableObject
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [BindNever, ValidateNever]
        public Project Project { get; set; } = default!;

        [Required(ErrorMessage = "This is a required field."), StringLength(200)]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "This is a required field."), StringLength(2000)]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "This is a required field.")]
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;

        [Required(ErrorMessage = "This is a required field.")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime EndDate { get; set; }

        [Required]
        public TaskManager.Models.MediaType MediaType { get; set; } = TaskManager.Models.MediaType.Text;

        [Required, StringLength(4000)]
        public string MediaContent { get; set; } = default!;

        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BindNever, ValidateNever]
        public string CreatedById { get; set; } = default!;

        [BindNever, ValidateNever]
        public ApplicationUser CreatedBy { get; set; } = default!;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<TaskAssignment> Assignments { get; set; } = new List<TaskAssignment>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    "End date must be greater than or equal to start date.",
                    new[] { nameof(EndDate) }
                );
            }
        }
    }
}