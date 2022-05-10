using System;
using System.Collections.Generic;

namespace ManageRewards
{
    public class Transaction
    {
        public string Payer { get; set; }

        public int Points { get; set; }

        public DateTime Timestamp { get; set; }

        public static string DBSpendQuery = @"WITH cte AS(SELECT *, SUM(Points) OVER (ORDER BY Timestamp) AS RunTotal FROM TransactionsInfo)SELECT TransactionID, Payer, Points, Timestamp FROM cte WHERE  cte.RunTotal - cte.Points <= "; 
        
        public Transaction( string payer, int points, DateTime timestamp)
        {
            Payer = payer;
            Points = points;
            Timestamp = timestamp;
        }
    }
}
