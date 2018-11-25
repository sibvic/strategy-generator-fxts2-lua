    assert(core.indicators:findIndicator("MACD") ~= nil, "Please, download and install MACD indicator");
    macd = core.indicators:create("MACD", trading_logic.MainSource, 14, 28, 10);