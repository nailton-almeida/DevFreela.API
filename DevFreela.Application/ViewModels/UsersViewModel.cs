using DevFreela.Core.Entities;

namespace DevFreela.Application.ViewModels


{
    public class UsersViewModel
    {
        public UsersViewModel(int id, string fullName, string email, DateTime createdAt, bool isActive)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            CreatedAt = createdAt;
            IsActive = isActive;
        }


        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
        public List<UserSkill> Skills { get; private set; }
        public List<Project> OwnerProject { get; private set; }
        public List<Project> FreelancerProject { get; private set; }
    }
}
