timezone_parameter = {};
timezone_parameter.Name = "Timezone Parameter";
timezone_parameter.Debug = false;
timezone_parameter.Version = "1.0";

timezone_parameter._ids_start = nil;
timezone_parameter.Selected_timezone = nil;

function timezone_parameter:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function timezone_parameter:OnNewModule(module) end
function timezone_parameter:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end

function timezone_parameter:Init(parameters)
    parameters:addInteger("timezone_to_time", "Convert the Date To", "", 6);
    parameters:addIntegerAlternative("timezone_to_time", "EST", "", 1);
    parameters:addIntegerAlternative("timezone_to_time", "UTC", "", 2);
    parameters:addIntegerAlternative("timezone_to_time", "Local", "", 3);
    parameters:addIntegerAlternative("timezone_to_time", "Server", "", 4);
    parameters:addIntegerAlternative("timezone_to_time", "Financial", "", 5);
    parameters:addIntegerAlternative("timezone_to_time", "Display", "", 6);
end

function timezone_parameter:Prepare(name_only)
    self.Selected_timezone = instance.parameters.timezone_to_time;
    if self.Selected_timezone == 1 then
        self.Selected_timezone = core.TZ_EST;
    elseif self.Selected_timezone == 2 then
        self.Selected_timezone = core.TZ_UTC;
    elseif self.Selected_timezone == 3 then
        self.Selected_timezone = core.TZ_LOCAL;
    elseif self.Selected_timezone == 4 then
        self.Selected_timezone = core.TZ_SERVER;
    elseif self.Selected_timezone == 5 then
        self.Selected_timezone = core.TZ_FINANCIAL;
    elseif self.Selected_timezone == 6 then
        self.Selected_timezone = core.TZ_TS;
    end
end
