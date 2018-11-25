    local _v1 = trading_logic:GetLastPeriod(period, source, indicator_1.Median);
    local _v2 = trading_logic:GetLastPeriod(period, source, instrument_1.close);
    if ((_v1 ~= nil and _v2 ~= nil) and (indicator_1.Median:size() >= 2 and instrument_1.close:size() >= 2 and core.crosses(indicator_1.Median, instrument_1.close, _v1, _v2))) then
        DoAction(source:instrument(), instance.parameters.action_1);
    end
end
function DoAction(instrument, action)
    if action == "buy" then
        return trading:MarketOrder(source:instrument()):SetSide("B"):SetDefaultAmount():Execute();
    elseif action == "sell" then
        return trading:MarketOrder(source:instrument()):SetSide("S"):SetDefaultAmount():Execute();
    elseif action == "exit" then
        return trading:CloseAllForInstrument(instrument);
    end
    return false;
end