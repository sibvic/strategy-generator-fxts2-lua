        local current_serial = source:serial(period);
        if (last_serial ~= current_serial) then
            last_serial = current_serial;
            if (instance.parameters.close_on_opposite) then
                local trades = trading:FindTrade():WhenSide("B"):WhenCustomID(custom_id):All();
                for _, trade in ipairs(trades) do
                    trading:Close(trade);
                end
            end
            local command = trading:MarketOrder(source:instrument()):SetSide("S"):SetDefaultAmount():SetCustomID(custom_id);
            if (instance.parameters.set_stop) then
                command = command:SetPipStop(nil, instance.parameters.stop, instance.parameters.trailing_stop and instance.parameters.trailing or nil);
            end
            if (instance.parameters.set_limit) then
                command = command:SetPipLimit(nil, instance.parameters.limit);
            end
            command:Execute();
        end