

namespace WriteToCompassion.Services;

public static class SessionService
{
    public static Guid SessionID { get; set; }

    public static void GenSessionID()
    {
        SessionID = Guid.NewGuid();
    }
}