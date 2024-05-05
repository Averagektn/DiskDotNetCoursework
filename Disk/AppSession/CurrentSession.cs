using Disk.Entity;

namespace Disk.AppSession;

public static class CurrentSession
{
    public static Doctor Doctor { get; set; } = new();
    public static Patient Patient { get; set; } = new();
}
