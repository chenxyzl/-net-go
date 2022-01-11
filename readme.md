# net6.0 vs go 热更新函数性能测试(有1个class或struct参数，无参数性能更高)
### go方案: plugin.Open
### c#方案: Assembly.LoadFile
---
---
## net6.0 100000000次函数调用花费时常
---
调用方式|耗时(纳秒)
---|:--:
local内调用|39967200
hotfix内调用|32057700
local内调用(delgate)|168931000
local调用hotfix(delgate)|180703700
---
## go1.7.5 100000000次函数调用花费时常
---
调用方式|耗时(纳秒)
---|:--:
local内调用|38792568
hotfix内调用|30208890
local调用hotfix|141151759
---
