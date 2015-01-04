### README

这里是 utouch 的项目页面

----------------------------------------

### Downloads

项目发布包可以在这里下载：  

[utouch-0.1.1](https://bitbucket.org/mc_gulu/utouch/downloads/utouch-0.1.1.7z) [2015-01-04 13:48]

----------------------------------------

### Release Note

[2015-01-04] v0.1.1 - 在 0.1 的基础上修了几个稳定性问题，整理出一个较稳定的 utouch 版本，发布给美术先用起来，以便后续迭代

    [13:51] 二进制发布到 utouch 的 download 区，并将项目改为公开可见，这样不需要注册 BitBucket 账号也能自由下载。
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