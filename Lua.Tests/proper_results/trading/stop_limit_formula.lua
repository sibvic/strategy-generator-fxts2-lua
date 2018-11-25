        local command = trading:EntryOrder(source:instrument()):SetSide("B"):SetDefaultAmount():SetRate(instr:tick(period)-5.0*source:pipSize()):SetCustomID(custom_id);
        command = command:SetStop(mva.DATA:tick(period));
        command = command:SetLimit(instr.open:tick(period));
        command:Execute();