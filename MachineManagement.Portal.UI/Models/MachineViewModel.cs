using MachineManagement.Portal.Domain.Services;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Threading.Tasks;

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
        [Display(Name = "Machine Username")]
        public string Username { get; set; }
        [Display(Name = "Machine Password")]
        public string Password { get; set; }
        [Display(Name = "Proxy IP Address")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Please enter a valid IP address.")]
        public string ProxyIP { get; set; }
        [Display(Name = "Proxy Username")]
        public string ProxyUsername { get; set; }
        [Display(Name = "Proxy Password")]
        public string ProxyPassword { get; set; }
        [Display(Name = "Bot Username")]
        public string BotUsername { get; set; }
        [Display(Name = "Bot Password")]
        public string BotPassword { get; set; }
        [Display(Name = "Proxy Machine Name")]
        public string ProxyName { get; set; }
        [Display(Name = "Proxy Resource Group")]
        public string ProxyResourceGroup { get; set; }


        public string DefaultMachineUsername => ConfigurationManager.AppSettings["DefaultMachineUsername"];
        public string DefaultMachinePassword => ConfigurationManager.AppSettings["DefaultMachinePassword"];
        public string DefaultProxyUsername => ConfigurationManager.AppSettings["DefaultProxyUsername"];
        public string DefaultProxyPassword => ConfigurationManager.AppSettings["DefaultProxyPassword"];
        public string DefaultProxyResourceGroup => ConfigurationManager.AppSettings["DefaultProxyResourceGroup"];
    }
}