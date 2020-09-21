using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    //基于AspNetCore的web api应用
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        //定义一个post action，供payjs服务器访问，访问地址格式：http://xxx/api/callback
        [HttpPost]
        public string callback()
        {
            Payjs payjs = new Payjs("your mchid","your key");

            //获取post参数
            var param = Request.Form.ToDictionary(s => s.Key, s => s.Value);


            //订单号
            string orderid = param.ContainsKey("out_trade_no")?param["out_trade_no"].ToString():"";
            //订单金额
            string total_fee = param.ContainsKey("total_fee") ? param["total_fee"].ToString() : "";
            //自定义数据
            string attach = param.ContainsKey("attach") ? param["attach"].ToString() : "";

            //这里需要对订单数据做基本校验，可以检查当前订单号是否存在，金额和自定义数据是否匹配等
            //比如，下单时将订单数据存redis，key为orderid，value为订单json数据，ttl为30分钟
            //这里便可通过orderid参数去redis中查询校验
            //当ttl过期或者订单成功被删除后，payjs发送过来的请求直接忽略

            // 参考代码
            //if (string.IsNullOrEmpty(redis.get(orderid)))
            //{
            //    //订单不存在了，直接退出
            //    return "";
            //}
            
            

            //post 参数字典转为<string,string>
            var dic = new Dictionary<string, string>();
            foreach (var keyPair in param)
            {
                dic.Add(keyPair.Key,keyPair.Value.ToString());
            }

            //对签名校验
            string sign = param["sign"];
            if (!payjs.notifyCheck(dic))
            {
                return "sign error";
            }

            //校验成功，进入自身业务逻辑(需在3s内响应
            //若超过3s，可以把自身业务放后端（比如通过tcp或者udp通知后端服务），然后这里就直接return success


            //自身逻辑完成，删除redis订单数据
            //redis.del(orderid);

            return "success";
        }
    }
}
