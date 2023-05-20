using SystemSprawozdan.Shared.Enums;

namespace SystemSprawozdan.Frontend.CustomClasses;

public static class EnumTranslations
{
    public static Dictionary<UserRoleEnum, string> UserRole = new()
    {
        { UserRoleEnum.Student , "Student"},
        { UserRoleEnum.Teacher , "Prowadzący"},
        { UserRoleEnum.Admin , "Administator"}
    };
    public static Dictionary<MarkEnum, string> Mark = new()
    {
        { MarkEnum.Two , "2.0"},
        { MarkEnum.Three , "3.0"},
        { MarkEnum.ThreeAndHalf , "3.5"},
        { MarkEnum.Four , "4.0"},
        { MarkEnum.FourAndHalf , "4.5"},
        { MarkEnum.Five , "5.0"},
        { MarkEnum.Failed , "Niezaliczone"},
        { MarkEnum.Passed , "Zaliczone"}
    };
};