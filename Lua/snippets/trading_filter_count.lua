trading_filter_count = {};
trading_filter_count.Name = "Trading filter: count";
trading_filter_count.Debug = false;
trading_filter_count.Version = "1.0";

trading_filter_count._ids_start = nil;
trading_filter_count._use_position_cap = nil;
trading_filter_count._max_number_of_position_in_any_direction = nil;
trading_filter_count._max_number_of_position = nil;

function trading_filter_count:trace(str) if not self.Debug then return; end core.host:trace(self.Name .. ": " .. str); end
function trading_filter_count:OnNewModule(module) end
function trading_filter_count:RegisterModule(modules) for _, module in pairs(modules) do self:OnNewModule(module); module:OnNewModule(self); end modules[#modules + 1] = self; self._ids_start = (#modules) * 100; end

function trading_filter_count:Init(parameters)
    parameters:addBoolean("use_position_cap", "Use Position Cap", "", false);	
	parameters:addInteger("max_number_of_position_in_any_direction", "Max Number Of Open Position In Any Direction", "", 2);
	parameters:addInteger("max_number_of_position", "Max Number Of Position In One Direction", "", 1);
end

function trading_filter_count:Prepare(name_only)
    --do what you usually do in prepare
    if name_only then
        return;
    end
    self._use_position_cap = instance.parameters.use_position_cap;
    self._max_number_of_position_in_any_direction = instance.parameters.max_number_of_position_in_any_direction;
    self._max_number_of_position = instance.parameters.max_number_of_position;
end

function trading_filter_count:BlockOrder(order_value_map)
    if not self._use_position_cap then
        return false;
    end
    local buy_position_count, sell_position_count = self:countTrades();
    if (buy_position_count + sell_position_count) >= self._max_number_of_position_in_any_direction then
        return true;
    end
    if order_value_map.BuySell == "B" then
        return buy_position_count >= self._max_number_of_position;
    else
        return sell_position_count >= self._max_number_of_position;
    end
end

function trading_filter_count:countTrades() 
    local buy_count = 0;
    local sell_count = 0;
    local enum = core.host:findTable("trades"):enumerator();
    local row = enum:next();
    while row ~= nil do
        if row.BS == "B" then
            buy_count = buy_count + 1;
        else
            sell_count = sell_count + 1;
        end

        row = enum:next();
    end

    return buy_count, sell_count;
end