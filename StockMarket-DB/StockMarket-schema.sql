-- Table1: Ticker Table
CREATE TABLE stock_exchange.ticker_table (
    ticker VARCHAR PRIMARY KEY,
    price DECIMAL NOT NULL,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Insert 10 sample tickers
INSERT INTO stock_exchange.ticker_table (ticker, price) VALUES
('NATW', 203.12),
('HSBC', 152.75),
('BARC', 173.85),
('VOD', 102.45),
('BT.A', 117.20),
('LLOY', 45.10),
('TSCO', 280.50),
('GSK', 1445.60),
('RDSA', 2210.30),
('AZN', 10010.55);

-- Table2: Registered Broker List
CREATE TABLE stock_exchange.broker_list (
    broker_id VARCHAR PRIMARY KEY,
    broker_name VARCHAR NOT NULL,
    username VARCHAR NOT NULL,
    password VARCHAR NOT NULL
);

-- Insert 10 sample brokers
INSERT INTO stock_exchange.broker_list (broker_id, broker_name, username, password) VALUES
('BRK-001', 'ICICI', 'icici_user', 'pass1'),
('BRK-002', 'HDFC', 'hdfc_user', 'pass2'),
('BRK-003', 'KOTAK', 'kotak_user', 'pass3'),
('BRK-004', 'SBI', 'sbi_user', 'pass4'),
('BRK-005', 'AXIS', 'axis_user', 'pass5'),
('BRK-006', 'YESBANK', 'yes_user', 'pass6'),
('BRK-007', 'IDFC', 'idfc_user', 'pass7'),
('BRK-008', 'PNB', 'pnb_user', 'pass8'),
('BRK-009', 'CANARA', 'canara_user', 'pass9'),
('BRK-010', 'UNION', 'union_user', 'pass10');

-- Table3: Stock Transactions
CREATE TABLE stock_exchange.stock_transactions (
    transaction_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    ticker VARCHAR NOT NULL,
    broker_id VARCHAR NOT NULL,
    quantity DECIMAL NOT NULL,
    price DECIMAL NOT NULL,
    timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ticker) REFERENCES stock_exchange.ticker_table(ticker),
    FOREIGN KEY (broker_id) REFERENCES stock_exchange.broker_list(broker_id)
);
