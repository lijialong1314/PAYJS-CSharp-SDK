# PAYJS-CSharp-SDK

本项目为PAYJS API接口的C#封装。关于payjs，可以在这里了解更多：https://help.payjs.cn/

## 说明

为了简化sdk，本项目仅有一个cs类文件，封装了payjs常用接口的调用、生成sign、验sign、发送请求等功能。

**不包含**响应结果的格式化功能，需要自行处理json字符串，推荐使用https://github.com/JamesNK/Newtonsoft.Json

src目录为类库源文件，可以引入自己的项目中，支持ASP.NET、ASP.NET Core、NetCore WebAPI等环境

## 快速使用

#### 1、初始化

```c#
Payjs pay = new Payjs("YOUR MCHID", "YOUR KEY");
```

#### 2、二维码

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["total_fee"] = "1";
param["out_trade_no"] = DateTime.Now.Ticks.ToString();
param["body"] = "test";
param["attch"] = "user id";
param["notify_url"] = "YOUR notify_url";

//返回原始json字符串
string jsonString = pay.native(param);
```

#### 3、收银台

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["total_fee"] = "1";
param["out_trade_no"] = DateTime.Now.Ticks.ToString();
param["body"] = "test";
param["attch"] = "user id";
param["notify_url"] = "YOUR notify_url";
param["callback_url"] = "YOUR callback_url";

//返回收银台地址，直接跳转到此地址即可
string url = pay.cashier(param);
```

#### 4、JSAPI

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["total_fee"] = "1";
param["out_trade_no"] = DateTime.Now.Ticks.ToString();
param["body"] = "test";
param["attch"] = "user id";
param["notify_url"] = "YOUR notify_url";
param["openid"] = "user's openid";//获取用户openid：https://help.payjs.cn/api-lie-biao/huo-qu-openid.html

//返回原始json字符串
string jsonString = pay.jsapi(param);
```

#### 5、刷卡支付

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["total_fee"] = "1";
param["out_trade_no"] = DateTime.Now.Ticks.ToString();
param["body"] = "test";
param["attch"] = "user id";
param["auth_code"] = "YOUR auth_code";


//返回原始json字符串
string jsonString = pay.micropay(param);
```

#### 6、订单查询

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["payjs_order_id"] = "2018xxxxxxx";

//返回原始json字符串
string jsonString = pay.check(param);
```

#### 7、关闭订单

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["payjs_order_id"] = "2018xxxxxxx";

//返回原始json字符串
string jsonString = pay.close(param);
```

#### 8、退款

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["payjs_order_id"] = "2018xxxxxxx";

//返回原始json字符串
string jsonString = pay.refund(param);
```

#### 9、获取用户资料

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["openid"] = "xxxxxxxxxxx";

//返回原始json字符串
string jsonString = pay.user(param);
```

#### 10、获取商户资料

```c#
//返回原始json字符串
string jsonString = pay.info(new Dictionary<string, string>());
```

#### 11、PAYJS回调请求的验签

```c#
//param参数为PAYJS服务器POST过来的数据，根据自己项目中所用的框架获取POST数据后，转为Dictionary<string,string>类型传入notifyCheck方法

//返回是否验证成功
bool issuccess = pay.notifyCheck(param);
```

#### 12、人脸支付

```c#
Dictionary<string, string> param = new Dictionary<string, string>();
param["total_fee"] = "1";
param["out_trade_no"] = DateTime.Now.Ticks.ToString();
param["body"] = "test";
param["attch"] = "user id";
param["face_code"] = "YOUR face_code";
param["openid"] = "user's openid";

string jsonString = pay.facepay(param);
```

