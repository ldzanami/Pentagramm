﻿namespace Pentagramm.Models.Entities
{
    public class Notification
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public bool IsReaded { get; set; } = false;
        public User User { get; set; }
    }
}
