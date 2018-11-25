mandatory_closing = {};
mandatory_closing.Name = "Mandatory Closing";
mandatory_closing.Debug = false;
mandatory_closing.Version = "1.0.2";
mandatory_closing.Default_use_mandatory_closing = false;
mandatory_closing.Default_mandatory_closing_exit_time = "23:59:00";

mandatory_closing._ids_start = nil;
mandatory_closing._use_mandatory_closing = nil;
mandatory_closing._exit_time = nil;
mandatory_closing._interval = nil;
mandatory_closing._timer_id = nil;
mandatory_closing._tz = nil;
mandatory_closing._signaler = nil;

function mandatory_closing:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function mandatory_closing:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end

function mandatory_closing:Init(parameters)
    parameters:addBoolean("use_mandatory_closing", "Use Mandatory Closing", "", self.Default_use_mandatory_closing);
    parameters:addString("mandatory_closing_exit_time", "Mandatory Closing Time", "", self.Default_mandatory_closing_exit_time);
    parameters:addInteger("mandatory_closing_valid_interval", "Valid Interval for Operation, in second", "", 60);
end

function mandatory_closing:ParseTime(time)
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

function mandatory_closing:Prepare(name_only)
    self._use_mandatory_closing = instance.parameters.use_mandatory_closing;
    self._interval = instance.parameters.mandatory_closing_valid_interval;
    local exit_time = instance.parameters.mandatory_closing_exit_time;
    self._exit_time, valid = self:ParseTime(exit_time);
    assert(valid, "Time " .. exit_time .. " is invalid");

    if name_only then
        return;
    end

    if self._use_mandatory_closing then
        self._timer_id = self._ids_start + 1;
        core.host:execute("setTimer", self._timer_id, math.max(self._interval / 2, 1));
    end

    self:trace(string.format("Use mandatory closing: %s. Time of mandatory closing: %s. Interval: %s",
        tostring(self._use_mandatory_closing), tostring(exit_time), tostring(self._interval)));
end

function mandatory_closing:OnNewModule(module)
    if module.Name == "Timezone Parameter" then
        self._tz = module;
    elseif module.Name == "Signaler" then
        self._signaler = module;
    end
end

function mandatory_closing:InRange(now, from, to)
    if start_time < stop_time then
        return now >= from and now <= to;
    end
    if start_time > stop_time then
        return now > from or now < to;
    end
    return now == from;
end

function mandatory_closing:AsyncOperationFinished(cookie, success, message, message1, message2)
    if cookie == self._timer_id then
        now = core.host:execute("getServerTime");
        if self._tz ~= nil then
            now = core.host:execute("convertTime", core.TZ_EST, self._tz.Selected_timezone, now);
        end
        -- get only time
        now = now - math.floor(now);
        
        -- check whether the time is in the exit time period
        if self:InRange(now, self._exit_time, self._exit_time + (self._interval / 86400.0)) then
            trading:FindTrade()
                :WhenCustomID(custom_id)
                :Do(function (trade) trading:Close(trade); end );
        end
    end
end