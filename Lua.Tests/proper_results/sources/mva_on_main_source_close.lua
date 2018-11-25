    assert(core.indicators:findIndicator("MVA") ~= nil, "Please, download and install MVA indicator");
    mva = core.indicators:create("MVA", trading_logic.MainSource.close);
    trading_logic.DoTrading = EntryFunction;
end
function EntryFunction(source, period)
    mva:update(core.UpdateLast);