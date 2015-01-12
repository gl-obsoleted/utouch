### README

这里是 utouch 的项目页面

----------------------------------------

### Download

项目发布包可以在这里下载：  

[utouch-0.1.2](https://bitbucket.org/mc_gulu/utouch/downloads/utouch-0.1.2.7z) [2015-01-12 16:19]  
[utouch-0.1.1](https://bitbucket.org/mc_gulu/utouch/downloads/utouch-0.1.1.7z) [2015-01-04 13:48]  

----------------------------------------

### Release Note

[2015-01-12] v0.1.2 - 在 v0.1.1 的基础上，调整和简化了目录结构。改进了一些调试机制。

    资源整理，配置挪到脚本：

      - 把所有的 atlas 挪到 res/atlases 目录内
      - 把所有的 samples 挪到 tests 目录内
      - 把默认 atlas 改名为 !atlas!
      - 把程序内对上面资源的引用都改为由 res.lua 获取

    删去老的不再使用的配置文件 cfg_default.json 和相应的代码，统一用 lua 配置
    开始把资源文件的配置信息从 app.config 转移到 lua 中去，同时把资源区分为开发时需要的（放入 dev 目录）和运行时需要的（放入 res 目录），第一步是移动 DefaultSkin.png 到 res 目录
    改进 MoonSharp 的错误处理，当出现异常时，能汇报更精确的信息（包含了脚本名和行号）
    改进了 logging （当汇报异常时，总会紧跟着 flush 一下，避免因直接退出而导致的关键信息未打印）


[2015-01-04] v0.1.1 - 在 0.1 的基础上修了几个稳定性问题，整理出一个较稳定的 utouch 版本，发布给美术先用起来，以便后续迭代

    [13:51] 二进制发布到 utouch 的 download 区，并将项目改为公开可见，这样美术不需要注册 BitBucket 账号也能自由下载。
    [13:26] [utouch/ngui_integration - bugfix] 修复了 ngui_integration 无法与最新的 utouch 协同工作的兼容性问题。
        - 在调用 Bootstrap.Init 时，没有提供 DesignTimeResolution 这个参数（编译时是不会报错的）
        - 由于在 utouch/udesign 中，该参数从 lua 中读取，而 ngui 集成目前还暂时没有对应的 lua 文件，所以创建一个临时的对象
        - 这是一个临时的方案，以后两边应尽量一致处理
    [10:39] [utouch - bugfix] 修复 Preview 面板在 Release 下报错后直接退出的问题
        1)  SysPost.AssertException 内的逻辑错误，已改为 assert 失败时抛异常
        2) 这里虽然预览面板抛出异常，但实际上是一个不影响正常流程的错误，对这种错误，通常只需弹框提醒即可，程序仍应无伤地继续运行。

---------------------------------

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact