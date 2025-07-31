using GorevYonetimUygulamasi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GorevYonetimUygulamasi.Dto
{
    // DTOs/ProjectCreateDto.cs
    public class ProjectCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> MemberIds { get; set; } = new();

        [Required]
        [EnumDataType(typeof(ProjectStatusEnum))]
        public ProjectStatusEnum Status { get; set; }
    }
}
