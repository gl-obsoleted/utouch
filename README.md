
### Download

发布包可以在这里下载：  

[utouch-0.1.3](https://github.com/mc-gulu/utouch/releases/download/v0.1.3/utouch-v0.1.3.zip) [2015-02-13 14:04]   
[utouch-0.1.2](https://bitbucket.org/mc_gulu/utouch/downloads/utouch-0.1.2.7z) [2015-01-12 16:19]  
[utouch-0.1.1](https://bitbucket.org/mc_gulu/utouch/downloads/utouch-0.1.1.7z) [2015-01-04 13:48]  


----------------------------------------

### Release Notes

#### [2015-02-13] v0.1.3 - 转移项目到 [GitHub](https://github.com/mc-gulu/utouch)；新增了可拖动画布的功能。

* 由 BitBucket 转到 GitHub，更方便对外发布。
* 统一 OpenGL 的背景颜色
* 首次支持整个 canvas 的拖动 (鼠标中键)，主要改动包括
  - 当鼠标中键拖动时，更新当前的 OrthoTransform (OT)
  - 调用 glOrtho 时，使用 OT 变换摄像机的投影矩阵，由 TransformCameraProjection 实现
  - 每个控件的裁剪都是通过设置 Renderer 的 ClipRegion 来实现的，使用 OT 变换，由 TransformClipRegion 实现
  - 把当前的鼠标传递给编辑器逻辑层时，使用 OT 变换，由 TransformMouseLocation 实现
* 把坐标系画出来，方便在画布上定位
* 新增用户脚本模板，在保存新建的 layout 文件时，该脚本将被复制到目标目录，用于记录用户图集信息
* add user script support; move LuaRuntime into ucore
* refactor - move class Session into ucore; adding new class UCoreStart; remove log4net dependency
* fix #1 - convert mouse position to help adding new control to the correct parent location
* fix #2, resizing control is fixed by properly transform the mouse location;
    the size of gwen canvas is also enlarged to a maximum possible size,
    in order to validate a given mouse location which now can go beyond the screen coordinate,
    after the implementing of the 'draggable scene'.
* close #4 recent accessed directory is now saved in per-user .config file


#### [2015-01-12] v0.1.2 - 在 v0.1.1 的基础上，调整和简化了目录结构。改进了一些调试机制。

    资源整理，配置挪到脚本：

      - 把所有的 atlas 挪到 res/atlases 目录内
      - 把所有的 samples 挪到 tests 目录内
      - 把默认 atlas 改名为 !atlas!
      - 把程序内对上面资源的引用都改为由 res.lua 获取

    删去老的不再使用的配置文件 cfg_default.json 和相应的代码，统一用 lua 配置
    开始把资源文件的配置信息从 app.config 转移到 lua 中去，同时把资源区分为开发时需要的（放入 dev 目录）和运行时需要的（放入 res 目录），第一步是移动 DefaultSkin.png 到 res 目录
    改进 MoonSharp 的错误处理，当出现异常时，能汇报更精确的信息（包含了脚本名和行号）
    改进了 logging （当汇报异常时，总会紧跟着 flush 一下，避免因直接退出而导致的关键信息未打印）


#### [2015-01-04] v0.1.1 - 在 0.1 的基础上修了几个稳定性问题，整理出一个较稳定的 utouch 版本，发布给美术先用起来，以便后续迭代

    [13:51] 二进制发布到 utouch 的 download 区，并将项目改为公开可见，这样美术不需要注册 BitBucket 账号也能自由下载。
    [13:26] [utouch/ngui_integration - bugfix] 修复了 ngui_integration 无法与最新的 utouch 协同工作的兼容性问题。
        - 在调用 Bootstrap.Init 时，没有提供 DesignTimeResolution 这个参数（编译时是不会报错的）
        - 由于在 utouch/udesign 中，该参数从 lua 中读取，而 ngui 集成目前还暂时没有对应的 lua 文件，所以创建一个临时的对象
        - 这是一个临时的方案，以后两边应尽量一致处理
    [10:39] [utouch - bugfix] 修复 Preview 面板在 Release 下报错后直接退出的问题
        1)  SysPost.AssertException 内的逻辑错误，已改为 assert 失败时抛异常
        2) 这里虽然预览面板抛出异常，但实际上是一个不影响正常流程的错误，对这种错误，通常只需弹框提醒即可，程序仍应无伤地继续运行。


