using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Entities
{
    public class WorkItemState
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Value { get; set; }
        public List<WorkItem> WorkItems { get; set; }
        
        
    }
}
