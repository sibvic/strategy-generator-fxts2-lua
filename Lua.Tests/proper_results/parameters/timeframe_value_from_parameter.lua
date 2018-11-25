    strategy.parameters:addString("mogalef_timeframe", "Mogalef timeframe", "desc", "m1");
    strategy.parameters:setFlag("mogalef_timeframe", core.FLAG_PERIODS);
    trading_logic:Init(strategy.parameters);
end
local last_serial;
local custom_id;
local indicatorSource = nil;
local indicatorSource_id = 1;
local indicator_1 = nil;
function Prepare(name_only)
    for _, module in pairs(Modules) do module:Prepare(nameOnly); end
    instance:name(profile:id() .. "(" .. instance.bid:name() ..  ")");
    if name_only then return ; end
    custom_id = profile:id() .. "_" .. instance.bid:name();
    assert(core.indicators:findIndicator("MOGALEF") ~= nil, "Please, download and install MOGALEF indicator");
    indicatorSource = ExtSubscribe(indicatorSource_id, nil, instance.parameters.mogalef_timeframe, true, "bar");