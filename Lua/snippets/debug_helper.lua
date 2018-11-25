debug_helper = {};
-- public fields
debug_helper.Name = "debug_helper";
debug_helper.Version = "1.1";
debug_helper.debug_helper = true;
--private fields
debug_helper._ids_start = nil;
debug_helper._orders_subscription_id = nil;
debug_helper._trades_subscription_id = nil;
debug_helper._file = nil;
debug_helper._headers = {};
debug_helper._data = {};
debug_helper._instruments = {};
debug_helper._indicators = {};
debug_helper._headers_printed = false;
function debug_helper:AddHeader(header_id)
    self._headers[#self._headers + 1] = header_id;
end
function debug_helper:PrintDebug(header_id, str)
    if self._file == nil then
        return;
    end
    local header_index = self:findHeaderIndex(header_id);
    self._data[header_index] = str;
end
function debug_helper:Next()
    if self._file == nil then
        return;
    end
    if (not self._headers_printed) then
        self._headers_printed = true;
        for _, instrument in ipairs(self._instruments) do
            self:printInstrumentHeader(instrument);
        end
        for _, indicator in ipairs(self._indicators) do
            self:printIndicatorHeader(indicator);
        end
        self:print(self._headers);
        self._file:write("\n");
    end
    for _, instrument in ipairs(self._instruments) do
        self:printInstrumentData(instrument);
    end
    for _, indicator in ipairs(self._indicators) do
        self:printIndicatorData(indicator);
    end
    self:print(self._data);
    self._file:write("\n");
    self._file:flush();
    self._data = {};
end
function debug_helper:AddInstrument(instrument)
    self._instruments[#self._instruments + 1] = instrument;
end
function debug_helper:AddIndicator(indicator)
    self._indicators[#self._indicators + 1] = indicator;
end
function debug_helper:Init(parameters)
    parameters:addFile("debug_helper_file", "debug_helper file, .csv", "", "");
end
function debug_helper:Prepare(name_only)
    self._file = io.open(instance.parameters.debug_helper_file, "a");
    core.host:execute("subscribeTradeEvents", self._orders_subscription_id, "orders");
    self:AddHeader("Orders");
    core.host:execute("subscribeTradeEvents", self._trades_subscription_id, "trades");
    self:AddHeader("Trades");    
end
function debug_helper:OnNewModule(module)
    if module.Name == "Timezone Parameter" then
        self._tz = module;
    elseif module.Name == "Signaler" then
        self._signaler = module;
    end
end
function debug_helper:RegisterModule(modules)
    for _, module in pairs(modules) do
        self:OnNewModule(module);
        module:OnNewModule(self);
    end
    modules[#modules + 1] = self;
    self._ids_start = (#modules) * 100;
    self._orders_subscription_id = self._ids_start + 1;
    self._trades_subscription_id = self._ids_start + 2;
end
function debug_helper:ReleaseInstance()
    self._file:close();
end
function debug_helper:findHeaderIndex(header_id)
    for index, value in ipairs(self._headers) do
        if value == header_id then
            return index;
        end
    end
    if self._headers[#self._headers] ~= "_other_" then
        self:AddHeader("_other_");
    end
    return #self._headers;
end
function debug_helper:print(data)
    if self._file == nil then
        return;
    end
    for _, value in ipairs(data) do
        if value ~= nil then
            self._file:write(value);
        end
        self._file:write(";");
    end
end
function debug_helper:printInstrumentHeader(instrument)
    local name = instrument:instrument() .. "(" .. instrument:barSize() .. "," .. tostring(instrument:isBid()) .. ")";
    self._file:write(name .. ".Date;");
    self._file:write(name .. ".Open;");
    self._file:write(name .. ".High;");
    self._file:write(name .. ".Low;");
    self._file:write(name .. ".Close;");
    self._file:write(name .. ".Volume;");
end
function debug_helper:printIndicatorHeader(indicator)
    local name = indicator:name();
    self._file:write(name .. ".Date;");
    for i = 0, indicator:getStreamCount() - 1 do
        self._file:write(name .. "." .. indicator:getStream(i):id() .. ";");
    end
end
function debug_helper:printInstrumentData(instrument)
    self._file:write(core.formatDate(instrument:date(NOW)) .. ";");
    self._file:write(instrument.open:tick(NOW) .. ";");
    self._file:write(instrument.high:tick(NOW) .. ";");
    self._file:write(instrument.low:tick(NOW) .. ";");
    self._file:write(instrument.close:tick(NOW) .. ";");
    self._file:write(instrument.volume:tick(NOW) .. ";");
end
function debug_helper:printIndicatorData(indicator)
    self._file:write(core.formatDate(indicator.DATA:date(NOW)) .. ";");
    for i = 0, indicator:getStreamCount() - 1 do
        local stream = indicator:getStream(i);
        if stream:size() == 0 then
            self._file:write("[no data yet];");
        elseif stream:hasData(NOW) then
            self._file:write(stream:tick(NOW) .. ";");
        else
            self._file:write(";");
        end
    end
end
function debug_helper:AsyncOperationFinished(cookie, success, message, message1, message2)
    if self._file == nil then
        return;
    end
    if cookie == self._orders_subscription_id then
        local header_index = self:findHeaderIndex("Orders");
        local message = string.format("Order %s event. New status: %s. Request: %s", tostring(message), tostring(message1), tostring(message2));
        if self._data[header_index] == nil then
            self._data[header_index] = message;
        else
            self._data[header_index] = self._data[header_index] .. "|" .. message;
        end
    elseif cookie == self._trades_subscription_id then
        local header_index = self:findHeaderIndex("Trades");
        local message = string.format("Trade %s event. Is open trade: %s. Order ID: %s. Request: %s", tostring(message), tostring(success), tostring(message1), tostring(message2));
        if self._data[header_index] == nil then
            self._data[header_index] = message;
        else
            self._data[header_index] = self._data[header_index] .. "|" .. message;
        end
    end
end
function debug_helper:ExtUpdate(id, source, period) end
function debug_helper:BlockTrading(id, source, period) return false; end
function debug_helper:BlockOrder(order_value_map) return false; end
function debug_helper:OnOrder(order_value_map) end
debug_helper.Debug = true;
debug_helper:RegisterModule(Modules);