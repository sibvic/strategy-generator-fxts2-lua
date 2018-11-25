trading_logic = {};
-- public fields
trading_logic.Name = "Trading logic by time";
trading_logic.Version = "1.0";
trading_logic.Debug = false;
trading_logic.DoTrading = nil;
trading_logic.MainSource = nil;
--private fields
trading_logic._ids_start = nil;
trading_logic._execution_time = nil;
trading_logic._timer_id = nil;
trading_logic._tz = nil;
trading_logic._last_executed_time = nil;

function trading_logic:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function trading_logic:OnNewModule(module) if module.Name == "Timezone Parameter" then self._tz = module; end end
function trading_logic:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end

function trading_logic:Init(parameters)
    parameters:addString("execution_time", "Execution Time", "", "23:59:00");
end

function trading_logic:Prepare(name_only)
    if name_only then
        return;
    end
    local time = instance.parameters.execution_time;
    self._execution_time, valid = self:ParseTime(time);
    assert(valid, "Time " .. time .. " is invalid");
    self._timer_id = self._ids_start + 1;
    core.host:execute("setTimer", self._timer_id, 1);
end

function trading_logic:ParseTime(time)
    local Pos = string.find(time, ":");
    if Pos == nil then
        return nil, false;
    end
    local h = tonumber(string.sub(time, 1, Pos - 1));
    time = string.sub(time, Pos + 1);
    Pos = string.find(time, ":");
    if Pos == nil then
        return nil, false;
    end
    local m = tonumber(string.sub(time, 1, Pos - 1));
    local s = tonumber(string.sub(time, Pos + 1));
    return (h / 24.0 +  m / 1440.0 + s / 86400.0),                          -- time in ole format
           ((h >= 0 and h < 24 and m >= 0 and m < 60 and s >= 0 and s < 60) or (h == 24 and m == 0 and s == 0)); -- validity flag
end

function trading_logic:AsyncOperationFinished(cookie, success, message, message1, message2)
    if cookie == self._timer_id then
        if self.MainSource ~= nil then
            self:triggerLogicIfNeeded(self.MainSource, self.MainSource:size() - 1);
        else
            self:triggerLogicIfNeeded();
        end
    end
end

function trading_logic:ExtUpdate(id, source, period)
    self:triggerLogicIfNeeded(source, period);
end

function trading_logic:triggerLogicIfNeeded(source, period)
    local now = core.host:execute("getServerTime");
    if self._tz ~= nil then
        now = core.host:execute("convertTime", core.TZ_EST, self._tz.Selected_timezone, now);
    end
    local date = math.floor(now); 
    if self._last_executed_time == nil then
        self._last_executed_time = date - 1;
        return;
    end
    if date == self._last_executed_time then
        return;
    end
    now = now - date;
    
    -- check whether the time is in the exit time period
    if now >= self._execution_time then
        self._last_executed_time = date;
        self.DoTrading(source, period);
    end
end
