using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.ChainOfResponsibility
{
    public class DailyUploadLimitExceededSubscriptionValidationChecker : SubscriptionViolationChecker
    {
        public DailyUploadLimitExceededSubscriptionValidationChecker()
        {
            _propertyName = "DailyUploadLimit";
        }

        protected override string SendInfo()
        {
            return "Your daily photo limit is exceeded";
        }
    }
}
