    assert(core.indicators:findIndicator("EMA") ~= nil, "Please, download and install EMA indicator");
    ema = core.indicators:create("EMA", trading_logic.MainSource, 14);