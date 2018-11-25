    assert(core.indicators:findIndicator("SAR") ~= nil, "Please, download and install SAR indicator");
    sar = core.indicators:create("SAR", trading_logic.MainSource);
    trading_logic.DoTrading = EntryFunction;
end
function EntryFunction(source, period)
    sar:update(core.UpdateLast);