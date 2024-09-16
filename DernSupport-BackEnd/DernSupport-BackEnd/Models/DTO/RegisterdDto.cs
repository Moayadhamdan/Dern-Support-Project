namespace DernSupport_BackEnd.Models.DTO
{
    public class RegisterdDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IList<string> Roles { get; set; }
        public string AccountType { get; set; }

    }
}
