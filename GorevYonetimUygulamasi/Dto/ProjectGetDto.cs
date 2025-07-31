using Görev_Yönetim_Uygulaması.Models;
using GorevYonetimUygulamasi.Models.Enums;

namespace GorevYonetimUygulamasi.Dto
{
    public class ProjectGetDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
        public List<string> MemberIds { get; set; }
        public ProjectStatusEnum Status { get; set; }
        public int TasksCount { get; set; }
    }
}
