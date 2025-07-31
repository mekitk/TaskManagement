using System.ComponentModel.DataAnnotations;

namespace GorevYonetimUygulamasi.Dto
{
    public class ProjectUpdateDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> MemberIds { get; set; } = new();
    }
}
