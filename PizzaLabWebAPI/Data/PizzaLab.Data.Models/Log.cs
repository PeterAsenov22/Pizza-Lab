namespace PizzaLab.Data.Models
{
    using Common;
    using System;

    public class Log : BaseModel<int>
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public string LogLevel { get; set; }

        public string StackTrace { get; set; }

        public string Message { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
