using Disk.Entity;

namespace Disk.AppSession;

public static class CurrentSession
{
    public static Doctor Doctor { get; set; } = new();
    public static Patient Patient { get; set; } = new();
    public static Card Card { get; set; } = new();
    public static Appointment Appointment { get; set; } = new();
}
