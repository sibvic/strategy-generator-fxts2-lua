trading_filter_side = {};
trading_filter_side.Name = "Trading filter: side";
trading_filter_side.Debug = false;
trading_filter_side.Version = "1.0";

trading_filter_side._ids_start = nil;
trading_filter_side._allow_side = nil;

function trading_filter_side:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function trading_filter_side:OnNewModule(module) end
function trading_filter_side:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end

function trading_filter_side:Init(parameters)
    parameters:addString("allow_side", "Allowed side", "Allowed side for trading or signaling, can be Sell, Buy or Both", "Both");
    parameters:addStringAlternative("allow_side", "Both", "", "Both");
    parameters:addStringAlternative("allow_side", "Buy", "", "Buy");
    parameters:addStringAlternative("allow_side", "Sell", "", "Sell");
end

function trading_filter_side:Prepare(name_only)
    --do what you usually do in prepare
    if name_only then
        return;
    end
    self._allow_side = instance.parameters.allow_side;
end

function trading_filter_side:BlockOrder(order_value_map)
    if self._allow_side == "Both" then
        return false;
    elseif self._allow_side == "Buy" then
        return order_value_map.BuySell ~= "B";
    else
        return order_value_map.BuySell ~= "S";
    end
end
