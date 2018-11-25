    assert(core.indicators:findIndicator("WMA") ~= nil, "Please, download and install WMA indicator");
    wma = core.indicators:create("WMA", trading_logic.MainSource, 10);