using System.ComponentModel.DataAnnotations;

namespace AuthorInstitution_XXXXXXXX.ViewModel
{
    public class LoginVM
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
