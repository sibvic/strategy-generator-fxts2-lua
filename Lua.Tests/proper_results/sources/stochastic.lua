    assert(core.indicators:findIndicator("STOCHASTIC") ~= nil, "Please, download and install STOCHASTIC indicator");
    stoch = core.indicators:create("STOCHASTIC", trading_logic.MainSource);