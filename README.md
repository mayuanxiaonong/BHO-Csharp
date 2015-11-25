# BHO-Csharp
使用BHO开发IE插件，实现监听、拦截页面的Http请求，根据页面数据，请求外部系统接口，并做出提示。

历程：

第一步：注入

1.注册OnBeforeNavigate2事件。

发现问题：只能监测页面跳转，不能监测ajax请求。

2.在OnDocumentComplete事件中注入js挂接页面DOM元素的click等事件，并回调BHO函数。

发现问题：F5刷新时，BHO失效。

3.在OnDownloadComplete事件替代OnDocumentComplete事件的逻辑。

发现问题：OnDownloadComplete会触发多次，引起多次js注入，最后导致回调的BHO函数执行多次。

4.增加计数器变量，OnDownloadComplete中判断计数器为1时注入js，并增加计数。

发现问题：计数器在刷新页面时没有重新定义，而是继续增加计数，导致后面的判断失效。

5.监听OnRefresh事件，重置计数器。

发现问题：OnDownloadComplete和OnRefresh事件发生时机不定，且发生次数不定，无法找到重置计数器的切入点。

6.加入OnBeforeNavigate2、OnDocumentComplete事件进行重置计数。

发现问题：直接打开页面和链接打开页面触发的事件顺序和次数不定，导致首次进入页面出现重复注入js和首次刷新后BHO失效。

7.在OnDownloadComplete事件中注入DOM元素，在每一次Download事件触发时判断该元素是否存在，不存在则注入DOM元素和js脚本。

目前测试可用，需要继续对iframe嵌套的页面场景进行测试。


