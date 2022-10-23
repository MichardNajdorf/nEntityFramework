using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Entities
{
    public class Epic : WorkItem
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class Issue : WorkItem
    {
        [Precision(5)]
        public decimal Effort { get; set; }
    }

    public class Task : WorkItem
    {
        public string Activity { get; set; }
        [Precision(5)]
        public decimal RemainingWork { get; set; }
    }


    public abstract class WorkItem
    {
        
        public int Id { get; set; }
        public string State { get; set; }
        [Column(TypeName = "varchar(200)")]
        public string Area { get; set; }
        public string IterationPath { get; set; }
        public int Priority  { get; set; }
        
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public User User { get; set; }
        public Guid UserGuid { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public WorkItemState WorkItemState { get; set; }
        public int WorkItemStateId { get; set; }


    }
}
