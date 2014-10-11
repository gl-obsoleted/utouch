
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
    { w = 1280, h = 720,    cat = ResCat_Desktop,   tag = "宽屏" },

    -- iOS
    { w = 1024, h = 768,    cat = ResCat_iOS,       tag = "Classic" },
    { w = 2048, h = 1536,   cat = ResCat_iOS,       tag = "Retina" },
    
    -- Android
    { w = 1024, h = 768,    cat = ResCat_Andriod },
    
    -- Custom
    { w = 1000, h = 500,    cat = ResCat_Custom,    tag = "纯测试" },
}

