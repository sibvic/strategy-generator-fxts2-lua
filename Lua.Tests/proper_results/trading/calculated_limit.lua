        local command = trading:EntryOrder(source:instrument()):SetSide("S"):SetDefaultAmount():SetRate(source.low:tick(period)):SetCustomID(custom_id);
        command = command:SetStop(source.high:tick(period));
        command = command:SetLimit(source_1.macd:tick(period)*3.0);
        command:Execute();