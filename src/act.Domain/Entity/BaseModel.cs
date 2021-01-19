using System;

namespace act.Domain.Entity
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTimeOffset CreateAt { get; set; } = DateTimeOffset.UtcNow;
    }
}