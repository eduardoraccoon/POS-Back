﻿namespace POS.Application.Dtos.Client.Response
{
    public class ClientResponseDto
    {
        public int ClientId { get; set; }
        public string? Name { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? State { get; set; }
    }
}
