

DIR_ScriptsRoot = "scripts/"

function run_script( filepath )
    dofile(DIR_ScriptsRoot..filepath)
end

-- 系统脚本
run_script("udesign/resolution.lua")
run_script("udesign/resources.lua")

