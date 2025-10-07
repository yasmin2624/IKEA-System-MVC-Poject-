using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewsModels.AccountViewModels
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }
    }
}
