# 🐟 原神自动钓鱼机

PC原神自动钓鱼机（支持不同游戏窗口大小、DPI缩放）。『你只需负责甩竿，后面的放着我来！』

操作最简单的自动钓鱼机，自带GUI，选区后一键开启，持续钓鱼，无需反复开关，简单易用，解放双手。

* **无需调整原神窗口大小、DPI缩放等显示设置就能使用（全屏也可以使用）**
* 鱼儿上钩后延迟 0 ~ 1s 自动提竿
* 钓鱼条识别率极高，个人测试无脱钩情况，鱼挣扎（黄条）情况也能正常识别
* **容错率高，你可以一直开着自动钓鱼进行传送、跑路、钓鱼**
* 兼容新活动钓鱼（只是钓鱼条被倒计时计时挤下去了，调整下选区即可）

![](https://raw.githubusercontent.com/babalae/genshin-fishing-toy/master/Image/demo.gif)

## 下载地址

[📥Github下载（1.3）](https://github.com/babalae/genshin-fishing-toy/releases/download/v1.3/GenshinAutoFish_v1.3.zip)

[📥Gitee下载（1.3）](https://gitee.com/babalae/genshin-auto-fish/attach_files/839858/download/GenshinAutoFish_v1.3.zip)

**如果你遇到了[内存溢出](https://github.com/babalae/genshin-fishing-toy/issues/18)的问题，或者你是64位的系统，可以直接下载[📥x64版本](https://wwn.lanzouy.com/ibNbY05bx50f)**

## 使用方法

你的系统需要满足以下条件：
  * Windows 7 或更高版本
  * [.NET Framework 4.6.1](https://support.microsoft.com/zh-cn/topic/windows-net-framework-4-6-1-%E8%84%B1%E6%9C%BA%E5%AE%89%E8%A3%85%E7%A8%8B%E5%BA%8F-842e545a-bad5-c538-e491-578d017e677c) 或更高版本。**低于此版本在打开程序时可能无反应，或者直接报错**。


首先移动半透明矩形选区选择识别范围，只需要框住钓鱼进度条就可以了，**不要框住下方的钓鱼总进度圈**。

确认选框位置正确后，就直接启动进行自动钓鱼啦（快捷键<kbd>F11</kbd>）。

甩竿后直接等待鱼儿上钩即可，保持当前原神窗口是激活状态，程序会自动根据当前图像识别的结果发送对应鼠标操作，自动化提竿、完成钓鱼进度。

不在意一些战斗误触的时候可以一直开着自动钓鱼识别，当然如果不钓鱼了最好还是停止自动钓鱼功能，部分角色重击动画可能会被识别成钓鱼条，影响实际操作。

## FAQ
* 为什么需要管理员权限？
  * 因为游戏以管理员权限启动，软件不以管理员权限启动的话没法模拟鼠标点击。
* 会不会封号？
  * 只能说理论上不会被封，但是mhy是自由的，封号理由上有第三方软件这一条。当前使用了 PostMessage 模拟鼠标点击，还是存在被检测的可能。只能说请低调使用，请不要跳脸官方。

## 投喂

觉的好用的话，可以支持作者哟ヾ(･ω･`｡) 👇
* [⚡爱发电](https://afdian.net/@huiyadanli)
* [🍚微信赞赏](https://github.com/huiyadanli/huiyadanli/blob/master/DONATE.md)
