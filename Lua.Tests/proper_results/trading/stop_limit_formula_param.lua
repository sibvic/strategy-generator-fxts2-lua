        local command = trading:EntryOrder(source:instrument()):SetSide("B"):SetDefaultAmount():SetRate(instr:tick(period)):SetCustomID(custom_id);
        command = command:SetStop(mva.DATA:tick(period)-instance.parameters.param_1*source:pipSize());
        command = command:SetLimit(instr.open:tick(period));
        command:Execute();