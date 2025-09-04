using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Activation State { get; set; }
        public int CourseId { get; set; }
        public bool IsReady { get; set; } = false;
        public Course Course { get; set; }

        public string Uuid { get; set; } 
        public string? Image { get; set; } 
        public string FolderId { get; set; }
        public string FolderName { get; set; }
        public string? Qualities { get; set; }
    }
}