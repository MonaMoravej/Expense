using System;
using System.Collections.Generic;
using System.Linq;


namespace Data.Entities.Expense
{
    public enum TransactionType
    {
        //2char key for begining of transaction key
        Expense, //EX
        Income, //IN
        Transfer,//TR
        StartBalance //ST
    }
}