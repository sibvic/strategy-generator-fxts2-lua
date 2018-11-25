            local command = trading:MarketOrder(source:instrument()):SetSide("S"):SetDefaultAmount():SetCustomID(custom_id);
            command = command:SetPipStop(15.0);
            if (instance.parameters.set_limit) then
                command = command:SetPipLimit(nil, instance.parameters.limit);
            end
            command:Execute();