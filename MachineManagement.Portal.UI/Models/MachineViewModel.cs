using System.ComponentModel.DataAnnotations;

namespace MachineManagement.Portal.UI.Models
{
    public class MachineViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }
        [Display(Name = "IP Address")]
        [Required(ErrorMessage = "Please enter an IP address.")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Please enter a valid IP address.")]
        public string Ip { get; set; }
        public string Notes { get; set; }

        public bool Connected { get; set; }
    }
}