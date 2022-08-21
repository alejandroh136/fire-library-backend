namespace FireLibrary2.DTOs
{
    public class LoginDTO
    {
        public string Token { get; set; }
        public int CustomerId { get; set; }
        public int timeInSecs { get; set; } = 10800;

    }
}
