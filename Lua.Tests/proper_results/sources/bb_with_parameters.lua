    assert(core.indicators:findIndicator("BB") ~= nil, "Please, download and install BB indicator");
    bb = core.indicators:create("BB", trading_logic.MainSource, 14, 1.5);