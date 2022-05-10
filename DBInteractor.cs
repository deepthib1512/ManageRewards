using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ManageRewards
{
    public class DBInteractor: DbContext
    {
        SqliteConnection sqlConn = null;
        public DBInteractor() : base()
        {
            sqlConn = new SqliteConnection(@"Data Source=C:\SQLite\RewardsDatabase.db");
        }

        public Dictionary<string, int> SpendPoints(int points)
        {
            /*
             * SQLite does not support stored procedures or update CTEs, so in this implementation, 
             * made separate call to update the payer's points after spending
             */
            Dictionary<string, int> retVal = new Dictionary<string, int>(); // end result which is returned from this method is saved in this dictionary
            Dictionary<int, Transaction> result = new Dictionary<int, Transaction>();
            sqlConn.Open();
            using (SqliteCommand command = sqlConn.CreateCommand())
            {
                command.CommandText = Transaction.DBSpendQuery + points;
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(Convert.ToInt32(reader[0]), 
                               new Transaction(Convert.ToString(reader[1]), Convert.ToInt32(reader[2]), Convert.ToDateTime(reader[3])));
                }
            }
            sqlConn.Close();
            // processing the points from the eligible transactions and updating their values further
            foreach(KeyValuePair<int, Transaction> kvp in result)
            {
                int diff = 0;
                if(kvp.Value.Points < 0)
                {
                    if (retVal.ContainsKey(kvp.Value.Payer))
                    {
                        retVal[kvp.Value.Payer] -= (kvp.Value.Points);
                        points = points - kvp.Value.Points;
                    }
                }
                else if (kvp.Value.Points - points >= 0)
                {
                    diff = kvp.Value.Points - points;
                    retVal[kvp.Value.Payer] = -points;
                }
                else
                {
                    diff = 0;
                    points = points - kvp.Value.Points;
                    retVal[kvp.Value.Payer] = -kvp.Value.Points;
                }

                UpdatePayerPointsAfterSpending(kvp.Key, kvp.Value,diff);
            }
            return retVal;
        }

        private void UpdatePayerPointsAfterSpending(int rowid, Transaction trans, int pointsAfterSpending)
        {
            sqlConn.Open();
            using (SqliteCommand command = sqlConn.CreateCommand())
            {
                command.CommandText = @"UPDATE TransactionsInfo SET Points = " + pointsAfterSpending + " WHERE TransactionID = " + rowid;
                command.ExecuteNonQuery();
            }
            sqlConn.Close();
        }
        public Dictionary<string, int> GetPointsBalance()
        {
            // this implementation will retrieve the available for points for each of the available payer from database
            Dictionary<string, int> retVal = new Dictionary<string, int>();
            sqlConn.Open();
            using (SqliteCommand command = sqlConn.CreateCommand())
            {
                command.CommandText = @"SELECT Payer, SUM(Points) FROM TransactionsInfo GROUP BY Payer";
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    retVal.Add(Convert.ToString(reader[0]), Convert.ToInt32(reader[1]));
                }
            }
            sqlConn.Close();
            return retVal;
        }
        public bool AddTransaction(Transaction trans)
        {
            // this implementation will add new transactions to the database
            sqlConn.Open();
            using (SqliteCommand command = sqlConn.CreateCommand())
            {
                if(String.IsNullOrEmpty(trans.Payer))
                    return false;
                command.CommandText = @"INSERT INTO TransactionsInfo (Payer, Points, Timestamp) VALUES ('" + (trans.Payer).ToUpperInvariant() + "', '" + trans.Points + "', '" + trans.Timestamp + "')";
                command.ExecuteNonQuery();
            }
            sqlConn.Close();
            return true;
        }
    }
}
