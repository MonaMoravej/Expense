using Data.Entities.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices
{
    public static class Utilities
    {
        public static AccountType ConvertToAccountType( this string Code)
        {
            switch (Code)
            {
                case "AS":
                    return AccountType.Asset;

                case "SA":
                    return AccountType.Savings;

                case "CH":
                    return AccountType.Checking;

                case "CR":
                    return AccountType.CreditCard;

                case "DE":
                    return AccountType.DebitCard;
                case "IN":
                    return AccountType.InvestingRetirement;
                case "LO":
                    return AccountType.Loan;
                case "OT":
                    return AccountType.Others;

                default:
                    return AccountType.Cash;
            }

        }

        public static string ConvertToCode( this AccountType type)
        {
            

            switch (type)
            {
                case AccountType.Asset:
                    return "AS";

                case AccountType.Savings:
                    return "SA";

                case AccountType.Checking:
                    return "CH";

                case AccountType.CreditCard:
                    return "CR";

                case AccountType.DebitCard:
                    return "DE";
                case AccountType.InvestingRetirement:
                    return "IN";
                case AccountType.Loan:
                    return "LO";
                case AccountType.Others:
                    return "OT";

                default:
                    return "CA";


            }
        }

        
    }
}
