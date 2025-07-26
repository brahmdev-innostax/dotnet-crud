namespace DotNetCRUD_8.Requests
{
    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
