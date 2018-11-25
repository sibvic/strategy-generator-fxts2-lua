        local command = trading:EntryOrder(source:instrument()):SetSide("S"):SetDefaultAmount():SetRate(source.low:tick(period)):SetCustomID(custom_id);
        command = command:SetStop(source_1.macd:tick(period)*0.15);
        if (instance.parameters.set_limit) then
            command = command:SetPipLimit(nil, instance.parameters.limit);
        end
        command:Execute();