trading_time = {};
trading_time.Name = "Trading time";
trading_time.Debug = false;
trading_time.Version = "1.0.1";

trading_time._ids_start = nil;
trading_time._tz = nil;
trading_time._start_time = nil;
trading_time._stop_time = nil;

function trading_time:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function trading_time:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end

function trading_time:Init(parameters)
    parameters:addString("trading_start_time", "Start Time for Trading", "", "00:00:00");
    parameters:addString("trading_stop_time", "Stop Time for Trading", "", "24:00:00");
end

function trading_time:ParseTime(time)
    local Pos = string.find(time, ":");
    if Pos == nil then return nil, false; end
    local h = tonumber(string.sub(time, 1, Pos - 1));
    time = string.sub(time, Pos + 1);
    Pos = string.find(time, ":");
    if Pos == nil then return nil, false; end
    local m = tonumber(string.sub(time, 1, Pos - 1));
    local s = tonumber(string.sub(time, Pos + 1));
    return (h / 24.0 +  m / 1440.0 + s / 86400.0),                          -- time in ole format
           ((h >= 0 and h < 24 and m >= 0 and m < 60 and s >= 0 and s < 60) or (h == 24 and m == 0 and s == 0)); -- validity flag
end

function trading_time:Prepare(name_only)
    local valid;
    self._start_time, valid = self:ParseTime(instance.parameters.trading_start_time);
    assert(valid, "Time " .. instance.parameters.trading_start_time .. " is invalid");
    self._stop_time, valid = self:ParseTime(instance.parameters.trading_stop_time);
    assert(valid, "Time " .. instance.parameters.trading_stop_time .. " is invalid");
    if name_only then
        return;
    end
end

function trading_time:OnNewModule(module)
    if module.Name == "Timezone Parameter" then
        self._tz = module;
    end
end

function trading_time:InRange(now, from, to)
    if start_time < stop_time then
        return now >= from and now <= to;
    end
    if start_time > stop_time then
        return now > from or now < to;
    end
    return now == from;
end

function trading_time:BlockTrading(id, source, period)
    --do what you usually do in Update
    local now = core.host:execute("getServerTime");
    if self._tz ~= nil then
        now = core.host:execute("convertTime", core.TZ_EST, self._tz.Selected_timezone, now);
    end
    -- get only time
    now = now - math.floor(now);
    if not (self:InRange(now, self._start_time, self._stop_time)) then
        return true;
    end
    return false;
end
