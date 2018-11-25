    assert(core.indicators:findIndicator("VWAP") ~= nil, "Please, download and install VWAP indicator");
    vwap = core.indicators:create("VWAP", trading_logic.MainSource);