    assert(core.indicators:findIndicator("MVA") ~= nil, "Please, download and install MVA indicator");
    mva = core.indicators:create("MVA", trading_logic.MainSource);