            local command = trading:MarketOrder(source:instrument()):SetSide("S"):SetDefaultAmount():SetCustomID(custom_id);
            if (instance.parameters.set_stop) then
                command = command:SetPipStop(nil, instance.parameters.stop, instance.parameters.trailing_stop and instance.parameters.trailing or nil);
            end
            command = command:SetPipLimit(25.5);
            command:Execute();