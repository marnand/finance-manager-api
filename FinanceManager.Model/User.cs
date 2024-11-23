﻿namespace FinanceManager.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public List<Account> Accounts { get; set; }
        //public List<Category> Categories { get; set; }
        //public List<Report> Reports { get; set; }
        //public List<SyncQueue> SyncQueues { get; set; }
    }
}
