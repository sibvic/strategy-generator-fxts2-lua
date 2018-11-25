    assert(core.indicators:findIndicator("CCI") ~= nil, "Please, download and install CCI indicator");
    cci = core.indicators:create("CCI", trading_logic.MainSource);