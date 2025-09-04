namespace ITRProject.API.PL.Dtos.ExamResult
{
    public class ReturnExamResultDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int UserCode { get; set; }
        public string PhoneNumber { get; set; }
        public int ExamId { get; set; }
        public string ExamTitle { get; set; }
        public DateTime StartSolution { get; set; }
        public DateTime EndSolution { get; set; }
        public int Result { get; set; }
        public int CorrectAnswer { get; set; }
        public int NumberOfQuestion { get; set; }
    }
}
