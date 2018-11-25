    local _v1 = trading_logic:GetLastPeriod(period, source, mva.DATA);
    if ((_v1 ~= nil) and (mva.DATA:size() >= 2 and core.crosses(mva.DATA, 1.0, _v1))) then