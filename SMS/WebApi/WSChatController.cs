using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebSockets;

namespace SMS.WebApi
{
    public class WSChatController : ApiController
    {
        public static Dictionary<string,WebSocket> Sockets = new Dictionary<string, WebSocket>();
        //public static List<WebSocket> Sockets = new List<WebSocket>();
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(ProcessWSChat);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        //[HttpGet]
        //[HttpPost]
        //public int GetCurrentNums()
        //{
        //    return Sockets.Count;
        //}
        public async Task ProcessWSChat(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;//传入的context中有当前的web socket对象
            string Id = context.QueryString["Id"].ToString();

            #region 用户添加连接池
            //第一次open时，添加到连接池中
                if (!Sockets.ContainsKey(Id))
                    Sockets.Add(Id, socket);//不存在，添加
                else if (socket.State == WebSocketState.Closed)
                {
                    Sockets.Remove(Id);
                }
                else if (socket != Sockets[Id])//当前对象不一致，更新
                    Sockets[Id] = socket;
            #endregion
            //Sockets.Add(socket);//此处将web socket对象加入一个静态列表中
            try
            {
                //进入一个无限循环，当web socket close是循环结束
                while (true)
                {
                    var buffer = new ArraySegment<byte>(new byte[1024]);
                    var receivedResult = await socket.ReceiveAsync(buffer, CancellationToken.None);//对web socket进行异步接收数据
                    if (receivedResult.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None);//如果client发起close请求，对client进行ack                     
                        Sockets.Remove(Id);
                        break;
                    }
                    if (socket.State == System.Net.WebSockets.WebSocketState.Open)
                    {
                        string recvMsg = Encoding.UTF8.GetString(buffer.Array, 0, receivedResult.Count);
                        var recvBytes = Encoding.UTF8.GetBytes(recvMsg);
                        var sendBuffer = new ArraySegment<byte>(recvBytes);
                        List<string> BadSockets = new List<string>();
                        foreach (var innerSocket in Sockets)//当接收到文本消息时，对当前服务器上所有web socket连接进行广播
                        {
                            try
                            {
                                if (innerSocket.Value != socket)
                                {
                                    await innerSocket.Value.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                            }
                            catch(Exception ex)
                            {
                                BadSockets.Add(innerSocket.Key);
                            }
                        }
                        foreach(var s in BadSockets)
                        {
                            Sockets.Remove(s);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Sockets.Remove(Id);
                string s = ex.Message;
                return;
            }
        }
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}