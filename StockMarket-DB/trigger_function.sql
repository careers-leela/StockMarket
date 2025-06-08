CREATE OR REPLACE FUNCTION stock_exchange.update_ticker_price()
RETURNS TRIGGER AS $$
BEGIN
    -- Update the ticker price to reflect the latest transaction price
    UPDATE stock_exchange.stocks
    SET price = NEW.price,
        timestamp = CURRENT_TIMESTAMP
    WHERE ticker = NEW.ticker;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;