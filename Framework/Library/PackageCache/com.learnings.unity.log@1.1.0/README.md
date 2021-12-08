# log-unity

package: `com.learnings.unity.log`

Print log for unity

### 目录

[TOC]

### 导入方式

```
    "dependencies": {
      "com.learnings.unity.log": "1.1.0"
    }，
    "registry": "https://internal-sdk.lexinshengwen.com/verdaccio/"
```

### 环境控制

    方式一：
    通过 DEV_BUILD 预处理命令控制，编辑环境不受此限制。
    真机环境在打包时 PlayerSettings->OtherSettings->ScriptingDefineSymbols 里面有这个命令则会进行 Log 输出，否则不会。
    
    方式二：
    使用 Log.D.IsDebug 变量控制 Log 输出，编辑器和真机环境都会生效，值为真时则会进行 Log 输出。

### Log 等级控制

    配置：非 DEV_BUILD 环境，Log.D.IsDebug 设置为 true
    新增预处理命令：
        LOGLEVEL1： 打印 Error 等级日志。
        LOGLEVEL2： 打印 Error 和 Warn 等级日志。
        LOGLEVEL3： 打印 Error，Warn 和 Info 等级日志。

## issues