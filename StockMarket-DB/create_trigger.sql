CREATE TRIGGER trg_update_ticker_price
AFTER INSERT ON stock_exchange.stock_transactions
FOR EACH ROW
EXECUTE FUNCTION stock_exchange.update_ticker_price();