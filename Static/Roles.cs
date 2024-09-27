using FurrLife.Models;

namespace FurrLife.Static
{
    public class UserRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static UserRoles Administrator => new UserRoles()
        {
            Id = "80aaf565-d80e-4617-893d-fb8eeeb6b6a9",
            Name = "Administrator"
        };

        public static UserRoles Veterinarian => new UserRoles()
        {
            Id = "0c6ae8f7-48e1-451b-8455-a9b503cc4bdd",
            Name = "Veterinarian"
        };

        public static UserRoles Staff => new UserRoles()
        {
            Id = "5aa2a168-44cd-43f2-9a81-d2e6923abf56",
            Name = "Staff"
        };

        public static UserRoles Customer => new UserRoles()
        {
            Id = "bb9825cc-d2d1-4121-ace8-7335225f2c89",
            Name = "Customer"
        };
    }
}
