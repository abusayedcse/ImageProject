namespace ImageService.Model
{
    public class MCreateUser
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
