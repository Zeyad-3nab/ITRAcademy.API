using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.Vod
{
    public class UpdateWebHookDto
    {
        public int? Id { get; set; }
        public string? user_id { get; set; }
        public int? lms_id { get; set; }
        public int? course_id { get; set; }
        public string? course_title { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? folder_id { get; set; }
        public string? originalvideo { get; set; }
        [Required(ErrorMessage = "Video is required")]
        public string video { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string image { get; set; }
        public int? free { get; set; }
        public double? size { get; set; }
        public string? published_at { get; set; }
        public string? finished_at { get; set; }
        public string? status { get; set; }
        public bool is_visable { get; set; } = true;    
        public string? notes { get; set; }
        public Dictionary<string, Dictionary<string, object>>? qualities { get; set; }
        public string? deleted_at { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
    }
}