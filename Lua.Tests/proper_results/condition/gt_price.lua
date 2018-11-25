    local _v2 = trading_logic:GetLastPeriod(period, source, source.close);
    if ((_v1 ~= nil and _v2 ~= nil) and mva.DATA:tick(_v1) > source.close:tick(_v2)) then