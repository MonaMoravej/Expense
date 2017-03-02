using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServices.Models
{
    public enum TransactionSearchByType
    {
        Date, 
        RangeDate,

        Account,
        Category,
        Payee,
        FromAccount,
        ToAccount,
        Type,

        // if we have just from date this means from date to now
        //if we have just to date this means from begining to this given date
        // if we have both, it means we should search between these dates

        // if we have Date( from above) we ignore fromDate/ToDate and fetch all transaction of given date(just for one day)
        FromDate,
        ToDate

    }
}
