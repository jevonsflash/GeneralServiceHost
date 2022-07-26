![logo](https://raw.githubusercontent.com/jevonsflash/GeneralServiceHost/master/GSH/logo.png)
# GeneralServiceHost

一个任务托管程序 以多线程的方式执行定时的任务并进行监控

这是独立开发者的一个开源项目, 希望得到您的意见反馈，请将Bugs汇报至我的邮箱

 ![avatar](https://raw.githubusercontent.com/jevonsflash/GeneralServiceHost/master/GSH/screenshot.png)
 
## 更新内容：


Date | Version | Content
:----------: | :-----------: | :-----------
V1.1         | 2018-1-22        | 修复统计信息不准确的bug
V2.0         | 2018-6-30        | 添加 不间断运行 执行计划；修复添加失败后主页闪退bug；小幅更改UI使得操作更人性化
V2.1         | 2022-5-10        | 升级项目框架至.Net 6
V2.1.1       | 2022-7-20        | 修复第二次打开添加任务报错问题；添加最小化至托盘图标功能
V2.1.2       | 2022-7-26        | 修复添加程序集BadImageFormatException异常的问题

## 特点：

* 指定一个可执行文件作为任务

* 指定任务执行的时间，并提供进程守护功能

* 运行状态和运行统计

* 日志查看和导出

## 安装说明：

1. 下载安装包 https://raw.githubusercontent.com/jevonsflash/GeneralServiceHost/master/GSH/gsh.zip

2. 解压并双击 setup.exe 安装

## 运行环境

* Microsoft Windows 7 x64 及以上

## 已知问题：

如要运行管理员权限的任务，需要在管理员模式下启用该程序

若程序出现异常，会弹出异常对话框，这在进程守护勾选的情况下，可能需要您手动关闭它才能继续执行

## 作者信息：

作者：林小

邮箱：jevonsflash@qq.com


## License

The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
