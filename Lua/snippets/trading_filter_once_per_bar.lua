trading_filter_once_per_bar = {};
-- public fields
trading_filter_once_per_bar.Name = "Trading filter: once per bar";
trading_filter_once_per_bar.Version = "1.0";
trading_filter_once_per_bar.Debug = false;
trading_filter_once_per_bar.Source = nil;
--private fields
trading_filter_once_per_bar._ids_start = nil;
trading_filter_once_per_bar._once_per_bar = true;
trading_filter_once_per_bar._last_trade_serial = nil;
trading_filter_once_per_bar._trading_logic = nil;

function trading_filter_once_per_bar:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function trading_filter_once_per_bar:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end

function trading_filter_once_per_bar:OnNewModule(module)
    if module.Name == "Trading logic" then
        self._trading_logic = module;
    end
end

function trading_filter_once_per_bar:Init(parameters)
    parameters.addBoolean("trade_once_per_bar", "Trade once per bar", "", true);
end

function trading_filter_once_per_bar:Prepare(name_only)
    --do what you usually do in prepare
    if name_only then
        return;
    end
    self._once_per_bar = instance.parameters.trade_once_per_bar or true;
    self:trace("Trade once per bar: " .. tostring(self._once_per_bar));
end

function trading_filter_once_per_bar:getSource()
    if self.Source ~= nil then
        return self.Source;
    end
    return self._trading_logic.MainSource;
end

function trading_filter_once_per_bar:BlockOrder(order_value_map)
    if not self._once_per_bar then
        return false;
    end
    if self:getSource():serial(NOW) == self._last_trade_serial then
        self:trace(string.format("Blocking %s: order already opened on this bar", tostring(order_value_map.OrderType)));
        return true;
    end
    return false;
end

function trading_filter_once_per_bar:OnOrder(order_value_map)
    self._last_trade_serial = self:getSource():serial(NOW);
    self:trace("Updating serial to " .. self:getSource():serial(NOW))
end