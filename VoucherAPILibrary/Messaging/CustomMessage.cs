using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Messaging
{
    public class CustomMessage
    {
        public CustomMessage(string description, string role, string @event, string eventDate)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Event = @event ?? throw new ArgumentNullException(nameof(@event));
            EventDate = eventDate ?? throw new ArgumentNullException(nameof(eventDate));
        }

        public string Description { get; set; }

        public string Role { get; set; }

        public string Event { get; set; }

        public string EventDate { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
