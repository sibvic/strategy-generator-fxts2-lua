    indicatorSource = ExtSubscribe(indicatorSource_id, nil, "m1", true, "bar");
    debug_helper:AddInstrument(indicatorSource);
    indicator_1 = core.indicators:create("MOGALEF", indicatorSource);
    debug_helper:AddIndicator(indicator_1);
    trading_logic.DoTrading = EntryFunction;
end
function EntryFunction(source, period)
    indicator_1:update(core.UpdateLast);
    debug_helper:Next();
end