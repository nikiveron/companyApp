namespace companyApp.Server.Filters;

public class InfoFilter
{
    public string Inn { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateOnly OgrnFrom { get; set; }
    public DateOnly OgrnTo { get; set; }
    public bool Priority { get; set; }

    public InfoFilter()
    {
        Inn = "";
        PhoneNumber = "";
        Email = "";
        OgrnFrom = DateOnly.MinValue;
        OgrnTo = DateOnly.MaxValue;
        Priority = false;
    }
    public InfoFilter(string inn, string phoneNumber, string email, DateOnly ogrnFrom, DateOnly ogrnTo, bool priority)
    {
        Inn = inn;
        PhoneNumber = phoneNumber;
        Email = email;
        OgrnFrom = ogrnFrom;
        OgrnTo = ogrnTo;
        Priority = priority;
    }
}
