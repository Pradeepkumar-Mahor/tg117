﻿namespace tg117.API.Dtos
{
    public class UserDetailDto
    {
        public string? Id { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string[]? Roles { get; set; }

        public string? PhoneNumber { get; set; }

        public bool TwoFacotrEnabled { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public int AccessFailedCount { get; set; }
    }
}