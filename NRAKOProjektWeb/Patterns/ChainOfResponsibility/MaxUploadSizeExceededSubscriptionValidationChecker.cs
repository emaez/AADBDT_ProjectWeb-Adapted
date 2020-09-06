using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.ChainOfResponsibility
{
    public class MaxUploadSizeExceededSubscriptionValidationChecker : SubscriptionViolationChecker
    {
        public MaxUploadSizeExceededSubscriptionValidationChecker()
        {
            _propertyName = "MaxUploadSize";
        }

        protected override string SendInfo()
        {
            return "Maximum photo upload size exceeded";
        }
    }
}
