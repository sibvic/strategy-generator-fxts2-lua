    if ((_v1 ~= nil) and (stoch.K:size() >= 2 and core.crossesUnder(stoch.K, 20.0, _v1))) then
        trading:CloseSideForInstrument(source:instrument(), "B");
    end