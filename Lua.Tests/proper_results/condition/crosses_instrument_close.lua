if ((_v1 ~= nil and _v2 ~= nil) and (macd.MACD:size() >= 2 and source_0.close:size() >= 2 and core.crosses(macd.MACD, source_0.close, _v1, _v2))) then