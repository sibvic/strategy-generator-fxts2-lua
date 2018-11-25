        local command = trading:EntryOrder(source:instrument()):SetSide("S"):SetDefaultAmount():SetRate(source.high:tick(period)):SetCustomID(custom_id);
        command = command:SetStop(source.low:tick(period));
        if (instance.parameters.set_limit) then
            command = command:SetPipLimit(nil, instance.parameters.limit);
        end
        command:Execute();