namespace BetterCinema.Domain.Enums;

[Flags]
public enum CommonAccessType
{
    Read =  1 << 0,
    Create = 1 << 1,
    Update = 1 << 2,
    Delete = 1 << 3,
}