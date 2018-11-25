local last_serial;
local custom_id;
local mva = nil;
function Prepare(name_only)
    for _, module in pairs(Modules) do module:Prepare(nameOnly); end
    instance:name(profile:id() .. "(" .. instance.bid:name() ..  ")");
    if name_only then return ; end
    custom_id = profile:id() .. "_" .. instance.bid:name();
