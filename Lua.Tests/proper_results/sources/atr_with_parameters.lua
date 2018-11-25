    assert(core.indicators:findIndicator("ATR") ~= nil, "Please, download and install ATR indicator");
    atr = core.indicators:create("ATR", trading_logic.MainSource, 10);