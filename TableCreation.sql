CREATE TABLE TransactionsInfo (
    TransactionID INTEGER  PRIMARY KEY AUTOINCREMENT
                           UNIQUE
                           NOT NULL,
    Payer         STRING,
    Points        INTEGER,
    Timestamp     DATETIME
);