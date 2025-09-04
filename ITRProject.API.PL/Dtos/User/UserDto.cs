namespace ITRProject.API.PL.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string role { get; set; }
        public DateOnly DateOfCreation { get; set; }
        public string Token { get; set; }
    }
}
