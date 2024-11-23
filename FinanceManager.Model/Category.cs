﻿namespace FinanceManager.Model
{
    public class Category
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Type { get; set; }
    }
}
