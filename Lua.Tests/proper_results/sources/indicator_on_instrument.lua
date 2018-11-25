local mva = nil;
local instrument = nil;
local instrument_id = 1;
function Prepare(name_only)
    for _, module in pairs(Modules) do module:Prepare(nameOnly); end
    instance:name(profile:id() .. "(" .. instance.bid:name() ..  ")");
    if name_only then return ; end
    custom_id = profile:id() .. "_" .. instance.bid:name();
    assert(core.indicators:findIndicator("MVA") ~= nil, "Please, download and install MVA indicator");
    instrument = ExtSubscribe(instrument_id, nil, "m1", true, "bar");
    mva = core.indicators:create("MVA", instrument.close);
    trading_logic.DoTrading = EntryFunction;
end