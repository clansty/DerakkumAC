# DerakkumAC

打 mai 太热了？开个空调！

https://www.bilibili.com/video/BV1Cm42157VQ

同时按下 1P 和 2P 的“选择”键就可以向 MQTT 服务器发送指定消息。从 MQTT 指定主题收到的消息会作为通知显示在屏幕上

## 安装方法

1. 游戏需要安装 [MelonLoader](https://github.com/LavaGang/MelonLoader)
2. 下载 [Release.zip](https://github.com/clansty/DerakkumAC/releases/download/1.0.0/Release.zip)，把里面的文件解压到游戏目录下
3. 编辑 DerakkumAC.yaml，填写 MQTT 的连接信息和发送接收的主题

## HomeAssistant 配置

需要在 HomeAssistant 上创建自动化来监听 MQTT 的消息，开空调以及向 MQTT 推送结果

[示例自动化 YAML](./hass.yaml)