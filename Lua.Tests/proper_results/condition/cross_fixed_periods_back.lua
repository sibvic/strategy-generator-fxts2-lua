if ((_v1 ~= nil and _v2 ~= nil) and (indicator_1.Median:size() >= 2 + 5 and instrument_1.close:size() >= 2 + 4 and core.crosses(indicator_1.Median, instrument_1.close, _v1, _v2))) then