namespace MachineManagement.Portal.Domain.Entities
{
    public class VirtualMachine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string Notes { get; set; }
    }
}
