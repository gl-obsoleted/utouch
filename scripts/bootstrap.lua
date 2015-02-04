

DIR_ScriptsRoot = "scripts/"

function run_script( filepath )
    dofile(DIR_ScriptsRoot..filepath)
end


-- 其他的启动脚本
run_script("res.lua")


-- 分辨率分组
ResCat_Desktop  = 0
ResCat_iOS      = 1
ResCat_Andriod  = 2
ResCat_Custom   = 3

-- 分辨率定义
Resolutions = {
    -- 桌面
    { w = 800,  h = 600,    cat = ResCat_Desktop },
    { w = 1024, h = 768,    cat = ResCat_Desktop },
    { w = 1280, h = 720,    cat = ResCat_Desktop,   tag = "16:9" },
    { w = 1440, h = 900,    cat = ResCat_Desktop,   tag = "16:10" },

    -- iOS
    { w = 480,  h = 320,    cat = ResCat_iOS,       tag = "iPhone Classic" },
    { w = 320,  h = 480,    cat = ResCat_iOS,       tag = "iPhone Classic, 竖屏" },
    { w = 960,  h = 640,    cat = ResCat_iOS,       tag = "iPhone 4" },
    { w = 640,  h = 960,    cat = ResCat_iOS,       tag = "iPhone 4, 竖屏" },
    { w = 1136, h = 640,    cat = ResCat_iOS,       tag = "iPhone 5" },
    { w = 640,  h = 1136,   cat = ResCat_iOS,       tag = "iPhone 5, 竖屏" },
    { w = 1024, h = 768,    cat = ResCat_iOS,       tag = "iPad Classic" },
    { w = 768,  h = 1024,   cat = ResCat_iOS,       tag = "iPad Classic, 竖屏" },
    { w = 2048, h = 1536,   cat = ResCat_iOS,       tag = "iPad Retina" },
    { w = 1536, h = 2048,   cat = ResCat_iOS,       tag = "iPad Retina, 竖屏" },
    
    -- Android
    { w = 1,    h = 1,      cat = ResCat_Andriod,   tag = "== 以下为2014年3月数据 =="},
    { w = 320,  h = 240,    cat = ResCat_Andriod,   tag = "1.1%"},
    { w = 480,  h = 320,    cat = ResCat_Andriod,   tag = "14.9%"},
    { w = 800,  h = 480,    cat = ResCat_Andriod,   tag = "33.1%"},
    { w = 854,  h = 480,    cat = ResCat_Andriod,   tag = "12.4%"},
    { w = 960,  h = 540,    cat = ResCat_Andriod,   tag = "9.1%"},
    { w = 1280, h = 720,    cat = ResCat_Andriod,   tag = "13.8%"},
    { w = 1280, h = 800,    cat = ResCat_Andriod,   tag = "1.5%"},
    { w = 1920, h = 1080,   cat = ResCat_Andriod,   tag = "5.4%"},
    { w = 1,    h = 1,      cat = ResCat_Andriod,   tag = "== 以下为对应的竖屏 =="},
    { w = 240,  h = 320,    cat = ResCat_Andriod,   tag = "1.1%"},
    { w = 320,  h = 480,    cat = ResCat_Andriod,   tag = "14.9%"},
    { w = 480,  h = 800,    cat = ResCat_Andriod,   tag = "33.1%"},
    { w = 480,  h = 854,    cat = ResCat_Andriod,   tag = "12.4%"},
    { w = 540,  h = 960,    cat = ResCat_Andriod,   tag = "9.1%"},
    { w = 720, h = 1280,    cat = ResCat_Andriod,   tag = "13.8%",   default = true},
    { w = 800, h = 1280,    cat = ResCat_Andriod,   tag = "1.5%"},
    { w = 1080, h = 1920,   cat = ResCat_Andriod,   tag = "5.4%"},
    
    -- Custom
    { w = 1000, h = 500,    cat = ResCat_Custom,    tag = "纯测试" },
}

