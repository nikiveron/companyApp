namespace companyApp.Server.Models.DTOs
{
    public class BankDTO
    {
        public int BankId { get; set; }

        public string RepLastName { get; set; } = string.Empty;
        public string RepFirstName { get; set; } = string.Empty;
        public string RepPatronymic { get; set; } = string.Empty;
        public string RepEmail { get; set; } = string.Empty;
        public string RepPhone { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public long Inn { get; set; }
        public int Kpp { get; set; }
        public long Ogrn { get; set; }
        public DateTime OgrnDateOfIssue { get; set; }

        //public List<AgentDTO> Agents { get; set; } = new List<AgentDTO>();

        public bool Priority { get; set; }
    }
}
