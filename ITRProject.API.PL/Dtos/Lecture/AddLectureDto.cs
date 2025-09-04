using ITR.API.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Dtos.Lecture
{
    public class AddLectureDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "State is required")]
        public Activation State { get; set; }

        [Required(ErrorMessage = "FolderId is required")]
        public string FolderId { get; set; }

        [Required(ErrorMessage = "FolderName is required")]
        public string FolderName { get; set; }

        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Qualities is required")]
        public List<string> Qualities { get; set; }
    }
}
