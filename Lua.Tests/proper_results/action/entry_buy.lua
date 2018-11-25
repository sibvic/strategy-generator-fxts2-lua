        local command = trading:EntryOrder(source:instrument()):SetSide("B"):SetDefaultAmount():SetRate(1.0):SetCustomID(custom_id);
        if (instance.parameters.set_stop) then
            command = command:SetPipStop(nil, instance.parameters.stop, instance.parameters.trailing_stop and instance.parameters.trailing or nil);
        end
        if (instance.parameters.set_limit) then
            command = command:SetPipLimit(nil, instance.parameters.limit);
        end
        command:Execute();