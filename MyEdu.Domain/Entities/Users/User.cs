using MyEdu.Domain.Common;
using MyEdu.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace MyEdu.Domain.Entities.Users
{
    public class User : IAuditable
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToLocalTime();
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public ItemState State { get; set; } = ItemState.Created;

        public void Update()
        {
            UpdatedAt = DateTime.Now.ToLocalTime();
            State = ItemState.Updated;
        }
    }
}
