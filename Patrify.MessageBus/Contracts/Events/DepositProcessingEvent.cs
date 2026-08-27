using System;
using System.Collections.Generic;
using System.Text;

namespace Patrify.MessageBus.Contracts.Events
{
    public record DepositProcessingEvent(
        Guid TransactionId   
    ) { }
}
