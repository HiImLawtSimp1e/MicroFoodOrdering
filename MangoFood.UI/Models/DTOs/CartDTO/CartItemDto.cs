﻿namespace MangoFood.UI.Models.DTOs.CartDTO
{
    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
