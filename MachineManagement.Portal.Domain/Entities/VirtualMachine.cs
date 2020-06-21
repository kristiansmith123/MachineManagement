namespace MachineManagement.Portal.Domain.Entities
{
    public class VirtualMachine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Notes { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProxyIP { get; set; }
        public string ProxyUsername { get; set; }
        public string ProxyPassword { get; set; }
        public string BotUsername { get; set; }
        public string BotPassword { get; set; }
        public string ProxyName { get; set; }
        public string ProxyResourceGroup { get; set; }

    }
}
