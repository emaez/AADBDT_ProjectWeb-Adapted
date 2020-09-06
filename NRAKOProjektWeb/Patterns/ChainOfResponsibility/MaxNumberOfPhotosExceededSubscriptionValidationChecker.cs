using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.ChainOfResponsibility
{
    public class MaxNumberOfPhotosExceededSubscriptionValidationChecker : SubscriptionViolationChecker
    {
        public MaxNumberOfPhotosExceededSubscriptionValidationChecker()
        {
            _propertyName = "MaxNumberOfPhotos";
        }

        protected override string SendInfo()
        {
            return "Maximum number of photos exceeded";
        }
    }
}
