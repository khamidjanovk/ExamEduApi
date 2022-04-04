using MyEdu.Domain.Enums;
using System;

namespace MyEdu.Domain.Common
{
    public interface IAuditable
    {
        long Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        ItemState State { get; set; }
    }
}
