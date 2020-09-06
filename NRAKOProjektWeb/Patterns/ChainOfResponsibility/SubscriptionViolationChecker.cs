using NRAKOProjektWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NRAKOProjektWeb.Patterns.ChainOfResponsibility
{
    public abstract class SubscriptionViolationChecker
    {
        protected string _propertyName;
        protected SubscriptionViolationChecker _next;

        public SubscriptionViolationChecker()
        {
        }

        public SubscriptionViolationChecker SetNext(SubscriptionViolationChecker next) {
            _next = next;
            return next;
        }

        public string CheckViolation(SubscriptionModel subscriptionModel, SubscriptionModel userConsumption)
        {
            dynamic maxValue = subscriptionModel.GetType().GetProperty(_propertyName).GetValue(subscriptionModel,null);
            dynamic currentValue = userConsumption.GetType().GetProperty(_propertyName).GetValue(userConsumption, null);


            if (currentValue >= maxValue)
            {
                return SendInfo();
            }
            else if (_next != null)
            {
                return _next.CheckViolation(subscriptionModel, userConsumption);
            }
            else
            {
                return null;
            }
        }

        protected abstract string SendInfo();
    }
}
