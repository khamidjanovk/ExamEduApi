using MyEdu.Domain.Common;
using MyEdu.Domain.Enums;
using OfficeOpenXml.Attributes;
using OfficeOpenXml.Table;
using System;
using System.Text.Json.Serialization;

namespace MyEdu.Domain.Entities.Users
{
    [EpplusTable
        (TableStyle = TableStyles.Dark1,
        PrintHeaders = true,
        AutofitColumns = true,
        AutoCalculate = true,
        ShowTotal = true,
        ShowFirstColumn = true)]
    public class User : IAuditable
    {
        [EpplusIgnore]
        public long Id { get; set; }

        [EpplusTableColumn(Order = 0, Header = "First name")]
        public string FirstName { get; set; }

        [EpplusTableColumn(Order = 1, Header = "Last name")]
        public string LastName { get; set; }

        [EpplusTableColumn(Order = 2)]
        public string Email { get; set; }
        
        [EpplusIgnore]
        public string Password { get; set; }

        [EpplusTableColumn(Order = 3, Header = "Phone number")]
        public string PhoneNumber { get; set; }

        [EpplusTableColumn(Order = 4, Header = "Username")]
        public string Username { get; set; }

        [JsonIgnore]
        [EpplusTableColumn(Order = 5, NumberFormat = "dd-MM-yyyy", Header = "Created date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToLocalTime();
        [JsonIgnore]
        [EpplusIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        [EpplusIgnore]
        public ItemState State { get; set; } = ItemState.Created;

        public void Update()
        {
            UpdatedAt = DateTime.Now.ToLocalTime();
            State = ItemState.Updated;
        }
    }
}
