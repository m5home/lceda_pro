# lceda_pro
  
作者:嗷嗷叫的老马(http://www.m5home.com)  
  
利用CefSharp包装了一下立创EDA的专业版页面(https://pro.lceda.cn/editor ),好处是不需要另外下载浏览器了.  
坏处嘛.....这CefSharp巨大,压缩后还得50MB....-_-!  
此代码需要使用VS2017打开,然后再从NuGet还原CefSharp包.  
另,此代码是基于https://github.com/yh371174229/CefSharpExample 修改的,在此感谢,省了很多事:D  
以下是具体说明:  

1,增加了一个启动窗口,因为CefSharp首次启动时有点慢,不显示点啥,总感觉呆等...那干脆就显示点啥好了:D  
2,做了个记忆窗口位置.这个没啥好说的.  
3,由于仅仅是个浏览器壳,所以这个壳一般来说可以不用动,反正实际内容都是在嘉立创,他们更新了,内容也就更新了,嘿.  

别的没啥了.
