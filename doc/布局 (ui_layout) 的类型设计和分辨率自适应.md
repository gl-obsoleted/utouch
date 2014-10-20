
### 布局 (ui_layout) 的类型设计和分辨率自适应

布局文件有两种：全屏的 Page 和非全屏的 Widget。

#### Page 全屏布局 (fullscreen layout)

Page 作为整体的页面单位，通常对应游戏中覆盖全屏的一个页面。创建 Page 时，可指定其设计时分辨率 (Design-Time Resolution)。

#### Widget 非全屏布局 (non-fullscreen layout)

Widget 通常是一个可以被其他布局文件 (Page 或另一个 Widget) 引用的组件，通常用于对被多个页面共享的公共元素进行复用。Widget 不支持自引用和循环引用 (A引用B，B又引用A)。

#### Multi-Resolution Auto-Adapting (MRAA) 多分辨率自适应机制

为了适应多个不同的分辨率，在新建布局时，提供一些选项，用于规划缩放和对齐的方案，具体如下：

- MRAA_ScaleMode: 可选择
  - “整体缩放” (full-scale) 控件尺寸和控件间的相对位置同步等比缩放，效果是整体缩放界面到目标尺寸，外观保持不变。
  - “保持原尺寸” (fixed-size) 控件尺寸保持不变，相对位置会被调整以适应新的分辨率；效果是当分辨率提高时，界面上各种控件的尺寸会变小，变得更稀疏
  
- MRAA_ClampMode: 可选择
  - “以宽度为基准来调整” (clamp-by-width) 
  - “以高度为基准来调整” (clamp-by-height)
  - “以较大的边为基准来调整” (clamp-by-larger)



