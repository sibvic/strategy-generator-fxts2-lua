    assert(core.indicators:findIndicator("RSI") ~= nil, "Please, download and install RSI indicator");
    rsi = core.indicators:create("RSI", trading_logic.MainSource.close);
    trading_logic.DoTrading = EntryFunction;
end
function EntryFunction(source, period)
    rsi:update(core.UpdateLast);