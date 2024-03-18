
using DevFreela.Core.Entities;

namespace DevFreela.Application.InputModel
{
    public class NewProjectInputModel
    {
        
        public string Title { get;  set; }
        public string Description { get; set; }
        public int ClientID { get; set; }
        public int FreelancerID { get;  set; }
        public decimal TotalCost { get;  set; }
        public DateTime StartedAt { get;  set; }
        public DateTime FinishedAt { get;  set; }
        public List<Skill> Skills { get; set; }


    }
}
