using ITR.API.DAL.Models;

namespace ITRProject.API.PL.Dtos.Lecture
{
    public class ReturnLectureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Activation State { get; set; }
        public int CourseId { get; set; }


        public string Uuid { get; set; }
        public string FolderId { get; set; }
        public string FolderName { get; set; }
        public string? Image { get; set; }
        public string? QualitiesAllowed { get; set; }
        public Dictionary<string , string>? Qualities { get; set; }

    }
}
