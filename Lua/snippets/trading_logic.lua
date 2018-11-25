trading_logic = {};
-- public fields
trading_logic.Name = "Trading logic";
trading_logic.Version = "1.2.3";
trading_logic.Debug = false;
trading_logic.DoTrading = nil;
trading_logic.DoExit = nil;
trading_logic.MainSource = nil;
--private fields
trading_logic._ids_start = nil;
trading_logic._trading_source_id = nil;
function trading_logic:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function trading_logic:OnNewModule(module) end
function trading_logic:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end
function trading_logic:Init(parameters)
    parameters:addGroup("Price");
    parameters:addBoolean("is_bid", "Price Type","", true);
    parameters:setFlag("is_bid", core.FLAG_BIDASK);
    parameters:addString("timeframe", "Time frame", "", "m5");
    parameters:setFlag("timeframe", core.FLAG_PERIODS);
    parameters:addString("entry_execution_type", "Entry Execution Type", "Once per bar close or on every tick", "Live");
    parameters:addStringAlternative("entry_execution_type", "End of Turn", "", "EndOfTurn");
    parameters:addStringAlternative("entry_execution_type", "Live", "", "Live");
    parameters:addString("exit_execution_type", "Exit Execution Type", "Once per bar close or on every tick", "Live");
    parameters:addStringAlternative("exit_execution_type", "End of Turn", "", "EndOfTurn");
    parameters:addStringAlternative("exit_execution_type", "Live", "", "Live");
end
function trading_logic:GetLastPeriod(source_period, source, target)
    if source_period < 0 or target:size() < 2 then
        return nil;
    end
    local s1, e1 = core.getcandle(source:barSize(), source:date(source_period), -7, 0);
    local s2, e2 = core.getcandle(target:barSize(), target:date(NOW - 1), -7, 0);
    if e1 == e2 then
        return target:size() - 2;
    else
        return target:size() - 1;
    end
end
function trading_logic:GetPeriod(source_period, source, target)
    if source_period < 0 then
        return nil;
    end
    local source_date = source:date(source_period);
    local index = core.findDate(target, source_date, false);
    if index == -1 then
        return nil;
    end
    return index;
end
function trading_logic:Prepare(name_only)
    if name_only then
        return;
    end
    if instance.parameters.entry_execution_type == "Live" then
        self._trading_source_id = self._ids_start + 1;
        ExtSubscribe(self._trading_source_id, nil, "t1", instance.parameters.is_bid, "close");
        self:trace("Trading on tick (live data)");
    else
        self._trading_source_id = self._ids_start + 2;
        self:trace(string.format("Trading on %s bar", instance.parameters.timeframe));
    end
    if instance.parameters.exit_execution_type == "Live" then
        self._exit_source_id = self._ids_start + 1;
        if self._trading_source_id ~= self._exit_source_id then
            ExtSubscribe(self._exit_source_id, nil, "t1", instance.parameters.is_bid, "close");
        end
        self:trace("Exit on tick (live data)");
    else
        self._exit_source_id = self._ids_start + 2;
        self:trace(string.format("Trading on %s bar", instance.parameters.timeframe));
    end
    self.MainSource = ExtSubscribe(self._ids_start + 2, nil, instance.parameters.timeframe, instance.parameters.is_bid, "bar");
end
function trading_logic:ExtUpdate(id, source, period)
    if id == self._trading_source_id and self.DoTrading ~= nil then
        local period2 = period;
        if source ~= self.MainSource then
            period2 = core.findDate(self.MainSource, source:date(period), false);
            if period2 == -1 then
                return;
            end
        end
        self.DoTrading(self.MainSource, period2);
    end
    if id == self._exit_source_id and self.DoExit ~= nil then
        local period2 = period;
        if source ~= self.MainSource then
            period2 = core.findDate(self.MainSource, source:date(period), false);
            if period2 == -1 then
                return;
            end
        end
        self.DoExit(self.MainSource, period2);
    end
end