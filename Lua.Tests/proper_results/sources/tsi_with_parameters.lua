    assert(core.indicators:findIndicator("TSI") ~= nil, "Please, download and install TSI indicator");
    tsi = core.indicators:create("TSI", trading_logic.MainSource, 8, 15);